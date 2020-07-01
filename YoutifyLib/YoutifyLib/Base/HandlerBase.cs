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
    }
}
