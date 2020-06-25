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
        public YouTubePlaylist(string title, string description, string id) : base(title, description)
        {
            ID = id;
        }

        // ID of YouTube Playlist
        public string ID { get; set; }
    }
}
