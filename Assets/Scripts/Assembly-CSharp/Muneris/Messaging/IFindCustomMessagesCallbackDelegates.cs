using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindCustomMessagesCallbackDelegates : ICallback, IFindCustomMessagesCallback
	{
		public delegate void onFindCustomMessagesDelegate(List<CustomMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindCustomMessagesDelegate m_onFindCustomMessagesDelegate;

		public IFindCustomMessagesCallbackDelegates(onFindCustomMessagesDelegate _onFindCustomMessagesDelegate)
		{
			m_onFindCustomMessagesDelegate = _onFindCustomMessagesDelegate;
		}

		public void onFindCustomMessages(List<CustomMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindCustomMessagesDelegate != null)
			{
				m_onFindCustomMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
