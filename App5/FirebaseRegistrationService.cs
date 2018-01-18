using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace App5
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseRegistrationService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            Task.Run(async () =>
            {
                await AzureNotificationHubService.RegisterAsync(
                    MainActivity.CurrentActivity.CurrentClient.GetPush(), token);
            });
        }
    }
}