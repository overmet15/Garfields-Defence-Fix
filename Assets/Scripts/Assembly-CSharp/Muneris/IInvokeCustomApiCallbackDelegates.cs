namespace Muneris
{
	public class IInvokeCustomApiCallbackDelegates : ICallback, IInvokeCustomApiCallback
	{
		public delegate void onInvokeCustomApiDelegate(string apiMethod, JsonObject apiParams, CallbackContext callbackContext, MunerisException munerisException);

		private onInvokeCustomApiDelegate m_onInvokeCustomApiDelegate;

		public IInvokeCustomApiCallbackDelegates(onInvokeCustomApiDelegate _onInvokeCustomApiDelegate)
		{
			m_onInvokeCustomApiDelegate = _onInvokeCustomApiDelegate;
		}

		public void onInvokeCustomApi(string apiMethod, JsonObject apiParams, CallbackContext callbackContext, MunerisException munerisException)
		{
			if (m_onInvokeCustomApiDelegate != null)
			{
				m_onInvokeCustomApiDelegate(apiMethod, apiParams, callbackContext, munerisException);
			}
		}
	}
}
