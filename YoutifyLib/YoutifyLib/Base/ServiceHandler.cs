﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutifyLib
{
    /// <summary>
    /// Base class for API handlers
    /// </summary>
    public abstract class ServiceHandler
    {
        // TODO: Make inheriting classes static, with static service, so that it will be configured only once. In order not to allow multiple service instanes within one application

        public ServiceHandler()
        {
            //Task.Run(ServiceInitAsync).Wait();
        }

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
        /// Asks the service, if it has any tracks matching the query
        /// </summary>
        /// <param name="query">Query to be passed</param>
        /// <param name="maxResults">Maximum number of results</param>
        /// <returns>List of tracks matching the query</returns>
        public abstract List<Track> SearchForTracks(string query, int maxResults = 5);
    }
}