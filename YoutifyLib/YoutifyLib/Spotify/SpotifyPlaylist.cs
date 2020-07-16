using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Spotify
{
    class SpotifyPlaylist : Playlist
    {
        public SpotifyPlaylist() { }

        public SpotifyPlaylist(string title, string description, string id, string status = "public")
            : base(title, description, status)
        {
            ID = id;
        }
    }
}
