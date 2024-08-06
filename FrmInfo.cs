using System.Diagnostics;

namespace TLMForwarder
{
	public partial class FrmInfo : Form
	{
		public FrmInfo(string versionInfo)
		{
			InitializeComponent();
			LblVersion.Text = $"v.{versionInfo}";
			LblGitHub.Links.Add(0, LblGitHub.Text.Length, "https://github.com/ytodo/TLMForwarder");
		}

		private void BtnCloseInfo_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
	
			if (e.Link != null && e.Link.LinkData is string link)
			{		

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
	}
}
