namespace TLMForwarder;

	partial class FrmMain
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
		DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
		DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
		BtnSettings = new Button();
		BtnQuit = new Button();
		LblDateTime = new Label();
		RbnUTC = new RadioButton();
		RbnLocal = new RadioButton();
		label1 = new Label();
		label2 = new Label();
		LblNoradID = new Label();
		ChkForwarding = new CheckBox();
		label3 = new Label();
		LblFrameNum = new Label();
		CmbSatName = new ComboBox();
		CmbDatabase = new ComboBox();
		label4 = new Label();
		LblMsg = new Label();
		LblAlarm = new Label();
		BtnInfo = new Button();
		PicTop = new PictureBox();
		tabControl = new TabControl();
		tabDigipeater = new TabPage();
		GrdDigipeater = new DataGridView();
		Column1 = new DataGridViewTextBoxColumn();
		Column2 = new DataGridViewTextBoxColumn();
		Column3 = new DataGridViewTextBoxColumn();
		Column4 = new DataGridViewTextBoxColumn();
		Column5 = new DataGridViewTextBoxColumn();
		tabTelemetry = new TabPage();
		TxtTelemetry = new TextBox();
		lblTelemetry = new Label();
		panel1 = new Panel();
		ColDelay = new DataGridViewTextBoxColumn();
		ColMessage = new DataGridViewTextBoxColumn();
		ColTo = new DataGridViewTextBoxColumn();
		ColFrom = new DataGridViewTextBoxColumn();
		ColTime = new DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)PicTop).BeginInit();
		tabControl.SuspendLayout();
		tabDigipeater.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)GrdDigipeater).BeginInit();
		tabTelemetry.SuspendLayout();
		SuspendLayout();
		// 
		// BtnSettings
		// 
		BtnSettings.BackColor = SystemColors.ButtonFace;
		BtnSettings.Location = new Point(0, 0);
		BtnSettings.Name = "BtnSettings";
		BtnSettings.Size = new Size(115, 23);
		BtnSettings.TabIndex = 0;
		BtnSettings.Text = "SETTINGS";
		BtnSettings.UseVisualStyleBackColor = false;
		BtnSettings.Click += BtnSettings_Click;
		// 
		// BtnQuit
		// 
		BtnQuit.BackColor = SystemColors.ButtonFace;
		BtnQuit.Location = new Point(558, 0);
		BtnQuit.Name = "BtnQuit";
		BtnQuit.Size = new Size(75, 23);
		BtnQuit.TabIndex = 1;
		BtnQuit.Text = "QUIT";
		BtnQuit.UseVisualStyleBackColor = false;
		BtnQuit.Click += BtnQuit_Click;
		// 
		// LblDateTime
		// 
		LblDateTime.AutoSize = true;
		LblDateTime.Font = new Font("メイリオ", 9.75F, FontStyle.Bold);
		LblDateTime.Location = new Point(379, 3);
		LblDateTime.Name = "LblDateTime";
		LblDateTime.Size = new Size(74, 20);
		LblDateTime.TabIndex = 3;
		LblDateTime.Text = "Date&Time";
		// 
		// RbnUTC
		// 
		RbnUTC.AutoSize = true;
		RbnUTC.Location = new Point(257, 2);
		RbnUTC.Name = "RbnUTC";
		RbnUTC.Size = new Size(46, 19);
		RbnUTC.TabIndex = 4;
		RbnUTC.Text = "UTC";
		RbnUTC.UseVisualStyleBackColor = true;
		// 
		// RbnLocal
		// 
		RbnLocal.AutoSize = true;
		RbnLocal.Checked = true;
		RbnLocal.Location = new Point(318, 2);
		RbnLocal.Name = "RbnLocal";
		RbnLocal.Size = new Size(53, 19);
		RbnLocal.TabIndex = 5;
		RbnLocal.TabStop = true;
		RbnLocal.Text = "Local";
		RbnLocal.UseVisualStyleBackColor = true;
		RbnLocal.CheckedChanged += RbnLocal_CheckedChanged;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(27, 37);
		label1.Name = "label1";
		label1.Size = new Size(88, 15);
		label1.TabIndex = 6;
		label1.Text = "Satellite Name :";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(309, 37);
		label2.Name = "label2";
		label2.Size = new Size(57, 15);
		label2.TabIndex = 7;
		label2.Text = "NoradID :";
		// 
		// LblNoradID
		// 
		LblNoradID.AutoSize = true;
		LblNoradID.Location = new Point(368, 37);
		LblNoradID.Name = "LblNoradID";
		LblNoradID.Size = new Size(51, 15);
		LblNoradID.TabIndex = 8;
		LblNoradID.Text = "NoradID";
		// 
		// ChkForwarding
		// 
		ChkForwarding.AutoSize = true;
		ChkForwarding.Location = new Point(449, 36);
		ChkForwarding.Name = "ChkForwarding";
		ChkForwarding.Size = new Size(134, 19);
		ChkForwarding.TabIndex = 9;
		ChkForwarding.Text = "Forward to SatNOGS";
		ChkForwarding.UseVisualStyleBackColor = true;
		ChkForwarding.CheckedChanged += ChkForwarding_CheckedChanged;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(466, 67);
		label3.Name = "label3";
		label3.Size = new Size(112, 15);
		label3.TabIndex = 10;
		label3.Text = "Forwarded Packets :";
		// 
		// LblFrameNum
		// 
		LblFrameNum.AutoSize = true;
		LblFrameNum.Location = new Point(580, 67);
		LblFrameNum.Name = "LblFrameNum";
		LblFrameNum.RightToLeft = RightToLeft.Yes;
		LblFrameNum.Size = new Size(13, 15);
		LblFrameNum.TabIndex = 11;
		LblFrameNum.Text = "0";
		LblFrameNum.TextAlign = ContentAlignment.TopCenter;
		// 
		// CmbSatName
		// 
		CmbSatName.FormattingEnabled = true;
		CmbSatName.Location = new Point(121, 34);
		CmbSatName.Name = "CmbSatName";
		CmbSatName.Size = new Size(169, 23);
		CmbSatName.Sorted = true;
		CmbSatName.TabIndex = 12;
		CmbSatName.SelectedIndexChanged += CmbSatName_SelectionIndexChanged;
		// 
		// CmbDatabase
		// 
		CmbDatabase.FormattingEnabled = true;
		CmbDatabase.Location = new Point(122, 64);
		CmbDatabase.Name = "CmbDatabase";
		CmbDatabase.Size = new Size(277, 23);
		CmbDatabase.TabIndex = 13;
		CmbDatabase.SelectedIndexChanged += CmbDatabase_SelectedIndexChanged;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Location = new Point(27, 67);
		label4.Name = "label4";
		label4.Size = new Size(61, 15);
		label4.TabIndex = 14;
		label4.Text = "Database :";
		// 
		// LblMsg
		// 
		LblMsg.AutoSize = true;
		LblMsg.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
		LblMsg.Location = new Point(27, 479);
		LblMsg.Name = "LblMsg";
		LblMsg.Size = new Size(40, 15);
		LblMsg.TabIndex = 16;
		LblMsg.Text = "Modem Status";
		// 
		// LblAlarm
		// 
		LblAlarm.AutoSize = true;
		LblAlarm.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
		LblAlarm.Location = new Point(318, 479);
		LblAlarm.Name = "LblAlarm";
		LblAlarm.Size = new Size(78, 15);
		LblAlarm.TabIndex = 17;
		LblAlarm.Text = "Status & Alarm";
		// 
		// BtnInfo
		// 
		BtnInfo.Location = new Point(121, 0);
		BtnInfo.Name = "BtnInfo";
		BtnInfo.Size = new Size(50, 23);
		BtnInfo.TabIndex = 18;
		BtnInfo.Text = "Info";
		BtnInfo.UseVisualStyleBackColor = true;
		BtnInfo.Click += BtnInfo_Click;
		// 
		// PicTop
		// 
		PicTop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		PicTop.BackColor = Color.Transparent;
		PicTop.Location = new Point(6, 117);
		PicTop.Image = Image.FromFile("config\\image.png");
		PicTop.Name = "PicTop";
		PicTop.Size = new Size(625, 351);
		PicTop.TabIndex = 19;
		PicTop.TabStop = false;
		// 
		// tabControl
		// 
		tabControl.CausesValidation = false;
		tabControl.Controls.Add(tabDigipeater);
		tabControl.Controls.Add(tabTelemetry);
		tabControl.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
		tabControl.ItemSize = new Size(120, 20);
		tabControl.Location = new Point(3, 94);
		tabControl.Margin = new Padding(0);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(633, 379);
		tabControl.SizeMode = TabSizeMode.Fixed;
		tabControl.TabIndex = 20;
		tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
		// 
		// tabDigipeater
		// 
		tabDigipeater.AutoScroll = true;
		tabDigipeater.BackColor = Color.FromArgb(0, 192, 0);
		tabDigipeater.BorderStyle = BorderStyle.FixedSingle;
		tabDigipeater.Controls.Add(GrdDigipeater);
		tabDigipeater.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
		tabDigipeater.ForeColor = Color.FromArgb(224, 224, 224);
		tabDigipeater.Location = new Point(4, 24);
		tabDigipeater.Margin = new Padding(0);
		tabDigipeater.Name = "tabDigipeater";
		tabDigipeater.Size = new Size(625, 351);
		tabDigipeater.TabIndex = 0;
		tabDigipeater.Text = "DIGIPEATER";
		// 
		// GrdDigipeater
		// 
		dataGridViewCellStyle1.BackColor = Color.Black;
		dataGridViewCellStyle1.Font = new Font("Cascadia Code", 10F);
		dataGridViewCellStyle1.ForeColor = Color.White;
		GrdDigipeater.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		GrdDigipeater.BackgroundColor = Color.Black;
		GrdDigipeater.BorderStyle = BorderStyle.None;
		GrdDigipeater.CellBorderStyle = DataGridViewCellBorderStyle.None;
		dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = Color.Black;
		dataGridViewCellStyle2.Font = new Font("Cascadia Code", 10F, FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = Color.White;
		dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
		GrdDigipeater.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		GrdDigipeater.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		GrdDigipeater.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
		dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = Color.Black;
		dataGridViewCellStyle3.Font = new Font("Cascadia Code", 10F);
		dataGridViewCellStyle3.ForeColor = Color.FromArgb(224, 224, 224);
		dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
		GrdDigipeater.DefaultCellStyle = dataGridViewCellStyle3;
		GrdDigipeater.GridColor = Color.Black;
		GrdDigipeater.ImeMode = ImeMode.Off;
		GrdDigipeater.Location = new Point(4, 4);
		GrdDigipeater.Name = "GrdDigipeater";
		GrdDigipeater.ReadOnly = true;
		GrdDigipeater.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = SystemColors.Control;
		dataGridViewCellStyle4.Font = new Font("Cascadia Code", 10F);
		dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
		GrdDigipeater.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		GrdDigipeater.RowHeadersVisible = false;
		dataGridViewCellStyle5.BackColor = Color.Black;
		dataGridViewCellStyle5.Font = new Font("Cascadia Code", 10F);
		dataGridViewCellStyle5.ForeColor = Color.White;
		dataGridViewCellStyle5.SelectionBackColor = Color.Black;
		GrdDigipeater.RowsDefaultCellStyle = dataGridViewCellStyle5;
		GrdDigipeater.RowTemplate.Height = 20;
		GrdDigipeater.RowTemplate.ReadOnly = true;
		GrdDigipeater.RowTemplate.Resizable = DataGridViewTriState.True;
		GrdDigipeater.ScrollBars = ScrollBars.Vertical;
		GrdDigipeater.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		GrdDigipeater.Size = new Size(616, 341);
		GrdDigipeater.TabIndex = 3;
		// 
		// Column1
		// 
		Column1.HeaderText = "Time";
		Column1.Name = "Column1";
		Column1.ReadOnly = true;
		Column1.Width = 80;
		// 
		// Column2
		// 
		Column2.HeaderText = "From";
		Column2.Name = "Column2";
		Column2.ReadOnly = true;
		// 
		// Column3
		// 
		Column3.HeaderText = "To";
		Column3.Name = "Column3";
		Column3.ReadOnly = true;
		Column3.Width = 150;
		// 
		// Column4
		// 
		Column4.HeaderText = "Message";
		Column4.Name = "Column4";
		Column4.ReadOnly = true;
		Column4.Width = 215;
		// 
		// Column5
		// 
		Column5.HeaderText = "Delay";
		Column5.Name = "Column5";
		Column5.ReadOnly = true;
		Column5.Width = 70;
		// 
		// tabTelemetry
		// 
		tabTelemetry.AutoScroll = true;
		tabTelemetry.BackColor = Color.Red;
		tabTelemetry.BorderStyle = BorderStyle.FixedSingle;
		tabTelemetry.Controls.Add(TxtTelemetry);
		tabTelemetry.Controls.Add(lblTelemetry);
		tabTelemetry.Font = new Font("Cascadia Code", 11F, FontStyle.Bold);
		tabTelemetry.ForeColor = Color.FromArgb(224, 224, 224);
		tabTelemetry.Location = new Point(4, 24);
		tabTelemetry.Margin = new Padding(0);
		tabTelemetry.Name = "tabTelemetry";
		tabTelemetry.Size = new Size(625, 351);
		tabTelemetry.TabIndex = 1;
		tabTelemetry.Text = "TELEMETRY";
		// 
		// TxtTelemetry
		// 
		TxtTelemetry.BackColor = Color.Black;
		TxtTelemetry.BorderStyle = BorderStyle.None;
		TxtTelemetry.Font = new Font("Cascadia Code", 10F);
		TxtTelemetry.ForeColor = Color.White;
		TxtTelemetry.Location = new Point(4, 4);
		TxtTelemetry.Multiline = true;
		TxtTelemetry.Name = "TxtTelemetry";
		TxtTelemetry.ReadOnly = true;
		TxtTelemetry.ScrollBars = ScrollBars.Vertical;
		TxtTelemetry.Size = new Size(615, 341);
		TxtTelemetry.TabIndex = 1;
		// 
		// lblTelemetry
		// 
		lblTelemetry.AutoSize = true;
		lblTelemetry.Location = new Point(1, 0);
		lblTelemetry.Name = "lblTelemetry";
		lblTelemetry.Size = new Size(0, 20);
		lblTelemetry.TabIndex = 0;
		// 
		// panel1
		// 
		panel1.BackColor = Color.Transparent;
		panel1.Location = new Point(4, 101);
		panel1.Name = "panel1";
		panel1.Size = new Size(626, 364);
		panel1.TabIndex = 20;
		// 
		// ColDelay
		// 
		ColDelay.DividerWidth = 1;
		ColDelay.HeaderText = "Delay";
		ColDelay.Name = "ColDelay";
		ColDelay.ReadOnly = true;
		ColDelay.Width = 60;
		// 
		// ColMessage
		// 
		ColMessage.DividerWidth = 1;
		ColMessage.HeaderText = "Message";
		ColMessage.Name = "ColMessage";
		ColMessage.ReadOnly = true;
		ColMessage.Width = 270;
		// 
		// ColTo
		// 
		ColTo.DividerWidth = 1;
		ColTo.HeaderText = "To";
		ColTo.Name = "ColTo";
		ColTo.ReadOnly = true;
		ColTo.Width = 120;
		// 
		// ColFrom
		// 
		ColFrom.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		ColFrom.DividerWidth = 1;
		ColFrom.HeaderText = "From";
		ColFrom.Name = "ColFrom";
		ColFrom.ReadOnly = true;
		ColFrom.Width = 85;
		// 
		// ColTime
		// 
		ColTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		ColTime.DividerWidth = 1;
		ColTime.HeaderText = "Time";
		ColTime.Name = "ColTime";
		ColTime.ReadOnly = true;
		ColTime.Width = 80;
		// 
		// FrmMain
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.ControlLight;
		ClientSize = new Size(635, 503);
		Controls.Add(tabControl);
		Controls.Add(panel1);
		Controls.Add(BtnInfo);
		Controls.Add(LblAlarm);
		Controls.Add(LblMsg);
		Controls.Add(label4);
		Controls.Add(CmbDatabase);
		Controls.Add(CmbSatName);
		Controls.Add(LblFrameNum);
		Controls.Add(label3);
		Controls.Add(ChkForwarding);
		Controls.Add(LblNoradID);
		Controls.Add(label2);
		Controls.Add(label1);
		Controls.Add(RbnLocal);
		Controls.Add(RbnUTC);
		Controls.Add(LblDateTime);
		Controls.Add(BtnQuit);
		Controls.Add(BtnSettings);
		Controls.Add(PicTop);
		Name = "FrmMain";
		Text = "TLMForwarder";
		Load += FrmMain_Load;
//		Load += new System.EventHandler(FrmMain_Load);
		((System.ComponentModel.ISupportInitialize)PicTop).EndInit();
		tabControl.ResumeLayout(false);
		tabDigipeater.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)GrdDigipeater).EndInit();
		tabTelemetry.ResumeLayout(false);
		tabTelemetry.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button BtnSettings;
	private Button BtnQuit;
