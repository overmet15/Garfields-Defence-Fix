namespace Muneris.Messaging
{
	public class IUnsubscribeChannelCallbackDelegates : ICallback, IUnsubscribeChannelCallback
	{
		public delegate void onUnsubscribeChannelDelegate(CallbackContext callbackContext, MunerisException exception);

		private onUnsubscribeChannelDelegate m_onUnsubscribeChannelDelegate;

		public IUnsubscribeChannelCallbackDelegates(onUnsubscribeChannelDelegate _onUnsubscribeChannelDelegate)
		{
			m_onUnsubscribeChannelDelegate = _onUnsubscribeChannelDelegate;
		}

		public void onUnsubscribeChannel(CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onUnsubscribeChannelDelegate != null)
			{
				m_onUnsubscribeChannelDelegate(callbackContext, exception);
			}
		}
	}
}
