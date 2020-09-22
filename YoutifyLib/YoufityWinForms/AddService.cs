using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutifyLib;
using YoutifyLib.Spotify;
using YoutifyLib.YouTube;

namespace YoufityWinForms
{
    /// <summary>
    /// Form for adding new service.
    /// Available via Service property after closing form.
    /// </summary>
    public partial class AddService : Form
    {
        internal ServiceHandler Service;

        public AddService()
        {
            InitializeComponent();
            lbServices.Items.Add("YouTube");
            lbServices.Items.Add("Spotify");
            Service = null;
        }
        /// <summary>
        /// Adding selected service
        /// </summary>
        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            pbAdd.Style = ProgressBarStyle.Marquee;
            lbServices.Enabled = btnAdd.Enabled = false;

            Task task;
            switch (lbServices.SelectedIndex)
            {
                case 0:
                    task = new Task(() => { Service = new YouTubeHandler(); });
                    task.Start();
                    await task;
                    break;
                case 1:
                    task = new Task(() => { Service = new SpotifyHandler(); });
                    task.Start();
                    await task;
                    break;
                default:
                    MessageBox.Show("Select service first!", "Invalid service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lbServices.Enabled = btnAdd.Enabled = true;
                    pbAdd.Style = ProgressBarStyle.Continuous;
                    return;
            }

            lbServices.Enabled = btnAdd.Enabled = true;
            pbAdd.Style = ProgressBarStyle.Continuous;

            this.Close();
        }
    }
}
