namespace Muneris.Messaging
{
	public class ISendCustomRequestMessageCallbackDelegates : ICallback, ISendCustomRequestMessageCallback
	{
		public delegate void onSendCustomRequestMessageDelegate(CustomRequestMessage message, CustomRequestMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomRequestMessageDelegate m_onSendCustomRequestMessageDelegate;

		public ISendCustomRequestMessageCallbackDelegates(onSendCustomRequestMessageDelegate _onSendCustomRequestMessageDelegate)
		{
			m_onSendCustomRequestMessageDelegate = _onSendCustomRequestMessageDelegate;
		}

		public void onSendCustomRequestMessage(CustomRequestMessage message, CustomRequestMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomRequestMessageDelegate != null)
			{
				m_onSendCustomRequestMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
