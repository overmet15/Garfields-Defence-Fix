namespace Muneris.Messaging
{
	public class IReceiveChatAcknowledgmentCallbackDelegates : ICallback, IReceiveChatAcknowledgmentCallback
	{
		public delegate void onReceiveChatAcknowledgmentDelegate(ChatAcknowledgment acknowledgment);

		private onReceiveChatAcknowledgmentDelegate m_onReceiveChatAcknowledgmentDelegate;

		public IReceiveChatAcknowledgmentCallbackDelegates(onReceiveChatAcknowledgmentDelegate _onReceiveChatAcknowledgmentDelegate)
		{
			m_onReceiveChatAcknowledgmentDelegate = _onReceiveChatAcknowledgmentDelegate;
		}

		public void onReceiveChatAcknowledgment(ChatAcknowledgment acknowledgment)
		{
			if (m_onReceiveChatAcknowledgmentDelegate != null)
			{
				m_onReceiveChatAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
