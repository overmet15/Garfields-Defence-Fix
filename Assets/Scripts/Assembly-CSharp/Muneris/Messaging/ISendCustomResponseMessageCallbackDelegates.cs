namespace Muneris.Messaging
{
	public class ISendCustomResponseMessageCallbackDelegates : ICallback, ISendCustomResponseMessageCallback
	{
		public delegate void onSendCustomResponseMessageDelegate(CustomResponseMessage message, CustomResponseMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendCustomResponseMessageDelegate m_onSendCustomResponseMessageDelegate;

		public ISendCustomResponseMessageCallbackDelegates(onSendCustomResponseMessageDelegate _onSendCustomResponseMessageDelegate)
		{
			m_onSendCustomResponseMessageDelegate = _onSendCustomResponseMessageDelegate;
		}

		public void onSendCustomResponseMessage(CustomResponseMessage message, CustomResponseMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendCustomResponseMessageDelegate != null)
			{
				m_onSendCustomResponseMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
