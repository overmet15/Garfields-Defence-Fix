namespace Muneris.Messaging
{
	public interface ISendCustomMessageCallback : ICallback
	{
		void onSendCustomMessage(CustomMessage message, CustomMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
