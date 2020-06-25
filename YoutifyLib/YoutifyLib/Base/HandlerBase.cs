using System;
using System.Collections.Generic;

namespace YoutifyLib
{
    /// <summary>
    /// Base class for API handlers
    /// </summary>
    public abstract class HandlerBase
    {
        /// <summary>
        /// Handles list of playlists
        /// </summary>
        public PaginationHandler PlaylistsPage;
    }
}
