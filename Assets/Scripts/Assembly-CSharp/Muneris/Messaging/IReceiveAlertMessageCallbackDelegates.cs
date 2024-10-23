namespace Muneris.Messaging
{
	public class IReceiveAlertMessageCallbackDelegates : ICallback, IReceiveAlertMessageCallback
	{
		public delegate void onReceiveAlertMessageDelegate(AlertMessage message);

		private onReceiveAlertMessageDelegate m_onReceiveAlertMessageDelegate;

		public IReceiveAlertMessageCallbackDelegates(onReceiveAlertMessageDelegate _onReceiveAlertMessageDelegate)
		{
			m_onReceiveAlertMessageDelegate = _onReceiveAlertMessageDelegate;
		}

		public void onReceiveAlertMessage(AlertMessage message)
		{
			if (m_onReceiveAlertMessageDelegate != null)
			{
				m_onReceiveAlertMessageDelegate(message);
			}
		}
	}
}
