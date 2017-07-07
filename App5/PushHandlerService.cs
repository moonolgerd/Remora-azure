using System;
using Android.App;
using Android.Content;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace App5
{
	[Service]
	class PushHandlerService : GcmServiceBase
	{
		public static string RegistrationID { get; private set; }

		public PushHandlerService() : base(ToDoBroadcastReceiver.senderIDs) { }

		protected override void OnRegistered(Context context, string registrationId)
		{
			System.Diagnostics.Debug.WriteLine("The device has been registered with GCM.", "Success!");

			var client = MainActivity.CurrentActivity.CurrentClient;
			var push = client.GetPush();

			// Define a message body for GCM.
			const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";

			// Define the template registration as JSON.
			var templates = new JObject
			{
				["genericMessage"] = new JObject
			{
				{"body", templateBodyGCM }
			}
			};
			try
			{
				MainActivity.CurrentActivity.RunOnUiThread(
					async () => await push.RegisterAsync(registrationId, templates));

				System.Diagnostics.Debug.WriteLine($"Push Installation Id: {push.InstallationId}");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(
					string.Format("Error with Azure push registration: {0}", ex.Message));
			}
		}


		protected override void OnMessage(Context context, Intent intent)
		{
			string message = string.Empty;

			// Extract the push notification message from the intent.
			if (intent.Extras.ContainsKey("message"))
			{
				message = intent.Extras.Get("message").ToString();
				var title = "New item added:";

				// Create a notification manager to send the notification.
				var notificationManager =
					GetSystemService(Context.NotificationService) as NotificationManager;

				// Create a new intent to show the notification in the UI. 
				PendingIntent contentIntent =
					PendingIntent.GetActivity(context, 0,
					new Intent(this, typeof(MainActivity)), 0);

				// Create the notification using the builder.
				var builder = new Notification.Builder(context);
				builder.SetAutoCancel(true);
				builder.SetContentTitle(title);
				builder.SetContentText(message);
				builder.SetSmallIcon(Resource.Drawable.Icon);
				builder.SetContentIntent(contentIntent);
				var notification = builder.Build();

				// Display the notification in the Notifications Area.
				notificationManager.Notify(1, notification);
			}
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
			throw new NotImplementedException();
		}

		protected override void OnError(Context context, string errorId)
		{
			System.Diagnostics.Debug.WriteLine($"Error occurred in the notification: {errorId}.");
		}
	}
}