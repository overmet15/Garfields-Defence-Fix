namespace Muneris.Messaging
{
	public interface ISendCustomRequestAcknowledgmentCallback : ICallback
	{
		void onSendCustomRequestAcknowledgment(CustomRequestAcknowledgment acknowledgment, CustomRequestAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
