using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using YoutifyLib;
using YoutifyLib.YouTube;
using YoutifyLib.Algorithm;

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

            // feat
            Algorithm.GetMetadata("Calvin Harris - Feels (Official Video) ft. Pharrell Williams, Katy Perry, Big Sean");
            // edit / remix
            Algorithm.GetMetadata("Nils Van Zandt & Fatman Scoop feat EMB - Destination Paradise (Radio Edit)");
            Algorithm.GetMetadata("Avancada vs Darius & Finlay - Xplode (Grahham Bell & Yoel Lewis Remix)");
            // hashtag
            Algorithm.GetMetadata("The Space Brothers - Shine (Jorn van Deynhoven Remix) [#ASOTIbiza2017]");

            // no separator
            Algorithm.GetMetadata("Lonely For You (Club Mix)");
            // nested brackets
            Algorithm.GetMetadata("Oh (Yes I Am) (Oh (Yes I Am))");

            // different separator -
            // feat inside a bracket
            Algorithm.GetMetadata("Robin Schulz – OK (feat. James Blunt) (Official Music Video)");
            // remix outside of a bracket
            Algorithm.GetMetadata("Tove Lo - Habits (Stay High) - Hippie Sabotage Remix");
            
            // russian lol
            Algorithm.GetMetadata("Элджей - Рваные джинсы");

            ///////////////////////////////////////////////////////
            //
            // create YouTube handler and treat it as a generic one
            //
            /*YouTubeHandler yth = new YouTubeHandler();
            HandlerBase service = yth;*/

            // try to call next/prev page beofre Search()
            //service.PlaylistsPage.NextPage(); // works fine
            //service.PlaylistsPage.PrevPage(); // works fine

            //////////////////////////////////////
            // 
            // Fetch first page of playlists
            //
            /*
            string id = service.GetId();
            var arg = new PlaylistSearchArguments
            {
                Type = PlaylistSearchType.Channel,
                ChannelId = "UC" + id, // for testing
                PlaylistId = "FL" + id, // for testing
                MaxResults = 10
            };
            service.PlaylistsPage.Search(arg);
            Utils.LogInfo("First page loaded");
            WritePlaylists(service.PlaylistsPage.CurrentList);*/

            /////////////////////////////////////////
            //
            // Try fetching contents of the playlist
            //

            /*Console.WriteLine("Number of playlist to fetch: ");
            int num;
            do
            {
                num = Convert.ToInt32(Console.ReadLine());
            }
            while (num < 1 || num > service.PlaylistsPage.CurrentList.Count);
            num--;*/
            /*var pl = new YouTubePlaylist("yes", "yes", "PLQQAs5duqv7I1VKNYAVgj6oN90vWsHnzF");
            Console.WriteLine("Please wait, while API fetches your videos");
            var res = service.PlaylistsPage.GetPlaylistContents(pl);// service.PlaylistsPage.CurrentList[num]);
            Console.WriteLine("Was fetching successful? " + res);
            if (res)
                WritePlaylistContents(pl);*/
                

            /*Console.WriteLine("Attempting to create playlist...");
            Playlist pl = new Playlist("XXX", "samo dobro", "prublic");
            string plid = yth.PlaylistsPage.CreatePlaylist(pl);
            Console.WriteLine("Created a playlist with id: " + plid);*/
            
            /*
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
            }*/

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
                Console.WriteLine("[{2}] {0} by: {1}", e.Title, e.Artist, ++i);
            }
        }
    }
}
