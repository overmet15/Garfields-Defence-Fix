namespace Muneris.Messaging
{
	public class IReceiveVirtualItemBundleMessageCallbackDelegates : ICallback, IReceiveVirtualItemBundleMessageCallback
	{
		public delegate void onReceiveVirtualItemBundleMessageDelegate(VirtualItemBundleMessage message);

		private onReceiveVirtualItemBundleMessageDelegate m_onReceiveVirtualItemBundleMessageDelegate;

		public IReceiveVirtualItemBundleMessageCallbackDelegates(onReceiveVirtualItemBundleMessageDelegate _onReceiveVirtualItemBundleMessageDelegate)
		{
			m_onReceiveVirtualItemBundleMessageDelegate = _onReceiveVirtualItemBundleMessageDelegate;
		}

		public void onReceiveVirtualItemBundleMessage(VirtualItemBundleMessage message)
		{
			if (m_onReceiveVirtualItemBundleMessageDelegate != null)
			{
				m_onReceiveVirtualItemBundleMessageDelegate(message);
			}
		}
	}
}
