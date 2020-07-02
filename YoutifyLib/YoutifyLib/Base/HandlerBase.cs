using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutifyLib
{
    /// <summary>
    /// Base class for API handlers
    /// </summary>
    public abstract class HandlerBase
    {
        public HandlerBase()
        {
            Task.Run(ServiceInitAsync).Wait();
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
        /// </summary>
        /// <param name="playlist">Playlist to be created</param>
        /// <returns>Id of created playlist</returns>
        public abstract string CreatePlaylist(Playlist playlist);
        /// <summary>
        /// Gets contents of a playlist, using proper API. Sets the Playlist's Songs property.
        /// </summary>
        /// <param name="playlist">Playlist to be fetched</param>
        /// <returns>If the operation was successful</returns>
        public abstract bool GetPlaylistContents(Playlist playlist);
        /// <summary>
        /// Adds track to the playlist, using proper API. Updates playlist's Songs property.
        /// </summary>
        /// <param name="playlist">Playlist to be modified</param>
        /// <param name="track">Track to be added</param>
        /// <param name="position">Optional position of the track. Leave empty to add at the end of the playlist
        /// </param>
        /// <returns>If the operation was successful</returns>
        public abstract bool AddTrackToPlaylist(Playlist playlist, Track track, int position = -1);
    }
}
