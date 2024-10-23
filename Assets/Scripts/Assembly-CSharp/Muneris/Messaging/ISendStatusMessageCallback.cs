namespace Muneris.Messaging
{
	public interface ISendStatusMessageCallback : ICallback
	{
		void onSendStatusMessage(StatusMessage message, StatusMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
