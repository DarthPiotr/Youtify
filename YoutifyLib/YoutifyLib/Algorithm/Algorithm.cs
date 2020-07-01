using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Algorithm
{
    public static class Algorithm
    {
        public static string[] generalDiv = { " - ", " – " };
        public static string[] artistDiv = { " feat", " feat.", " ft", " ft.", " vs", " vs.", " x " };
        public static string[] brackets = { "(", ")", "[", "]", "{", "}", "<", ">" };
        public static string[] removable = { "official", "video", "lyrics" };
        public static string GetArtist(string title, string channel)
        {
            title = title.ToLower();

            foreach (var elem in removable)
            {
                while (title.IndexOf(elem) != -1)
                    title = title.Remove(title.IndexOf(elem), elem.Length);
            }

            var split = title.Split(generalDiv, StringSplitOptions.RemoveEmptyEntries);
            List<string[]> list = new List<string[]>();
            foreach (string s in split)
            {
                list.Add(s.Split(brackets, StringSplitOptions.RemoveEmptyEntries));
            }

            return "";
        }
    }
}
