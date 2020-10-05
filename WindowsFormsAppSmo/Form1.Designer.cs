namespace WindowsFormsAppSmo
{
    partial class Form1
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
            this.lstDatabaseNames = new System.Windows.Forms.ListBox();
            this.cmdLoadDatabaseNames = new System.Windows.Forms.Button();
            this.lstTableNames = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstDatabaseNames
            // 
            this.lstDatabaseNames.FormattingEnabled = true;
            this.lstDatabaseNames.Location = new System.Drawing.Point(15, 14);
            this.lstDatabaseNames.Name = "lstDatabaseNames";
            this.lstDatabaseNames.Size = new System.Drawing.Size(214, 251);
            this.lstDatabaseNames.TabIndex = 0;
            // 
            // cmdLoadDatabaseNames
            // 
            this.cmdLoadDatabaseNames.Location = new System.Drawing.Point(15, 269);
            this.cmdLoadDatabaseNames.Name = "cmdLoadDatabaseNames";
            this.cmdLoadDatabaseNames.Size = new System.Drawing.Size(214, 23);
            this.cmdLoadDatabaseNames.TabIndex = 1;
            this.cmdLoadDatabaseNames.Text = "Load databases";
            this.cmdLoadDatabaseNames.UseVisualStyleBackColor = true;
            this.cmdLoadDatabaseNames.Click += new System.EventHandler(this.cmdLoadDatabaseNames_Click);
            // 
            // lstTableNames
            // 
            this.lstTableNames.FormattingEnabled = true;
            this.lstTableNames.Location = new System.Drawing.Point(235, 12);
            this.lstTableNames.Name = "lstTableNames";
            this.lstTableNames.Size = new System.Drawing.Size(208, 251);
            this.lstTableNames.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 300);
            this.Controls.Add(this.lstTableNames);
            this.Controls.Add(this.cmdLoadDatabaseNames);
            this.Controls.Add(this.lstDatabaseNames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstDatabaseNames;
        private System.Windows.Forms.Button cmdLoadDatabaseNames;
        private System.Windows.Forms.ListBox lstTableNames;
    }
}

