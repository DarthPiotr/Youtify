using System;
using System.Collections.Generic;
using System.Text;
using YoutifyLib.Algorithm;

namespace YoutifyLib
{
    /// <summary>
    /// Base class for Tracks
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Stores metadata of the track
        /// </summary>
        public Metadata Metadata { get; set; }
        /// <summary>
        /// ID of a track
        /// </summary>
        public virtual string ID { get; set; }

        public Track()
        {
            Metadata = new Metadata();
        }
    }
}
