using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        /// <summary>
        /// List of all added services with its roles
        /// </summary>
        private List<ServiceRole> Services { get; set; }
        /// <summary>
        /// Id of the source playlist
        /// </summary>
        private string SourceId
        {
            get => sourceid;
            set
            {
                labSelectedIdInput.Text = "Selected id: " + value;
                sourceid = value;
            }
        }
        /// <summary>
        /// Id of the target playlist
        /// </summary>
        private string TargetId
        {
            get => targetid;
            set
            {
                labSelectedIdOutput.Text = "Selected id: " + value;
                targetid = value;
            }
        }
        private string sourceid;
        private string targetid;

        public Main()
        {
            InitializeComponent();
            Services = new List<ServiceRole>();
            
            btnSelectInputUrl.Enabled = false;
            btnBrowseInput.Enabled = false;
            btnSelectOutputUrl.Enabled = false;
            btnBrowseOutput.Enabled = false;
            btnDelService.Enabled = false;
        }

        /// <summary>
        /// Selects source service
        /// </summary>
        /// <param name="index">Index of the service in Services</param>
        private void SelectSourceService(int index)
        {
            for (int i = 0; i < Services.Count; i++)
                Services[i].Source = i == index;
        }
        /// <summary>
        /// Selects target service
        /// </summary>
        /// <param name="index">Index of the service in Services</param>
        private void SelectTargetService(int index)
        {
            for (int i = 0; i < Services.Count; i++)
                Services[i].Target = i == index;
        }

        /////////////////////////////
        //      Event Handlers

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
                SourceId = "";
            }
        }
        /// <summary>
        /// Delete key handling
        /// </summary>
        private void LbServices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                BtnDelService_Click(sender, e);
        }
        /// <summary>
        /// Selecting Service
        /// </summary>
        private void LbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelService.Enabled = lbServices.SelectedIndex != -1;
        }
        /// <summary>
        /// Selecting input Url
        /// </summary>
        private void BtnSelectInputUrl_Click(object sender, EventArgs e)
        {
            var selUrl = new SelectUrl(Services[cbSourceService.SelectedIndex].Service);
            selUrl.ShowDialog();

            SourceId = selUrl.Id;
        }
        /// <summary>
        /// Browsing for input url
        /// </summary>
        private void BtnBrowseInput_Click(object sender, EventArgs e)
        {
            var browsePlaylists = new BrowsePlaylists(Services[cbSourceService.SelectedIndex].Service);
            browsePlaylists.ShowDialog();

            SourceId = browsePlaylists.SelectedPlaylist.Id;
        }
        /// <summary>
        /// Adding Service, making sure everything is the same
        /// </summary>
        /// <param name="service">Service to add</param>
        private void AddService(ServiceHandler service)
        {
            Services.Add(new ServiceRole(service));
            lbServices.Items.Add(service.Name);
            cbSourceService.Items.Add(service.Name);
            cbTargetService.Items.Add(service.Name);
        }
        /// <summary>
        /// Removing Service, making sure everything is the same
        /// </summary>
        /// <param name="service">Service to add</param>
        private void RemoveServiceAt(int index)
        {
            var result = MessageBox.Show(
                "Deleting any service will reset all selected playlists.\nDo you wish to continue?",
                "Deleting service",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
                return;
            
            Services.RemoveAt(index);
            lbServices.Items.RemoveAt(index);

            SourceId = "";
            cbSourceService.Items.RemoveAt(index);
            cbSourceService.SelectedIndex = -1;
            cbSourceService.SelectedIndex = -1;

            TargetId = "";
            cbTargetService.Items.RemoveAt(index);
            cbTargetService.SelectedIndex = -1;
            cbTargetService.SelectedIndex = -1;
        }
        /// <summary>
        /// Selecting input service
        /// </summary>
        private void CbSourceService_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectInputUrl.Enabled =
            btnBrowseInput.Enabled =
                cbSourceService.SelectedIndex != -1;

            SelectSourceService(cbTargetService.SelectedIndex);
        }
        /// <summary>
        /// Selecting target service
        /// </summary>
        private void CbTargetService_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectOutputUrl.Enabled =
            btnBrowseOutput.Enabled =
                cbTargetService.SelectedIndex != -1;

            SelectTargetService(cbTargetService.SelectedIndex);
        }   
    }
}
