using Android.App;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;

namespace App5
{
	[Activity(Label = "App5", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private static MainActivity instance = new MainActivity();
		private MobileServiceClient _client;

        public static MainActivity CurrentActivity => instance;

        public MobileServiceClient CurrentClient => _client;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			CurrentPlatform.Init();
			_client = new MobileServiceClient("http://remora-azure.azurewebsites.net");

			instance = this;

			SetContentView (Resource.Layout.Main);
		}
	}
}

