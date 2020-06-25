using System;
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
            HandlerBase service = (HandlerBase)yth;

            // request first page
            service.PlaylistsPage.FirstPage();
            Utils.LogInfo("First page loaded");

            // request next page
            service.PlaylistsPage.NextPage();
            Utils.LogInfo("Next page loaded");

            // request prevoius page
            service.PlaylistsPage.PrevPage();
            Utils.LogInfo("Prev page loaded");

            Console.WriteLine("Test completed!");

            Console.ReadKey();
        }
    }
}
