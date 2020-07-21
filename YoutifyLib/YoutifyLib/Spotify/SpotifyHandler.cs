using SpotifyAPI;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YoutifyLib.Spotify
{
    public class SpotifyHandler : ServiceHandler
    {
        /// <summary>
        /// Spotify API instance used to perform operations
        /// </summary>
        private static SpotifyClient Service { get; set; }

        /// <summary>
        /// Current User's Id
        /// </summary>
        private static string UserId { get; set; }
        /// <summary>
        /// Embedded OAuth server to get response for authorization
        /// </summary>
        private static EmbedIOAuthServer _server;
        /// <summary>
        /// Waits for Spotify event to recieve athorization and continue with the initialization
        /// </summary>
        private static AutoResetEvent are = new AutoResetEvent(false);

        public SpotifyHandler()
        {
            if (Service == null)
                Task.Run(ServiceInitAsync).Wait();
        }
        /// <summary>
        /// Creates a playlist, using Spotify API.
        /// Changes the type of created playlist from generic type to the SpotifyPlaylist.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist. Null if there were problems</returns>
        public override string CreatePlaylist(ref Playlist playlist)
        {
            var plreq = new PlaylistCreateRequest(playlist.Title) {
                Description = playlist.Description,
                Public = playlist.Status == "public"
            };

            var request = Service.Playlists.Create(UserId, plreq);
            request.Wait();

            var spotifyPlaylist = playlist.ToType<SpotifyPlaylist>();
            spotifyPlaylist.Id = request.Result.Id;
            playlist = spotifyPlaylist;

            return playlist.Id;
        }

        public override bool ExportPlaylist(Playlist playlist, ExportType type)
        {
            throw new NotImplementedException();
        }

        public override string GetId(string channelName = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a playlist with all tracks (if specified) and metadata
        /// </summary>
        /// <param name="playlistId">Id of a playlist to be imported</param>
        /// <param name="onlyMeta">If only metadata should be imported, skipping song list</param>
        /// <returns>The requested playlist</returns>
        public override Playlist ImportPlaylist(string playlistId, bool onlyMeta = false)
        {

            var playlist = new SpotifyPlaylist() { Id = playlistId };
            try
            {
                var req = Service.Playlists.Get(playlistId);
                req.Wait();

                playlist.Title = req.Result.Name;
                playlist.Description = req.Result.Description;
                if (req.Result.Public != null)
                    playlist.Status = (bool)req.Result.Public ? "public" : "private";

                if (onlyMeta)
                    return playlist;

                foreach (var track in req.Result.Tracks.Items)
                {
                    playlist.Songs.Add(new SpotifyTrack((FullTrack)track.Track));
                }

            }
            catch (Exception e)
            {
                Utils.LogError(e.Message);
                return null;
            }

            return playlist;
        }
        /// <summary>
        /// Returns a list of results, matching the query
        /// </summary>
        /// <param name="query">String used to search for tracks</param>
        /// <param name="maxResults">Maximum number of results to return</param>
        /// <returns>List of matching results</returns>
        public override List<Track> SearchForTracks(string query, int maxResults = 5)
        {
            List<Track> list = new List<Track>();
            try { 
                var req = Service.Search.Item(new SearchRequest(SearchRequest.Types.Track, query) { Limit = maxResults });
                req.Wait();

                foreach(var track in req.Result.Tracks.Items)
                    list.Add(new SpotifyTrack(track));
            }
            catch (Exception e)
            {
                Utils.LogError(e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// Updates snippet, that is title, description and privacy status of playlist
        /// </summary>
        /// <param name="playlist">Playlist with snippet to update</param>
        /// <returns>If the operation was successful</returns>
        public override bool UpdateSnippet(Playlist playlist)
        {
            try
            {
                var details = new PlaylistChangeDetailsRequest
                {
                    Description = playlist.Description,
                    Name = playlist.Title,
                    Public = playlist.Status == "public"
                };

                var req = Service.Playlists.ChangeDetails(playlist.Id, details);
                req.Wait();

                return req.Result;
            }
            catch(Exception e)
            {
                Utils.LogError("While updating snippet: {0}, {1}", e.Message, e.InnerException.Message);
                return false;
            }
        }
        /// <summary>
        /// Setting up Spotify Service with API key and OAuth. Used in constructor.
        /// </summary>
        /// <returns>Task to setup Spotify Service</returns>
        //  From Spotify API docs:
        //  https://github.com/JohnnyCrazy/SpotifyAPI-NET/blob/master/SpotifyAPI.Docs/docs/authorization_code.md
        protected override async Task ServiceInitAsync()
        {
            Utils.LogInfo("Initializing Spotify service...");

            // you don't want server logging mess in your console output
            Swan.Logging.Logger.UnregisterLogger<Swan.Logging.ConsoleLogger>();

            try
            {
                _server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000); 
                await _server.Start();
                _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;

                var request = new LoginRequest(_server.BaseUri, Secrets.SpotifyClientID, LoginRequest.ResponseType.Code)
                {
                    Scope = new List<string> {
                        Scopes.UserReadPrivate,
                        Scopes.PlaylistModifyPublic,
                        Scopes.PlaylistModifyPrivate,
                        Scopes.PlaylistReadCollaborative,
                        Scopes.PlaylistReadPrivate }
                };
                BrowserUtil.Open(request.ToUri());
                Utils.LogInfo("Waiting for Authorization Token...");
                are.WaitOne();
                Utils.LogInfo("Auth Token Recieved!");
                try
                {
                    var req = Service.UserProfile.Current();
                    req.Wait();
                    UserId = req.Result.Id;

                    Utils.LogInfo("User Id is ready to use!");
                }
                catch (Exception e)
                {
                    Utils.LogError("While getting user ID: {0}, {1}", e.Message, e.InnerException.Message);
                }
            }
            catch (Exception e)
            {
                Utils.LogError("While initializing Spotify: {0}, {1}", e.Message, e.InnerException.Message);
            }
            Utils.LogInfo("Initializing Spotify done!");
        }
        /// <summary>
        /// Handler for OAuth server, when authorization code is recieved, finish setting up the Service property
        /// </summary>
        private static async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _server.Stop();

            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
              new AuthorizationCodeTokenRequest(
                Secrets.SpotifyClientID, Secrets.SpotifyClientSecret, response.Code,
                new Uri("http://localhost:5000/callback")
              )
            );

            Service = new SpotifyClient(tokenResponse.AccessToken);
            // TODO: save token?

            // Continue initialization
            are.Set();
        }
    }
}
