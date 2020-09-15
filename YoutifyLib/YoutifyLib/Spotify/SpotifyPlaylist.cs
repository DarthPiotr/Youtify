using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Spotify
{
    class SpotifyPlaylist : Playlist
    {

        public SpotifyPlaylist() { }

        public SpotifyPlaylist(SimplePlaylist playlist)
        {
            Title = playlist.Name;
            Description = playlist.Description;
            Id = playlist.Id;
            Status = (bool)playlist.Public ? "public" : "private";
        }

        public SpotifyPlaylist(string title, string description, string id, string status = "public")
            : base(title, description, status)
        {
            Id = id;
        }
    }
}
