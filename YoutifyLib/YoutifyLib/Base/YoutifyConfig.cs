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
        /// Youtube Client Id
        /// Get yourself one here:
        /// https://developers.google.com/youtube/v3/getting-started
        /// </summary>
        public static string YouTubeClientId { internal get; set; }
        /// <summary>
        /// Youtube Client Secret
        /// Get yourself one here:
        /// https://developers.google.com/youtube/v3/getting-started
        /// </summary>
        public static string YouTubeClientSecret { internal get; set; }
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
