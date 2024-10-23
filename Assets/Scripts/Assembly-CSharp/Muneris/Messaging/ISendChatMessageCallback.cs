namespace Muneris.Messaging
{
	public interface ISendChatMessageCallback : ICallback
	{
		void onSendChatMessage(ChatMessage message, ChatMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
