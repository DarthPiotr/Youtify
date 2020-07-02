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
        /// Fetches next page of results (if available)
        /// </summary>
        /// <returns>If operation was successful</returns>
        bool NextPage();

        /// <summary>
        /// Fetches previous page of results (if available)
        /// </summary>
        /// <returns>If operation was successful</returns>
        bool PrevPage();
    }
}
