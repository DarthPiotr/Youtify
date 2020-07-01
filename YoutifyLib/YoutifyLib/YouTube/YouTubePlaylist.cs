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
        public YouTubePlaylist(string title, string description, string id, string status = "public")
            : base(title, description, status)
        {
            ID = id;
        }

        /// <summary>ID of YouTube Playlist</summary> 
        public string ID { get; set; }
    }
}
