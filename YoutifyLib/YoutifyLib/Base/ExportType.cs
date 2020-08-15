using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{ 
    /// <summary>
    /// Specifies the synchronization type of the playlist on export
    /// </summary>
    public enum ExportType
    {
        /// <summary>
         /// Do not export anything
         /// </summary>
        None,
        /// <summary>
        /// Adds only tracks that are not already on playlist
        /// </summary>
        AddDistinct,
        /// <summary>
        /// Adds all tracks that are on Songs list, regardless if they already are on playlist
        /// </summary>
        AddAll,
        /// <summary>
        /// Overrides the whole playlist with the one stored in Songs property
        /// </summary>
        Override
    }
}
