namespace Muneris.Messaging
{
	public interface IReceiveAlertAcknowledgmentCallback : ICallback
	{
		void onReceiveAlertAcknowledgment(AlertAcknowledgment acknowledgment);
	}
}
