namespace Muneris.Messaging
{
	public class IReceiveChatMessageCallbackDelegates : ICallback, IReceiveChatMessageCallback
	{
		public delegate void onReceiveChatMessageDelegate(ChatMessage message);

		private onReceiveChatMessageDelegate m_onReceiveChatMessageDelegate;

		public IReceiveChatMessageCallbackDelegates(onReceiveChatMessageDelegate _onReceiveChatMessageDelegate)
		{
			m_onReceiveChatMessageDelegate = _onReceiveChatMessageDelegate;
		}

		public void onReceiveChatMessage(ChatMessage message)
		{
			if (m_onReceiveChatMessageDelegate != null)
			{
				m_onReceiveChatMessageDelegate(message);
			}
		}
	}
}
