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

        public YouTubePlaylist(Google.Apis.YouTube.v3.Data.Playlist playlist)
        {
            Title = playlist.Snippet.Title;
            Description = playlist.Snippet.Description;
            Id = playlist.Id;
            Status = playlist.Status.PrivacyStatus;
        }

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
        {
            var googlePlaylist = new Google.Apis.YouTube.v3.Data.Playlist
            {
                Id = this.Id,
                Snippet = new PlaylistSnippet
                {
                    Title = this.Title
                },
                Status = new PlaylistStatus
                {
                    PrivacyStatus = this.Status
                }
            };
            if (!string.IsNullOrEmpty(this.Description))
                googlePlaylist.Snippet.Description = this.Description;

            return googlePlaylist;
        }
    }
}
