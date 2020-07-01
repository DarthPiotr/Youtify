using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.YouTube
{
    /// <summary>
    /// An instance of YouTube Track
    /// </summary>
    public class YouTubeTrack : Track
    {
        public YouTubeTrack() { }

        public YouTubeTrack(Video vid)
        {
            Title = vid.Snippet.Title;
            Artist = vid.Snippet.ChannelTitle;
        }
        /// <summary>
        /// YouTube ID of a track
        /// </summary>
        public string ID { get; set; }
    }
}
