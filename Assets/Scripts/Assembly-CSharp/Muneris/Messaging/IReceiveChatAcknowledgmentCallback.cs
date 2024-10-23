namespace Muneris.Messaging
{
	public interface IReceiveChatAcknowledgmentCallback : ICallback
	{
		void onReceiveChatAcknowledgment(ChatAcknowledgment acknowledgment);
	}
}
