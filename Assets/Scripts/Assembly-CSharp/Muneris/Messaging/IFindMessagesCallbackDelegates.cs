using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindMessagesCallbackDelegates : ICallback, IFindMessagesCallback
	{
		public delegate void onFindMessagesDelegate(List<Message> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindMessagesDelegate m_onFindMessagesDelegate;

		public IFindMessagesCallbackDelegates(onFindMessagesDelegate _onFindMessagesDelegate)
		{
			m_onFindMessagesDelegate = _onFindMessagesDelegate;
		}

		public void onFindMessages(List<Message> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindMessagesDelegate != null)
			{
				m_onFindMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
