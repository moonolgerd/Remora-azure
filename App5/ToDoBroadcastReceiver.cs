using Android.App;
using Android.Content;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace App5
{
	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
		Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
		Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
	Categories = new string[] { "@PACKAGE_NAME@" })]
	class ToDoBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
	{
		// Set the Google app ID.
		public static string[] senderIDs = new string[] { "877781133075" };
	}
}