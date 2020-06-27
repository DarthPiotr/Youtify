using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using YoutifyLib;
using YoutifyLib.YouTube;

namespace YoutifyConsole
{
    /// <summary>
    /// Console "front-end" for testing purposes
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a test");

            // create YouTube handler and treat it as a generic one
            YouTubeHandler yth = new YouTubeHandler();
            HandlerBase service = yth;


            var id = ((YouTubePlaylistsHandler)service.PlaylistsPage).GetId();

            // request first page
            var arg = new PlaylistSearchArguments {
                Type = PlaylistSearchType.Channel,
                ChannelId = "UC" + id, // for testing
                PlaylistId = "FL" + id, // for testing
                MaxResults = 10
            };


            // try to call next/prev page beofre Search()
            //service.PlaylistsPage.NextPage(); // works fine
            //service.PlaylistsPage.PrevPage(); // works fine

            service.PlaylistsPage.Search(arg);
            Utils.LogInfo("First page loaded");
            WritePlaylists(service.PlaylistsPage.CurrentList);
            
            string key;
            while ((key = Console.ReadLine().ToString().ToLower()) != "q")
            {
                if (key == "n")
                {
                    // request next page
                    if (service.PlaylistsPage.NextPage())
                    {
                        Utils.LogInfo("Next page loaded");
                        WritePlaylists(service.PlaylistsPage.CurrentList);
                    }
                }

                if (key == "p")
                {
                    // request previous page
                    if (service.PlaylistsPage.PrevPage())
                    {
                        Utils.LogInfo("Prev page loaded");
                        WritePlaylists(service.PlaylistsPage.CurrentList);
                    }
                }
            }

            Console.WriteLine("Test completed!");
        }

        static void WritePlaylists(IList list)
        {
            Console.WriteLine("\n----- Writing page: ----");
            int i = 0;
            foreach (var e in (List<Playlist>)list)
            {
                Console.WriteLine(String.Format("[{1}] {0}", e.Title, ++i));
            }
        }
    }
}
