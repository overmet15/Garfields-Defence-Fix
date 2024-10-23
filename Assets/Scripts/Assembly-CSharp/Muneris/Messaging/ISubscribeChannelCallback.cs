namespace Muneris.Messaging
{
	public interface ISubscribeChannelCallback : ICallback
	{
		void onSubscribeChannel(CallbackContext callbackContext, MunerisException exception);
	}
}
