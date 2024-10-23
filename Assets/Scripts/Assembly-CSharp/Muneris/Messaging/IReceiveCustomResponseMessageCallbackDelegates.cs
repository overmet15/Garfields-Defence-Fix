namespace Muneris.Messaging
{
	public class IReceiveCustomResponseMessageCallbackDelegates : ICallback, IReceiveCustomResponseMessageCallback
	{
		public delegate void onReceiveCustomResponseMessageDelegate(CustomResponseMessage message);

		private onReceiveCustomResponseMessageDelegate m_onReceiveCustomResponseMessageDelegate;

		public IReceiveCustomResponseMessageCallbackDelegates(onReceiveCustomResponseMessageDelegate _onReceiveCustomResponseMessageDelegate)
		{
			m_onReceiveCustomResponseMessageDelegate = _onReceiveCustomResponseMessageDelegate;
		}

		public void onReceiveCustomResponseMessage(CustomResponseMessage message)
		{
			if (m_onReceiveCustomResponseMessageDelegate != null)
			{
				m_onReceiveCustomResponseMessageDelegate(message);
			}
		}
	}
}
