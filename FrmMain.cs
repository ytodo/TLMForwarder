using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Data;
using System.Runtime.InteropServices;
using System.Timers;
using Serilog;
using System.CodeDom.Compiler;

namespace TLMForwarder;

public partial class FrmMain : Form
{
	///
	/// 変数･関数･定数の一括定義
	///
	
	// 設定ファイルのパスをreadonlyで指定（readonlyだとそれぞれのフォームで指定要）
	public readonly string confPath = @".\config\TLMForwarder.ini";	// 設定用ファイル
	private readonly string version = "2.3.1";						// バージョン
	public readonly FrmSettings frmSettings = new();				// FrmSettingsのインスタンス
	private List<string[]>? sat_list = [];					// 文字配列リストとしてsat_listを作成
	public static object? classInstance;							// DLLのクラスインスタンス
	public Type? classType;											// DLLのクラスタイプ
	public bool clientStop = false;									// tcpclientに対するstop flag
	private byte[]? recvData;										// 受信データのメインスレッド側受け皿
	private string? tlmPath;										// ログファイルのパス
	private string? digPath;										// ログファイルのパス
	private string? kssPath;										// ログファイルのパス
	private string? hexPath;										// ログファイルのパス
	public readonly byte[]? tmpBuff;								// SubThreadからMainThreadへの受け渡しデータ
	public int forwardNum = 0;										// 転送(送信)した回数
	public int apliedNum = 0;										// 受け付けられたテレメトリー数
	private static System.Timers.Timer? aTimer;
	private static FrmMain? instance;
	private TcpClient? client;
	private NetworkStream? stream;
	
	// APIを呼び出すため、対象のＤＬＬをインポート(Formの[X]コントロールを消す為）
	[LibraryImport("USER32.DLL")]
	private static partial IntPtr GetSystemMenu(IntPtr hWnd, uint bRevert);
	[LibraryImport("USER32.DLL")]
	private static partial uint RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

	// 定数定義 [X]牡丹を無効化するため
	private const uint SC_CLOSE = 0x0000F060;
	private const uint MF_BYCOMMAND = 0x00000000;

