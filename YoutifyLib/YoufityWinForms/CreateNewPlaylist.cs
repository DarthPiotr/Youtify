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
    public partial class CreateNewPlaylist : Form
    {
        /// <summary>
        /// Playlist created with this form. May be null or Id may be not net if "Create now" checkbox is not set
        /// </summary>
        internal Playlist CreatedPlaylist;
        /// <summary>
        /// Service to make calls
        /// </summary>
        private readonly ServiceHandler Service;

        public CreateNewPlaylist(ServiceHandler service)
        {
            InitializeComponent();

            Service = service;
            if (service.Name != "YouTube")
                rbUnlisted.Enabled = false;
        }
        /// <summary>
        /// Help button for Create now
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "If checked, will create empty playlist with provided title, description and privacy status, when \"Create\" button is pressed. If anything goes wrong later, this playlist will remain empty on your account.\n\n" +
                "If not checked, playlist will be created later, after converting songs.",
                "Create now - Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Question);
        }
        /// <summary>
        /// Cancel button
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CreatedPlaylist = null;
            this.Close();
        }
        /// <summary>
        /// Create playlist
        /// </summary>
        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            tbTitle.Enabled =
            rtbDescription.Enabled =
            gbPrivacy.Enabled =
            cbCreate.Enabled =
            btnCancel.Enabled =
            btnCreate.Enabled = false;
            pbCreating.Style = ProgressBarStyle.Marquee;

            bool canCreate = true;

            if (!string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                Playlist playlist = new Playlist()
                {
                    Title = tbTitle.Text.Trim(),
                    Description = rtbDescription.Text.Trim()
                };
                if (rbPrivate.Checked) playlist.Status = "private";
                else if (rbPublic.Checked) playlist.Status = "public";
                else if (rbUnlisted.Checked) playlist.Status = "unlisted";

                if (cbCreate.Checked)
                {
                    Task t = new Task(() =>
                    {
                        Service.CreatePlaylist(ref playlist);
                        Service.UpdateSnippet(playlist);
                    });
                    t.Start();
                    await t;
                }

                CreatedPlaylist = playlist;
            }
            else
            {
                canCreate = false;
                tbTitle.BackColor = Color.Salmon;
                MessageBox.Show("Title is required", "Insufficient information", MessageBoxButtons.OK);
            }

            pbCreating.Style = ProgressBarStyle.Continuous;
            tbTitle.Enabled =
            rtbDescription.Enabled =
            gbPrivacy.Enabled =
            cbCreate.Enabled =
            btnCancel.Enabled =
            btnCreate.Enabled = true;
            
            if(canCreate)
                this.Close();
        }
        /// <summary>
        /// Set color back to normal
        /// </summary>
        private void SetColor(object sender, KeyEventArgs e)
        {
            var control = sender as Control;
            control.BackColor = SystemColors.Window;
        }
    }
}
