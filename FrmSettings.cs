namespace TLMForwarder
{
	using Serilog;

	public partial class FrmSettings : Form
	{
		// 設定ファイル保存ボタン関数用ファイルパス
		private readonly string filePath = @".\config\TLMForwarder.ini";

		public FrmSettings()
		{
			InitializeComponent();
		}

		//************************************
		//	設定ウインドウを閉じる 
		//************************************
		private void BtnClose_Click(object sender, EventArgs e)
		{
			Log.Information("Settings didn't be saved, just CLOSE.");
			
			// Formを閉じる
			this.Close();
		}

		//************************************
		//	ファイル保存用ボタン 
		//************************************
		private void BtnStore_Click(object sender, EventArgs e)
		{
			SaveDatatoFile();

			DialogResult result = MessageBox.Show("Saved the settings to TLMForwarder.ini file.", "Save completed.",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
			if (result == DialogResult.OK)
			{
				Log.Information("Settings completely saved.");
				this.Close();
			}
		}

		//************************************
		//	表示されている設定値を保存する 
		//************************************
		public void SaveDatatoFile()
		{
			// ファイルにデータを書き込む
			if (filePath != "")
			{
				using StreamWriter writer = new(filePath);
				foreach (Control control in Controls.OfType<TextBox>().Reverse())
				{
					if (control is TextBox textBox)
					{
						string key = textBox.Name[3..];
						string value = textBox.Text;
						string line = $"{key}={value}";

						writer.WriteLine(line);                     // 一行ずつ書き込む
					}
				}
			}
		}

		//*************************************************
		//	LogDirの[Path]ボタンを押した時呼び出す関数
		//*************************************************
		private void BtnPath_Click_1(object sender, EventArgs e)
		{
			SelectLogDir();
		}

		//*************************************************
		// [Path]ボタンを押した時フォルダを選択するダイアログ
		//*************************************************
		private void SelectLogDir()
		{
			FolderBrowserDialog logFolderDialog = new()
			{
				Description = "Select the folder for log files."
			};
			DialogResult dialogResult = logFolderDialog.ShowDialog();

			if (dialogResult == DialogResult.OK)
			{
				string logFolderPath = logFolderDialog.SelectedPath;
				TxtLogDir.Text = logFolderPath;
				Log.Information("Selected LogData folder is " + TxtLogDir.Text);
			}
		}


		//*************************************************
		//	TLEの[Path]ボタンを押した時呼び出す関数
		//*************************************************
		private void BtnPathTle_Click(object sender, EventArgs e)
		{
			SelectTleFile();
		}

		//*************************************************
		// [Path]ボタンを押した時フォルダを選択するダイアログ
		//*************************************************
		private void SelectTleFile()
		{
			OpenFileDialog tleFileDialog = new()
			{
				Title = "Select TLE File",
				Filter = "Text Files (*.txt)|*.txt"
				//InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			DialogResult dialogResult_tle = tleFileDialog.ShowDialog();

			if (dialogResult_tle == DialogResult.OK)
			{
				string tleFilePath = tleFileDialog.FileName;
				TxtTLE_Source.Text = tleFilePath;
				Log.Information("Selected TLE file is " + TxtTLE_Source.Text);
			}
		}

		//************************************************************
		//	TLMForwarder.iniから設定を読みFrmSettingsの各項目に代入する
		//************************************************************

		public void ReadSettingsFromfile()
		{
			// ファイルが存在するか確認
			if (File.Exists(filePath))
			{
				// ファイルからすべての行を読み込む
				string[] lines = File.ReadAllLines(filePath);               // 設定ファイルからの行データを取得

				// 各行を処理する
				foreach (string line in lines)
				{
					// セパレータ'='で分割
					string[] parts = line.Split('=');

					if (parts.Length == 2)
					{
						// 項目名をテキストボックス名として取得
						string textBoxName = "Txt" + parts[0].Trim();
						string textBoxText = parts[1].Trim();


						// 対応するテキストボックスに設定値を代入
						if (Controls.ContainsKey(textBoxName))
						{
							TextBox? textBox = Controls[textBoxName] as TextBox;

							if (textBox != null)
							{
								try
								{
									// 他のフォームのため Invoke が必要か判断
									if (InvokeRequired)
									{
										Invoke((System.Windows.Forms.MethodInvoker)delegate
										{
											textBox.Text = textBoxText;
										});
									}
									else
									{	// Invokeが不要な場合
										textBox.Text = textBoxText;
									}
								}
								catch (Exception ex)
								{
									Log.Warning($"Error setting text for {textBoxName}: {ex.Message}");
								}
							}
							else
							{
								Log.Warning ($"{textBoxName} is not a TextBox.");
							}
						}
						else
						{
							Log.Warning($"{textBoxName} does not exist in frmSettings.Controls.");
        		
						}
					}
				}
				Log.Information("TLMForwarder.ini has been successfully read.");
			}
			else
			{
				/* ファイルが存在しない場合の処理 */
				MessageBox.Show("TLMForwarder.ini must be in CURRENT Dir.\n" +
								"Current DIR is now " + Directory.GetCurrentDirectory());
				Log.Warning("No file named TLMForwarder.ini.");
			}
		}
	}
}

