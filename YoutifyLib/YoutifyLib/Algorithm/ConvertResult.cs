using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Algorithm
{
    /// <summary>
    /// Handles response of playlist conversion between services
    /// </summary>
    public class ConvertResult
    {
        /// <summary>
        /// If the operation was successful.
        /// When false, check Exception
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Exception thrown during conversion. Null if none was thrown
        /// </summary>
        public Exception Exception { get; set; } = null;
        /// <summary>
        /// List of tracks, with scores
        /// </summary>
        public List<KeyValuePair<Track,int>> Tracklist { get; set; } = null;
        /// <summary>
        /// Returns the list of successfully converted tracks
        /// </summary>
        public List<Track> GetSongs()
        {
            List<Track> songs = new List<Track>();
            foreach(var track in Tracklist)
            {
                if (track.Value != -1)
                    songs.Add(track.Key);
            }
            return songs;
        }
        /// <summary>
        /// Returns the list of tracks that could not be converted
        /// </summary>
        public List<Track> GetErrors()
        {
            List<Track> errors = new List<Track>();
            foreach (var track in Tracklist)
            {
                if (track.Value == -1)
                    errors.Add(track.Key);
            }
            return errors;
        }
    }
}
