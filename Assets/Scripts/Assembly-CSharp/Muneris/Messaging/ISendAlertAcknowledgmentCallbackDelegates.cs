namespace Muneris.Messaging
{
	public class ISendAlertAcknowledgmentCallbackDelegates : ICallback, ISendAlertAcknowledgmentCallback
	{
		public delegate void onSendAlertAcknowledgmentDelegate(AlertAcknowledgment acknowledgment, AlertAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendAlertAcknowledgmentDelegate m_onSendAlertAcknowledgmentDelegate;

		public ISendAlertAcknowledgmentCallbackDelegates(onSendAlertAcknowledgmentDelegate _onSendAlertAcknowledgmentDelegate)
		{
			m_onSendAlertAcknowledgmentDelegate = _onSendAlertAcknowledgmentDelegate;
		}

		public void onSendAlertAcknowledgment(AlertAcknowledgment acknowledgment, AlertAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendAlertAcknowledgmentDelegate != null)
			{
				m_onSendAlertAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
