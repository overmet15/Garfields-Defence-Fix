namespace Muneris.Messaging
{
	public class IReceiveCustomMessageCallbackDelegates : ICallback, IReceiveCustomMessageCallback
	{
		public delegate void onReceiveCustomMessageDelegate(CustomMessage message);

		private onReceiveCustomMessageDelegate m_onReceiveCustomMessageDelegate;

		public IReceiveCustomMessageCallbackDelegates(onReceiveCustomMessageDelegate _onReceiveCustomMessageDelegate)
		{
			m_onReceiveCustomMessageDelegate = _onReceiveCustomMessageDelegate;
		}

		public void onReceiveCustomMessage(CustomMessage message)
		{
			if (m_onReceiveCustomMessageDelegate != null)
			{
				m_onReceiveCustomMessageDelegate(message);
			}
		}
	}
}
