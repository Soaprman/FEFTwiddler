namespace FEFTwiddler.GUI.UnitViewer
{
    partial class Supports
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
            this.flwSupport = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMaxSupports = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnMaxSupportsConversation = new System.Windows.Forms.Button();
            this.cmbAPlus = new System.Windows.Forms.ComboBox();
            this.lblAPlus = new System.Windows.Forms.Label();
            this.chkPolygamy = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // flwSupport
            // 
            this.flwSupport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flwSupport.AutoScroll = true;
            this.flwSupport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flwSupport.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwSupport.Location = new System.Drawing.Point(12, 13);
            this.flwSupport.Name = "flwSupport";
            this.flwSupport.Size = new System.Drawing.Size(214, 237);
            this.flwSupport.TabIndex = 0;
            this.flwSupport.WrapContents = false;
            // 
            // btnMaxSupports
            // 
            this.btnMaxSupports.Location = new System.Drawing.Point(12, 327);
            this.btnMaxSupports.Name = "btnMaxSupports";
            this.btnMaxSupports.Size = new System.Drawing.Size(214, 23);
            this.btnMaxSupports.TabIndex = 1;
            this.btnMaxSupports.Text = "Max All Supports";
            this.btnMaxSupports.UseVisualStyleBackColor = true;
            this.btnMaxSupports.Click += new System.EventHandler(this.btnMaxSupports_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(214, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMaxSupportsConversation
            // 
            this.btnMaxSupportsConversation.Location = new System.Drawing.Point(12, 356);
            this.btnMaxSupportsConversation.Name = "btnMaxSupportsConversation";
            this.btnMaxSupportsConversation.Size = new System.Drawing.Size(214, 23);
            this.btnMaxSupportsConversation.TabIndex = 3;
            this.btnMaxSupportsConversation.Text = "Max All Supports (Conversation)";
            this.btnMaxSupportsConversation.UseVisualStyleBackColor = true;
            this.btnMaxSupportsConversation.Click += new System.EventHandler(this.btnMaxSupportsConversation_Click);
            // 
            // cmbAPlus
            // 
            this.cmbAPlus.FormattingEnabled = true;
            this.cmbAPlus.Location = new System.Drawing.Point(100, 256);
            this.cmbAPlus.Name = "cmbAPlus";
            this.cmbAPlus.Size = new System.Drawing.Size(126, 21);
            this.cmbAPlus.TabIndex = 4;
            // 
            // lblAPlus
            // 
            this.lblAPlus.AutoSize = true;
            this.lblAPlus.Location = new System.Drawing.Point(12, 259);
            this.lblAPlus.Name = "lblAPlus";
            this.lblAPlus.Size = new System.Drawing.Size(57, 13);
            this.lblAPlus.TabIndex = 5;
            this.lblAPlus.Text = "A+ Partner";
            // 
            // chkPolygamy
            // 
            this.chkPolygamy.AutoSize = true;
            this.chkPolygamy.Location = new System.Drawing.Point(12, 290);
            this.chkPolygamy.Name = "chkPolygamy";
            this.chkPolygamy.Size = new System.Drawing.Size(142, 17);
            this.chkPolygamy.TabIndex = 6;
            this.chkPolygamy.Text = "Allow multiple S supports";
            this.chkPolygamy.UseVisualStyleBackColor = true;
            // 
            // Supports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(238, 420);
            this.Controls.Add(this.chkPolygamy);
            this.Controls.Add(this.lblAPlus);
            this.Controls.Add(this.cmbAPlus);
            this.Controls.Add(this.btnMaxSupportsConversation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnMaxSupports);
            this.Controls.Add(this.flwSupport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Supports";
            this.Text = "Supports";
            this.Load += new System.EventHandler(this.Supports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flwSupport;
        private System.Windows.Forms.Button btnMaxSupports;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnMaxSupportsConversation;
        private System.Windows.Forms.ComboBox cmbAPlus;
        private System.Windows.Forms.Label lblAPlus;
        private System.Windows.Forms.CheckBox chkPolygamy;
    }
}