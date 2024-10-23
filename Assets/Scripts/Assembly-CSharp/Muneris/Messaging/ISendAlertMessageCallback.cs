namespace Muneris.Messaging
{
	public interface ISendAlertMessageCallback : ICallback
	{
		void onSendAlertMessage(AlertMessage message, AlertMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
