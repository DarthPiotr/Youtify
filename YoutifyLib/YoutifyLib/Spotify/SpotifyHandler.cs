using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI;
using SpotifyAPI.Web;

namespace YoutifyLib.Spotify
{
    public class SpotifyHandler : ServiceHandler
    {
        private SpotifyClient Service { get; set; } 

        public override string CreatePlaylist(ref Playlist playlist)
        {
            throw new NotImplementedException();
        }

        public override bool ExportPlaylist(Playlist playlist, ExportType type)
        {
            throw new NotImplementedException();
        }

        public override string GetId(string channelName = null)
        {
            throw new NotImplementedException();
        }

        public override Playlist ImportPlaylist(string playlistId, bool onlyMeta = false)
        {

            var playlist = new SpotifyPlaylist() { ID = playlistId };
            //try
            //{
            var req = Service.Playlists.Get(playlistId);
            req.Wait();

            playlist.Title = req.Result.Name;
            playlist.Description = req.Result.Description;
            if(req.Result.Public != null)
            playlist.Status = (bool)req.Result.Public ? "public" : "private";

            if(onlyMeta)
                return playlist;

            foreach (var track in req.Result.Tracks.Items)
            {
                playlist.Songs.Add(new SpotifyTrack((FullTrack)track.Track));
            }

            //}
            //catch (Exception e)
            //{
            //    Utils.LogError(e.Message);
            //}

            return playlist;
        }

        public override List<Track> SearchForTracks(string query, int maxResults = 5)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateSnippet(Playlist playlist)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Setting up Spotify Service with API key and OAuth. Used in constructor.
        /// </summary>
        /// <returns>Task to setup Spotify Service</returns>
        protected override async Task ServiceInitAsync()
        {
            Utils.LogInfo("Initializing Spotify service...");
            try
            {
                var config = SpotifyClientConfig.CreateDefault();

                var request = new ClientCredentialsRequest(Secrets.SpotifyClientID, Secrets.SpotifyClientSecret);
                var response = await new OAuthClient(config).RequestToken(request);

                Service = new SpotifyClient(config.WithToken(response.AccessToken));
            }
            catch (Exception e)
            {
                Utils.LogError(e.Message);
            }
            Utils.LogInfo("Initializing Spotify done!");
        }
    }
}
