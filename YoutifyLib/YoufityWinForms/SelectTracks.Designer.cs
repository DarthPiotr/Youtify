namespace YoufityWinForms
{
    partial class SelectTracks
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
            this.lbSource = new System.Windows.Forms.ListBox();
            this.labSource = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.ListBox();
            this.labTarget = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.labLoading = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.labDescription = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSource
            // 
            this.lbSource.FormattingEnabled = true;
            this.lbSource.IntegralHeight = false;
            this.lbSource.ItemHeight = 20;
            this.lbSource.Location = new System.Drawing.Point(12, 69);
            this.lbSource.Name = "lbSource";
            this.lbSource.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSource.Size = new System.Drawing.Size(350, 271);
            this.lbSource.TabIndex = 0;
            this.lbSource.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ListBox_Format);
            // 
            // labSource
            // 
            this.labSource.AutoSize = true;
            this.labSource.Location = new System.Drawing.Point(12, 46);
            this.labSource.Name = "labSource";
            this.labSource.Size = new System.Drawing.Size(105, 20);
            this.labSource.TabIndex = 1;
            this.labSource.Text = "Source playlist";
            // 
            // lbTarget
            // 
            this.lbTarget.FormattingEnabled = true;
            this.lbTarget.IntegralHeight = false;
            this.lbTarget.ItemHeight = 20;
            this.lbTarget.Location = new System.Drawing.Point(412, 69);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTarget.Size = new System.Drawing.Size(350, 271);
            this.lbTarget.TabIndex = 0;
            this.lbTarget.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ListBox_Format);
            // 
            // labTarget
            // 
            this.labTarget.AutoSize = true;
            this.labTarget.Location = new System.Drawing.Point(412, 46);
            this.labTarget.Name = "labTarget";
            this.labTarget.Size = new System.Drawing.Size(101, 20);
            this.labTarget.TabIndex = 1;
            this.labTarget.Text = "Target playlist";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(369, 123);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(37, 37);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(369, 166);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(37, 37);
            this.btnAddAll.TabIndex = 2;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(369, 209);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(37, 37);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(369, 252);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(37, 37);
            this.btnRemoveAll.TabIndex = 2;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // pbLoading
            // 
            this.pbLoading.Location = new System.Drawing.Point(12, 348);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(350, 32);
            this.pbLoading.TabIndex = 3;
            // 
            // labLoading
            // 
            this.labLoading.AutoSize = true;
            this.labLoading.Location = new System.Drawing.Point(12, 383);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(114, 20);
            this.labLoading.TabIndex = 4;
            this.labLoading.Text = "Loading tracks...";
            this.labLoading.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(668, 385);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(94, 29);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labDescription
            // 
            this.labDescription.AutoSize = true;
            this.labDescription.Location = new System.Drawing.Point(12, 9);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(264, 20);
            this.labDescription.TabIndex = 6;
            this.labDescription.Text = "Select tracks you want to be converted";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(568, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SelectTracks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 426);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.labLoading);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labTarget);
            this.Controls.Add(this.labSource);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.lbSource);
            this.Name = "SelectTracks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Tracks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSource;
        private System.Windows.Forms.Label labSource;
        private System.Windows.Forms.ListBox lbTarget;
        private System.Windows.Forms.Label labTarget;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label labLoading;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label labDescription;
        private System.Windows.Forms.Button btnCancel;
    }
}