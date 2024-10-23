namespace Muneris.Messaging
{
	public class ISendChatMessageCallbackDelegates : ICallback, ISendChatMessageCallback
	{
		public delegate void onSendChatMessageDelegate(ChatMessage message, ChatMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendChatMessageDelegate m_onSendChatMessageDelegate;

		public ISendChatMessageCallbackDelegates(onSendChatMessageDelegate _onSendChatMessageDelegate)
		{
			m_onSendChatMessageDelegate = _onSendChatMessageDelegate;
		}

		public void onSendChatMessage(ChatMessage message, ChatMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendChatMessageDelegate != null)
			{
				m_onSendChatMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
