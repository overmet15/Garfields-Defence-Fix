using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindChannelsCallbackDelegates : ICallback, IFindChannelsCallback
	{
		public delegate void onFindChannelsDelegate(List<Channel> channels, CallbackContext callbackContext, MunerisException exception);

		private onFindChannelsDelegate m_onFindChannelsDelegate;

		public IFindChannelsCallbackDelegates(onFindChannelsDelegate _onFindChannelsDelegate)
		{
			m_onFindChannelsDelegate = _onFindChannelsDelegate;
		}

		public void onFindChannels(List<Channel> channels, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindChannelsDelegate != null)
			{
				m_onFindChannelsDelegate(channels, callbackContext, exception);
			}
		}
	}
}
