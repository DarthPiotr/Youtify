﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YoutifyLib
{
    public abstract class PlaylistsHandler : IPagination
    {
        /// <summary>
        /// Stores current page of playlists
        /// </summary>
        public List<Playlist> CurrentList { set; get; }
        /// <summary>
        /// Search for playlists. Sets CurrentList property as a result
        /// </summary>
        /// <param name="arg">Arguments to search</param>
        /// <returns>Returns if search was successful. Still can return true if there was no matching result</returns>
        public abstract bool Search(PlaylistSearchArguments arg);
        /// <summary>
        /// IPagination implementatnion required
        /// </summary>
        public abstract bool NextPage();
        /// <summary>
        /// IPagination implementatnion required
        /// </summary>
        public abstract bool PrevPage();
    }
}