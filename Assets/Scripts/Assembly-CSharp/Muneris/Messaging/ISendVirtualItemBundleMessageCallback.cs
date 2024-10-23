namespace Muneris.Messaging
{
	public interface ISendVirtualItemBundleMessageCallback : ICallback
	{
		void onSendVirtualItemBundleMessage(VirtualItemBundleMessage message, VirtualItemBundleMessage outboxMessage, CallbackContext callbackContext, MunerisException exception);
	}
}
