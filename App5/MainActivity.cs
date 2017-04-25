using Android.App;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Gcm.Client;
using Remora_azure.Shared;

namespace App5
{
	[Activity(Label = "App5", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private static MainActivity instance = new MainActivity();
		private MobileServiceClient _client;

		public static MainActivity CurrentActivity
		{
			get
			{
				return instance;
			}
		}

		public MobileServiceClient CurrentClient
		{
			get
			{
				return _client;
			}
		}

		protected override async void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			CurrentPlatform.Init();
			_client = new MobileServiceClient("http://remora-azure.azurewebsites.net");

			instance = this;
			GcmClient.CheckDevice(this);
			GcmClient.CheckManifest(this);
			GcmClient.Register(this, ToDoBroadcastReceiver.senderIDs);

			SetContentView (Resource.Layout.Main);


			var table = await CurrentClient.GetTable<ITodoItem>().ReadAsync();
		}
	}
}

