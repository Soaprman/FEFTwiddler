namespace FEFTwiddler.GUI.Controls
{
    partial class HexBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblColumns = new System.Windows.Forms.Label();
            this.pnlRows = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRowLabels = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumns.ForeColor = System.Drawing.Color.Blue;
            this.lblColumns.Location = new System.Drawing.Point(27, 2);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(384, 16);
            this.lblColumns.TabIndex = 1;
            this.lblColumns.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F";
            // 
            // pnlRows
            // 
            this.pnlRows.BackColor = System.Drawing.SystemColors.Window;
            this.pnlRows.Location = new System.Drawing.Point(28, 19);
            this.pnlRows.Name = "pnlRows";
            this.pnlRows.Size = new System.Drawing.Size(384, 10);
            this.pnlRows.TabIndex = 4;
            // 
            // pnlRowLabels
            // 
            this.pnlRowLabels.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlRowLabels.Location = new System.Drawing.Point(1, 22);
            this.pnlRowLabels.Name = "pnlRowLabels";
            this.pnlRowLabels.Size = new System.Drawing.Size(25, 10);
            this.pnlRowLabels.TabIndex = 5;
            // 
            // HexBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlRowLabels);
            this.Controls.Add(this.pnlRows);
            this.Controls.Add(this.lblColumns);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HexBox";
            this.Size = new System.Drawing.Size(411, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.FlowLayoutPanel pnlRows;
        private System.Windows.Forms.FlowLayoutPanel pnlRowLabels;
    }
}
