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

            // init service and treat like generic one
            YoutifyConfig.YouTubeApiKey   = Secrets.YTKey;
            YoutifyConfig.SpotifyClientId = Secrets.SpotifyClientID;

            var spotify = new SpotifyHandler();
            var youtube = new YouTubeHandler();

            var resp = Algorithm.Convert(youtube, "PLQQAs5duqv7L8XMCxWJ9POnp7-qsgMeqV", spotify);

            Console.WriteLine("Success: {0}\nException: {1}\nErrored tracks: {2}\n",
                resp.Success, resp?.Exception?.Message, resp.Errors.Count);

            Console.WriteLine("Test completed!");
            Console.ReadKey();
        }

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
                Console.WriteLine("[{2}] {0}", e.Metadata.GetSearchString(), ++i);
            }
        }
    }
}
