namespace Muneris.Messaging
{
	public interface IReceiveStatusMessageCallback : ICallback
	{
		void onReceiveStatusMessage(StatusMessage message);
	}
}
