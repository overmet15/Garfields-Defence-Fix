namespace Muneris.Messaging
{
	public interface ISendCustomResponseAcknowledgmentCallback : ICallback
	{
		void onSendCustomResponseAcknowledgment(CustomResponseAcknowledgment acknowledgment, CustomResponseAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
