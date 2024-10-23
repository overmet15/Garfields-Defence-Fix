using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindCustomResponseMessagesCallbackDelegates : ICallback, IFindCustomResponseMessagesCallback
	{
		public delegate void onFindCustomResponseMessagesDelegate(List<CustomResponseMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindCustomResponseMessagesDelegate m_onFindCustomResponseMessagesDelegate;

		public IFindCustomResponseMessagesCallbackDelegates(onFindCustomResponseMessagesDelegate _onFindCustomResponseMessagesDelegate)
		{
			m_onFindCustomResponseMessagesDelegate = _onFindCustomResponseMessagesDelegate;
		}

		public void onFindCustomResponseMessages(List<CustomResponseMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindCustomResponseMessagesDelegate != null)
			{
				m_onFindCustomResponseMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
