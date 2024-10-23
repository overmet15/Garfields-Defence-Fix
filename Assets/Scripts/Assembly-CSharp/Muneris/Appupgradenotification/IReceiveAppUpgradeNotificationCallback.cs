namespace Muneris.Appupgradenotification
{
	public interface IReceiveAppUpgradeNotificationCallback : ICallback
	{
		void onReceiveAppUpgradeNotification(AppUpgradeNotification appUpgradeNotification);
	}
}
