using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public class Playlist
    {
        public Playlist() {
            Status = "public";
        }
        public Playlist(string title, string description, string status = "public")
        {
            Title = title;
            Description = description;
            Status = status;
        }

        /// <summary>
        /// The title of the playlist
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Privacy status of the playlist. Available values are: public, private, unlisted.
        /// Default is public.
        /// </summary>
        public string Status
        {
            get => status;
            set
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

        /// <summary>ID of Playlist</summary> 
        public virtual string Id { get; set; }

        /// <summary>
        /// List of songs on that playlist
        /// </summary>
        public List<Track> Songs { get; set; } = new List<Track>();

        /// <summary>
        /// Description of a playlist
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Converts Playlist to another class instance
        /// </summary>
        /// <typeparam name="T">A type to be converted to. Must inherit from Playlist</typeparam>
        /// <returns>A new instance of specifed class</returns>
        public T ToType<T>() where T : Playlist, new() 
        {
            T newObj = new T
            {
                Songs = this.Songs,
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Status = this.Status
            };
            return newObj;
        }
    }
}
