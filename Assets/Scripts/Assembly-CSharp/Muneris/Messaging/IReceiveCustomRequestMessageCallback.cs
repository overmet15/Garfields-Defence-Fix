namespace Muneris.Messaging
{
	public interface IReceiveCustomRequestMessageCallback : ICallback
	{
		void onReceiveCustomRequestMessage(CustomRequestMessage message);
	}
}
