namespace Muneris.Messaging
{
	public interface IReceiveAlertMessageCallback : ICallback
	{
		void onReceiveAlertMessage(AlertMessage message);
	}
}
