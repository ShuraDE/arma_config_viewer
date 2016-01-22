namespace arma_config_viewer
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNewConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.trvObj = new System.Windows.Forms.TreeView();
            this.lstDetail = new System.Windows.Forms.ListView();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1080, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNewConfigToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // loadNewConfigToolStripMenuItem
            // 
            this.loadNewConfigToolStripMenuItem.Name = "loadNewConfigToolStripMenuItem";
            this.loadNewConfigToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadNewConfigToolStripMenuItem.Text = "Load new config";
            this.loadNewConfigToolStripMenuItem.Click += new System.EventHandler(this.loadNewConfigToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // dlgFile
            // 
            this.dlgFile.FileName = "openFileDialog1";
            // 
            // trvObj
            // 
            this.trvObj.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvObj.Location = new System.Drawing.Point(0, 24);
            this.trvObj.Name = "trvObj";
            this.trvObj.Size = new System.Drawing.Size(325, 445);
            this.trvObj.TabIndex = 1;
            this.trvObj.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvObj_NodeMouseClick);
            // 
            // lstDetail
            // 
            this.lstDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDetail.Location = new System.Drawing.Point(325, 24);
            this.lstDetail.Name = "lstDetail";
            this.lstDetail.Size = new System.Drawing.Size(755, 445);
            this.lstDetail.TabIndex = 2;
            this.lstDetail.UseCompatibleStateImageBehavior = false;
            this.lstDetail.View = System.Windows.Forms.View.Details;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 469);
            this.Controls.Add(this.lstDetail);
            this.Controls.Add(this.trvObj);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNewConfigToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgFile;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TreeView trvObj;
        private System.Windows.Forms.ListView lstDetail;
    }
}

