namespace Muneris.Messaging
{
	public class ISendCustomAcknowledgmentCallbackDelegates : ICallback, ISendCustomAcknowledgmentCallback
	{
		public delegate void onSendCustomAcknowledgmentDelegate(CustomAcknowledgment acknowledgment, CustomAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomAcknowledgmentDelegate m_onSendCustomAcknowledgmentDelegate;

		public ISendCustomAcknowledgmentCallbackDelegates(onSendCustomAcknowledgmentDelegate _onSendCustomAcknowledgmentDelegate)
		{
			m_onSendCustomAcknowledgmentDelegate = _onSendCustomAcknowledgmentDelegate;
		}

		public void onSendCustomAcknowledgment(CustomAcknowledgment acknowledgment, CustomAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomAcknowledgmentDelegate != null)
			{
				m_onSendCustomAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
