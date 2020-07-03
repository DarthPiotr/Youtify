using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace YoutifyLib.Algorithm
{
    public static class Algorithm
    {
        /// <summary>
        /// Opening brackets list. Indexes must match with corresponding closing brackets in cBrackets
        /// </summary>
        public static List<char> oBrackets          = new List<char> { '(', '[', '{', '<' };
        /// <summary>
        /// Closing brackets list. Indexes must match with corresponding opening brackets in oBrackets
        /// </summary>
        public static List<char> cBrackets          = new List<char> { ')', ']', '}', '>' };

        /// <summary>
        /// Symbols that divide track into artist and title part
        /// </summary>
        public static List<string> generalDiv       = new List<string> { "-", "–" };
        /// <summary>
        /// Strings that indicates "featuring" part. Without dot at the end.
        /// </summary>
        public static List<string> featDiv          = new List<string> { "feat", "ft", "with"};
        /// <summary>
        /// Strings that divide artists
        /// </summary>
        public static List<string> artistDiv        = new List<string> { "vs", "vs.", "x", "&", "," };
        /// <summary>
        /// Strings that a bracket content that will be ignored
        /// </summary>
        public static List<string> ignoreBracket    = new List<string> { "official", "lyric", "video", "version", "#" };
        /// <summary>
        /// Characters that should be removed before processing title
        /// </summary>
        public static List<string> removeChars      = new List<string> { "\"", "'" };


        /// <summary>
        /// Returns metedata extracted from a YouTube title. Data can be not 100% accurate.
        /// </summary>
        /// <param name="title">Title to extract data from</param>
        /// <returns>Extracted metadata</returns>
        public static Metadata GetMetadata(string title)
        {
            // init variables
            int i;                              // iterator for various loops
            string artistPart = "";             // first part before division
            string titlePart  = "";             // second part before division
            Metadata meta = new Metadata();     // metadata to output

            string lowtitle = title.ToLower();  // copy title in lowercase
            

            // remove unwanted characters, ie. " '
            foreach (var c in removeChars)
                lowtitle.Replace(c, "");

            // split the title in two
            foreach (var div in generalDiv)
            {
                i = lowtitle.IndexOf(" " + div + " "); // find index of the division string
                if (i != -1) // if found
                {
                    // divide title in two parts
                    artistPart = lowtitle.Substring(0, i); 
                    titlePart = lowtitle.Substring(i + div.Length + 2);
                    break;
                }
            }
            // if no division found, whole title is the title part.
            if (artistPart == "") titlePart = lowtitle;

            // extract text in brackets
            var titleBrackets = ExtractBracketContents(ref titlePart); 
            var artistBrackets = ExtractBracketContents(ref artistPart);

            // remove hashtags
            titlePart = RemoveHashtags(titlePart);
            artistPart = RemoveHashtags(artistPart);


            // for each element extracted from brackets, extract their contents
            // into meta object
            for (i = 0; i < titleBrackets.Count; i++)
            {
                bool wasAdded = HandleBracketsContent(titleBrackets[i], meta);

                // save extra title info for the first title bracket contents
                if (!wasAdded && i == 0)
                    meta.TitleExtra = titleBrackets[i];
            }
            foreach(var e in artistBrackets) // the same for artist part
                HandleBracketsContent(e, meta);

            // if "featuring" part wasn't in a bracket, extract now
            if (string.IsNullOrEmpty(meta.Featuring))
                meta.Featuring = ExtractFeatPart(ref titlePart);

            if (string.IsNullOrEmpty(meta.Featuring))
                meta.Featuring = ExtractFeatPart(ref artistPart);

            // remaining part must be the title and artist.
            meta.Title = titlePart;
            meta.Artist = artistPart;

            return meta;
        }
        
        
        /// <summary>
        /// Used to extract "featuring" part of the video. This part will be removed from the snippet.
        /// </summary>
        /// <param name="snippet">String to extract from. "Featuring" part will be removed from the string.</param>
        /// <returns>"Featuring" part</returns>
        private static string ExtractFeatPart(ref string snippet)
        {
            string featPart = "";
            foreach (var div in featDiv) // for each string that can divide "featuring" part
            {
                // "featuring" string must be preceded with space,
                // in case it's last letters of some other word
                var tmpi = snippet.IndexOf(" " + div); 
                if (tmpi != -1) // if found
                {
                    featPart = TrimFeaturing(snippet.Substring(tmpi+1), div); // cut "featuring" part
                    snippet = snippet.Substring(0, tmpi-1);  // make snippet without "featuring" part
                    break;
                }
            }
            return featPart;
        }
        /// <summary>
        /// Extracts text from every bracket. Brackets will be removed from the snippet.
        /// </summary>
        /// <param name="snippet">String to extract text from brackets. Brackets will be removed from the string.</param>
        /// <returns>List of brackets' contents</returns>
        public static List<string> ExtractBracketContents(ref string snippet)
        {
            // init necessary variables
            List<string> list = new List<string>(); // init new list
            string newSnipp = snippet;              // copy contents of a snippet intolocal variable
            int ix;                                 // stores index of the current opening bracket

            // for every opening bracket in the list
            for (int i = 0; i < oBrackets.Count; i++) 
            {
                // find new occurence of opening bracket
                ix = newSnipp.IndexOf(oBrackets[i]);
                // as long as there are new brackets
                while (ix != -1) 
                {
                    // find index of the matching closing bracket
                    int last = IndexOfClosingBracket(newSnipp.Substring(ix), i) + ix;
                    // extract content
                    string content = newSnipp.Substring(
                        ix + 1,        // from the '(' character excluded
                        last - ix - 1  // to   the ')' character excluded
                        );
                    // remove content and brackets form string
                    newSnipp = newSnipp.Remove(ix, last - ix + 1);

                    // check if the content is on the list to ignore
                    bool important = true;
                    foreach(var e in ignoreBracket)
                    {
                        // if it is, mark an unimportant
                        if (content.Contains(e))
                        {
                            important = false;
                            break;
                        }
                    }
                    // if it is important, add to the list.
                    if(important)
                        list.Add(content);

                    // find next bracket
                    ix = newSnipp.IndexOf(oBrackets[i], ix);
                }
            }

            // remove mutliple spaces left after removing brackets
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            regex.Replace(newSnipp, " ");
            snippet = newSnipp.Trim();

            // return list of bracket contents
            return list;
        }
        /// <summary>
        /// Searches for matching closing bracket in the string. 
        /// </summary>
        /// <param name="snippet">String to search</param>
        /// <param name="bracketNum">Index of the bracket in oBracket/cBracket lists.</param>
        /// <returns>Index of matching closing bracket.</returns>
        private static int IndexOfClosingBracket(string snippet, int bracketNum)
        {
            int cnt = 0;          // bracket counter
            bool started = false; // if there was first bracket

            // loop through every character
            for(int i = 0; i < snippet.Length; i++)
            {
                // if opening bracket, increment counter
                if (snippet[i] == oBrackets[bracketNum])
                {
                    cnt++;
                    started = true;
                }
                // if closing bracket, decrement counter
                else if (snippet[i] == cBrackets[bracketNum])
                {
                    cnt--;
                    started = true;
                }

                // if there were any brackets and they match, return index
                if (started && cnt == 0) return i;
            }

            // brackets are mismatching, return index of last closing bracket
            return snippet.LastIndexOf(cBrackets[bracketNum]); 
        }
        /// <summary>
        /// Removes "feat" string (ie. from featDiv list) from string. Includes dot, if present.
        /// </summary>
        /// <param name="snippet">String to remove "feat" </param>
        /// <param name="featString">String to be removed</param>
        /// <returns>Trimmed string</returns>
        private static string TrimFeaturing(string snippet, string featString)
        {
            // if the character following the featString sequence is a dot, remove it too
            var beginning = featString.Length;
            if (snippet[beginning] == '.')
                beginning++;

            // return trimmed string
            return snippet.Substring(beginning).TrimStart();
        }
        /// <summary>
        /// Removes hashtags from string.
        /// </summary>
        /// <param name="snippet">String to remove hashtags from</param>
        /// <returns>String without hashtags</returns>
        private static string RemoveHashtags(string snippet)
        {
            int i = snippet.IndexOf('#'); // get first occurence of #
            while(i != -1)                // while there are still #'es
            {
                int j = snippet.IndexOf(' ', i); // get first occurence of space
                if (j != -1)                     // if it's not end of string
                    snippet = snippet.Remove(i, j - i + 1); // remove from # to space
                else                             
                    snippet = snippet.Remove(i);            // remove from # to the end

                i = snippet.IndexOf('#'); // get next # index
            }

            return snippet.Trim(); // clear empty spaces
        }
        /// <summary>
        /// Evaluates contents of the brackets, extracted with ExtractBracketContents()
        /// </summary>
        /// <param name="content">Content of the bracket</param>
        /// <param name="meta">Metadata object to fill properties in</param>
        /// <returns>If current content was used to fill any property</returns>
        private static bool HandleBracketsContent(string content, Metadata meta)
        {
            // if it is a remix, assign to Remix property
            if (content.Contains("remix"))
            {
                meta.Remix = content;
                return true;
            }
            // if it is a mix, assign to Mix property
            else if (content.Contains("mix"))
            {
                meta.Mix = content;
                return true;
            }

            // if it is "featuring" info, assign it to Featuring property
            foreach (var e in featDiv)
            {
                if (content.Contains(e))
                {
                    meta.Featuring = TrimFeaturing(content, e);
                    return true;
                }
            }

            // content wasn't used to assign a property
            return false;
        }
    }
}
