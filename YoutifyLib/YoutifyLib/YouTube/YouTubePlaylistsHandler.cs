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
                Utils.LogWarning("Called NextPage when there was no next page!");
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
                Utils.LogWarning("Called PrevPage when there was no next page!");
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
