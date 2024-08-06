namespace TLMForwarder
{
	partial class FrmInfo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInfo));
			pictureBox1 = new PictureBox();
			label1 = new Label();
			LblVersion = new Label();
			BtnCloseInfo = new Button();
			label2 = new Label();
			LblCopyright = new Label();
			label3 = new Label();
			label4 = new Label();
			LblGitHub = new LinkLabel();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(28, 10);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(120, 120);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Yu Gothic UI", 11.25F);
			label1.Location = new Point(161, 20);
			label1.Name = "label1";
			label1.Size = new Size(103, 20);
			label1.TabIndex = 1;
			label1.Text = "TLMForwarder";
			label1.TextAlign = ContentAlignment.TopRight;
			// 
			// LblVersion
			// 
			LblVersion.AutoSize = true;
			LblVersion.Font = new Font("Yu Gothic UI", 11.25F);
			LblVersion.Location = new Point(264, 20);
			LblVersion.Name = "LblVersion";
			LblVersion.Size = new Size(57, 20);
			LblVersion.TabIndex = 2;
			LblVersion.Text = "Version";
			// 
			// BtnCloseInfo
			// 
			BtnCloseInfo.BackColor = SystemColors.ButtonFace;
			BtnCloseInfo.Location = new Point(149, 229);
			BtnCloseInfo.Name = "BtnCloseInfo";
			BtnCloseInfo.Size = new Size(75, 23);
			BtnCloseInfo.TabIndex = 3;
			BtnCloseInfo.Text = "Close";
			BtnCloseInfo.UseVisualStyleBackColor = false;
			BtnCloseInfo.Click += BtnCloseInfo_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(165, 97);
			label2.Name = "label2";
			label2.Size = new Size(0, 15);
			label2.TabIndex = 4;
			// 
			// LblCopyright
			// 
			LblCopyright.AutoSize = true;
			LblCopyright.Location = new Point(25, 140);
			LblCopyright.Name = "LblCopyright";
			LblCopyright.Size = new Size(332, 60);
			LblCopyright.TabIndex = 5;
			LblCopyright.Text = resources.GetString("LblCopyright.Text");
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(167, 87);
			label3.Name = "label3";
			label3.Size = new Size(100, 15);
			label3.TabIndex = 6;
			label3.Text = "Copyright © 2024";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(167, 110);
			label4.Name = "label4";
			label4.Size = new Size(144, 15);
			label4.TabIndex = 7;
			label4.Text = "JE3HCZ / TODO Yoshiharu";
			// 
			// LblGitHub
			// 
			LblGitHub.AutoSize = true;
			LblGitHub.Location = new Point(203, 185);
			LblGitHub.Name = "LblGitHub";
			LblGitHub.Size = new Size(108, 15);
			LblGitHub.TabIndex = 8;
			LblGitHub.TabStop = true;
			LblGitHub.Text = "https://github.com";
			LblGitHub.LinkClicked += LblGitHub_LinkClicked;
			// 
			// FrmInfo
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ControlLight;
			ClientSize = new Size(372, 257);
			Controls.Add(LblGitHub);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(LblCopyright);
			Controls.Add(label2);
			Controls.Add(BtnCloseInfo);
			Controls.Add(LblVersion);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "FrmInfo";
			Text = "Info";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox1;
		private Label label1;
		private Label LblVersion;
		private Button BtnCloseInfo;
		private Label label2;
		private Label LblCopyright;
		private Label label3;
		private Label label4;
		private LinkLabel LblGitHub;
	}
}