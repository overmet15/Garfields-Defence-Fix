namespace Muneris.Messaging
{
	public interface IReceiveCustomResponseMessageCallback : ICallback
	{
		void onReceiveCustomResponseMessage(CustomResponseMessage message);
	}
}
