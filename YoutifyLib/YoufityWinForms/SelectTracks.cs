using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YoutifyLib;
using static System.Windows.Forms.ListBox;

namespace YoufityWinForms
{
    /// <summary>
    /// Selecting tracks from playlist to convert
    /// </summary>
    public partial class SelectTracks : Form
    {
        /// <summary>
        /// Tracks that are selected to be converted
        /// </summary>
        public List<Track> Tracks {
            get {
                if (TargetList == null)
                    return null;

                return new List<Track>(TargetList);
            }
        }

        /// <summary>
        /// List of the tracks available on playlist
        /// </summary>
        private BindingList<Track> SourceList;
        /// <summary>
        /// List of chosen tracks
        /// </summary>
        private BindingList<Track> TargetList;

        public SelectTracks(ServiceHandler service, string playlistId)
        {
            InitializeComponent();
            TargetList = new BindingList<Track>();
            lbTarget.DataSource = TargetList;

            LoadTracks(service, playlistId);
        }

        /// <summary>
        /// Loads tracks from selected playlist
        /// </summary>
        private void LoadTracks(ServiceHandler service, string playlistId)
        {
            btnAdd.Enabled = false;
            btnAddAll.Enabled = false;
            btnRemove.Enabled = false;
            btnRemoveAll.Enabled = false;
            btnNext.Enabled = false;
            lbSource.Enabled = false;
            lbTarget.Enabled = false;

            pbLoading.Style = ProgressBarStyle.Marquee;
            labLoading.Visible = true;

            var playlist = service.ImportPlaylist(playlistId);
            SourceList = new BindingList<Track>(playlist.Songs);
            lbSource.DataSource = SourceList;
            lbSource.DisplayMember = "Metadata.Title";

            pbLoading.Style = ProgressBarStyle.Continuous;
            labLoading.Visible = false;

            btnAdd.Enabled = true;
            btnAddAll.Enabled = true;
            btnRemove.Enabled = true;
            btnRemoveAll.Enabled = true;
            btnNext.Enabled = true;
            lbSource.Enabled = true;
            lbTarget.Enabled = true;
        }

        /// <summary>
        /// Displays tracks with proper Title - Artist format
        /// </summary>
        /// <param name="sender">ListBox displaying tracks</param>
        /// <param name="e">Track to be displayed</param>
        private void ListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            Track track = (Track)e.ListItem;

            e.Value = track.Metadata.Title + " - " + track.Metadata.Artist;
        }
        /// <summary>
        /// Cancel and close window
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TargetList = null;
            this.Close();
        }
        /// <summary>
        /// Moves selected tracks in lbSource to lbTarget
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // TODO: make this cleaner and more efficient
            List<Track> tracks = new List<Track>();

            for(int i = lbSource.SelectedIndices.Count - 1; i >= 0; i--)
                tracks.Add(SourceList[lbSource.SelectedIndices[i]]);

            tracks.Reverse();
            foreach(var track in tracks)
            {
                TargetList.Add(track);
                SourceList.Remove(track);
            }

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbTarget.SelectedIndex = lbTarget.Items.Count - 1;
        }
        /// <summary>
        /// Moves selected tracks in lbTarget back to lbSource
        /// </summary>
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            // TODO: make this cleaner and more efficient
            List<Track> tracks = new List<Track>();

            for (int i = lbTarget.SelectedIndices.Count - 1; i >= 0; i--)
                tracks.Add(TargetList[lbTarget.SelectedIndices[i]]);

            tracks.Reverse();
            foreach (var track in tracks)
            {
                SourceList.Add(track);
                TargetList.Remove(track);
            }

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbSource.SelectedIndex = lbSource.Items.Count - 1;
        }
        /// <summary>
        /// Moves all tracks in lbSource to lbTarget
        /// </summary>
        private void BtnAddAll_Click(object sender, EventArgs e)
        {
            foreach(Track item in SourceList)
                TargetList.Add(item);
            SourceList.Clear();

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbTarget.SelectedIndex = lbTarget.Items.Count - 1;
        }
        /// <summary>
        /// Moves all tracks in lbTarget back to lbSource
        /// </summary>
        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            foreach (Track item in TargetList)
                SourceList.Add(item);
            TargetList.Clear();

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbSource.SelectedIndex = lbSource.Items.Count - 1;
        }
        /// <summary>
        /// Checks if everything is set properly and closes the form
        /// </summary>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (TargetList?.Count < 1)
            {
                MessageBox.Show(
                    "Select tracks to convert first.",
                    "Insufficient information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            this.Close();
        }
    }
}
