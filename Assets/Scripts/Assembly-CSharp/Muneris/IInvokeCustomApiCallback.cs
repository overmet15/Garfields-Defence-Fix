namespace Muneris
{
	public interface IInvokeCustomApiCallback : ICallback
	{
		void onInvokeCustomApi(string apiMethod, JsonObject apiParams, CallbackContext callbackContext, MunerisException munerisException);
	}
}
