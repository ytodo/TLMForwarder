using System.Diagnostics;
using Serilog;
using TLMForwarder;


public class FileControl
{
	//**************************************************************
	//	衛星毎のログファイルを作成する
	//**************************************************************
	public static (string, string, string, string) CreateAppLog(FrmSettings frmSettings, FrmMain frmMain)
	{
		string logDir = string.Empty;

		// LogDirを取得し整形する
		if (frmSettings.InvokeRequired)
		{
			frmSettings.Invoke(new Action(() =>
			{
				logDir = frmSettings.TxtLogDir.Text;
			}));
		}
		else
		{
			logDir = frmSettings.TxtLogDir.Text;
		}

Debug.WriteLine("FileControl" + logDir);

		try
		{

			char lastChar = logDir[^1];
			if (lastChar != '\\')
			{
				logDir += "\\";
			}
			logDir += frmMain.LblNoradID.Text + "\\";
		
		}
		catch (IndexOutOfRangeException)
		{
			MessageBox.Show("Please Choose the folder for \"LogDir\" in the Settings.");
			Log.Error("Nothing selected for LogDir.");
			return ("", "", "", "");
		}
		

		// LogDirが存在しない時は作成する
		if (!Directory.Exists(logDir))
		{
			Directory.CreateDirectory(logDir);
			Log.Information("Made Dir " + logDir);
		}

		Log.Information("LogDir: " + logDir);

		// ファイル名を作成する
		string timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ").Replace(":", "");
		string tlmFile = frmMain.LblNoradID.Text + "_" + timeStamp + "_" + "forwarding.log";
		string digFile = frmMain.LblNoradID.Text + "_" + timeStamp + "_" + "digipeater.log";
		string kssFile = frmMain.LblNoradID.Text + "_" + timeStamp + ".kss";
		string hexFile = frmMain.LblNoradID.Text + "_" + timeStamp + ".hex";

		// パブリックで定義
		string tlmPath = Path.Combine(logDir, tlmFile);
		string digPath = Path.Combine(logDir, digFile);
		string kssPath = Path.Combine(logDir, kssFile);
		string hexPath = Path.Combine(logDir, hexFile);

		// 先ず空のファイルが有れば前もって削除する
		string[] files = Directory.GetFiles(logDir);
		foreach (string file in files)
		{
			FileInfo? fileInfo = new(file);
			if (fileInfo.Length == 0)
			{
				File.Delete(file);
				Log.Information($"{file}" + " (Size: 0) was deleted.");
			}
		}

		// TelemetryとDigipeaterのログファイルを生成
		File.Create(tlmPath).Close();
		File.Create(digPath).Close();
		File.Create(kssPath).Close();

		return (tlmPath, digPath, kssPath, hexPath);
	}

	//***********************
	// Closing 処理
	//***********************
	public static void ClosingProcess(string kssPath, string hexPath)
	{
		//
		// 空のファイルが有れば削除する
		//

		// FILE名からそのDirectoryを抽出
		string? logDir = Path.GetDirectoryName(kssPath);

		if (logDir != null)
		{
			string[] files = Directory.GetFiles(logDir, "*", SearchOption.AllDirectories);  // サブフォルダも含む・・
		
			foreach (string file in files)
			{
				FileInfo? fileInfo = new(file);
				if (fileInfo.Length == 0)                                                   // 空ファイルを・・
				{
					File.Delete(file);                                                      // すべて削除。
					Log.Information($"{file}" + " (Size: 0) was deleted.");
				}
			}
		}

		//
		//	transaction fileである TLE.txt を削除
		//
		if (File.Exists(@".\TLE.txt"))
		{
			File.Delete(@".\TLE.txt");
		}

		//
		// もし.kssファイルが存在したら.hexに変換する
		//
		if (File.Exists(kssPath))
		{
			try
			{
				// certutilコマンドを使ってKISSファイルを16進数のテキストファイルに変換
				ProcessStartInfo psi = new()
				{
					FileName = "certutil",                                              // コマンド
					Arguments = $"-encodehex \"{kssPath}\" \"{hexPath}\"",              // パラメータとパス
					RedirectStandardOutput = true,                                      // 標準的メッセージ出力
					RedirectStandardError = true,                                       // エラーメッセージ出力
					UseShellExecute = false,                                            // シェルを起ち上げるか
					CreateNoWindow = true,                                              // 別途Windowを起ち上げるか
				};

				using Process? process = Process.Start(psi);

				// コマンドの実行結果を表示
				using (StreamReader? reader = process?.StandardOutput)
				{
					string? result = reader?.ReadToEnd();
					result = result?.Replace(Environment.NewLine, "");
					Log.Information($"KSS->HEX: {result}");
				}
				// コマンド実行時エラーを表示
				using (StreamReader? reader = process?.StandardError)
				{
					string? error = reader?.ReadToEnd();
					if (error != "")
					{
						Log.Error("Error: " + error);
					}
				}
				process?.WaitForExit();
			}
			catch (Exception ex)
			{
				Log.Error($"Error: {ex.Message}");
			}
		}
		else
		{
			Log.Information("KISS File is not exist.");
		}
	}
}
