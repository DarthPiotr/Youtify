using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using YoutifyLib;
using YoutifyLib.Spotify;
using YoutifyLib.YouTube;
using YoutifyLib.Algorithm;

namespace YoutifyConsole
{
    /// <summary>
    /// Console "front-end" for testing purposes
    /// </summary>
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This is a test");

            ///////////////////////////////////////////
            //
            //  Creating/Importing Playlist and adding songs
            //

            // Init Secrets
            YoutifyConfig.YouTubeApiKey       = Secrets.YouTubeKey;
            YoutifyConfig.YouTubeClientId     = Secrets.YouTubeClientId;
            YoutifyConfig.YouTubeClientSecret = Secrets.YouTubeClientSecret;
            YoutifyConfig.SpotifyClientId     = Secrets.SpotifyClientId;

            // Init services and treat like generic ones
            ServiceHandler spotify = new SpotifyHandler();
            ServiceHandler youtube = new YouTubeHandler();

            // Import and convert playlist
            Playlist sourcePlaylist = youtube.ImportPlaylist("PLQQAs5duqv7KoiC3IrbglUDE_SzKsWrYh");
            ConvertResult convertionResult = Algorithm.Convert(sourcePlaylist.Songs, spotify);

            // Prepare new playlist
            Playlist newPlaylist = spotify.NewPlaylist(sourcePlaylist); // Get new playlist and copy metadata
            newPlaylist.Songs = convertionResult.GetSongs();            // Add songs that were sucessfully converted

            // Export playlist
            //spotify.CreatePlaylist(ref newPlaylist);                    // Create new playlist
            //spotify.ExportPlaylist(newPlaylist, ExportType.AddAll);     // Export tracks


            Console.WriteLine("Test completed!");
            Console.ReadKey();
        }

#pragma warning disable IDE0051 // Remove unused private members
        static void WritePlaylists(IList list)
        {
            Console.WriteLine("\n----- Writing page: ----");
            int i = 0;
            foreach (var e in (List<Playlist>)list)
            {
                Console.WriteLine("[{1}] {0}", e.Title, ++i);
            }
        }
        static void WritePlaylistContents(Playlist list)
        {
            Console.WriteLine("\n----- {0} ----", list.Title);
            int i = 0;
            foreach (var e in list.Songs)
            {
                Console.WriteLine("[{2}] {0}", e.Metadata.GetSearchString(false, true), ++i);
            }
        }
#pragma warning restore IDE0051
    }
}
