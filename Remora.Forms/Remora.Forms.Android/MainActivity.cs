using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Remora.Forms.Droid
{
	[Activity (Label = "Remora.Forms", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			// Initialization for Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			// This MobileServiceClient has been configured to communicate with the Azure Mobile App and
			// Azure Gateway using the application url. You're all set to start working with your Mobile App!
			Microsoft.WindowsAzure.MobileServices.MobileServiceClient Remora_azureClient = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(
			"http://remora-azure.azurewebsites.net");

			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new Remora_azure.Shared.App ());
		}
	}
}

