using System;
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
        /// <summary>
        /// <para>Computes Levenshtein Distance for two strings. 
        /// The lower the number is, the more similar strings are.</para>
        /// <para>Source: <see href="http://dotnetperls.com/levenshtein"/></para>
        /// </summary>
        /// <param name="s">First string</param>
        /// <param name="t">Second string</param>
        /// <returns>Levenshtein Distance for two strings</returns>

        public static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0) return m;
            if (m == 0) return n;

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
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
