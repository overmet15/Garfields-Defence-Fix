namespace Muneris.Messaging
{
	public class ISendVirtualItemBundleAcknowledgmentCallbackDelegates : ICallback, ISendVirtualItemBundleAcknowledgmentCallback
	{
		public delegate void onSendVirtualItemBundleAcknowledgmentDelegate(VirtualItemBundleAcknowledgment acknowledgment, VirtualItemBundleAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);

		private onSendVirtualItemBundleAcknowledgmentDelegate m_onSendVirtualItemBundleAcknowledgmentDelegate;

		public ISendVirtualItemBundleAcknowledgmentCallbackDelegates(onSendVirtualItemBundleAcknowledgmentDelegate _onSendVirtualItemBundleAcknowledgmentDelegate)
		{
			m_onSendVirtualItemBundleAcknowledgmentDelegate = _onSendVirtualItemBundleAcknowledgmentDelegate;
		}

		public void onSendVirtualItemBundleAcknowledgment(VirtualItemBundleAcknowledgment acknowledgment, VirtualItemBundleAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendVirtualItemBundleAcknowledgmentDelegate != null)
			{
				m_onSendVirtualItemBundleAcknowledgmentDelegate(acknowledgment, outboxAcknowledgment, callbackContext, exception);
			}
		}
	}
}
