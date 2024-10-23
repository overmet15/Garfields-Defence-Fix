namespace Muneris.Messaging
{
	public class ISendCustomRequestAcknowledgmentCallbackDelegates : ICallback, ISendCustomRequestAcknowledgmentCallback
	{
		public delegate void onSendCustomRequestAcknowledgmentDelegate(CustomRequestAcknowledgment acknowledgment, CustomRequestAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomRequestAcknowledgmentDelegate m_onSendCustomRequestAcknowledgmentDelegate;

		public ISendCustomRequestAcknowledgmentCallbackDelegates(onSendCustomRequestAcknowledgmentDelegate _onSendCustomRequestAcknowledgmentDelegate)
		{
			m_onSendCustomRequestAcknowledgmentDelegate = _onSendCustomRequestAcknowledgmentDelegate;
		}

		public void onSendCustomRequestAcknowledgment(CustomRequestAcknowledgment acknowledgment, CustomRequestAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomRequestAcknowledgmentDelegate != null)
			{
				m_onSendCustomRequestAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
