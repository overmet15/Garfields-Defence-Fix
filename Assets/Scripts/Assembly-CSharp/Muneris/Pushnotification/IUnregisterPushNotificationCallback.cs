namespace Muneris.Pushnotification
{
	public interface IUnregisterPushNotificationCallback : ICallback
	{
		void onUnregisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception);
	}
}
