namespace Muneris.Messaging
{
	public class IReceiveCustomRequestMessageCallbackDelegates : ICallback, IReceiveCustomRequestMessageCallback
	{
		public delegate void onReceiveCustomRequestMessageDelegate(CustomRequestMessage message);

		private onReceiveCustomRequestMessageDelegate m_onReceiveCustomRequestMessageDelegate;

		public IReceiveCustomRequestMessageCallbackDelegates(onReceiveCustomRequestMessageDelegate _onReceiveCustomRequestMessageDelegate)
		{
			m_onReceiveCustomRequestMessageDelegate = _onReceiveCustomRequestMessageDelegate;
		}

		public void onReceiveCustomRequestMessage(CustomRequestMessage message)
		{
			if (m_onReceiveCustomRequestMessageDelegate != null)
			{
				m_onReceiveCustomRequestMessageDelegate(message);
			}
		}
	}
}
