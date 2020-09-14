using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YoutifyLib.Algorithm
{
    public static class Algorithm
    {
        ///////////////////////////////
        //   Configuration
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
        public static List<string> generalDiv       = new List<string> { "-", "–", "‒", "|" };
        /// <summary>
        /// Strings that indicates "featuring" part. Without dot at the end.
        /// </summary>
        public static List<string> featDiv          = new List<string> { "feat", "ft" };
        /// <summary>
        /// Strings that divide artists
        /// </summary>
        public static List<string> artistDiv        = new List<string> { " vs ", " vs. ", " x ", " and ", "&", "," };
        /// <summary>
        /// Strings that a bracket content that will be ignored
        /// </summary>
        public static List<string> ignoreBracket    = new List<string> { "official", "lyric", "video", "version", "#", "audio" };
        /// <summary>
        /// Characters that should be removed before processing title
        /// </summary>
        public static List<string> removeStrings     = new List<string> { "\"", "'", "“", "”" };

        /////////////////////////////
        //     Public Methods
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
            
            // entering messy part.
            // good luck :)

            // remove unwanted characters, ie. " '
            foreach (var c in removeStrings)
                lowtitle = lowtitle.Replace(c, "");

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


            // in case there is still some info, divide the rest again
            titleBrackets = new List<string>(titlePart.Split(generalDiv.ToArray(), StringSplitOptions.RemoveEmptyEntries));
            // first part is usually the title
            meta.Title = titleBrackets[0].Replace(" audio ", " ").Trim();
            // and handle the rest, as if in brackets
            for (i = 1; i < titleBrackets.Count; i++) 
                HandleBracketsContent(titleBrackets[i].Trim(), meta);

            // the same if artist is not empty
            if (artistPart != "")
            {
                // in case there is still some info, divide the rest again
                var divs = generalDiv;
                divs.AddRange(artistDiv);
                artistBrackets = new List<string>(artistPart.Split(divs.ToArray(), StringSplitOptions.RemoveEmptyEntries));
                // first part is usually the title
                meta.Artist = artistBrackets[0].Trim();
                // save the rest just in case
                artistBrackets.RemoveAt(0);
                meta.CoArtist = string.Join(",", artistBrackets).Replace(" ,", ",").Trim();
                // and handle the rest, as if in brackets
                for (i = 1; i < artistBrackets.Count; i++)
                    HandleBracketsContent(artistBrackets[i].Trim(), meta);
            }

            return meta;
        }
        /// <summary>
        /// Returns metedata extracted from a Spotify title. Data can be not 100% accurate.
        /// </summary>
        /// <param name="title">Title to extract data from</param>
        /// <returns>Extracted metadata</returns>
        public static Metadata GetMetadataNoArtist(string title)
        {
            // init variables
            int i;                              // iterator for various loops
            string lowtitle = title.ToLower();  // copy title in lowercase
            Metadata meta = new Metadata();     // metadata to output

            // remove unwanted characters, ie. " '
            foreach (var c in removeStrings)
                lowtitle = lowtitle.Replace(c, "");

            // of course there can be brackets in a title...
            var brackets = ExtractBracketContents(ref lowtitle);

            for (i = 0; i < brackets.Count; i++)
            {
                bool wasAdded = HandleBracketsContent(brackets[i], meta);

                if (!wasAdded && i == 0)
                    meta.TitleExtra = brackets[i].Trim();
            }

            // split the string using predefined separators
            var parts = lowtitle.Split(generalDiv.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            // on Spotify, the title is always the first part (almost I guess, but I don't know any track like that)
            meta.Title = parts[0].TrimEnd();

            // process the rest of the title
            for(i = 1; i < parts.Length; i++)
            {
                bool wasAdded = HandleBracketsContent(parts[i], meta);

                // save extra title info for the first title bracket contents
                if (string.IsNullOrEmpty(meta.TitleExtra) && !wasAdded && i == 1)
                    meta.TitleExtra = parts[i].Trim();
            }

            return meta;
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
            snippet = Utils.RemoveDoubleSpaces(newSnipp).Trim();

            // return list of bracket contents
            return list;
        }
        /// <summary>
        /// Converts a playlist between services
        /// </summary>
        /// <param name="serviceFrom">A service that is used by source playlist</param>
        /// <param name="playlistId">Id of the source playlist</param>
        /// <param name="serviceTo">The targeted service</param>
        /// <returns>An instance of Convert Result</returns>
        public static ConvertResult Convert(List<Track> sourcelist, ServiceHandler serviceTo)
        {
            List<KeyValuePair<Track, int>> tracklist                            // output list
                = new List<KeyValuePair<Track, int>>();

            foreach (Track track in sourcelist)
            {
                string query = "";                                          // stores a query to search
                List<Track> searchResult;                                   // stores current search results
                List<KeyValuePair<Track, int>> scoredSearch;                // stores scored current search results
                KeyValuePair<Track, int> bestMatch                          // the best scored search results.
                    = new KeyValuePair<Track, int>(null, int.MaxValue);

                for(int i = 0; i < 3; i++)
                {
                    // choose query
                    switch (i)
                    {
                        case 0:
                            // firstly, look simply for title and artist
                            query = track.Metadata.GetSearchString(false, false);
                            break;
                        case 1:
                            // on the second try, add co artist
                            query = track.Metadata.GetSearchString(false, true);
                            break;
                        case 2:
                            // on the third try, add extra title
                            query = track.Metadata.GetSearchString(true, true);
                            break;
                    }

                try { 
                    // search 5 tracks, given the query
                    searchResult = serviceTo.SearchForTracks(query, 5);
                }
                catch (Exception e)
                {
                    Utils.LogError("During conversion: {0}, {1}", e.Message, e.InnerException);
                    return new ConvertResult
                    {
                        Success = false,
                        Exception = e
                    };
                }

                // if anything was found
                if (searchResult.Count > 0)
                    {
                        // score the results
                        scoredSearch = ScoreTracks(searchResult, track);

                        //and add to the current list with sorting
                        foreach (var item in scoredSearch)
                        {
                            if (item.Value < bestMatch.Value)
                            {
                                bestMatch = item;
                                if (bestMatch.Value == 0) break;
                            }
                        }
                    }

                    // if there are no major differences, don't query any more
                    if (bestMatch.Value < 10000) break;
                }

                // add the best match to the list
                tracklist.Add(new KeyValuePair<Track, int>(
                    bestMatch.Key ?? new Track() { Metadata = track.Metadata },
                    ScoreToPercent(bestMatch)
                    ));
            }            

            return new ConvertResult
            {
                Success = true,
                Tracklist = tracklist
            };
        }
        
        /////////////////////////////////
        //   Private Methods
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
                    featPart = TrimFeaturing(snippet.Substring(tmpi + 1), div); // cut "featuring" part
                    snippet = snippet.Substring(0, tmpi);  // make snippet without "featuring" part
                    break;
                }
            }
            return featPart;
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
                meta.Remix = content.Trim();
                return true;
            }
            // if it is a mix, assign to Mix property
            else if (content.Contains("mix"))
            {
                meta.Mix = content.Trim();
                return true;
            }
            else if (content.Contains("edit"))
            {
                meta.Edit = content.Trim();
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
    
        /// <summary>
        /// Scores track's similarity to original
        /// </summary>
        /// <param name="tracks">Track to be scored</param>
        /// <param name="original">Original track</param>
        /// <returns></returns>
        private static List<KeyValuePair<Track, int>> ScoreTracks(List<Track> tracks, Track original)
        {
            var list = new List<KeyValuePair<Track, int>>();
            foreach(var track in tracks)
            {
                int score = 0;

                score += 10000 * ScoreVariations(
                    track.   Metadata.Title.     ToLower(),
                    track.   Metadata.TitleExtra.ToLower(),
                    original.Metadata.Title.     ToLower(),
                    original.Metadata.TitleExtra.ToLower()
                    );
                score += 10000 * ScoreVariations(
                    track.   Metadata.Artist.   ToLower(),
                    track.   Metadata.CoArtist. ToLower(),
                    track.   Metadata.Featuring.ToLower(),
                    original.Metadata.Artist.   ToLower(),
                    original.Metadata.CoArtist. ToLower(),
                    original.Metadata.Featuring.ToLower()
                    );
                score += Utils.ComputeLevenshteinDistance(
                    track.   Metadata.Mix.ToLower().Replace("mix", ""),
                    original.Metadata.Mix.ToLower().Replace("mix", ""));
                score += Utils.ComputeLevenshteinDistance(
                    track.   Metadata.Remix.ToLower().Replace("remix", ""),
                    original.Metadata.Remix.ToLower().Replace("remix", ""));
                score += Utils.ComputeLevenshteinDistance(
                    track.   Metadata.Edit.ToLower().Replace("edit", ""),
                    original.Metadata.Edit.ToLower().Replace("edit", ""));
                /*score += Utils.ComputeLevenshteinDistance(
                    track.   Metadata.TitleExtra.ToLower(),
                    original.Metadata.TitleExtra.ToLower());*/

                list.Add(new KeyValuePair<Track, int>(track, score));
            }

            list.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            return new List<KeyValuePair<Track, int>>(list);
        }
        /// <summary>
        /// <para>Scores two-part strings such as Title+TitleExtra</para>
        /// <para>First part is more important than the second part</para>
        /// </summary>
        /// <param name="origp1">Original part 1</param>
        /// <param name="origp2">Original part 2</param>
        /// <param name="testp1">To test part 1</param>
        /// <param name="testp2">To test part 2</param>
        /// <returns>Minimum levenshtein distance</returns>
        private static int ScoreVariations(string origp1, string origp2, string testp1, string testp2)
        {
            // O1 vs T1
            int min = Utils.ComputeLevenshteinDistance(origp1, testp1);

            // O1+O2 vs T1
            int current = Utils.ComputeLevenshteinDistance(origp1+origp2, testp1);
            if (current < min) min = current;   
            
            // O1 vs T1+T2
            current = Utils.ComputeLevenshteinDistance(origp1, testp1+testp2);
            if (current < min) min = current;  

            // O1+O2 vs T1+T2
            current = Utils.ComputeLevenshteinDistance(origp1+origp2, testp1+testp2);
            if (current < min) min = current;             

            return min;
        }
        /// <summary>
        /// <para>Scores three-part strings such as Artist+CoArtist+Featuring</para>
        /// <para>First part is more important than the second part</para>
        /// </summary>
        /// <param name="origp1">Original part 1</param>
        /// <param name="origp2">Original part 2</param>
        /// <param name="origp3">Original part 3</param>
        /// <param name="testp1">To test part 1</param>
        /// <param name="testp2">To test part 2</param>
        /// <param name="testp3">To test part 3</param>
        /// <returns>Minimum levenshtein distance</returns>
        private static int ScoreVariations(string origp1, string origp2, string origp3, string testp1, string testp2, string testp3)
        {
            // O1    vs T1
            // O1+O2 vs T1
            // O1    vs T1+T2
            // O1+O2 vs T1+T2
            int min = ScoreVariations(origp1, origp2, testp1, testp2);

            // O1+O2+O3 vs T1
            int current = Utils.ComputeLevenshteinDistance(origp1 + origp2 + origp3, testp1);
            if (current < min) min = current;

            // O1+O2+O3 vs T1+T2
            current = Utils.ComputeLevenshteinDistance(origp1 + origp2 + origp3, testp1 + testp2);
            if (current < min) min = current;

            // O1+O2+O3 vs T1+T2+T3
            current = Utils.ComputeLevenshteinDistance(origp1 + origp2 + origp3, testp1 + testp2+ testp3);
            if (current < min) min = current;


            return min;
        }
        /// <summary>
        /// Converts Algorithm's internal scoring system to readable percentage score
        /// </summary>
        /// <param name="entry">Key Value pair to convert the score from</param>
        /// <returns>Score of similarity in percents: 100-the same, 0-totally different</returns>
        private static int ScoreToPercent(KeyValuePair<Track, int> entry)
        {
            if (entry.Key == null)
                return -1;
            
            var meta   = entry.Key.Metadata;    // metadata short reference
            int score  = entry.Value/10000 + entry.Value%10000; // current score without importance space
            int length = (meta.Title + meta.TitleExtra + meta.Mix + meta.Remix + meta.Edit + meta.Artist + meta.CoArtist).Length; // length of the full thing

            return (int)(Math.Max((float)length - score, 0.0f) * 100) / length;
        }
    }
}
