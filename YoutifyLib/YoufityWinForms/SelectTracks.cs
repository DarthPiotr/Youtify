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
    public partial class SelectTracks : Form
    {
        public List<Track> Tracks {
            get {
                if (TargetList == null)
                    return null;

                return new List<Track>(TargetList);
            }
        }

        private BindingList<Track> SourceList;
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

        private void ListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            Track track = (Track)e.ListItem;

            e.Value = track.Metadata.Title + " - " + track.Metadata.Artist;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TargetList = null;
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
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

        private void BtnRemove_Click(object sender, EventArgs e)
        {
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

        private void BtnAddAll_Click(object sender, EventArgs e)
        {
            foreach(Track item in SourceList)
                TargetList.Add(item);
            SourceList.Clear();

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbTarget.SelectedIndex = lbTarget.Items.Count - 1;
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            foreach (Track item in TargetList)
                SourceList.Add(item);
            TargetList.Clear();

            lbSource.ClearSelected();
            lbTarget.ClearSelected();
            lbSource.SelectedIndex = lbSource.Items.Count - 1;
        }

        private void btnNext_Click(object sender, EventArgs e)
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
