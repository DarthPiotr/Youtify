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
        static void Main(string[] args)
        {
            Console.WriteLine("This is a test");

            ///////////////////////////////////////////
            //
            //  Creating/Importing Playlist and adding songs
            //

            // init service and treat like generic one
            var serv = new SpotifyHandler();
            ServiceHandler service = serv;

        /////////////////////////////
        // Creating a new playlist

        //var p = new Playlist("Testing Playlists", "Description");
        //service.CreatePlaylist(ref p);

          var x = service.ImportPlaylist("0Z55nJHwYrK9qYNDfLjFxu");
            x.Description = "At least for me. I hope for you too!";
            service.UpdateSnippet(x);
            
            x.Songs.Add(service.SearchForTracks("never gonna give you up")[0]);

            Console.WriteLine("Lolxd it works");

            x.Songs.Add(service.SearchForTracks("blah blah blah Armin")[0]);
            // x.Songs.Add(service.SearchForTracks("no no no elybeatmaker")[0]);

            //service.ExportPlaylist(x, ExportType.AddDistinct);

            x.Status = "private";
            x.Title = "Yay, a new title!";
            x.Description = "Yay, a new description!";

            //service.UpdateSnippet(x);            

            /*
            // feat
            Console.WriteLine(Algorithm.GetMetadata("Calvin Harris - Feels (Official Video) ft. Pharrell Williams, Katy Perry, Big Sean").GetSearchString());
            // edit / remix
            Console.WriteLine(Algorithm.GetMetadata("Nils Van Zandt & Fatman Scoop feat EMB - Destination Paradise (Radio Edit)").GetSearchString());
            Console.WriteLine(Algorithm.GetMetadata("Avancada vs Darius & Finlay - Xplode (Grahham Bell & Yoel Lewis Remix)").GetSearchString());
            // hashtag
            Console.WriteLine(Algorithm.GetMetadata("The Space Brothers - Shine (Jorn van Deynhoven Remix) [#ASOTIbiza2017]").GetSearchString());

            // no separator
            Console.WriteLine(Algorithm.GetMetadata("Lonely For You (Club Mix)").GetSearchString());
            // nested brackets
            Console.WriteLine(Algorithm.GetMetadata("Oh (Yes I Am) (Oh (Yes I Am))").GetSearchString());

            // comma -> multiple information in bracket
             Console.WriteLine(Algorithm.GetMetadata("Junior Senior - Move Your Feet (Official music video, HD)").GetSearchString());

            // useless extra title
            Console.WriteLine(Algorithm.GetMetadata("Michael Jackson - They Don’t Care About Us (Brazil Version) (Official Video)").GetSearchString());

            // different separator -
            // feat inside a bracket
            Console.WriteLine(Algorithm.GetMetadata("Robin Schulz – OK (feat. James Blunt) (Official Music Video)").GetSearchString());
            // remix outside of a bracket
            Console.WriteLine(Algorithm.GetMetadata("Tove Lo - Habits (Stay High) - Hippie Sabotage Remix").GetSearchString());
            
            // russian lol
            Console.WriteLine(Algorithm.GetMetadata("Элджей - Рваные джинсы").GetSearchString());

            ///////////////////////////////////////////////////////
            //
            // create YouTube handler and treat it as a generic one
            //
            
            Console.WriteLine(((YouTubeTrack)service.SearchForTracks("never gonna give you up")[0]).ID);

            // try to call next/prev page beofre Search()
            //service.PlaylistsPage.NextPage(); // works fine
            //service.PlaylistsPage.PrevPage(); // works fine

            //////////////////////////////////////
            // 
            // Fetch first page of playlists
            //
            
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
            WritePlaylists(service.PlaylistsPage.CurrentList);

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
            num--;*//*
            var pl = new YouTubePlaylist("yes", "yes", "PLQQAs5duqv7I1VKNYAVgj6oN90vWsHnzF");
            Console.WriteLine("Please wait, while API fetches your videos");
            var res = service.GetPlaylistContents(pl);// service.PlaylistsPage.CurrentList[num]);
            pl.Songs = res;
            Console.WriteLine("Was fetching successful? " + (res != null));
            if (res != null)
                WritePlaylistContents(pl);//*//*

            pl.Songs.Add(new YouTubeTrack() { Metadata = new Metadata() { Title = "test", Artist = "atest" } });
            service.ExportPlaylist(pl, ExportType.AddDistinct);

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
                Console.WriteLine("[{2}] {0}", e.Metadata.GetSearchString(), ++i);
            }
        }
    }
}
