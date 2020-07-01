using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// Handles lists of Playlists on YouTube
    /// </summary>
    public class YouTubePlaylistsHandler : PlaylistsHandler
    {
        ///////////////////////////////////
        // Properties
        /// <summary>
        /// You Tube API service used to get playlists 
        /// </summary>
        private YouTubeService Service { get; }
        /// <summary>
        /// Token to the previous page provided by API. May be null.
        /// </summary>
        private string PrevPageToken { set; get; }
        /// <summary>
        /// Token to the next page provided by API. May be null.
        /// </summary>
        private string NextPageToken { set; get; }
        private PlaylistSearchArguments Arguments { set; get; } = null;

        //////////////////////////////////////
        //  Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="service">YouTube API service used to send requests</param>
        public YouTubePlaylistsHandler(YouTubeService service)
        {
            Service = service;
            PrevPageToken = null;
            NextPageToken = null;
        }


        ///////////////////////////////////////
        // Public methods
        /// <summary>
        /// Creates a playlist, using YouTube API.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist</returns>
        public override string CreatePlaylist(Playlist playlist)
        {
            if(string.IsNullOrWhiteSpace(playlist.Title))
            {
                Utils.LogError("Tried to create playlist with no title!");
                return null;
            }

            var request = Task.Run(() =>
            {
                try
                {
                    var ytPlaylist = ((YouTubePlaylist)playlist).GetYouTubePlaylist();
                    var req = Service.Playlists.Insert( ytPlaylist , "id, snippet, status");

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
        public override bool Search(PlaylistSearchArguments arg)
        {
            Arguments = arg;

            Utils.LogInfo("Requested first page");
            var request = Task.Run(() => GetPlaylistPageAsync(null));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }
        public override bool NextPage()
        {
            if (NextPageToken == null)
            {
                Utils.LogWarning("Called Next Page when there was no next page!");
                return false;
            }

            Utils.LogInfo("Requested next page");
            var request = Task.Run(() => GetPlaylistPageAsync(NextPageToken));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }
        public override bool PrevPage()
        {
            if (PrevPageToken == null)
            {
                Utils.LogWarning("Called Next Page when there was no next page!");
                return false;
            }

            Utils.LogInfo("Requested previous page");
            var request = Task.Run(() => GetPlaylistPageAsync(PrevPageToken));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }
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
                        var vidRequest = Task.Run(() =>
                        {
                            var req = Service.Videos.List("snippet");
                            req.Id = item.Snippet.ResourceId.VideoId;
                            var res = req.ExecuteAsync();
                            return res;
                        });

                        try
                        {
                            vidRequest.Wait();
                        }
                        catch (Exception ex)
                        {
                            Utils.LogError("While trying to fetch video : {1}, exception was thrown at GetPlaylistContents: {0} ", ex.Message, item.Snippet.ResourceId.VideoId);
                            continue;
                        }
                        if (vidRequest.Result.Items.Count > 0)
                        {
                            var e = vidRequest.Result.Items[0];

                            playlist.Songs.Add(new YouTubeTrack(e));
                            Utils.LogInfo("[Video] {0} added", e.Snippet.Title);
                        }
                        else
                        {
                            Utils.LogWarning("[Video] While trying to fetch video : {0} ({1}), API returned no results!",
                                item.Snippet.Title, item.Snippet.ResourceId.VideoId);
                        }
                    }
                    nextToken = request.Result.NextPageToken;
                }   
            }
            catch(Exception e)
            {
                Utils.LogError("In GetPlaylistContents: {0}", e.Message);
                return false;
            }

            return true;
        }

        ////////////////////////////////////
        // Private methods
        /// <summary>
        /// Requests page async from the list of Playlists on User's channel
        /// </summary>
        /// <param name="pageToken">Token of the page about to be requested</param>
        /// <returns>Asynchronus task</returns>
        private async Task<PlaylistListResponse> GetPlaylistPageAsync(string pageToken)
        {
            if(Arguments == null)
            {
                Utils.LogError("No arguments were passed to search.");
                return null;
            }

            try
            {
                var request = Service.Playlists.List("id, snippet, status");
                request.MaxResults = Arguments.MaxResults;

                switch (Arguments.Type)
                {
                    case PlaylistSearchType.Mine:
                        request.Mine = true;
                        break;
                    case PlaylistSearchType.Channel:
                        request.ChannelId = Arguments.ChannelId;
                        break;
                    case PlaylistSearchType.Id:
                        request.Id = Arguments.PlaylistId;
                        break;
                }

                
                if (pageToken != null)
                    request.PageToken = pageToken;
                /*
                service.PlaylistItems.List("contentDetails, status");
                request.PlaylistId = "PL7VmhWGNRxKgtwHFgDMCnutcmiafoP1c4";
                */

                var result = await request.ExecuteAsync();
                return result;
            }
            catch (Exception e)
            {
                Utils.LogError("In GetPlaylistPageAsync {0}", e.Message);
                return null;
            }
        }
        /// <summary>
        /// Processes the result of API
        /// </summary>
        /// <param name="response">API response</param>
        private void EvalResult (PlaylistListResponse response) {
            if (response != null)
            {
                NextPageToken = response.NextPageToken;
                PrevPageToken = response.PrevPageToken;

                List<Playlist> YTlist = new List<Playlist>();
                foreach (var elem in response.Items)
                    YTlist.Add(new YouTubePlaylist(
                        elem.Snippet.Title,
                        elem.Snippet.Description,
                        elem.Id,
                        elem.Status.PrivacyStatus
                    ));

                // Override current playlist. Required for pagination.
                // Saving results should be handled outside of the library.
                CurrentList = YTlist;
            }
        }
    }
}
