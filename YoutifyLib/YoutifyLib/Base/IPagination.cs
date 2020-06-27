using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    /// <summary>
    /// Handles pagination
    /// </summary>
    public interface IPagination
    {        
        /// <summary>
        /// Sets CurrentList to next page of results (if available)
        /// </summary>
        /// <returns>If operation was successful</returns>
        bool NextPage();

        /// <summary>
        /// Sets CurrentList to previous page of results (if available)
        /// </summary>
        /// <returns>If operation was successful</returns>
        bool PrevPage();
    }
}
