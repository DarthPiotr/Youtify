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
            this.labSelectedIdInput = new System.Windows.Forms.Label();
            this.labSelectedServiceInput = new System.Windows.Forms.Label();
            this.gbStep3 = new System.Windows.Forms.GroupBox();
            this.cbTargetService = new System.Windows.Forms.ComboBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnNewOutput = new System.Windows.Forms.Button();
            this.btnSelectOutputUrl = new System.Windows.Forms.Button();
            this.labSelectedIdOutput = new System.Windows.Forms.Label();
            this.labSelectedServiceOutput = new System.Windows.Forms.Label();
            this.gbStep1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.gbStep3.SuspendLayout();
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
            this.gbStep2.Controls.Add(this.labSelectedIdInput);
            this.gbStep2.Controls.Add(this.labSelectedServiceInput);
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
            this.btnBrowseInput.Text = "Browse playlists";
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
            // labSelectedIdInput
            // 
            this.labSelectedIdInput.AutoSize = true;
            this.labSelectedIdInput.Location = new System.Drawing.Point(18, 60);
            this.labSelectedIdInput.Name = "labSelectedIdInput";
            this.labSelectedIdInput.Size = new System.Drawing.Size(86, 20);
            this.labSelectedIdInput.TabIndex = 2;
            this.labSelectedIdInput.Text = "Selected id:";
            // 
            // labSelectedServiceInput
            // 
            this.labSelectedServiceInput.AutoSize = true;
            this.labSelectedServiceInput.Location = new System.Drawing.Point(18, 32);
            this.labSelectedServiceInput.Name = "labSelectedServiceInput";
            this.labSelectedServiceInput.Size = new System.Drawing.Size(59, 20);
            this.labSelectedServiceInput.TabIndex = 1;
            this.labSelectedServiceInput.Text = "Service:";
            // 
            // gbStep3
            // 
            this.gbStep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbStep3.Controls.Add(this.cbTargetService);
            this.gbStep3.Controls.Add(this.btnBrowseOutput);
            this.gbStep3.Controls.Add(this.btnNewOutput);
            this.gbStep3.Controls.Add(this.btnSelectOutputUrl);
            this.gbStep3.Controls.Add(this.labSelectedIdOutput);
            this.gbStep3.Controls.Add(this.labSelectedServiceOutput);
            this.gbStep3.Location = new System.Drawing.Point(272, 184);
            this.gbStep3.Name = "gbStep3";
            this.gbStep3.Size = new System.Drawing.Size(346, 157);
            this.gbStep3.TabIndex = 3;
            this.gbStep3.TabStop = false;
            this.gbStep3.Text = "Step 3: Select target playlist";
            // 
            // cbTargetService
            // 
            this.cbTargetService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetService.FormattingEnabled = true;
            this.cbTargetService.Location = new System.Drawing.Point(83, 29);
            this.cbTargetService.Name = "cbTargetService";
            this.cbTargetService.Size = new System.Drawing.Size(238, 28);
            this.cbTargetService.TabIndex = 3;
            this.cbTargetService.SelectedIndexChanged += new System.EventHandler(this.CbTargetService_SelectedIndexChanged);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(100, 87);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(129, 58);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "Browse playlists";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.BtnBrowseOutput_Click);
            // 
            // btnNewOutput
            // 
            this.btnNewOutput.Location = new System.Drawing.Point(235, 87);
            this.btnNewOutput.Name = "btnNewOutput";
            this.btnNewOutput.Size = new System.Drawing.Size(88, 58);
            this.btnNewOutput.TabIndex = 4;
            this.btnNewOutput.Text = "Create new";
            this.btnNewOutput.UseVisualStyleBackColor = true;
            this.btnNewOutput.Click += new System.EventHandler(this.BtnNewOutput_Click);
            // 
            // btnSelectOutputUrl
            // 
            this.btnSelectOutputUrl.Location = new System.Drawing.Point(6, 87);
            this.btnSelectOutputUrl.Name = "btnSelectOutputUrl";
            this.btnSelectOutputUrl.Size = new System.Drawing.Size(88, 58);
            this.btnSelectOutputUrl.TabIndex = 4;
            this.btnSelectOutputUrl.Text = "Select Url";
            this.btnSelectOutputUrl.UseVisualStyleBackColor = true;
            this.btnSelectOutputUrl.Click += new System.EventHandler(this.BtnSelectOutputUrl_Click);
            // 
            // labSelectedIdOutput
            // 
            this.labSelectedIdOutput.AutoSize = true;
            this.labSelectedIdOutput.Location = new System.Drawing.Point(18, 60);
            this.labSelectedIdOutput.Name = "labSelectedIdOutput";
            this.labSelectedIdOutput.Size = new System.Drawing.Size(86, 20);
            this.labSelectedIdOutput.TabIndex = 2;
            this.labSelectedIdOutput.Text = "Selected id:";
            // 
            // labSelectedServiceOutput
            // 
            this.labSelectedServiceOutput.AutoSize = true;
            this.labSelectedServiceOutput.Location = new System.Drawing.Point(18, 32);
            this.labSelectedServiceOutput.Name = "labSelectedServiceOutput";
            this.labSelectedServiceOutput.Size = new System.Drawing.Size(59, 20);
            this.labSelectedServiceOutput.TabIndex = 1;
            this.labSelectedServiceOutput.Text = "Service:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 409);
            this.Controls.Add(this.gbStep3);
            this.Controls.Add(this.gbStep2);
            this.Controls.Add(this.gbStep1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtify";
            this.gbStep1.ResumeLayout(false);
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.gbStep3.ResumeLayout(false);
            this.gbStep3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbServices;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnDelService;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.Label labSelectedServiceInput;
        private System.Windows.Forms.Label labSelectedIdInput;
        private System.Windows.Forms.Button btnSelectInputUrl;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.ComboBox cbSourceService;
        private System.Windows.Forms.GroupBox gbStep3;
        private System.Windows.Forms.ComboBox cbTargetService;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnSelectOutputUrl;
        private System.Windows.Forms.Label labSelectedIdOutput;
        private System.Windows.Forms.Label labSelectedServiceOutput;
        private System.Windows.Forms.Button btnNewOutput;
    }
}

