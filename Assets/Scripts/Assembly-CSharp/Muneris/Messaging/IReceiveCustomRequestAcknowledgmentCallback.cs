namespace Muneris.Messaging
{
	public interface IReceiveCustomRequestAcknowledgmentCallback : ICallback
	{
		void onReceiveCustomRequestAcknowledgment(CustomRequestAcknowledgment acknowledgment);
	}
}
