namespace Muneris.Messaging
{
	public interface IReceiveChatMessageCallback : ICallback
	{
		void onReceiveChatMessage(ChatMessage message);
	}
}
