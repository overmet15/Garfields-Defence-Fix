namespace Muneris.Messaging
{
	public interface ISendCustomRequestMessageCallback : ICallback
	{
		void onSendCustomRequestMessage(CustomRequestMessage message, CustomRequestMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
