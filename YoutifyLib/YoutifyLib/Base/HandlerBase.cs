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
    }
}
