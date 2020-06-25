using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public class Playlist
    {   
        public Playlist(string title, string description)
        {
            Title = title;
            Description = description;
        }

        /// <summary>
        /// The title of the playlist
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// List of songs on that playlist
        /// </summary>
        public List<Track> Songs { get; set; } = null;

        /// <summary>
        /// Description of a playlist
        /// </summary>
        public string Description { get; protected set; }
    }
}
