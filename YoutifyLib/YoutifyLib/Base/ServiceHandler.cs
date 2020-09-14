using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutifyLib
{
    /// <summary>
    /// Base class for API handlers
    /// </summary>
    public abstract class ServiceHandler
    {
        /// <summary>
        /// Name of the Service
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Handles list of playlists
        /// </summary>
        public PlaylistsHandler PlaylistsPage;

        /// <summary>
        /// Initlialize required service components async. Should be properly implemented in child classes.
        /// </summary>
        /// <returns>Async task</returns>
        protected abstract Task ServiceInitAsync();

        /// <summary>
        /// Returns id of a channel/user
        /// </summary>
        /// <param name="channelName">A channel name to resolve.
        /// Leave empty or null to resolve current channel (OAuth)</param>
        /// <returns>Id of a channel/user</returns>
        [Obsolete("This feature is not supported by Spotify API", false)]
        public abstract string GetId(string channelName = null);
        /// <summary>
        /// Stores ChannelID/UserID if it was already resolved.
        /// Should be used only be GetId method.
        /// </summary>
        protected string channelId;

        /// <summary>
        /// Creates a playlist, using proper API.
        /// Changes the type of created playlist from generic type to the proper one.
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist. Null if there were problems</returns>
        public abstract string CreatePlaylist(ref Playlist playlist);
        /// <summary>
        /// Imports contents of the playlist and sets Songs property
        /// </summary>
        /// <param name="playlist">Playlist to be imported</param>
        /// <param name="onlyMeta">If only metadata should be imported, skipping songs list</param>
        /// <returns>If the operation was successful</returns>
        public abstract Playlist ImportPlaylist(string playlistId, bool onlyMeta = false);
        /// <summary>
        /// Sychronizes playlist in the service with playlist instance
        /// </summary>
        /// <param name="playlist">Playlist to be synchronizes</param>
        /// <param name="type">Type of synchronization. Overrides must implement all types</param>
        /// <returns>If the operation was successful</returns>
        public abstract bool ExportPlaylist(Playlist playlist, ExportType type);
        /// <summary>
        /// Updates playlist snippet and privacy status
        /// </summary>
        /// <param name="playlist">Playlist to be updated</param>
        /// <returns>If the operation was successful</returns>
        public abstract bool UpdateSnippet(Playlist playlist);
        /// <summary>
        /// Removes the specified tracks from playlist
        /// </summary>
        /// <param name="playlist">Playlist that will be modified</param>
        /// <param name="tracks">List of track to remove</param>
        /// <returns>If the operation was successful</returns>
        public abstract bool RemoveFromPlaylist(Playlist playlist, List<Track> track);
        /// <summary>
        /// Asks the service, if it has any tracks matching the query
        /// </summary>
        /// <param name="query">Query to be passed</param>
        /// <param name="maxResults">Maximum number of results</param>
        /// <returns>List of tracks matching the query</returns>
        public abstract List<Track> SearchForTracks(string query, int maxResults = 5);

        /// <summary>
        /// Returns new empty playlist of the proper service type
        /// </summary>
        /// <param name="source">Source playlist to copy metedata from</param>
        public abstract Playlist NewPlaylist(Playlist source = null);
    }
}
