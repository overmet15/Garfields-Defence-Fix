namespace Muneris
{
	public interface IReceiveDeepLinkDataCallback : ICallback
	{
		void onDeepLinkDataReceive(JsonObject data);
	}
}
