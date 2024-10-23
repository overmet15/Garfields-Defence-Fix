namespace Muneris.Pushnotification
{
	public class IOpenPushNotificationCallbackDelegates : ICallback, IOpenPushNotificationCallback
	{
		public delegate void onOpenPushNotificationDelegate(JsonObject data);

		private onOpenPushNotificationDelegate m_onOpenPushNotificationDelegate;

		public IOpenPushNotificationCallbackDelegates(onOpenPushNotificationDelegate _onOpenPushNotificationDelegate)
		{
			m_onOpenPushNotificationDelegate = _onOpenPushNotificationDelegate;
		}

		public void onOpenPushNotification(JsonObject data)
		{
			if (m_onOpenPushNotificationDelegate != null)
			{
				m_onOpenPushNotificationDelegate(data);
			}
		}
	}
}
