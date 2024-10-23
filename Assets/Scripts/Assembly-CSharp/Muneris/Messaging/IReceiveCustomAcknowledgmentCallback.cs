namespace Muneris.Messaging
{
	public interface IReceiveCustomAcknowledgmentCallback : ICallback
	{
		void onReceiveCustomAcknowledgment(CustomAcknowledgment acknowledgment);
	}
}
