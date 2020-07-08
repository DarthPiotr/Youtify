using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// Class responsible for using YouTube API
    /// </summary>
    public class YouTubeHandler : HandlerBase
    {
        /// <summary>
        /// YouTube API Service
        /// </summary>
        private YouTubeService Service { get; set; }

        public YouTubeHandler() : base()
        {
            PlaylistsPage = new YouTubePlaylistsHandler(Service);
        }

        /// <summary>
        /// Setting up YouTube Service with API key and OAuth
        /// </summary>
        /// <returns>Task to setup YT Service</returns>
        protected async override Task ServiceInitAsync()
        {
            Utils.LogInfo("Initializing service...");
            try
            {
                // OAuth 
                UserCredential credential;
                using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        // This OAuth 2.0 access scope allows for full read/write access to the
                        // authenticated user's account.
                        new[] { YouTubeService.Scope.Youtube },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(this.GetType().ToString())
                    );
                }

                // Google YouTube API Service
                Service = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKey.YTKey,
                    HttpClientInitializer = credential,
                    ApplicationName = this.GetType().ToString()
                });
            }
            catch (Exception e)
            {
                // TODO: Proper exception handling
                Debug.WriteLine(String.Format("[ERROR] {0}", e.Message));
            }
            Debug.WriteLine("Initializing service done!");
        }
        /// <summary>
        /// Returns id of a channel, without UC prefix.
        /// </summary>
        /// <param name="channelName">A channel name to resolve.
        /// Leave empty or null to resolve current channel (OAuth)</param>
        /// <returns>Id of a channel, without UC prefix</returns>
        public override string GetId(string channelName = null)
        {
            if (channelId != null &&
                channelName == null)
                return channelId;

            var request = Task.Run(() =>
            {
                try
                {
                    var req = Service.Channels.List("id");
                    if (channelName == null)
                        req.Mine = true;
                    else
                        req.ForUsername = channelName;

                    var res = req.ExecuteAsync();
                    return res;
                }
                catch (Exception e)
                {
                    Utils.LogError(e.Message);
                    return null;
                }
            });
            request.Wait();

            var result = request.Result;
            if (channelId != null &&
                channelName == null)
                channelId = result.Items[0].Id.Substring(2);
            return result.Items[0].Id.Substring(2);
        }
        /// <summary>
        /// Creates a playlist, using YouTube API.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist</returns>
        public override string CreatePlaylist(Playlist playlist)
        {
            if (string.IsNullOrWhiteSpace(playlist.Title))
            {
                Utils.LogError("Tried to create playlist with no title!");
                return null;
            }

            var request = Task.Run(() =>
            {
                try
                {
                    var ytPlaylist = ((YouTubePlaylist)playlist).GetYouTubePlaylist();
                    var req = Service.Playlists.Insert(ytPlaylist, "id, snippet, status");

                    var res = req.ExecuteAsync();
                    return res;
                }
                catch (Exception e)
                {
                    Utils.LogError("Inside CreatePlaylist: {0}", e.Message);
                    return null;
                }
            });
            request.Wait();

            if (request.Result == null)
            {
                Utils.LogError("Playlist creation returned null.");
                return null;
            }

            return request.Result.Id;

        }
        /// <summary>
        /// Search for the playlist(s) with specified arguments.
        /// </summary>
        /// <param name="arg">Arguments to search with</param>
        /// <returns>If operation was successfull (Can be true if nothing was found!)</returns>
        /// 
        /// To get special playlists on Youtube, use these prefixes to current channel ID.
        /// !!! Keep in mind, ChannelId is UC[Id]
        /// Favorites:   FL[Id]
        /// Likes:       LL[Id]
        /// Uploads:     Ul[Id]
        /// History:     HL            (no ChannelId, but OAuth)
        /// Watch later: WL            (no ChannelId, but OAuth)
        /// <summary>
        /// Gets contents of a playlist, using YouTube API.
        /// Sets the Songs property.
        /// </summary>
        /// <returns>If the operation was successful</returns>
        public override bool GetPlaylistContents(Playlist playlist)
        {
            if (!(playlist is YouTubePlaylist))
            {
                Utils.LogError("Tried to fetch contents of playlist that is not compatible with YouTube API.");
                return false;
            }

            YouTubePlaylist pl = (YouTubePlaylist)playlist;

            Utils.LogInfo("Requested contents of playlist with Id: {0}", pl.ID);

            try
            {
                var nextToken = "";
                while (nextToken != null)
                {
                    var request = Task.Run(() =>
                    {
                        var req = Service.PlaylistItems.List("snippet");
                        req.PlaylistId = pl.ID;
                        req.MaxResults = 20;
                        req.PageToken = nextToken;

                        var res = req.ExecuteAsync();
                        return res;
                    });
                    request.Wait();
                    Utils.LogInfo("[Playlist] Fetched {0} tracks", request.Result.Items.Count);

                    foreach (var item in request.Result.Items)
                    {
                        playlist.Songs.Add(new YouTubeTrack(item));
                        Utils.LogInfo("[Video] {0} added", item.Snippet.Title);
                    }
                    nextToken = request.Result.NextPageToken;
                }
            }
            catch (Exception e)
            {
                Utils.LogError("In GetPlaylistContents: {0}", e.Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Adds track to the playlist, using proper API. Updates playlist's Songs property.
        /// </summary>
        /// <param name="playlist">Playlist to be modified</param>
        /// <param name="track">Track to be added</param>
        /// <param name="position">Optional position of the track. Leave empty to add at the end of the playlist
        /// </param>
        /// <returns>If the operation was successful</returns>
        public override bool AddTrackToPlaylist(Playlist playlist, Track track, int position = -1)
        {
            if (!(playlist is YouTubePlaylist))
            {
                Utils.LogError("Tried to operate on playlist that is not compatible with YouTube API.");
                return false;
            }
            if (!(track is YouTubeTrack))
            {
                Utils.LogError("Tried to operate on a track that is not compatible with YouTube API.");
                return false;
            }

            var pl = (YouTubePlaylist)playlist;
            var tr = (YouTubeTrack)track;

            var request = Task.Run(() => {

                var pli = tr.ToPlaylistItem(pl.ID);
                pli.Snippet.PlaylistId = pl.ID;
                if (position >= 0)
                    pli.Snippet.Position = position;

                var req = Service.PlaylistItems.Insert(pli, "snippet");
                var res = req.ExecuteAsync();

                return res.Result;
            });
            request.Wait();

            if (request.Result != null)
            {
                if (position >= 0)
                    playlist.Songs.Insert(position, track);
                else
                    playlist.Songs.Add(track);

                return true;
            }

            return false;
        }
        /// <summary>
        /// Returns a list of results, matching the query
        /// </summary>
        /// <param name="query">String used to search for tracks</param>
        /// <param name="maxResults">Maximum number of results to return</param>
        /// <returns>List of matching results</returns>
        public override List<Track> SearchForTracks(string query, int maxResults = 5)
        {
            var list = new List<Track>();

            var request = Task.Run(() => {
                var req = Service.Search.List("snippet");
                req.Q = query;
                req.MaxResults = maxResults;
                req.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.None; // just in case
                req.Type = "youtube#video";
                req.Order = SearchResource.ListRequest.OrderEnum.Relevance;

                var res = req.ExecuteAsync();

                return res.Result;
            });

            request.Wait();

            foreach(var result in request.Result.Items)
            {
                list.Add(new YouTubeTrack
                {
                    Title = result.Snippet.Title,
                    ID = result.Id.VideoId
                });
            }

            return list;
        }
    }
}
