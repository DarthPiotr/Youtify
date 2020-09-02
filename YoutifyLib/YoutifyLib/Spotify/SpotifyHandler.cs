using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

using static SpotifyAPI.Web.Scopes;

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
        private static EmbedIOAuthServer server;

        public SpotifyHandler()
        {
            if (Service == null)
            {
                Utils.LogInfo("Initializing Spotify service...");
                if(File.Exists(YoutifyConfig.CredentialsPath))
                    Task.Run(ReadCredentials).Wait();
                else
                    Task.Run(ServiceInitAsync).Wait();
            }
                
        }
        /// <summary>
        /// Creates a playlist, using Spotify API.
        /// Changes the type of created playlist from generic type to the SpotifyPlaylist.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist. Null if there were problems</returns>
        public override string CreatePlaylist(ref Playlist playlist)
        {
            try
            {
                var plreq = new PlaylistCreateRequest(playlist.Title)
                {
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
            catch(Exception e)
            {
                Utils.LogError("While creating playlist: {0}, {1}", e.Message, e.InnerException);
                return null;
            }
        }
        /// <summary>
        /// Synchronizes playlist content on Service with playlist instance
        /// </summary>
        /// <param name="playlist">Playlist to be exported</param>
        /// <param name="type">Type of export</param>
        /// <returns>If the operation was successful</returns>
        public override bool ExportPlaylist(Playlist playlist, ExportType type)
        {
            switch (type)
            {
                case ExportType.AddAll:
                    return ExportList(Utils.SongsToIdList(playlist.Songs), playlist.Id);

                case ExportType.AddDistinct:
                    var toSubmit = Utils.SongsToIdList(playlist.Songs);
                    var imported = ImportPlaylist(playlist.Id);
                    var current = Utils.SongsToIdList(imported.Songs);

                    foreach (var e in current)
                        toSubmit.RemoveAll(x => { return x == e; });

                    return ExportList(toSubmit, playlist.Id);
                case ExportType.Override:
                    RemoveFromPlaylist(playlist);
                    return ExportList(Utils.SongsToIdList(playlist.Songs), playlist.Id);
                case ExportType.None:
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Adds every id to the playlist
        /// </summary>
        /// <param name="idList">list of ids to add</param>
        /// <param name="playlistId">id of a playlist</param>
        /// <returns>if the operation was successful</returns>
        private bool ExportList(List<string> idList, string playlistId)
        {
            try { 
                for (int i = 0; i < idList.Count; i++)
                    idList[i] = "spotify:track:" + idList[i];

                var plair = new PlaylistAddItemsRequest(idList);
                var request = Service.Playlists.AddItems(playlistId, plair);
                request.Wait();

                return request.Result != null;
            }
            catch (Exception e)
            {
                Utils.LogError("While exporting playlist: {0}, {1}", e.Message, e.InnerException);
                return false;
            }
        }
        /// <summary>
        /// Returns id of channel. Spotify API does not support this.
        /// </summary>
        /// <param name="channelName">An artist name to resolve.
        /// Leave empty or null to resolve current channel (OAuth)</param>
        /// <returns>Id of a channel/user</returns>
        [Obsolete("This feature is not supported by Spotify API", false)]
        public override string GetId(string channelName = null)
        {
            if (channelId != null &&
                channelName == null)
                return channelId;

            return string.Empty;
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
                Utils.LogError("While importing playlist: {0}, {1}", e.Message, e.InnerException);
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
                Utils.LogError("While searching for tracks: {0}, {1}", e.Message, e.InnerException);
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
                    Name = playlist.Title,
                    Public = playlist.Status == "public"
                };
                if (!string.IsNullOrEmpty(playlist.Description))
                    details.Description = playlist.Description;

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
        /// Setting up Spotify Service with API key and OAuth. Only when credentials file is not found.
        /// </summary>
        /// <returns>Task to setup Spotify Service</returns>
        //  From Spotify API .NET docs:
        //  https://github.com/JohnnyCrazy/SpotifyAPI-NET/blob/master/SpotifyAPI.Docs/docs/authorization_code.md
        protected override async Task ServiceInitAsync()
        {
            // you don't want server logging mess in your console output
            try
            {
                Swan.Logging.Logger.UnregisterLogger<Swan.Logging.ConsoleLogger>();
            }
            catch(Exception e)
            {
                Utils.LogWarning("Could not unregister Console logger: {0}", e.Message);
            }

            try
            {
                var (verifier, challenge) = PKCEUtil.GenerateCodes();

                // prepare OAuth server for verification
                server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000);
                await server.Start();
                server.AuthorizationCodeReceived += 
                    async (sender, response) =>
                    {
                        await server.Stop();

                        PKCETokenResponse token = await new OAuthClient().RequestToken(
                            new PKCETokenRequest(YoutifyConfig.SpotifyClientId, response.Code, server.BaseUri, verifier)
                        );

                        await File.WriteAllTextAsync(YoutifyConfig.CredentialsPath, JsonConvert.SerializeObject(token));
                        await ReadCredentials();
                    };

                var request = new LoginRequest(server.BaseUri, YoutifyConfig.SpotifyClientId, LoginRequest.ResponseType.Code)
                {
                    CodeChallenge = challenge,
                    CodeChallengeMethod = "S256",
                    Scope = new List<string> {
                        UserReadPrivate,
                        PlaylistModifyPublic,
                        PlaylistModifyPrivate,
                        PlaylistReadCollaborative,
                        PlaylistReadPrivate }
                };

                BrowserUtil.Open(request.ToUri());
            }
            catch (Exception e)
            {
                Utils.LogError("While initializing Spotify: {0}, {1}", e.Message, e.InnerException.Message);
            }
        }
        /// <summary>
        /// Setting up Spotify using saved credentials
        /// </summary>
        // Auth magic from here
        //    https://github.com/JohnnyCrazy/SpotifyAPI-NET/blob/master/SpotifyAPI.Web.Examples/Example.CLI.PersistentConfig/Program.cs
        private static async Task ReadCredentials()
        {
            var json = await File.ReadAllTextAsync(YoutifyConfig.CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(YoutifyConfig.SpotifyClientId, token);
            authenticator.TokenRefreshed += (sender, token) =>
                File.WriteAllText(YoutifyConfig.CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(authenticator);

            // creating Spotify Client to use later
            Service = new SpotifyClient(config);
            
            server?.Dispose();

            // Continue initialization, get user Id
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
            Utils.LogInfo("Initializing Spotify done!");
        }
        /// <summary>
        /// Removes the specified tracks from playlist
        /// </summary>
        /// <param name="playlist">Playlist that will be modified</param>
        /// <param name="tracks">List of track to remove</param>
        /// <returns>If the operation was successful</returns>
        public override bool RemoveFromPlaylist(Playlist playlist, List<Track> tracks = null)
        {
            try
            {
                // get current version of the playlist
                var list = ImportPlaylist(playlist.Id).Songs;

                // leave on the list only elements to remove
                if (tracks != null)
                {
                    bool found;
                    foreach (SpotifyTrack imported in list.ToArray())
                    {
                        found = false;
                        foreach (SpotifyTrack track in tracks)
                        {
                            if (track.ID == imported.ID)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                            list.RemoveAll(e => imported == e);
                    }
                }

                var priri = new List<PlaylistRemoveItemsRequest.Item>();
                foreach(SpotifyTrack st in list)
                {
                    priri.Add(
                        new PlaylistRemoveItemsRequest.Item()
                            { Uri = "spotify:track:" + st.ID }
                        );
                }

                var prir = new PlaylistRemoveItemsRequest()
                {
                    Tracks = priri
                };
                var request = Service.Playlists.RemoveItems(playlist.Id, prir);
                request.Wait();

                return request.Result != null;

            }
            catch (Exception e)
            {
                Utils.LogError("While removing from playlist: {0}, {1}", e.Message, e.InnerException);
                return false;
            }
}
    }
}
