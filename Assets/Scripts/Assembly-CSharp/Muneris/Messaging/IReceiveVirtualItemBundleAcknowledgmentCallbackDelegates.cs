namespace Muneris.Messaging
{
	public class IReceiveVirtualItemBundleAcknowledgmentCallbackDelegates : ICallback, IReceiveVirtualItemBundleAcknowledgmentCallback
	{
		public delegate void onReceiveVirtualItemBundleAcknowledgmentDelegate(VirtualItemBundleAcknowledgment acknowledgment);

		private onReceiveVirtualItemBundleAcknowledgmentDelegate m_onReceiveVirtualItemBundleAcknowledgmentDelegate;

		public IReceiveVirtualItemBundleAcknowledgmentCallbackDelegates(onReceiveVirtualItemBundleAcknowledgmentDelegate _onReceiveVirtualItemBundleAcknowledgmentDelegate)
		{
			m_onReceiveVirtualItemBundleAcknowledgmentDelegate = _onReceiveVirtualItemBundleAcknowledgmentDelegate;
		}

		public void onReceiveVirtualItemBundleAcknowledgment(VirtualItemBundleAcknowledgment acknowledgment)
		{
			if (m_onReceiveVirtualItemBundleAcknowledgmentDelegate != null)
			{
				m_onReceiveVirtualItemBundleAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
