using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    /// <summary>
    /// Handles pagination
    /// </summary>
    /// <typeparam name="T">Type of object that will be listed</typeparam>
    public abstract class PaginationHandler
    {
        /// <summary>
        /// Returns list contained on the current page
        /// </summary>
        public List<Playlist> CurrentList { get; protected set; }

        /// <summary>
        /// Sets CurrentList to first page of results
        /// </summary>
        /// <returns>If operation was successful</returns>
        public abstract bool FirstPage();
        
        /// <summary>
        /// Sets CurrentList to next page of results
        /// </summary>
        /// <returns>If operation was successful</returns>
        public abstract bool NextPage();

        /// <summary>
        /// Sets CurrentList to previous page of results
        /// </summary>
        /// <returns>If operation was successful</returns>
        public abstract bool PrevPage();
    }
}
