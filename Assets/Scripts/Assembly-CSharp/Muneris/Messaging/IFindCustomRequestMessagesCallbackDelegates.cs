using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindCustomRequestMessagesCallbackDelegates : ICallback, IFindCustomRequestMessagesCallback
	{
		public delegate void onFindCustomRequestMessagesDelegate(List<CustomRequestMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindCustomRequestMessagesDelegate m_onFindCustomRequestMessagesDelegate;

		public IFindCustomRequestMessagesCallbackDelegates(onFindCustomRequestMessagesDelegate _onFindCustomRequestMessagesDelegate)
		{
			m_onFindCustomRequestMessagesDelegate = _onFindCustomRequestMessagesDelegate;
		}

		public void onFindCustomRequestMessages(List<CustomRequestMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindCustomRequestMessagesDelegate != null)
			{
				m_onFindCustomRequestMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
