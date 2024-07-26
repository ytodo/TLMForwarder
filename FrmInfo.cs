namespace TLMForwarder
{
	public partial class FrmInfo : Form
	{
		public FrmInfo(string versionInfo)
		{
			InitializeComponent();
			LblVersion.Text = versionInfo;
		}

		private void BtnCloseInfo_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
