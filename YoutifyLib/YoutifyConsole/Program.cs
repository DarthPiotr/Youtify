using System;
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

            // request first page
            service.PlaylistsPage.FirstPage();
            Utils.LogInfo("First page loaded");
            WritePlaylists(service.PlaylistsPage.CurrentList);

            string key;
            while ((key = Console.ReadLine().ToString().ToLower()) != "q")
            {
                if (key == "n")
                {
                    // request next page
                    service.PlaylistsPage.NextPage();
                    Utils.LogInfo("Next page loaded");
                    WritePlaylists(service.PlaylistsPage.CurrentList);
                }

                if (key == "p")
                {
                    // request previous page
                    service.PlaylistsPage.PrevPage();
                    Utils.LogInfo("Prev page loaded");
                    WritePlaylists(service.PlaylistsPage.CurrentList);
                }
            }

            Console.WriteLine("Test completed!");

            Console.ReadKey();
        }

        static void WritePlaylists(List<Playlist> list)
        {
            Console.WriteLine("\n----- Writing page: ----");
            int i = 0;
            foreach (var e in list)
            {
                Console.WriteLine(String.Format("[{1}] {0}", e.Title, ++i));
            }
        }
    }
}
