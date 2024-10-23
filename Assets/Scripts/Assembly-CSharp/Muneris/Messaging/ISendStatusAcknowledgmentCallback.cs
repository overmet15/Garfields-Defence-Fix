namespace Muneris.Messaging
{
	public interface ISendStatusAcknowledgmentCallback : ICallback
	{
		void onSendStatusAcknowledgment(StatusAcknowledgment acknowledgment, StatusAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
