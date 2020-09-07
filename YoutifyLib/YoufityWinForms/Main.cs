using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutifyLib;

namespace YoufityWinForms
{
    public partial class Main : Form
    {
        private List<ServiceRole> Services { get; set; }
        private string PlaylistId
        {
            get => playlistid;
            set
            {
                labSelectedId.Text = "Selected id: " + value;
                playlistid = value;
            }
        }
        private string playlistid;

        public Main()
        {
            InitializeComponent();
            Services = new List<ServiceRole>();
            
            btnSelectInputUrl.Enabled = false;
            btnDelService.Enabled = false;
        }
        /// <summary>
        /// Adding Service
        /// </summary>
        private void BtnAddService_Click(object sender, EventArgs e)
        {
            var AddServiceWindow = new AddService();
            AddServiceWindow.FormClosed += (s, e) =>
            {
                var addServ = s as AddService;
                if (addServ.Service != null)
                {
                    AddService(addServ.Service);
                }
            };
            AddServiceWindow.ShowDialog();

        }
        /// <summary>
        /// Removing Service from list
        /// </summary>
        private void BtnDelService_Click(object sender, EventArgs e)
        {
            if (lbServices.SelectedIndex >= 0 && lbServices .SelectedIndex <= Services.Count)
            {
                RemoveServiceAt(lbServices.SelectedIndex);
                PlaylistId = "";
            }
        }
        /// <summary>
        /// Selecting Service
        /// </summary>
        private void LbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelService.Enabled = lbServices.SelectedIndex != -1;
        }
        /// <summary>
        /// Browsing user playlists
        /// </summary>
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            gbStep1.Enabled = false;

            var serv = Services[lbServices.SelectedIndex];

            gbStep1.Enabled = true;
        }

        private void BtnSelectInputUrl_Click(object sender, EventArgs e)
        {
            gbStep1.Enabled = false;

            var selUrl = new SelectUrl(Services[lbServices.SelectedIndex].Service);

            selUrl.FormClosed += (s, e) =>
            {
                var urlwin = s as SelectUrl;
                PlaylistId = urlwin.Id;
            };

            selUrl.ShowDialog();

            gbStep1.Enabled = true;
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            int i = 0;
        }

        private void AddService(ServiceHandler sh)
        {
            Services.Add(new ServiceRole(sh));
            lbServices.Items.Add(sh.Name);
            cbSourceService.Items.Add(sh.Name);
        }

        private void RemoveServiceAt(int index)
        {
            Services.RemoveAt(index);
            lbServices.Items.RemoveAt(index);
            cbSourceService.Items.RemoveAt(index);

            CbSourceService_SelectedIndexChanged(lbServices, null);
        }

        private void CbSourceService_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectInputUrl.Enabled = cbSourceService.SelectedIndex != -1;
            for (int i = 0; i < Services.Count; i++)
                Services[i].Source = i == cbSourceService.SelectedIndex;
        }

        private void LbServices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                BtnDelService_Click(sender, e);
        }
    }
}
