using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YoutifyLib;
using YoutifyLib.Spotify;
using YoutifyLib.YouTube;

namespace YoufityWinForms
{
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            switch (lbServices.SelectedIndex)
            {
                case 0:
                    Service = new YouTubeHandler();
                    break;
                case 1:
                    Service = new SpotifyHandler();
                    break;
                default:
                    MessageBox.Show("Select service first!", "Invalid service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            this.Close();
        }
    }
}
