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
        private List<ServiceHandler> Services { get; set; }
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
            Services = new List<ServiceHandler>();
            
            InitializeComponent();
            
            gbStep2.Enabled = listBox1.SelectedIndex >= 0
                           && listBox1.SelectedIndex <= Services.Count;
        }
        /// <summary>
        /// Adding Service
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            var AddServiceWindow = new AddService();
            AddServiceWindow.FormClosed += (s, e) =>
            {
                var addServ = s as AddService;
                if (addServ.Service != null)
                {
                    Services.Add(addServ.Service);
                    listBox1.Items.Add(addServ.Service);
                }
            };
            AddServiceWindow.ShowDialog();
        }
        /// <summary>
        /// Removing Service from list
        /// </summary>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex <= Services.Count)
            {
                Services.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                PlaylistId = "";
            }
        }
        /// <summary>
        /// Selecting Service
        /// </summary>
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labSelectedService.Text = string.Format(
                "Selected service: {0}",
                listBox1.SelectedItem);
            PlaylistId = "";

            gbStep2.Enabled = listBox1.SelectedIndex >= 0
                           && listBox1.SelectedIndex <= Services.Count;
        }
        /// <summary>
        /// Browsing user playlists
        /// </summary>
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            gbStep1.Enabled = false;

            var serv = Services[listBox1.SelectedIndex];

            gbStep1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gbStep1.Enabled = false;

            var selUrl = new SelectUrl(Services[listBox1.SelectedIndex]);

            selUrl.FormClosed += (s, e) =>
            {
                var urlwin = s as SelectUrl;
                PlaylistId = urlwin.Id;
            };

            selUrl.ShowDialog();

            gbStep1.Enabled = true;
        }
    }
}
