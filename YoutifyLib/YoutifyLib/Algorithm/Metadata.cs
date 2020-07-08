using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib.Algorithm
{
    public class Metadata
    {
        /// <summary>
        /// Title of the track
        /// </summary>
        public string Title         { get; set; } = "";
        /// <summary>
        /// Additonal information about the title. Usually first part in the brackets.
        /// </summary>
        public string TitleExtra    { get; set; } = "";
        /// <summary>
        /// Artist of the track
        /// </summary>
        public string Artist        { get; set; } = "";
        /// <summary>
        /// Others artists mentioned in the track of the track
        /// </summary>
        public string CoArtist        { get; set; } = "";
        /// <summary>
        /// Artist of the remix
        /// </summary>
        public string Remix         { get; set; } = "";
        /// <summary>
        /// Featuting artists
        /// </summary>
        public string Featuring     { get; set; } = "";
        /// <summary>
        /// Name of a mix i.e. club mix
        /// </summary>
        public string Mix           { get; set; } = "";

        public string GetSearchString(bool includeExtraTitle = true)
        {
            string search = "";
            // Remix artist is prefered to original artist
            if (string.IsNullOrEmpty(Remix))
            {
                string[] art = Artist?.Split(Algorithm.artistDiv.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                if (art?.Length > 0)
                    search += art[0] + " ";
            }
            else
            {
                var rem = Remix.Replace("remix", "").Split(Algorithm.artistDiv.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                if (rem.Length > 0)
                    search += rem[0] + " ";
            }

            // add title
            if(!string.IsNullOrEmpty(Title))
                search += Title + " ";
            // and extra title if wanted
            if (includeExtraTitle && !string.IsNullOrEmpty(TitleExtra))
                search += TitleExtra + " ";

            // add mix info
            if (!string.IsNullOrEmpty(Mix))
                search += Mix;

            // finishing touch
            return Utils.RemoveDoubleSpaces(search).TrimEnd();
        }
    }
}
