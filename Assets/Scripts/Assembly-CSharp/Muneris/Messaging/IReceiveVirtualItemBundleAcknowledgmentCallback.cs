namespace Muneris.Messaging
{
	public interface IReceiveVirtualItemBundleAcknowledgmentCallback : ICallback
	{
		void onReceiveVirtualItemBundleAcknowledgment(VirtualItemBundleAcknowledgment acknowledgment);
	}
}
