using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// Class responsible for using YouTube API
    /// </summary>
    public class YouTubeHandler : ServiceHandler
    {
        /// <summary>
        /// YouTube API Service
        /// </summary>
        private YouTubeService Service { get; set; }

        public YouTubeHandler()
        {
            Name = "YouTube";
            if (Service == null)
                Task.Run(ServiceInitAsync).Wait();
        }

        /// <summary>
        /// Setting up YouTube Service with API key and OAuth. Used in constructor.
        /// </summary>
        /// <returns>Task to setup YT Service</returns>
        protected override async Task ServiceInitAsync()
        {
            Utils.LogInfo("Initializing service...");
            try {
                // OAuth 
                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets() {
                        ClientId = YoutifyConfig.YouTubeClientId,
                        ClientSecret = YoutifyConfig.YouTubeClientSecret
                    },
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );

                // Google YouTube API Service
                Service = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = YoutifyConfig.YouTubeApiKey,
                    HttpClientInitializer = credential,
                    ApplicationName = this.GetType().ToString()
                });
            }
            catch (Exception e)
            {
                // TODO: Proper exception handling ?
                Utils.LogError(e.Message);
            }
            Utils.LogInfo("Initializing service done!");
        }


        /// <summary>
        /// Returns id of a channel, without UC prefix.
        /// </summary>
        /// <param name="channelName">A channel name to resolve.
        /// Leave empty or null to resolve current channel (OAuth)</param>
        /// <returns>Id of a channel, without UC prefix</returns>
        [Obsolete("This feature is not supported by Spotify API", false)]
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
        /// Changes the type of created playlist from generic type to the YouTubePlaylist.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist. Null if there were problems</returns>
        public override string CreatePlaylist(ref Playlist playlist)
        {
            if (string.IsNullOrWhiteSpace(playlist.Title))
            {
                Utils.LogError("Tried to create playlist with no title!");
                return null;
            }

            var ytpl = playlist.ToType<YouTubePlaylist>();
            var ytPlaylist = ytpl.GetYouTubePlaylist();
            var request = Task.Run(() =>
                {
                    var req = Service.Playlists.Insert(ytPlaylist, "id, snippet, status");

                    var res = req.ExecuteAsync();
                    return res;
                });
            try
            {
                request.Wait();
            }
            catch (Exception e)
            {
                Utils.LogError("Inside CreatePlaylist: {0}, {1}", e.Message, e.InnerException);
                return null;
            }

            if (request.Result == null)
            {
                Utils.LogError("Playlist creation returned null.");
                return null;
            }
            ytpl.Id = request.Result.Id;
            playlist = ytpl;
            return request.Result.Id;

        }
        /// <summary>
        /// Returns a list of results, matching the query
        /// </summary>
        /// <param name="query">String used to search for tracks</param>
        /// <param name="maxResults">Maximum number of results to return</param>
        /// <returns>List of matching results</returns>
        public override List<Track> SearchForTracks(string query, int maxResults = 5)
        {
            try
            { 
                // output list
                var list = new List<Track>();

                // prepare request
                var request = Task.Run(() =>
                {
                    var req = Service.Search.List("snippet");
                    req.Q = query;
                    req.MaxResults = maxResults;
                    req.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.None; // just in case
                    req.Type = "youtube#video";
                    req.Order = SearchResource.ListRequest.OrderEnum.Relevance;

                    var res = req.ExecuteAsync();

                    return res.Result;
                });

            // execute till it's done
            request.Wait();

            // add results to the list
            foreach (var result in request.Result.Items)
            {
                list.Add(new YouTubeTrack
                {
                    Metadata = Algorithm.Algorithm.GetMetadata(result.Snippet.Title),
                    ID = result.Id.VideoId
                });
            }

            return list;
            }
            catch (Exception e)
            {
                Utils.LogError("While searching for tracks playlist: {0}, {1}", e.Message, e.InnerException);
                return null;
            }
        }
        /// <summary>
        /// Returns a playlist with all tracks (if specified) and metadata
        /// </summary>
        /// <param name="playlistId">Id of a playlist to be imported</param>
        /// <param name="onlyMeta">If only metadata should be imported, skipping song list</param>
        /// <returns>The requested playlist</returns>
        public override Playlist ImportPlaylist(string playlistId, bool onlyMeta = false)
        {
            // TODO: Refactoring

            YouTubePlaylist pl = new YouTubePlaylist { Id = playlistId };

            Utils.LogInfo("Requested contents of playlist with Id: {0}", pl.Id);

            try
            {
                var requestInfo = Task.Run(() =>
                {
                    var req = Service.Playlists.List("snippet, status");
                    req.Id = pl.Id;

                    var res = req.ExecuteAsync();
                    return res;
                });
                requestInfo.Wait();

                pl.Title = requestInfo.Result.Items[0].Snippet.Title;
                pl.Description = requestInfo.Result.Items[0].Snippet.Description;
                pl.Status = requestInfo.Result.Items[0].Status.PrivacyStatus;

                if (!onlyMeta)
                {
                    var nextToken = "";

                    while (nextToken != null)
                    {
                        var requestTracks = Service.PlaylistItems.List("snippet");
                        requestTracks.PlaylistId = playlistId;
                        requestTracks.MaxResults = 20;
                        requestTracks.PageToken = nextToken;

                        var requestTracksTask = Task.Run(() =>
                            requestTracks.ExecuteAsync()
                        );
                        requestTracksTask.Wait();
                        Utils.LogInfo("[Playlist] Fetched {0} tracks",
                            requestTracksTask.Result.Items.Count);

                        foreach (var item in requestTracksTask.Result.Items)
                        {
                            pl.Songs.Add(new YouTubeTrack(item));
                            Utils.LogInfo("[Video] {0} added", item.Snippet.Title);
                        }

                        nextToken = requestTracksTask.Result.NextPageToken;
                        requestTracks.PageToken = nextToken;
                    }
                }

                return pl;
            }
            catch (Exception e)
            {
                Utils.LogError("While importing playlist: {0}, {1}", e.Message, e.InnerException);
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
            try
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
                }
                return false;
            }
            catch (Exception e)
            {
                Utils.LogError("While exporting playlist: {0}, {1}", e.Message, e.InnerException);
                return false;
            }
        }        
        /// <summary>
        /// Adds every id to the playlist
        /// </summary>
        /// <param name="videoIds">list of ids to add</param>
        /// <param name="playlistId">id of a playlist</param>
        /// <returns>if the operation was successful</returns>
        private bool ExportList(List<string> videoIds, string playlistId)
        {
            foreach (var id in videoIds)
            {
                try
                {
                    var request = Task.Run(() =>
                    {

                        var pli = new PlaylistItem
                        {
                            Snippet = new PlaylistItemSnippet
                            {
                                ResourceId = new ResourceId
                                {
                                    Kind = "youtube#video",
                                    VideoId = id,
                                    PlaylistId = playlistId
                                }
                            }
                        };
                        pli.Snippet.PlaylistId = playlistId;

                        var req = Service.PlaylistItems.Insert(pli, "snippet");
                        var res = req.ExecuteAsync();

                        return res.Result;
                    });
                    request.Wait();
                }
                catch (Exception e)
                {
                    Utils.LogError("An error occured while exporting tracks. {0}, {1}", e.Message, e.InnerException);
                    return false;
                }
            }

            return true;
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
                var request = Task.Run(() =>
                {

                    var pl = playlist.ToType<YouTubePlaylist>().GetYouTubePlaylist();
                    var req = Service.Playlists.Update(pl, "snippet, status");
                    var res = req.ExecuteAsync();

                    return res.Result;
                });
                request.Wait();
            }
            catch (Exception e)
            {
                Utils.LogError("An error occured while updating snippet. {0}, {1}", e.Message, e.InnerException);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Removes specifed tracks from playlist
        /// </summary>
        /// <param name="playlist">Playlist to work on</param>
        /// <param name="tracks">List of tracks to remove</param>
        /// <returns>If the operation was successful</returns>
        public override bool RemoveFromPlaylist(Playlist playlist, List<Track> tracks = null)
        {
            // get up-to-date contents of the playlist
            var list = ImportPlaylist(playlist.Id).Songs ;

            // leave on the list only elements to remove
            if (tracks != null)
            {
                bool found;
                foreach (YouTubeTrack imported in list.ToArray())
                {
                    found = false;
                    foreach (YouTubeTrack track in tracks)
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
            // then remove what was found
            try
            {
                Task<string> request;
                foreach (YouTubeTrack track in list)
                {
                    request = Task.Run(() => {
                        var req = Service.PlaylistItems.Delete(track.PlaylistItemId);
                        var res = req.ExecuteAsync();

                        return res.Result;
                    });
                    request.Wait();
                }
            }
            catch (Exception e)
            {
                Utils.LogError("While trying to remove from playlist: {0}, {1}", e.Message, e.InnerException.Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Returns new empty playlist of the proper service type
        /// </summary>
        /// <param name="source">Source playlist to copy metedata from</param>
        public override Playlist NewPlaylist(Playlist source = null)
        {
            var playlist = new YouTubePlaylist();
            if (source != null)
            {
                playlist.Title = source.Title;
                playlist.Description = source.Description;
                playlist.Status = source.Status;
            }

            return playlist;
        }
        /// <summary>
        /// Returns list of User's playlists
        /// </summary>
        /// <param name="maxResults">Max number of results</param>
        /// <returns>List of playlists</returns>
        public override List<Playlist> SearchForMyPlaylists(int maxResults = 0)
        {
            List<Playlist> list = new List<Playlist>();
            int page = 20;
            if (maxResults <= 0)
                maxResults = int.MaxValue;

            var pageToken = "";
            var request = Service.Playlists.List("id, snippet, status");
            request.Mine = true;

            try
            {
                while (pageToken != null && maxResults > 0)
                {
                    if (maxResults / page >= 1)
                        request.MaxResults = maxResults;
                    else
                        request.MaxResults = maxResults;

                    request.PageToken = pageToken;

                    var task = Task.Run(async () =>
                    {
                        var result = await request.ExecuteAsync();
                        return result;
                    });
                    task.Wait();

                    pageToken = task.Result.NextPageToken;

                    foreach (var playlist in task.Result.Items)
                        list.Add(new YouTubePlaylist(playlist));

                    maxResults -= (int)request.MaxResults;
                }
            }
            catch (Exception e)
            {
                Utils.LogError("While searching for User's YouTube playlists: {0}, {1}", e.Message, e.InnerException.Message);
            }

            return list;
        }
    }
}
