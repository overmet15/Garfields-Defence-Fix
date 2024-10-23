namespace Muneris.Messaging
{
	public interface ISendCustomAcknowledgmentCallback : ICallback
	{
		void onSendCustomAcknowledgment(CustomAcknowledgment acknowledgment, CustomAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
