namespace Muneris.Messaging
{
	public class IReceiveStatusMessageCallbackDelegates : ICallback, IReceiveStatusMessageCallback
	{
		public delegate void onReceiveStatusMessageDelegate(StatusMessage message);

		private onReceiveStatusMessageDelegate m_onReceiveStatusMessageDelegate;

		public IReceiveStatusMessageCallbackDelegates(onReceiveStatusMessageDelegate _onReceiveStatusMessageDelegate)
		{
			m_onReceiveStatusMessageDelegate = _onReceiveStatusMessageDelegate;
		}

		public void onReceiveStatusMessage(StatusMessage message)
		{
			if (m_onReceiveStatusMessageDelegate != null)
			{
				m_onReceiveStatusMessageDelegate(message);
			}
		}
	}
}
