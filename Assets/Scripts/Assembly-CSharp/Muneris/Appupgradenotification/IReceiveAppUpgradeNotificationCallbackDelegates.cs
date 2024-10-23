namespace Muneris.Appupgradenotification
{
	public class IReceiveAppUpgradeNotificationCallbackDelegates : IReceiveAppUpgradeNotificationCallback, ICallback
	{
		public delegate void onReceiveAppUpgradeNotificationDelegate(AppUpgradeNotification appUpgradeNotification);

		private onReceiveAppUpgradeNotificationDelegate m_onReceiveAppUpgradeNotificationDelegate;

		public IReceiveAppUpgradeNotificationCallbackDelegates(onReceiveAppUpgradeNotificationDelegate _onReceiveAppUpgradeNotificationDelegate)
		{
			m_onReceiveAppUpgradeNotificationDelegate = _onReceiveAppUpgradeNotificationDelegate;
		}

		public void onReceiveAppUpgradeNotification(AppUpgradeNotification appUpgradeNotification)
		{
			if (m_onReceiveAppUpgradeNotificationDelegate != null)
			{
				m_onReceiveAppUpgradeNotificationDelegate(appUpgradeNotification);
			}
		}
	}
}
