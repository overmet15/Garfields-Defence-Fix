namespace Muneris.Messaging
{
	public interface ISendChatAcknowledgmentCallback : ICallback
	{
		void onSendChatAcknowledgment(ChatAcknowledgment acknowledgment, ChatAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
