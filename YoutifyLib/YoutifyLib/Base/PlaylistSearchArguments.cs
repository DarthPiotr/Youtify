using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    /// <summary>
    /// Enum for type of a search.
    /// </summary>
    public enum PlaylistSearchType
    {
        /// <summary>
        /// Current user's (OAuth required) playlists
        /// </summary>
        Mine,
        /// <summary>
        /// Playlists from a channel specified with ChannelId parameter
        /// </summary>
        Channel,
        /// <summary>
        /// Playlists with an id specified with PlaylistId parameter
        /// </summary>
        Id
    };

    public class PlaylistSearchArguments
    {
        /// <summary>
        /// Type of the search. Should Be properly implemented in PlaylistHandler-inheriting classes
        /// </summary>
        public PlaylistSearchType Type { set; get; } = PlaylistSearchType.Mine;
        /// <summary>
        /// Youtube Channel Id to search for playlists. Use with type Channel
        /// </summary>
        public string ChannelId { set; get; }
        /// <summary>
        /// YouTube playlist id to return. Use with type Id
        /// </summary>
        public string PlaylistId { set; get; }
        /// <summary>
        /// Specifies number of results on one page.
        /// </summary>
        public int MaxResults { get => maxResults; set => maxResults = Math.Max(value, 1); }
        private int maxResults = 5;
    }
}
