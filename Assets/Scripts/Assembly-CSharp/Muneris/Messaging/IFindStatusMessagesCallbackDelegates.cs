using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindStatusMessagesCallbackDelegates : ICallback, IFindStatusMessagesCallback
	{
		public delegate void onFindStatusMessagesDelegate(List<StatusMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindStatusMessagesDelegate m_onFindStatusMessagesDelegate;

		public IFindStatusMessagesCallbackDelegates(onFindStatusMessagesDelegate _onFindStatusMessagesDelegate)
		{
			m_onFindStatusMessagesDelegate = _onFindStatusMessagesDelegate;
		}

		public void onFindStatusMessages(List<StatusMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindStatusMessagesDelegate != null)
			{
				m_onFindStatusMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
