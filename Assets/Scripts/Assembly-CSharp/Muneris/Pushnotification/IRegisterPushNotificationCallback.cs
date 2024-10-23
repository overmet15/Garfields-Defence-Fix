namespace Muneris.Pushnotification
{
	public interface IRegisterPushNotificationCallback : ICallback
	{
		void onRegisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception);
	}
}
