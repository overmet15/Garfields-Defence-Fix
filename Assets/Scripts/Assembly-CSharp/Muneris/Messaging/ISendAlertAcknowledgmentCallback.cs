namespace Muneris.Messaging
{
	public interface ISendAlertAcknowledgmentCallback : ICallback
	{
		void onSendAlertAcknowledgment(AlertAcknowledgment acknowledgment, AlertAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
