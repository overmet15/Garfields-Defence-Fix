namespace Muneris.Messaging
{
	public interface IReceiveStatusAcknowledgmentCallback : ICallback
	{
		void onReceiveStatusAcknowledgment(StatusAcknowledgment acknowledgment);
	}
}
