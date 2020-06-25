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
    public class YouTubePlaylistsHandler : PaginationHandler
    {
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

        public override bool FirstPage()
        {
            var request = Task.Run(() => GetPlaylistPageAsync(null));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }

        public override bool NextPage()
        {
            var request = Task.Run(() => GetPlaylistPageAsync(NextPageToken));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }

        public override bool PrevPage()
        {
            var request = Task.Run(() => GetPlaylistPageAsync(PrevPageToken));
            request.Wait();

            if (request.Result == null)
                return false;

            EvalResult(request.Result);
            return true;
        }

        /// <summary>
        /// Requests page async from the list of Playlists on User's channel
        /// </summary>
        /// <param name="pageToken">Token of the page about to be requested</param>
        /// <returns>Asynchronus task</returns>
        private async Task<PlaylistListResponse> GetPlaylistPageAsync(string pageToken)
        {
            try
            {
                var request = Service.Playlists.List("id, snippet");
                request.Mine = true;
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
                Utils.LogError(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Porcesses the result of API
        /// </summary>
        /// <param name="response">API response</param>
        private void EvalResult (PlaylistListResponse response) {
            if (response != null)
            {
                NextPageToken = response.NextPageToken;
                PrevPageToken = response.PrevPageToken;

                List<YouTubePlaylist> YTlist = new List<YouTubePlaylist>();
                foreach (var elem in response.Items)
                {
                    YTlist.Add(
                        new YouTubePlaylist
                        {
                            ID = elem.Id
                        }
                    );
                }
                CurrentList = YTlist;
            }
        }
    }
}
