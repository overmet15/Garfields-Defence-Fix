using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindChatMessagesCallbackDelegates : ICallback, IFindChatMessagesCallback
	{
		public delegate void onFindChatMessagesDelegate(List<ChatMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindChatMessagesDelegate m_onFindChatMessagesDelegate;

		public IFindChatMessagesCallbackDelegates(onFindChatMessagesDelegate _onFindChatMessagesDelegate)
		{
			m_onFindChatMessagesDelegate = _onFindChatMessagesDelegate;
		}

		public void onFindChatMessages(List<ChatMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindChatMessagesDelegate != null)
			{
				m_onFindChatMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
