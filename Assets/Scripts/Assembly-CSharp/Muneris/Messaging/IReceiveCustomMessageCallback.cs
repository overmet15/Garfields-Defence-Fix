namespace Muneris.Messaging
{
	public interface IReceiveCustomMessageCallback : ICallback
	{
		void onReceiveCustomMessage(CustomMessage message);
	}
}
