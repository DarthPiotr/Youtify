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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.labSelectedId = new System.Windows.Forms.Label();
            this.labSelectedService = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.gbStep1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(6, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(315, 303);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(177, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add new...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(6, 335);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 39);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // gbStep1
            // 
            this.gbStep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbStep1.Controls.Add(this.listBox1);
            this.gbStep1.Controls.Add(this.button1);
            this.gbStep1.Controls.Add(this.button2);
            this.gbStep1.Location = new System.Drawing.Point(12, 12);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(327, 385);
            this.gbStep1.TabIndex = 2;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Step 1: Add Services";
            // 
            // gbStep2
            // 
            this.gbStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbStep2.Controls.Add(this.btnBrowse);
            this.gbStep2.Controls.Add(this.button3);
            this.gbStep2.Controls.Add(this.labSelectedId);
            this.gbStep2.Controls.Add(this.labSelectedService);
            this.gbStep2.Location = new System.Drawing.Point(345, 13);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(273, 157);
            this.gbStep2.TabIndex = 3;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2: Select playlist";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 58);
            this.button3.TabIndex = 3;
            this.button3.Text = "Select Url";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labSelectedId
            // 
            this.labSelectedId.AutoSize = true;
            this.labSelectedId.Location = new System.Drawing.Point(18, 52);
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
            this.labSelectedService.Size = new System.Drawing.Size(155, 20);
            this.labSelectedService.TabIndex = 1;
            this.labSelectedService.Text = "Selected service: none";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(137, 87);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(125, 58);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse playlists (unavailable)";
            this.btnBrowse.UseVisualStyleBackColor = true;
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
            this.Text = "Youtify";
            this.gbStep1.ResumeLayout(false);
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.Label labSelectedService;
        private System.Windows.Forms.Label labSelectedId;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnBrowse;
    }
}

