namespace Muneris.Messaging
{
	public interface IReceiveVirtualItemBundleMessageCallback : ICallback
	{
		void onReceiveVirtualItemBundleMessage(VirtualItemBundleMessage message);
	}
}
