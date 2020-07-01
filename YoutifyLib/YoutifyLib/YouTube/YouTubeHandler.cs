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
    }
}