	///********************************************
	/// メインフォームの初期設定　（コンストラクタ）
	///********************************************
    public FrmMain()
    {
        InitializeComponent();

		// このフォーム自体のインスタンス
		instance = this;

		// フォームアイコンの設定
		Icon = new Icon("config\\icon.ico");

		// コントロールボックスの［閉じる］ボタンの無効化
		IntPtr hMenu = GetSystemMenu(Handle, 0);			// システムメニュー（フォームの）ハンドル取得する
		_ = RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);		// [×]ボタンを無効化する。
	}

	///********************************************
	/// 起動と同時に実行されるフォームロードイベント
	///********************************************
	private void FrmMain_Load(object sender, EventArgs e)
	{
		// FormのTitleを”TLMForwarder"とし、バージョンを標記
		Text = Application.ProductName + " v." + version;

		// メニューバーに表示するDate/Timeのクロック
		TimerInit();

		// トップ画像を最前面に
		PicTop.BringToFront();

		// タブコントロールの選択でトップ画像より全面にする関数呼び出し
		tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged!;    // !はnull許容参照型として扱う

		// 設定ファイルよりデータを読みSettingsフォームに表示する関数呼び出し
		frmSettings.ReadSettingsFromfile();

		// Formの起動位置を指定
		if (frmSettings.TxtWindow_x.Text != "" && frmSettings.TxtWindow_y.Text != "")
		{
			int winX = int.Parse(frmSettings.TxtWindow_x.Text);
			int winY = int.Parse(frmSettings.TxtWindow_y.Text);
			Location = new Point(winX, winY);
		}

		// TLEファイルからコンボボックス用衛星リストを作成し表示する
		SatnameList();

		// データベース選択コンボボックスのItem表示
		CmbDatabase.Items.AddRange([
			"https://db.satnogs.org",
			"https://db-dev.satnogs.org"
		]);
		string dataBase = frmSettings.TxtDatabase.Text;
		CmbDatabase.SelectedItem = dataBase;

		// チェックボックスの初期設定
		ChkForwadingInit();

		// TimeZoneボタンの初期設定
		BtnTimeZoneInit();

		// KISS Client用サブスレッドの起動
		StartKissThread();
	}

	//*************************************************
	//	タブコントロールの選択でトップ画像より全面にする	
	//*************************************************
	private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
	{
		TabControl? tabControl = (TabControl)sender;

		if (tabControl.SelectedTab == tabDigipeater)        // DEGIPEATERが選択された場合
		{
			tabControl.BringToFront();
		}
		else if (tabControl.SelectedTab == tabTelemetry)    // TELEMETRYが選択された場合
		{
			tabControl.BringToFront();
		}
	}

	//************************************************************
	// Date,Timeの表示フォーマットとラジオボタンによるロケールの切替
	//************************************************************ <summary>

	// Timerのセット
	public void TimerInit()
	{
		// タイマーの設定（1000ミリ秒）
		aTimer = new System.Timers.Timer(1000);

		// タイマーイベントの設定
		aTimer.Elapsed += OnTimeEvent;

		// タイマーの自動リセットを有効にする
		aTimer.AutoReset = true;

		// タイマーを開始
		aTimer.Enabled = true;
	}

	// セットしたタイマーによるイベント実行
	private void OnTimeEvent(object? sender, ElapsedEventArgs e)
	{
		// UIスレッドでテキストボックスを更新するため Invoke を使用
		if (InvokeRequired)
		{
			Invoke(new Action(() => UpdateTextBox(e.SignalTime)));
		}
		else
		{
			UpdateTextBox(e.SignalTime);
		}
	}

	// 実行されるイベントの内容
	private void UpdateTextBox(DateTime signalTime)
	{
		// メッセージ受け渡し変数に代入
		if (RbnUTC.Checked == true)
		{
			LblDateTime.Text = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
			frmSettings.TxtScreenTime.Text = "UTC";
		}
		else if (RbnLocal.Checked == true)
		{
			LblDateTime.Text = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
			frmSettings.TxtScreenTime.Text = "local";
		}
	}

	//***********************
	// Quitボタンイベント
	//***********************
	private void BtnQuit_Click(object sender, EventArgs e)
	{
		// TCPクライアントとサブスレッドの停止
		clientStop = true;

		// 終了時、後処理
		ClosingProcess();

		// 終了ログ情報とログ最終書き出し
		Log.Information("Exit the Application." + '\n');
		Log.CloseAndFlush();                                                            //　ログを更新して終了

		// アプリケーション終了
		Application.Exit();
	}

	//***********************
	// Closing 処理
	//***********************
	private void ClosingProcess()
	{
		//
		// 空のファイルが有れば削除する
		//
		if (frmSettings.TxtLogDir.Text != "")
		{
			string logDir = frmSettings.TxtLogDir.Text;                                     // LogDir内の・・
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

		//
		// フォームの現在座標を取得し次回起動時継承するためファイルに保存
		//
		int x = this.Location.X;
		int y = this.Location.Y;
		frmSettings.TxtWindow_x.Text = x.ToString();
		frmSettings.TxtWindow_y.Text = y.ToString();
		frmSettings.SaveDatatoFile();
		Log.Information("Settings completely saved.");
	}

	//************************************
	// Infoボタンのクリックイベント
	//************************************
	private void BtnInfo_Click(object sender, EventArgs e)
	{
		FrmInfo? frmInfo = new(version);
		frmInfo.ShowDialog(this);
	}

	//************************************************************
	//	設定パネルを表示(データはメインフォーム立ち上げ時に代入済み 
	//************************************************************
	private void BtnSettings_Click(object sender, EventArgs e)
	{
		frmSettings.ShowDialog();
	}

	//************************************************************
	//	ダウンロードしたTLEファイルから satname:noradID リストを作る 
	//************************************************************
	private void SatnameList()
	{
		// 設定ファイルからURLを読み、TLE.txt名でダウンロード
		string tleURL = frmSettings.TxtTLE_Source.Text;

		// クラス名とメソッド名で他ファイルの処理を呼び出す。（元データの作成）
		var results = SatNameList.TLE2SatnameList(tleURL);
		sat_list = results.sat_list;
		LblAlarm.Text = results.text;
		LblAlarm.ForeColor = results.color;

		if (sat_list != null)
		{
			foreach (string[] line in sat_list)
			{
				// sat_listの内容を表示する for debug 
				//Debug.WriteLine(line[0] + line[1]);

				// sat_listを衛星選択コンボボックスのitemに指定
				CmbSatName.Items.Add(line[0][..^1]);

				// 前回使用した衛星をデフォルトとして表示
				string lastUsed = frmSettings.TxtLastUsed.Text;
				CmbSatName.SelectedItem = lastUsed;

				// コンボボックスに選択された衛星が無いとき
				if (CmbSatName.SelectedItem == null)
				{
					// メッセージと色の指定
					LblAlarm.Text = "Select Satellite.";
					LblAlarm.ForeColor = Color.Blue;
				}
			}
		}
	}

	//***************************************************************
	//	転送先のデータベース選択 
	//***************************************************************
	private void CmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
	{
		frmSettings.TxtDatabase.Text = CmbDatabase.Text;
			Log.Information("Database set to " + CmbDatabase.Text + ".");
	}

	//***********************************************************
	//	転送チェックボックスの初期値設定と変更の保存
	//***********************************************************

	// Settingsの状況でチェック･アンチェックを設定
	private void ChkForwadingInit()
	{
		if (frmSettings.TxtForwarding.Text == "yes")
		{
			ChkForwarding.Checked = true;
		}
		else
		{
			ChkForwarding.Checked = false;
		}
	}
	
	// チェックボックスを変更したら状況をSettingsに反映
	private void ChkForwarding_CheckedChanged(object sender, EventArgs e)
	{
		// 選択した衛星名を設定パネルの TxtLastUsed.text に反映させる
		if (ChkForwarding.Checked == true)
		{
			frmSettings.TxtForwarding.Text = "yes";
			Log.Information("Forwarding set to YES.");
		}
		else
		{
			frmSettings.TxtForwarding.Text = "no";
			Log.Information("Forwarding set to NO.");
		}
	}

	//***********************************************************
	//	スクリーンタイムの初期値設定と変更の保存
	//***********************************************************
	/* Settingsの状況で選択状態を設定 */
	private void BtnTimeZoneInit()
	{
		if (frmSettings.TxtScreenTime.Text == "local")
		{
			RbnLocal.Checked = true;
		}
		else
		{
			RbnUTC.Checked = true;
		}
	}

	// スクリーンタイムの変更状況をSettingsに反映
	private void RbnLocal_CheckedChanged(object sender, EventArgs e)
	{
		// スクリーンタイムの変更をに反映させる
		if (RbnLocal.Checked == true)
		{
			frmSettings.TxtScreenTime.Text = "local";
			Log.Information("Screen Time set to local.");
		}
		else
		{
			frmSettings.TxtScreenTime.Text = "UTC";
			Log.Information("Screen Time set to UTC.");
		}
	}

	//**************************************************************
	//	衛星選択コンボボックスのイベントハンドラーで衛星毎の処理をする 
	//**************************************************************
	private void CmbSatName_SelectionIndexChanged(object sender, EventArgs e)
	{

		// 選択した衛星名を設定パネルの TxtLastUsed.text に反映させる
		frmSettings.TxtLastUsed.Text = CmbSatName.Text;
		Log.Information("Set satellite to " + CmbSatName.Text + ".");
		
		
		if (sat_list == null) return;

		foreach (string[] line in sat_list)
		{
			if (line[0] == CmbSatName.Text + ':')
			{
				LblNoradID.Text = line[1];
			}
		}

		// Tabのテキストとグリッドをクリアする
		TxtTelemetry.Text = string.Empty;
		GrdDigipeater.Rows.Clear();
		forwardNum = 0;
		apliedNum = 0;
		LblFrameNum.Text = string.Empty;

		/* 衛星名に即したログファイルを作成 */
		CreateAppLog();

		// DLLのクラスインスタンスを作成
		string dllFilePath = "./dll/" + LblNoradID.Text + ".dll";
		if (File.Exists(dllFilePath))
		{
			// DLLが見つかったメッセージ
			LblAlarm.Text = "Found DLL for " + CmbSatName.Text;
			LblAlarm.ForeColor = Color.Green;

			// DLLをロードする
			Assembly? assembly = Assembly.LoadFrom(dllFilePath);

			// 動的に使用するNamespaceとクラス名
			string namespaceName = "_" + LblNoradID.Text;
			string className = namespaceName;                               // namespace:_53106 class:_53106

			// インスタンスを生成
			classType = assembly.GetType($"{namespaceName}.{className}");   // _53106._53106
			if (classType == null)
			{
				MessageBox.Show("Namespace and/or classname of DLL is not specified.");
				return;
			}
			classInstance = Activator.CreateInstance(classType);            // クラスインスタンスを使ってどこからでもアクセス可

		}
		else
		{
			// DLLが見つからなかった時のメッセージ
			LblAlarm.Text = "Not exists " + dllFilePath + " for " + CmbSatName.Text;
			LblAlarm.ForeColor = Color.Red;
			Log.Error("Not exists " + dllFilePath + " for " + CmbSatName.Text + ".");
		}
	}

	//**************************************************************
	//	衛星毎のログファイルを作成する
	//**************************************************************
	public void CreateAppLog()
	{
		// LogDirを取得し整形する
		string logDir = frmSettings.TxtLogDir.Text;

		try
		{
			char lastChar = logDir[^1];
			if (lastChar != '\\')
			{
				logDir += "\\";
			}
			logDir += LblNoradID.Text + "\\";
		}
		catch (IndexOutOfRangeException)
		{
			MessageBox.Show("Please Choose the folder for \"LogDir\" in the Settings.");
			Log.Error("Nothing selected for LogDir.");
			return;
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
		string tlmFile = LblNoradID.Text + "_" + timeStamp + "_" + "forwarding.log";
		string digFile = LblNoradID.Text + "_" + timeStamp + "_" + "digipeater.log";
		string kssFile = LblNoradID.Text + "_" + timeStamp + ".kss";
		string hexFile = LblNoradID.Text + "_" + timeStamp + ".hex";

		// パブリックで定義
		tlmPath = Path.Combine(logDir, tlmFile);
		digPath = Path.Combine(logDir, digFile);
		kssPath = Path.Combine(logDir, kssFile);
		hexPath = Path.Combine(logDir, hexFile);

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
	}


	//**************************************************
	// フォームがロードされた後、バックグラウンドスレッドを開始
	//**************************************************
	
	//
	//	サブスレッド起ち上げ時におけるスレッド間処理
	//
	private void StartKissThread()
	{
		// KISSデータ取得専用サブスレッドを起ち上げる
	    Thread kissThread = new Thread(() => 
		{
        	// Settingsの接続情報チェックをした上でポートに接続を試みる処理
			bool isConnected = TryConnectToPort();
			bool isPortSet = PortCheck();
			string msg =  isConnected ? "Connected to KISS Port." :
							isPortSet ? "KISS Port is incorrect or TNC is not running." :
						 			    "Setup the KISS Address and/or the Port.";
			Color color = isConnected ? Color.Green :
							isPortSet ?	Color.Red :
										Color.Red;
			UpdateLblMsgCallback(msg, color);

			// サブスレッドで受信データの処理をする(Program.cs)
			if (isConnected)
			{
				StartClient();
			}
		});

		kissThread.Start();
	}

	
	//	StartKissThreadにおけるサブメソッド -----------------------------------
	// サブスレッドにおける代理メソッド
	public delegate void DelegateUpdateLblMsg(string msg, Color color);

	// ラベル変更に関するコールバック
	public static void UpdateLblMsgCallback(string msg, Color color)
	{
		if (instance != null)
		{
	    	if (instance.InvokeRequired)
		    {
    		    // Invoke処理が必要な場合
				instance.Invoke(new DelegateUpdateLblMsg(instance.UpdateLblMsg), [msg, color]);
		    }
    		else
	    	{
    	    	instance.UpdateLblMsg(msg, color);
	    	}
		}
	}

	// UIスレッドにおけるラベルの変更
	private void UpdateLblMsg(string msg, Color color)
	{
		if(LblMsg.InvokeRequired)
		{
			Invoke(new DelegateUpdateLblMsg(UpdateLblMsg));
			return;
		}
		LblMsg.Text = msg;
		LblMsg.ForeColor = color;
	}
	
	// 指定アドレス・ポートへの接続を試みた結果をリターン
	private bool TryConnectToPort()
	{
		// Settingsからアドレスとポートのデータを読む
		string addr = frmSettings.TxtKISS_Address.Text;
		string port = frmSettings.TxtKISS_Port.Text;

		//　接続に使用する情報を定義
		string kissAddr;
		int kissPort;

		// 接続情報が設定されていたとき
		if (addr.Length != 0 && port.Length != 0)
		{
			kissAddr = addr;				// string同士で代入の必要性は無い
			kissPort = int.Parse(port);
		}
		else
		{
			return false;					// PortCheckと同じ結果だがあくまでこのメソッドの結果
		}

    	// ポート接続のロジックを実装
	    try
    	{
			client = new TcpClient(kissAddr, kissPort);
			
			return true;					// 接続された
		}
    	catch (Exception)
	    {
    	    return false;					// 接続情報以外の理由で失敗
	    }
	}
	
	// Settingsの接続情報が設定されているかのチェック結果をリターン
	private bool PortCheck()
	{
		string addr = frmSettings.TxtKISS_Address.Text;
		string port = frmSettings.TxtKISS_Port.Text;

		// アドレス･ポートのどちらか又は両方が設定されていない
		if (addr.Length != 0 && port.Length != 0)
		{
			return true;			
		}
		else
		{
			return false;
		}
	}

	//*******************************************************
	//	TNC KISS Portからの受信
	//*******************************************************

	//
	// サブスレッドでTCPクライアントからのデータを処理する（サブスレッド）
	// 
	private void StartClient()
	{
		// KISS Dataの受信用バッファを定義
		byte[] recvBuff = new byte[1024];
		clientStop = false;

		/* 受信が無くても1秒に一度ループする　*/
		if ( client != null)	
		{
			client.ReceiveTimeout = 1000;

			// 受信ループ
			while (!clientStop)
			{
				try
				{
					// ソケットからデータを受信、実長さも取得
					stream = client.GetStream();
					int recvBytes = stream.Read(recvBuff, 0, recvBuff.Length);

					// 実長さに沿った一時バッファを生成
					byte[] tmpBuff = new byte[recvBytes];

					// 受信データ（1024バイト）を実長さにカット
					Array.Copy(recvBuff, tmpBuff, recvBytes);

					// メインスレッドにデータ（実長さ）を渡す
					SetReceivedData(tmpBuff);
				}
				catch (Exception)
				{
					/*// Debug
					//string filePath = @"S:\Satellites_Data\SATLOG\53106\53106_test.kss";
					string filePath = @"S:\Satellites_Data\SATLOG\53106\53106_test_digi.kss";
					//string filePath = @"S:\Satellites_Data\SATLOG\59508\59508_2024-06-19T025201.053Z.kss";


					byte[] tmpBuff;
					using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read))
					{
						tmpBuff = new byte[fs.Length];
						fs.Read(tmpBuff, 0, tmpBuff.Length);
					}
					SetReceivedData(tmpBuff);
					// Debug */

					// タイムアウトでループしても空回しする
					continue;
				}
			}

			// clientStop = true でループを抜けた時の処理
			client.Close();
			client.Dispose();

			// メッセージ受け渡し変数に代入
			Log.Information("Closing KISS Client and Thread.");
		}
	}

	//***********************************************************
	// 受信データをメインスレッドの変数で受け処理をする（メインスレッド）
	//***********************************************************
	public delegate void DelegateSetRecevedData(byte[] data);
	private void SetReceivedData(byte[] data)
	{
		// メインスレットの変数にデータを代入
		recvData = data;

		// RAWデータファイル NoradID_yyyy-mm-ddTHHMMSS.kss に保存
		try
		{
			// ファイルが存在しない場合は新規作成、存在する場合は追加書き込み
			if (kssPath == null)
			{
				MessageBox.Show("File path for received raw data is not exist.");
				return;
			}
			using FileStream fs = new(kssPath, FileMode.Append, FileAccess.Write);
			fs.Write(recvData, 0, recvData.Length);
			fs.Close();
		}
		catch
		{
				Log.Information("Error occurred while saving data");
		}

		// 受信データの不要な3バイトを取り除き、KISS逆変換する(DLL依存)
		MethodInfo? method = classType?.GetMethod("ReceiveDataFormat");
		if (recvData != null && recvData.Length != 0 && ChkForwarding.Checked)
		{
			/////////////////////////////////////////////////////////////////
			// DLLにデータを供給するパラメータ (各衛星IDで変更接続されるDLL依存）
			/////////////////////////////////////////////////////////////////
			object[] parameters = [recvData];

			// 2要素のタプルで処理データを受け取る
			//string hexString = string.Empty;
			string telemetryString = string.Empty;
			string logData = string.Empty;
			object? dllResult = method?.Invoke(classInstance, parameters);
			if (dllResult is ValueTuple<string, string> tupleResult)
			{
				(telemetryString, logData) = tupleResult;
			}
			/////////////////////////////////////////////////////////////////

			///
			/// Telemetryデータの処理
			/// 
			if (!string.IsNullOrEmpty(telemetryString))
			{
				telemetryString = telemetryString.Trim();
				string statCode = string.Empty;

				if (telemetryString[0] != 0)
				{
					// 転送回数をインクリメント
					forwardNum++;

					// 転送用データを作成しJSON形式で受け取る(表示用文字列含む）
					var result = CreateForwardingData(telemetryString);

					string payloadString = result.payloadString;
					string dispTelemetry = result.dispTelemetry.ToLower();

					// telemetryタブに表示
					if (!string.IsNullOrEmpty(dispTelemetry))
					{
						// Telemetry Tabに表示
						UpdateTextBox(dispTelemetry);
					}

					// 接続するデータベースのURL
					string url = frmSettings.TxtDatabase.Text;
					string database = url + "/api/telemetry/?";

					foreach (var outputLine in ExecuteTestExe(payloadString, database))
					{
						// 返り値は改行されたpayloadとstatus Codeで構成されている
						string[] results = $"{outputLine}".Split('\n');

						// 結果の長さが5文字より長いならjsonデータ
						if ($"{outputLine}".Length > 5)
						{
							// 分割した文字列を該当する変数に入れる
							string payloadLog = results[0].Trim();
							statCode = results[1].Trim();

							// Telemetry LOGファイル出力
							if (tlmPath != null)
							{
								// 日付、ペイロード、ステータス、転送番号
								File.AppendAllText(tlmPath, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")   // 常にUTC
								+ " - " + payloadLog.Trim() + " --> " + statCode + " "
								+ $"[{forwardNum}]" + Environment.NewLine + Environment.NewLine);
							}
							else
							{	// ログファイルのパスが無いとき
								Debug.WriteLine("File path of Telemetry Log is NULL.");
							}
						}
						else
						{
							// ステータスコードしか入っていないとき
							statCode = results[1].Trim();
						}
					} 
				}
				else
				{
					return;
				}

				// リスポンスをUIスレッドのTxtTelemetry.Textに追記する(改行2回追加）
				if (statCode != string.Empty)
				{
					string response;

					//転送が成功した時
					if (statCode == "201")
					{
						// 適用された回数をインクリメント
						apliedNum++;

						// telemetryカウンターのインクリメント(HTTP 200のみ）
						UpdateLabel(forwardNum.ToString());

						// メッセージの作成
						response = "Status Code : " + statCode + " = Forwarding successful." +
											Environment.NewLine + Environment.NewLine;
					}
					// 転送が成功しなかった時
					else
					{
						// Statusメッセージを作成
						response = "Status Code : " + statCode + " = Not Accepted." +
											Environment.NewLine + Environment.NewLine;
					}

					// Telemetry Tabに表示
					UpdateTextBox(response);
				}

			}

			// Digipeaterデータ（交信ログ）の処理
			if (!string.IsNullOrEmpty(logData))
			{
				DisplayQSO(logData);
			}
		}
		else
		{
			Log.Error("Receive DATA and/or LOG file is not exist.");
			return;
		}
	}

	static IEnumerable<string> ExecuteTestExe(string arg1, string arg2)
	{
		// プロセスの設定
		ProcessStartInfo startInfo = new()
		{
			FileName = @".\pyenv\post_request.exe",    // 実行するexeのパス
			Arguments = $"{arg1} {arg2}",           // 引数を指定
			RedirectStandardOutput = true,          // 標準出力をリダイレクト
			UseShellExecute = false,                // シェル機能を使わない
			CreateNoWindow = true                   // コンソールウィンドウを表示しない
		};

		// プロセスを開始
		using var process = new Process { StartInfo = startInfo };
		process.Start();

			// 標準出力から結果を取得
		while (!process.StandardOutput.EndOfStream) 
		{
			yield return process.StandardOutput.ReadToEnd();
		}
	}


	// UI要素(TxtTelemetry)が作成されたスレッド以外から安全に書き込める様にする
	private void UpdateTextBox(string text)
	{
		if (TxtTelemetry.InvokeRequired)
		{
			TxtTelemetry.Invoke(new Action<string>(UpdateTextBox), text);
		}
		else
		{
			if (!TxtTelemetry.IsDisposed)
			{
				TxtTelemetry.AppendText(text);
			}
		}
	}

	// UI要素(LblFrameNum)が作成されたスレッド以外から安全に書き込める様にする
	private void UpdateLabel(string text)
	{
		if (LblFrameNum.InvokeRequired)
		{
			LblFrameNum.Invoke(new Action<string>(UpdateLabel), text);
		}
		else
		{
			if (!LblFrameNum.IsDisposed)
			{
				LblFrameNum.Text = text;
			}
		}
	}

	///
	///	Telemetryデータを転送できるフォーマットに変更（ログ、画面表示）
	///
	private (string payloadString, string dispTelemetry) CreateForwardingData(string data)
	{
		string telemetryString = data;

		// Telemetryのバイト数を算出（表示に使用）
		int telemetryLength = telemetryString.Length / 2;

		// 表示・ログ・JSONに使用するTimeStampを生成
		DateTime dtUtc = DateTime.UtcNow;                                       // UTC限定用
		DateTime dt;                                                            // ローカル・UTC切替用
		if (RbnLocal.Checked)
		{
			dt = DateTime.Now;
		}
		else
		{
			dt = dtUtc;
		}

		// テキストボックスの数字を数値に変換
		int.TryParse(LblNoradID.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int NoradID);

		// payloadの構成要素を設定より取得し文字列を作成(jsonはPythonに作らせるためデータのみ）
		string payloadString =
			$"{NoradID}," +
			$"{frmSettings.TxtCallsign.Text.Trim()}," +
			$"{dtUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ").Trim()}," +
			$"{telemetryString}," +
			$"longLat," +
			$"{frmSettings.TxtLongitude.Text.Trim()}," +
			$"{frmSettings.TxtLatitude.Text.Trim()}";

		// Telemetry 画面出力用にバイト毎スペース区切り
		telemetryString = string.Join(" ", Enumerable.Range(0, telemetryLength)
				.Select(i => telemetryString.Substring(i * 2, 2)));

		// Telemetry画面表示フォーマットの定義
		string dispTelemetry = $"[{forwardNum}] "
							 + $"{dt:HH:mm:ss} "                               // スクリーンタイムの指定による
							 + " [Packet length : "
							 + telemetryLength + "]"
							 + Environment.NewLine                             // 改行
							 + telemetryString.Trim()
							 + Environment.NewLine;

		// 送信用・表示用とも呼び出し元へ戻す
		return (payloadString, dispTelemetry);
	}

	///
	///  テーブル形式に整形して画面表示、ログファイルを残す。
	/// 
	private void DisplayQSO(string logData)
	{
		// 表示・ログ・JSONに使用するTimeStampを生成
		DateTime dtUtc = DateTime.UtcNow;                                       // UTC限定用
		DateTime dt;                                                            // ローカル・UTC切替用

		// local/UTCどちらが選択されているかによってdtのTimeZoneを決める
		if (RbnLocal.Checked)
		{
			dt = DateTime.Now;
		}
		else
		{
			dt = dtUtc;
		}

		// Digipeater LOGファイルに出力
		if (digPath != null)
		{
			File.AppendAllText(
			digPath,
			dtUtc.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + logData
			+ Environment.NewLine
			);
		}
		else
		{
			Debug.WriteLine("Error: Digipeater Log path is NULL.");
		}

		// Digipeaterグリッド表示用に新しい行を追加（行数カウント）
		int rowIndex = GrdDigipeater.RowCount - 1;                              // 何行目か算出（ヘッダー分を引く）

		// 表示用データをスレッド間表示サブへ送る
		string[] dispData = logData[1..^1].Split(",");
		string time = dt.ToString("HH:mm:ss");
		UpdateGrid(dispData, rowIndex, time);
	}

	// UI要素(GrdDigipeater)が作成されたスレッド以外から安全に書き込める様にする
	private void UpdateGrid(string[] data, int row, string time)
	{
		if (GrdDigipeater.InvokeRequired)
		{
			GrdDigipeater.Invoke(new Action<string[], int, string>(UpdateGrid), data, row, time);
		}
		else
		{
			if (data != null && row >= 0 && time != null && !TxtTelemetry.IsDisposed)
			{
				// 新しい行にデータを表示する
				GrdDigipeater.Rows.Insert(
					row,
					time,
					data[0].Trim(),
					data[1].Trim(),
					data[2].Trim(),
					data[3].Trim()
				);

				// データの内自局分を色分けする
				DataGridViewCellStyle stringColor = new();
					
				// 送信元が自局の場合（自局送信のリピート受信）
				if (data[0].Trim() == frmSettings.TxtCallsign.Text.Trim())
				{
					stringColor.ForeColor = Color.Orange;
					stringColor.BackColor = Color.Black;
					GrdDigipeater.Rows[row].DefaultCellStyle = stringColor;
				}
				// 自局を呼ばれた場合（相手先が自局）
				else if (data[1].Trim() == frmSettings.TxtCallsign.Text.Trim())
				{
					stringColor.ForeColor = Color.LightGreen;
					stringColor.BackColor = Color.Black;
					GrdDigipeater.Rows[row].DefaultCellStyle = stringColor;
				}
				else
				{
					GrdDigipeater.Rows[row].DefaultCellStyle = null;
				}

				// 表示可能な行数を把握してその行が見える最終までスクロールする
				GrdDigipeater.FirstDisplayedScrollingRowIndex = GrdDigipeater.Rows.GetLastRow(DataGridViewElementStates.Visible);
			}
		}

	}
}

