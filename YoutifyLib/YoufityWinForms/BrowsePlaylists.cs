using Swan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutifyLib;

namespace YoufityWinForms
{
    /// <summary>
    /// Used to select Playlist. Use SelectedPlaylist to get playlist. May be null.
    /// /// </summary>
    public partial class BrowsePlaylists : Form
    {
        /// <summary>
        /// Service to call for playlists
        /// </summary>
        private readonly ServiceHandler Service;
        /// <summary>
        /// Stores list of playlists
        /// </summary>
        private List<Playlist> Playlists;
        /// <summary>
        /// Playlist selected by the User
        /// </summary>
        public Playlist SelectedPlaylist;

        public BrowsePlaylists(ServiceHandler service)
        {
            InitializeComponent();

            Service = service;
            this.Text = "Browse playlists: " + Service.Name;
            lbPlaylists.SelectedIndex = -1;
            btnSelect.Enabled = false;
        }
        
        /// <summary>
        /// Refresh/Load all user's playlists
        /// </summary>
        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            btnSelect.Enabled =
            btnCancel.Enabled =
            btnRefresh.Enabled =
            lbPlaylists.Enabled = false;
            pbRefresh.Style = ProgressBarStyle.Marquee;

            lbPlaylists.Items.Clear();
            lbPlaylists.SelectedIndex = -1;

            Task task = new Task(() => {
                Playlists = Service.SearchForMyPlaylists();
            });
            task.Start();
            await task;

            foreach (var item in Playlists)
                lbPlaylists.Items.Add(item.Title);

            pbRefresh.Style = ProgressBarStyle.Continuous;
            btnSelect.Enabled =
            btnCancel.Enabled =
            btnRefresh.Enabled =
            lbPlaylists.Enabled = true;
        }
        /// <summary>
        /// Changing selected playlist
        /// </summary>
        private void LbPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbPlaylists.SelectedIndex != -1)
            {
                labTitleText.Text = Playlists[lbPlaylists.SelectedIndex].Title;
                rtbDescription.Text = Playlists[lbPlaylists.SelectedIndex].Description;
            }
            else
            {
                labTitleText.Text = rtbDescription.Text = "";
                btnSelect.Enabled = false;
            }
        }
        /// <summary>
        /// Selecting playlist
        /// </summary>
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SelectedPlaylist = Playlists[lbPlaylists.SelectedIndex];
            this.Close();
        }
        /// <summary>
        /// Canceling operation
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SelectedPlaylist = null;
            this.Close();
        }
    }
}
