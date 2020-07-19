using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Spotify
{
    public class SpotifyTrack : Track
    {
        public SpotifyTrack(FullTrack track)
        {
            ID = track.Id;

            Metadata = Algorithm.Algorithm.GetMetadataNoArtist(track.Name);
            if (track.Artists.Count > 0)
            {
                Metadata.Artist = track.Artists[0].Name;

                List<string> ca = new List<string>();
                for (int i = 1; i < track.Artists.Count; i++)
                    ca.Add(track.Artists[i].Name);
                Metadata.CoArtist = string.Join(",", ca);
            }       
        }
    }
}
