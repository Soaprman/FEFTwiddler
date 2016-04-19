namespace FEFTwiddler.GUI.UnitViewer
{
    partial class HexEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRawNumberOfSupports = new System.Windows.Forms.Label();
            this.lblRawEndBlockType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.hexRawEndBlock = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawBlock3 = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawDeployedUnitInfo = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawLearnedSkills = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawBlock2 = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawSupports = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawInventory = new FEFTwiddler.GUI.Controls.HexBox();
            this.hexRawBlock1 = new FEFTwiddler.GUI.Controls.HexBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "RawBlock1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "RawInventory:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "RawSupports:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(892, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 63);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save and Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "RawBlock2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(527, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "RawLearnedSkills:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(508, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "RawDeployedUnitInfo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(557, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "RawBlock3:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(544, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "RawEndBlock:";
            // 
            // lblRawNumberOfSupports
            // 
            this.lblRawNumberOfSupports.AutoSize = true;
            this.lblRawNumberOfSupports.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRawNumberOfSupports.Location = new System.Drawing.Point(7, 280);
            this.lblRawNumberOfSupports.Name = "lblRawNumberOfSupports";
            this.lblRawNumberOfSupports.Size = new System.Drawing.Size(80, 16);
            this.lblRawNumberOfSupports.TabIndex = 17;
            this.lblRawNumberOfSupports.Text = "Count: 00";
            // 
            // lblRawEndBlockType
            // 
            this.lblRawEndBlockType.AutoSize = true;
            this.lblRawEndBlockType.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRawEndBlockType.Location = new System.Drawing.Point(544, 303);
            this.lblRawEndBlockType.Name = "lblRawEndBlockType";
            this.lblRawEndBlockType.Size = new System.Drawing.Size(72, 16);
            this.lblRawEndBlockType.TabIndex = 18;
            this.lblRawEndBlockType.Text = "Type: 00";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(718, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 63);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // hexRawEndBlock
            // 
            this.hexRawEndBlock.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawEndBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawEndBlock.Location = new System.Drawing.Point(625, 281);
            this.hexRawEndBlock.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawEndBlock.Name = "hexRawEndBlock";
            this.hexRawEndBlock.Size = new System.Drawing.Size(413, 136);
            this.hexRawEndBlock.TabIndex = 15;
            // 
            // hexRawBlock3
            // 
            this.hexRawBlock3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawBlock3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawBlock3.Location = new System.Drawing.Point(625, 212);
            this.hexRawBlock3.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawBlock3.Name = "hexRawBlock3";
            this.hexRawBlock3.Size = new System.Drawing.Size(413, 69);
            this.hexRawBlock3.TabIndex = 13;
            // 
            // hexRawDeployedUnitInfo
            // 
            this.hexRawDeployedUnitInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawDeployedUnitInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawDeployedUnitInfo.Location = new System.Drawing.Point(625, 76);
            this.hexRawDeployedUnitInfo.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawDeployedUnitInfo.Name = "hexRawDeployedUnitInfo";
            this.hexRawDeployedUnitInfo.Size = new System.Drawing.Size(413, 136);
            this.hexRawDeployedUnitInfo.TabIndex = 11;
            // 
            // hexRawLearnedSkills
            // 
            this.hexRawLearnedSkills.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawLearnedSkills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawLearnedSkills.Location = new System.Drawing.Point(625, 9);
            this.hexRawLearnedSkills.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawLearnedSkills.Name = "hexRawLearnedSkills";
            this.hexRawLearnedSkills.Size = new System.Drawing.Size(413, 67);
            this.hexRawLearnedSkills.TabIndex = 9;
            // 
            // hexRawBlock2
            // 
            this.hexRawBlock2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawBlock2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawBlock2.Location = new System.Drawing.Point(92, 396);
            this.hexRawBlock2.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawBlock2.Name = "hexRawBlock2";
            this.hexRawBlock2.Size = new System.Drawing.Size(413, 136);
            this.hexRawBlock2.TabIndex = 7;
            // 
            // hexRawSupports
            // 
            this.hexRawSupports.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawSupports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawSupports.Location = new System.Drawing.Point(92, 260);
            this.hexRawSupports.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawSupports.Name = "hexRawSupports";
            this.hexRawSupports.Size = new System.Drawing.Size(413, 136);
            this.hexRawSupports.TabIndex = 4;
            // 
            // hexRawInventory
            // 
            this.hexRawInventory.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawInventory.Location = new System.Drawing.Point(92, 190);
            this.hexRawInventory.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawInventory.Name = "hexRawInventory";
            this.hexRawInventory.Size = new System.Drawing.Size(413, 70);
            this.hexRawInventory.TabIndex = 2;
            // 
            // hexRawBlock1
            // 
            this.hexRawBlock1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hexRawBlock1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexRawBlock1.Location = new System.Drawing.Point(92, 9);
            this.hexRawBlock1.Margin = new System.Windows.Forms.Padding(0);
            this.hexRawBlock1.Name = "hexRawBlock1";
            this.hexRawBlock1.Size = new System.Drawing.Size(413, 181);
            this.hexRawBlock1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(731, 449);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(304, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Save at your own risk! If the program crashes, it\'s your problem.";
            // 
            // HexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 540);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblRawEndBlockType);
            this.Controls.Add(this.lblRawNumberOfSupports);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hexRawEndBlock);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hexRawBlock3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hexRawDeployedUnitInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hexRawLearnedSkills);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hexRawBlock2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hexRawSupports);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hexRawInventory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hexRawBlock1);
            this.Name = "HexEditor";
            this.Text = "HexEditor";
            this.Load += new System.EventHandler(this.HexEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.HexBox hexRawBlock1;
        private System.Windows.Forms.Label label1;
        private Controls.HexBox hexRawInventory;
        private System.Windows.Forms.Label label2;
        private Controls.HexBox hexRawSupports;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private Controls.HexBox hexRawBlock2;
        private System.Windows.Forms.Label label4;
        private Controls.HexBox hexRawLearnedSkills;
        private System.Windows.Forms.Label label5;
        private Controls.HexBox hexRawDeployedUnitInfo;
        private System.Windows.Forms.Label label6;
        private Controls.HexBox hexRawBlock3;
        private System.Windows.Forms.Label label7;
        private Controls.HexBox hexRawEndBlock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRawNumberOfSupports;
        private System.Windows.Forms.Label lblRawEndBlockType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
    }
}