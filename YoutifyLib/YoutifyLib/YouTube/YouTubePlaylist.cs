using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// Stores information about YouTube Playlist
    /// </summary>
    public class YouTubePlaylist : Playlist
    {
        public YouTubePlaylist() { }

        public YouTubePlaylist(string title, string description, string id, string status = "public")
            : base(title, description, status)
        {
            Id = id;
        }

        /// <summary>
        /// Converts generic playlist to Google's YouTube API specific type.
        /// </summary>
        /// <returns></returns>
        public Google.Apis.YouTube.v3.Data.Playlist GetYouTubePlaylist()
            => new Google.Apis.YouTube.v3.Data.Playlist
            {
                Id = this.Id,
                Snippet = new PlaylistSnippet
                {
                    Title = this.Title,
                    Description = this.Description
                },
                Status = new PlaylistStatus
                {
                    PrivacyStatus = this.Status
                }
            };
    }
}
