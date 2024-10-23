namespace Muneris.Messaging
{
	public class ISendStatusMessageCallbackDelegates : ICallback, ISendStatusMessageCallback
	{
		public delegate void onSendStatusMessageDelegate(StatusMessage message, StatusMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendStatusMessageDelegate m_onSendStatusMessageDelegate;

		public ISendStatusMessageCallbackDelegates(onSendStatusMessageDelegate _onSendStatusMessageDelegate)
		{
			m_onSendStatusMessageDelegate = _onSendStatusMessageDelegate;
		}

		public void onSendStatusMessage(StatusMessage message, StatusMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendStatusMessageDelegate != null)
			{
				m_onSendStatusMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
