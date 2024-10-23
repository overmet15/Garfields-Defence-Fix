namespace Muneris.Messaging
{
	public class ISendCustomResponseAcknowledgmentCallbackDelegates : ICallback, ISendCustomResponseAcknowledgmentCallback
	{
		public delegate void onSendCustomResponseAcknowledgmentDelegate(CustomResponseAcknowledgment acknowledgment, CustomResponseAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomResponseAcknowledgmentDelegate m_onSendCustomResponseAcknowledgmentDelegate;

		public ISendCustomResponseAcknowledgmentCallbackDelegates(onSendCustomResponseAcknowledgmentDelegate _onSendCustomResponseAcknowledgmentDelegate)
		{
			m_onSendCustomResponseAcknowledgmentDelegate = _onSendCustomResponseAcknowledgmentDelegate;
		}

		public void onSendCustomResponseAcknowledgment(CustomResponseAcknowledgment acknowledgment, CustomResponseAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomResponseAcknowledgmentDelegate != null)
			{
				m_onSendCustomResponseAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
