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

        public YouTubeHandler()
        {
            Task.Run(ServiceInit).Wait();
            PlaylistsPage = new YouTubePlaylistsHandler(Service);
        }

        /// <summary>
        /// Setting up YouTube Service with API key and OAuth
        /// </summary>
        /// <returns>Task to setup YT Service</returns>
        private async Task ServiceInit()
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
                        new[] { YouTubeService.Scope.YoutubeReadonly },
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

    }
}
