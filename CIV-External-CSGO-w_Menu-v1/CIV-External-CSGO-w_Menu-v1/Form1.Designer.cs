﻿namespace CIV_External_CSGO_w_Menu_v1
{
    partial class MenuForm
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
            this.MenuBrowser = new System.Windows.Forms.WebBrowser();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // MenuBrowser
            // 
            this.MenuBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuBrowser.Location = new System.Drawing.Point(0, 0);
            this.MenuBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.MenuBrowser.Name = "MenuBrowser";
            this.MenuBrowser.Size = new System.Drawing.Size(584, 761);
            this.MenuBrowser.TabIndex = 0;
            this.MenuBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.MenuBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.MenuBrowser_DocumentCompleted);
            // 
            // ApplyButton
            // 
            this.ApplyButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.ApplyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ApplyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyButton.Location = new System.Drawing.Point(489, 693);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(69, 56);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = false;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(375, 53);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(154, 290);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 761);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.MenuBrowser);
            this.Name = "MenuForm";
            this.Text = "CIV - CS:GO External v1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser MenuBrowser;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.ListBox listBox1;
    }
}

