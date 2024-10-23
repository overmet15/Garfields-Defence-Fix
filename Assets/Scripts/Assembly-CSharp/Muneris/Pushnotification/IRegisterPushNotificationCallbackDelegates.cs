namespace Muneris.Pushnotification
{
	public class IRegisterPushNotificationCallbackDelegates : ICallback, IRegisterPushNotificationCallback
	{
		public delegate void onRegisterPushNotificationDelegate(string registrationId, PushNotificationServiceProvider provider, MunerisException exception);

		private onRegisterPushNotificationDelegate m_onRegisterPushNotificationDelegate;

		public IRegisterPushNotificationCallbackDelegates(onRegisterPushNotificationDelegate _onRegisterPushNotificationDelegate)
		{
			m_onRegisterPushNotificationDelegate = _onRegisterPushNotificationDelegate;
		}

		public void onRegisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception)
		{
			if (m_onRegisterPushNotificationDelegate != null)
			{
				m_onRegisterPushNotificationDelegate(registrationId, provider, exception);
			}
		}
	}
}
