namespace Muneris.Messaging
{
	public class ISendAlertMessageCallbackDelegates : ICallback, ISendAlertMessageCallback
	{
		public delegate void onSendAlertMessageDelegate(AlertMessage message, AlertMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendAlertMessageDelegate m_onSendAlertMessageDelegate;

		public ISendAlertMessageCallbackDelegates(onSendAlertMessageDelegate _onSendAlertMessageDelegate)
		{
			m_onSendAlertMessageDelegate = _onSendAlertMessageDelegate;
		}

		public void onSendAlertMessage(AlertMessage message, AlertMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendAlertMessageDelegate != null)
			{
				m_onSendAlertMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
