namespace Muneris.Pushnotification
{
	public interface IOpenPushNotificationCallback : ICallback
	{
		void onOpenPushNotification(JsonObject data);
	}
}
