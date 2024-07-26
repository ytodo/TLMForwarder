using Serilog;

namespace TLMForwarder;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>

    [STAThread]
    static void Main()
    {
		// アプリケーション運用ログの生成
		CreateSysLog();

 	    // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new FrmMain());
	}	

	/*******************************************************
	 *	アプリケーション運用ログの生成 
	 *******************************************************/
	private static void CreateSysLog()
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Information()                                                 // Information以上に重要なもののみ
			.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)   // MicrosoftからのWarning以上も
			.Enrich.FromLogContext()
			.WriteTo.File(
				@".\\syslog\\TLMForwarder-.log",
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
				rollingInterval: RollingInterval.Day,
				retainedFileCountLimit: 10
			)
			.CreateLogger();        							// 上記設定でLoggerインスタンスを作成

		try
		{
			Log.Information("[Starting Application and Logger... ]");
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "The Application has closed.");
		}
		// 終了時には必ず	Log.CloseAndFlush(); を実行する (quitボタンなど）
	}
}
