namespace Muneris.Messaging
{
	public interface ISendVirtualItemBundleAcknowledgmentCallback : ICallback
	{
		void onSendVirtualItemBundleAcknowledgment(VirtualItemBundleAcknowledgment acknowledgment, VirtualItemBundleAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception);
	}
}
