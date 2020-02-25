namespace SWBOptionPackageInstaller
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPackagesPath = new System.Windows.Forms.TextBox();
            this.lbPathLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbPackagesPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbPathLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.20737F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.79263F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbPackagesPath
            // 
            this.tbPackagesPath.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPackagesPath.Location = new System.Drawing.Point(397, 3);
            this.tbPackagesPath.Name = "tbPackagesPath";
            this.tbPackagesPath.Size = new System.Drawing.Size(358, 20);
            this.tbPackagesPath.TabIndex = 0;
            this.tbPackagesPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPackagesPath_KeyDown);
            // 
            // lbPathLabel
            // 
            this.lbPathLabel.AutoSize = true;
            this.lbPathLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbPathLabel.Location = new System.Drawing.Point(331, 0);
            this.lbPathLabel.Name = "lbPathLabel";
            this.lbPathLabel.Size = new System.Drawing.Size(60, 66);
            this.lbPathLabel.TabIndex = 1;
            this.lbPathLabel.Text = "Enter Path:";
            this.lbPathLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbPackagesPath;
        private System.Windows.Forms.Label lbPathLabel;
    }
}

