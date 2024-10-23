namespace Muneris.Messaging
{
	public interface IUnsubscribeChannelCallback : ICallback
	{
		void onUnsubscribeChannel(CallbackContext callbackContext, MunerisException exception);
	}
}
