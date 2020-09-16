using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutifyLib;

namespace YoufityWinForms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            YoutifyConfig.YouTubeApiKey = Secrets.YouTubeKey;
            YoutifyConfig.YouTubeClientId = Secrets.YouTubeClientId;
            YoutifyConfig.YouTubeClientSecret = Secrets.YouTubeClientSecret;
            YoutifyConfig.SpotifyClientId = Secrets.SpotifyClientId;

            Application.Run(new Main());


        }
    }
}
