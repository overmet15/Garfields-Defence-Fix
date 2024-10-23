namespace Muneris.Pushnotification
{
	public class IUnregisterPushNotificationCallbackDelegates : ICallback, IUnregisterPushNotificationCallback
	{
		public delegate void onUnregisterPushNotificationDelegate(string registrationId, PushNotificationServiceProvider provider, MunerisException exception);

		private onUnregisterPushNotificationDelegate m_onUnregisterPushNotificationDelegate;

		public IUnregisterPushNotificationCallbackDelegates(onUnregisterPushNotificationDelegate _onUnregisterPushNotificationDelegate)
		{
			m_onUnregisterPushNotificationDelegate = _onUnregisterPushNotificationDelegate;
		}

		public void onUnregisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception)
		{
			if (m_onUnregisterPushNotificationDelegate != null)
			{
				m_onUnregisterPushNotificationDelegate(registrationId, provider, exception);
			}
		}
	}
}
