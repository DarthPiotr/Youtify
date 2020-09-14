namespace YoufityWinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lbServices = new System.Windows.Forms.ListBox();
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnDelService = new System.Windows.Forms.Button();
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.cbSourceService = new System.Windows.Forms.ComboBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnSelectInputUrl = new System.Windows.Forms.Button();
            this.labSelectedId = new System.Windows.Forms.Label();
            this.labSelectedService = new System.Windows.Forms.Label();
            this.gbStep1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbServices
            // 
            this.lbServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbServices.FormattingEnabled = true;
            this.lbServices.IntegralHeight = false;
            this.lbServices.ItemHeight = 20;
            this.lbServices.Location = new System.Drawing.Point(6, 26);
            this.lbServices.Name = "lbServices";
            this.lbServices.Size = new System.Drawing.Size(242, 303);
            this.lbServices.TabIndex = 0;
            this.lbServices.SelectedIndexChanged += new System.EventHandler(this.LbServices_SelectedIndexChanged);
            this.lbServices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LbServices_KeyDown);
            // 
            // btnAddService
            // 
            this.btnAddService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddService.Location = new System.Drawing.Point(104, 335);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(144, 39);
            this.btnAddService.TabIndex = 1;
            this.btnAddService.Text = "Add new...";
            this.btnAddService.UseVisualStyleBackColor = true;
            this.btnAddService.Click += new System.EventHandler(this.BtnAddService_Click);
            // 
            // btnDelService
            // 
            this.btnDelService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelService.Location = new System.Drawing.Point(6, 335);
            this.btnDelService.Name = "btnDelService";
            this.btnDelService.Size = new System.Drawing.Size(94, 39);
            this.btnDelService.TabIndex = 2;
            this.btnDelService.Text = "Delete";
            this.btnDelService.UseVisualStyleBackColor = true;
            this.btnDelService.Click += new System.EventHandler(this.BtnDelService_Click);
            // 
            // gbStep1
            // 
            this.gbStep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbStep1.Controls.Add(this.lbServices);
            this.gbStep1.Controls.Add(this.btnAddService);
            this.gbStep1.Controls.Add(this.btnDelService);
            this.gbStep1.Location = new System.Drawing.Point(12, 12);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(254, 385);
            this.gbStep1.TabIndex = 0;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Step 1: Add Services";
            // 
            // gbStep2
            // 
            this.gbStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbStep2.Controls.Add(this.cbSourceService);
            this.gbStep2.Controls.Add(this.btnBrowseInput);
            this.gbStep2.Controls.Add(this.btnSelectInputUrl);
            this.gbStep2.Controls.Add(this.labSelectedId);
            this.gbStep2.Controls.Add(this.labSelectedService);
            this.gbStep2.Location = new System.Drawing.Point(272, 13);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(346, 157);
            this.gbStep2.TabIndex = 3;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2: Select source playlist";
            // 
            // cbSourceService
            // 
            this.cbSourceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceService.FormattingEnabled = true;
            this.cbSourceService.Location = new System.Drawing.Point(83, 29);
            this.cbSourceService.Name = "cbSourceService";
            this.cbSourceService.Size = new System.Drawing.Size(238, 28);
            this.cbSourceService.TabIndex = 3;
            this.cbSourceService.SelectedIndexChanged += new System.EventHandler(this.CbSourceService_SelectedIndexChanged);
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Location = new System.Drawing.Point(180, 87);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(160, 58);
            this.btnBrowseInput.TabIndex = 5;
            this.btnBrowseInput.Text = "Browse playlists (unavailable)";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.BtnBrowseInput_Click);
            // 
            // btnSelectInputUrl
            // 
            this.btnSelectInputUrl.Location = new System.Drawing.Point(6, 87);
            this.btnSelectInputUrl.Name = "btnSelectInputUrl";
            this.btnSelectInputUrl.Size = new System.Drawing.Size(151, 58);
            this.btnSelectInputUrl.TabIndex = 4;
            this.btnSelectInputUrl.Text = "Select Url";
            this.btnSelectInputUrl.UseVisualStyleBackColor = true;
            this.btnSelectInputUrl.Click += new System.EventHandler(this.BtnSelectInputUrl_Click);
            // 
            // labSelectedId
            // 
            this.labSelectedId.AutoSize = true;
            this.labSelectedId.Location = new System.Drawing.Point(18, 60);
            this.labSelectedId.Name = "labSelectedId";
            this.labSelectedId.Size = new System.Drawing.Size(86, 20);
            this.labSelectedId.TabIndex = 2;
            this.labSelectedId.Text = "Selected id:";
            // 
            // labSelectedService
            // 
            this.labSelectedService.AutoSize = true;
            this.labSelectedService.Location = new System.Drawing.Point(18, 32);
            this.labSelectedService.Name = "labSelectedService";
            this.labSelectedService.Size = new System.Drawing.Size(59, 20);
            this.labSelectedService.TabIndex = 1;
            this.labSelectedService.Text = "Service:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 409);
            this.Controls.Add(this.gbStep2);
            this.Controls.Add(this.gbStep1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtify";
            this.gbStep1.ResumeLayout(false);
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbServices;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnDelService;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.Label labSelectedService;
        private System.Windows.Forms.Label labSelectedId;
        private System.Windows.Forms.Button btnSelectInputUrl;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.ComboBox cbSourceService;
    }
}

