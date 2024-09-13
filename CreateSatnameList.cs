using System.Diagnostics;
using Serilog;

public class CreateSatList
{
	/// <summary>
	/// FrmMain.csからtleURLを与えられて、そこから読み取ったTLE.txtを元に
	/// Sattellite name:NoradID のリストを作成して、その成果と状況を示す
	/// ラベル様データとしてメッセージと文字色を返すクラスメソッド
	/// </summary>
	/// <param name="tleURL"></param>
	/// <returns></returns>
	public static (List<string[]>? sat_list, string text, Color color) TLE2SatnameList(string tleURL)
	{
		// リスト sat_list を定義
		List<string[]>? sat_list = [];

		//	異常終了により残ったTLE.txt が有れば削除
		if (File.Exists(@".\TLE.txt"))
		{
			File.Delete(@".\TLE.txt");
		}

		// TLE.txt名でダウンロード
		string fileName = @"TLE.txt";
		string text;

		// tleURLが指定されていないとき
		if (tleURL == "")
		{
			text = $"Set the TLE FILE/URL in Settings";
			Color colr1 = Color.Red;
			Log.Error($"Not set FILE/URL for Satellite List.");

			return (null, text, colr1);
		}

		// 指定されているときの有効性判断
		if (tleURL != "http://www.amsat.org/tle/current/daily-bulletin.txt"
			&& tleURL[..2] != "C:" 
			&& tleURL[..2] != "c:")
		{
			// TLEの処理が出来ない場合のメッセージ
			text = "Change URL \"daily-bulletin.txt\" or \"Local handmade.txt\"";
			Color colr2 = Color.Red;
			Log.Warning($"TLE url : {tleURL} could not use.");

			return (null, text, colr2);
		}

		// tleURLから**非同期で**ダウンロード、カレントディレクトリーに fileNameで保存
		string content;

		if (tleURL == "http://www.amsat.org/tle/current/daily-bulletin.txt")
		{
			using (HttpClient? client = new())
			content = client.GetStringAsync(tleURL).Result;
			File.WriteAllText(fileName, content);
		}
		else
		{
			// Pathの表示をエスケープする
			tleURL = tleURL.Replace("\\", "\\\\");
			Debug.WriteLine(tleURL);

			try
			{	
				// WEB指定ではなくローカルファイルの場合単純コピー
				File.Copy(tleURL, fileName);
			}
			catch (IOException)
			{
				// TLEの処理が出来ない場合のメッセージ
				text = "\"Local handmade.txt\" file could'nt use.";
				Color colr3 = Color.Red;
				Log.Warning($"TLE url : {tleURL} could not use.");
	
				return (null, text, colr3);
			}
		}

		// ファイルからすべての行を読み込む
		string[] lines = File.ReadAllLines(fileName);

		// リストを定義
		string[] sat = new string[2];
		var num = 0;                        // ヘッダー部を含む行番号
		var row = 0;                        // データ部のみの行番号
		var headnum = 0;

		// ヘッダーを除く各行を処理して配列に追加
		foreach (string line in lines)
		{
			// TLEのソース増やす必要が有る時はこの条件を追加する
			if (tleURL[^18..] == "daily-bulletin.txt")
			{
				// 特定の文字列で行数を判断しヘッダー部を省く
				string headerEnd = "www.amsat.org/tle/dailytle.txt";
				if (line.Contains(headerEnd))
				{
					headnum = num + 2;
				}
			} 
			else
			{
				// 特定の文字列で行数を判断しヘッダー部を省く
				string headerEnd = "Handmade TLE List";
				if (line.Contains(headerEnd))   
					// 特定の文字列で行数を判断しヘッダー部を省く
					headnum = num + 11;
			}

			///
			/// tleURLが増えた時はその処理をここに記述する ///
			/// 

			// 衛星, NoradIDリストの作成（各ソースで共通）
			if (num > headnum && headnum != 0)
			{
				// １行目は衛星名、２・3行目が2ラインエレメント(TLE)
				if (row % 3 == 0)
				{
					sat[0] = line + ':';
				}

				// 3行名のスペース区切り2桁目がNoradID
				if (row % 3 == 2)
				{
					string[] parts = line.Split(' ');
					sat[1] = parts[1];
					string[] tmp = [sat[0], sat[1]];
					sat_list.Add(tmp);
				}
				row++;
			}
			num++;
		}

		// 衛星のリストに無いものをテストとしてリスト末端に加える
		string[] tmp2 = ["ZZ/ TEST /ZZ:", "00000"];
		sat_list.Add(tmp2);

		text = "";
		Color colr4 = Color.Empty;

		return (sat_list, text, colr4);
	}
}
