namespace Muneris.Messaging
{
	public class ISubscribeChannelCallbackDelegates : ICallback, ISubscribeChannelCallback
	{
		public delegate void onSubscribeChannelDelegate(CallbackContext callbackContext, MunerisException exception);

		private onSubscribeChannelDelegate m_onSubscribeChannelDelegate;

		public ISubscribeChannelCallbackDelegates(onSubscribeChannelDelegate _onSubscribeChannelDelegate)
		{
			m_onSubscribeChannelDelegate = _onSubscribeChannelDelegate;
		}

		public void onSubscribeChannel(CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onSubscribeChannelDelegate != null)
			{
				m_onSubscribeChannelDelegate(callbackContext, exception);
			}
		}
	}
}
