using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Algorithm
{
    /// <summary>
    /// Handles response of playlist conversion between services
    /// </summary>
    public class ConvertResponse
    {
        /// <summary>
        /// If the operation was successful.
        /// Please note that with Success=true, not every track is converted
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Exception thrown during conversion. Null if none was thrown
        /// </summary>
        public Exception Exception { get; set; } = null;
        /// <summary>
        /// A new playlist
        /// </summary>
        public Playlist Playlist { get; set; } = null;
        /// <summary>
        /// List of tracks that could not be converted
        /// </summary>
        public List<Track> Errors { get; set; } = new List<Track>();
    }
}
