using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public static class YoutifyConfig
    {
        /// <summary>
        /// Youtube API key
        /// Get yourself one here:
        /// https://developers.google.com/youtube/v3/getting-started
        /// </summary>
        public static string YouTubeApiKey   { internal get; set; }

        /// <summary>
        /// Name of YouTube Client Secret file, for OAuth
        /// </summary>
        public static string YouTubeClientSecretFile { internal get; set; } = "client_secrets.json";

        /// <summary>
        /// Spotify Client Id
        /// Get yourself one here: 
        /// https://developer.spotify.com/documentation/general/guides/app-settings/
        /// </summary>
        public static string SpotifyClientId { internal get; set; }

        /// <summary>
        /// Specifies the path to file where access token is stored.
        /// </summary>
        public static string CredentialsPath { internal get; set; } = "credentials.json";
    }
}
