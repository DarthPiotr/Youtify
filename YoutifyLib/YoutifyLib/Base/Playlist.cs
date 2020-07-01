using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public class Playlist
    {   
        public Playlist(string title, string description, string status = "public")
        {
            Title = title;
            Description = description;
            Status = status;
        }

        /// <summary>
        /// The title of the playlist
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Privacy status of the playlist. Available values are: public, private, unlisted.
        /// Default is public.
        /// </summary>
        public string Status
        {
            get => status;
            protected set
            {
                if (value == "public" || value == "private" || value == "unlisted")
                {
                    status = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Status", value,
                        "Privacy status must be either 'public', 'private' or 'unlisted'");
            }
        }
        private string status;

        /// <summary>
        /// List of songs on that playlist
        /// </summary>
        public List<Track> Songs { get; set; } = new List<Track>();

        /// <summary>
        /// Description of a playlist
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Converts generic playlist to Google's YouTube API specific type.
        /// </summary>
        /// <returns></returns>
        public Google.Apis.YouTube.v3.Data.Playlist GetYouTubePlaylist()
        {
            return new Google.Apis.YouTube.v3.Data.Playlist
            {
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
}
