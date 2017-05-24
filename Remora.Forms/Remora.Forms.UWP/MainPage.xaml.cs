namespace Remora.Forms.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			LoadApplication(new Remora_azure.Shared.App());
		}
	}
}
