using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace YoutifyLib
{
    /// <summary>
    /// Utilities class
    /// </summary>
    internal static class Utils
    {
        ///////////////////////////////////////
        //    Debug Logs

        public static void LogError(string message)
            => Debug.WriteLine("[!!ERROR]: " + message);
        public static void LogError(string format, params object[] args)
            => Debug.WriteLine("[!!ERROR]: " + string.Format(format, args));

        public static void LogWarning(string message)
            => Debug.WriteLine("[!WARN]: " + message);
        public static void LogWarning(string format, params object[] args)
            => Debug.WriteLine("[!WARN]: " + string.Format(format, args));

        public static void LogInfo(string message)
            => Debug.WriteLine("[INFO]: " + message);
        public static void LogInfo(string format, params object[] args)
            => Debug.WriteLine("[INFO]: " + string.Format(format, args));

        ///////////////////////////////////
        //     Text Processing
        public static string RemoveDoubleSpaces(string input)
        {
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            return regex.Replace(input, " ");
        }

        //////////////////////////////////
        //    Other

        /// <summary>
        /// Returns a list of tracks' ids, instead of all metadata
        /// </summary>
        /// <returns></returns>
        public static List<string> SongsToIdList(List<Track> songs)
        {
            List<string> list = new List<string>();
            foreach (var ytt in songs)
                list.Add(ytt.ID);

            return list;
        }
    }
}