//	private System.Windows.Forms.Timer Timer1;
	private Label LblDateTime;
	private RadioButton RbnUTC;
	private RadioButton RbnLocal;
	private Label label1;
	private Label label2;
	private Label LblNoradID;
	public CheckBox ChkForwarding;
	private Label label3;
	private Label LblFrameNum;
	private ComboBox CmbSatName;
	private ComboBox CmbDatabase;
	private Label label4;
	internal Label LblMsg;
	private Label LblAlarm;
	private Button BtnInfo;
	private PictureBox PicTop;
	private TabControl tabControl;
	private TabPage tabTelemetry;
	private Panel panel1;
	private Label lblTelemetry;
	private TextBox TxtTelemetry;
	private TabPage tabDigipeater;
	private DataGridView GrdDigipeater;
	private DataGridViewTextBoxColumn ColDelay;
	private DataGridViewTextBoxColumn ColMessage;
	private DataGridViewTextBoxColumn ColTo;
	private DataGridViewTextBoxColumn ColFrom;
	private DataGridViewTextBoxColumn ColTime;
	private DataGridViewTextBoxColumn Column1;
	private DataGridViewTextBoxColumn Column2;
	private DataGridViewTextBoxColumn Column3;
	private DataGridViewTextBoxColumn Column4;
	private DataGridViewTextBoxColumn Column5;
}