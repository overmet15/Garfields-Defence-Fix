namespace Muneris.Messaging
{
	public class ISendStatusAcknowledgmentCallbackDelegates : ICallback, ISendStatusAcknowledgmentCallback
	{
		public delegate void onSendStatusAcknowledgmentDelegate(StatusAcknowledgment acknowledgment, StatusAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendStatusAcknowledgmentDelegate m_onSendStatusAcknowledgmentDelegate;

		public ISendStatusAcknowledgmentCallbackDelegates(onSendStatusAcknowledgmentDelegate _onSendStatusAcknowledgmentDelegate)
		{
			m_onSendStatusAcknowledgmentDelegate = _onSendStatusAcknowledgmentDelegate;
		}

		public void onSendStatusAcknowledgment(StatusAcknowledgment acknowledgment, StatusAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendStatusAcknowledgmentDelegate != null)
			{
				m_onSendStatusAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
