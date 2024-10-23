using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindAlertMessagesCallbackDelegates : ICallback, IFindAlertMessagesCallback
	{
		public delegate void onFindAlertMessagesDelegate(List<AlertMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindAlertMessagesDelegate m_onFindAlertMessagesDelegate;

		public IFindAlertMessagesCallbackDelegates(onFindAlertMessagesDelegate _onFindAlertMessagesDelegate)
		{
			m_onFindAlertMessagesDelegate = _onFindAlertMessagesDelegate;
		}

		public void onFindAlertMessages(List<AlertMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindAlertMessagesDelegate != null)
			{
				m_onFindAlertMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
