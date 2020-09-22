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
    public partial class SelectUrl : Form
    {
        ServiceHandler service;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                btnSelect.Enabled = !string.IsNullOrEmpty(id);
            }
        }
        string id;

        public SelectUrl(ServiceHandler handler)
        {
            InitializeComponent();
            service = handler;
            Id = string.Empty;
        }

        /// <summary>
        /// Searching for Url
        /// </summary>
        private async void BtnSearchUrl_Click(object sender, EventArgs e)
        {
            tbUrl.Enabled =
            btnCancel.Enabled =
            btnSelect.Enabled =
            btnSearch.Enabled = false;

            var id = tbUrl.Text;
            // ugly, I know
            if (id.Contains("spotify"))
            {
                int begin = id.LastIndexOf('/');

                int length;
                if (id.IndexOf('?', begin) >= 0)
                    length = id.IndexOf('?', begin) - begin - 1;
                else
                    length = id.Length - begin - 1;

                id = id.Substring(begin + 1, length);
            }
            else if (id.Contains("youtube"))
            {
                int begin = id.IndexOf("list=") + 4;

                int length;
                if (id.IndexOf('&', begin) >= 0)
                    length = id.IndexOf('&', begin) - begin - 1;
                else
                    length = id.Length - begin - 1;

                id = id.Substring(begin + 1, length);
            }

            labId.Text = id;
            Playlist plinfo = null;

            Task task = new Task(() => { plinfo = service.ImportPlaylist(id, true); });
            task.Start();
            await task;

            if (plinfo == null)
            {
                Id = "";
                labTitle.Text = "Invalid id!";
                labDescription.Text = "Invalid id!";
            }
            else
            {
                Id = id;
                labTitle.Text = plinfo.Title;
                labDescription.Text = plinfo.Description;
            }

            tbUrl.Enabled =
            btnCancel.Enabled =
            btnSelect.Enabled =
            btnSearch.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Id = string.Empty;
            Close();
        }
    }
}
