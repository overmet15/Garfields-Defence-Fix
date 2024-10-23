namespace Muneris.Messaging
{
	public interface IReceiveCustomResponseAcknowledgmentCallback : ICallback
	{
		void onReceiveCustomResponseAcknowledgment(CustomResponseAcknowledgment acknowledgment);
	}
}
