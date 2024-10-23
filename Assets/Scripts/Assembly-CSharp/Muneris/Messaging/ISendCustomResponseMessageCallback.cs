namespace Muneris.Messaging
{
	public interface ISendCustomResponseMessageCallback : ICallback
	{
		void onSendCustomResponseMessage(CustomResponseMessage message, CustomResponseMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
