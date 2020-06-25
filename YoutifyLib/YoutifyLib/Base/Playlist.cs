﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public class Playlist
    {   
        /// <summary>
        /// List of songs on that playlist
        /// </summary>
        public List<Track> Songs { get; set; } = null;

        /// <summary>
        /// Description of a playlist
        /// </summary>
        public string Description { get; set; }
    }
}