namespace Muneris.Messaging
{
	public class ISendVirtualItemBundleMessageCallbackDelegates : ICallback, ISendVirtualItemBundleMessageCallback
	{
		public delegate void onSendVirtualItemBundleMessageDelegate(VirtualItemBundleMessage message, VirtualItemBundleMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);

		private onSendVirtualItemBundleMessageDelegate m_onSendVirtualItemBundleMessageDelegate;

		public ISendVirtualItemBundleMessageCallbackDelegates(onSendVirtualItemBundleMessageDelegate _onSendVirtualItemBundleMessageDelegate)
		{
			m_onSendVirtualItemBundleMessageDelegate = _onSendVirtualItemBundleMessageDelegate;
		}

		public void onSendVirtualItemBundleMessage(VirtualItemBundleMessage message, VirtualItemBundleMessage outboxMessage, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSendVirtualItemBundleMessageDelegate != null)
			{
				m_onSendVirtualItemBundleMessageDelegate(message, outboxMessage, callbackContext, exception);
			}
		}
	}
}
