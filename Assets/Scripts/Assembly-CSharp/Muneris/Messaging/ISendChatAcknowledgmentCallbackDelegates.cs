namespace Muneris.Messaging
{
	public class ISendChatAcknowledgmentCallbackDelegates : ICallback, ISendChatAcknowledgmentCallback
	{
		public delegate void onSendChatAcknowledgmentDelegate(ChatAcknowledgment acknowledgment, ChatAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendChatAcknowledgmentDelegate m_onSendChatAcknowledgmentDelegate;

		public ISendChatAcknowledgmentCallbackDelegates(onSendChatAcknowledgmentDelegate _onSendChatAcknowledgmentDelegate)
		{
			m_onSendChatAcknowledgmentDelegate = _onSendChatAcknowledgmentDelegate;
		}

		public void onSendChatAcknowledgment(ChatAcknowledgment acknowledgment, ChatAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendChatAcknowledgmentDelegate != null)
			{
				m_onSendChatAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
