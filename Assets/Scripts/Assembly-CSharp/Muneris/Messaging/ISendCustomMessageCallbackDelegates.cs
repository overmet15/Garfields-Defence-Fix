namespace Muneris.Messaging
{
	public class ISendCustomMessageCallbackDelegates : ICallback, ISendCustomMessageCallback
	{
		public delegate void onSendCustomMessageDelegate(CustomMessage message, CustomMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomMessageDelegate m_onSendCustomMessageDelegate;

		public ISendCustomMessageCallbackDelegates(onSendCustomMessageDelegate _onSendCustomMessageDelegate)
		{
			m_onSendCustomMessageDelegate = _onSendCustomMessageDelegate;
		}

		public void onSendCustomMessage(CustomMessage message, CustomMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomMessageDelegate != null)
			{
				m_onSendCustomMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
