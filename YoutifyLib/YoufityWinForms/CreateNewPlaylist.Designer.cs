namespace YoufityWinForms
{
    partial class CreateNewPlaylist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labTitle = new System.Windows.Forms.Label();
            this.labDescription = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.rbPublic = new System.Windows.Forms.RadioButton();
            this.gbPrivacy = new System.Windows.Forms.GroupBox();
            this.rbUnlisted = new System.Windows.Forms.RadioButton();
            this.rbPrivate = new System.Windows.Forms.RadioButton();
            this.cbCreate = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbCreating = new System.Windows.Forms.ProgressBar();
            this.gbPrivacy.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(12, 9);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(38, 20);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "Title";
            // 
            // labDescription
            // 
            this.labDescription.AutoSize = true;
            this.labDescription.Location = new System.Drawing.Point(12, 62);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(85, 20);
            this.labDescription.TabIndex = 0;
            this.labDescription.Text = "Description";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.BackColor = System.Drawing.SystemColors.Window;
            this.tbTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbTitle.Location = new System.Drawing.Point(12, 32);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(298, 27);
            this.tbTitle.TabIndex = 1;
            this.tbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetColor);
            // 
            // rtbDescription
            // 
            this.rtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDescription.Location = new System.Drawing.Point(12, 85);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(298, 190);
            this.rtbDescription.TabIndex = 2;
            this.rtbDescription.Text = "";
            // 
            // rbPublic
            // 
            this.rbPublic.AutoSize = true;
            this.rbPublic.Checked = true;
            this.rbPublic.Location = new System.Drawing.Point(6, 26);
            this.rbPublic.Name = "rbPublic";
            this.rbPublic.Size = new System.Drawing.Size(70, 24);
            this.rbPublic.TabIndex = 3;
            this.rbPublic.TabStop = true;
            this.rbPublic.Text = "Public";
            this.rbPublic.UseVisualStyleBackColor = true;
            // 
            // gbPrivacy
            // 
            this.gbPrivacy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbPrivacy.Controls.Add(this.rbUnlisted);
            this.gbPrivacy.Controls.Add(this.rbPrivate);
            this.gbPrivacy.Controls.Add(this.rbPublic);
            this.gbPrivacy.Location = new System.Drawing.Point(12, 278);
            this.gbPrivacy.Name = "gbPrivacy";
            this.gbPrivacy.Size = new System.Drawing.Size(98, 116);
            this.gbPrivacy.TabIndex = 4;
            this.gbPrivacy.TabStop = false;
            this.gbPrivacy.Text = "Privacy";
            // 
            // rbUnlisted
            // 
            this.rbUnlisted.AutoSize = true;
            this.rbUnlisted.Location = new System.Drawing.Point(6, 86);
            this.rbUnlisted.Name = "rbUnlisted";
            this.rbUnlisted.Size = new System.Drawing.Size(84, 24);
            this.rbUnlisted.TabIndex = 3;
            this.rbUnlisted.Text = "Unlisted";
            this.rbUnlisted.UseVisualStyleBackColor = true;
            // 
            // rbPrivate
            // 
            this.rbPrivate.AutoSize = true;
            this.rbPrivate.Location = new System.Drawing.Point(6, 56);
            this.rbPrivate.Name = "rbPrivate";
            this.rbPrivate.Size = new System.Drawing.Size(75, 24);
            this.rbPrivate.TabIndex = 3;
            this.rbPrivate.Text = "Private";
            this.rbPrivate.UseVisualStyleBackColor = true;
            // 
            // cbCreate
            // 
            this.cbCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCreate.AutoSize = true;
            this.cbCreate.Location = new System.Drawing.Point(116, 289);
            this.cbCreate.Name = "cbCreate";
            this.cbCreate.Size = new System.Drawing.Size(106, 24);
            this.cbCreate.TabIndex = 5;
            this.cbCreate.Text = "Create now";
            this.cbCreate.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(222, 286);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(30, 29);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(222, 336);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(88, 58);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(116, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // pbCreating
            // 
            this.pbCreating.Location = new System.Drawing.Point(12, 400);
            this.pbCreating.Name = "pbCreating";
            this.pbCreating.Size = new System.Drawing.Size(298, 20);
            this.pbCreating.TabIndex = 9;
            // 
            // CreateNewPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 432);
            this.ControlBox = false;
            this.Controls.Add(this.pbCreating);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.cbCreate);
            this.Controls.Add(this.gbPrivacy);
            this.Controls.Add(this.rtbDescription);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.labTitle);
            this.MaximumSize = new System.Drawing.Size(341, 1000);
            this.MinimumSize = new System.Drawing.Size(341, 450);
            this.Name = "CreateNewPlaylist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new playlist";
            this.gbPrivacy.ResumeLayout(false);
            this.gbPrivacy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Label labDescription;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.RadioButton rbPublic;
        private System.Windows.Forms.GroupBox gbPrivacy;
        private System.Windows.Forms.RadioButton rbPrivate;
        private System.Windows.Forms.RadioButton rbUnlisted;
        private System.Windows.Forms.CheckBox cbCreate;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pbCreating;
    }
}