using System.Diagnostics;
using System.Drawing.Text;
using TLMForwarder;

namespace TLMForwarder
{
	public partial class FrmInfo : Form
	{
		private readonly string NoradID;

		//
		// フォーム起ち上げ時の処理
		//
		public FrmInfo(string versionInfo, string noradID)
		{
			InitializeComponent();

			// バージョン情報の表示
			LblVersion.Text = $"v.{versionInfo}";

			// URLをクリックした時のリンク先
			LblGitHub.Links.Add(0, LblGitHub.Text.Length, "https://github.com/ytodo/TLMForwarder");
			LblSatNOGS.Links.Add(0, LblSatNOGS.Text.Length, "https://db.satnogs.org/satellite/");

			// 現在FrmMainで選択されている衛星の、転送されたIDをクラス変数に置き換える
			NoradID = noradID;
		}

		//
		//	Closeボタン
		//
		private void BtnCloseInfo_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		//
		//	GitHubのリンクをクリックしたときの処理
		//
		private void LblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// e.Linkがnullでない、且つLinkDataをstring型とすることで null 参照エラーを回避
			if (e.Link != null && e.Link.LinkData is string link)
			{
				// 規定のブラウザーでリンク先を開く
				ProcessStartInfo proc = new()
				{
					FileName = link,
					UseShellExecute = true,
				};
				Process.Start(proc);

				//リンク先に移動したフラグを立てる
				LblGitHub.LinkVisited = true;
			}
		}

		//
		//	SatNOGSのリンクをクリックした時の処理
		//
		private void LblSatNOGS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// e.Linkがnullでない、且つLinkDataをstring型とすることで null 参照エラーを回避
			if (e.Link != null && e.Link.LinkData is string link)
			{
				// 規定のプラウザーでリンク先に衛星区分を付けて開く
				ProcessStartInfo proc = new()
				{
					FileName = link + NoradID,
					UseShellExecute = true,
				};
				Process.Start(proc);

				//リンク先に移動したフラグを立てる
				LblSatNOGS.LinkVisited = true;
			}
		}
	}
}
