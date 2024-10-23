using System.Collections.Generic;

namespace Muneris.Messaging
{
	public class IFindVirtualItemBundleMessagesCallbackDelegates : ICallback, IFindVirtualItemBundleMessagesCallback
	{
		public delegate void onFindVirtualItemBundleMessagesDelegate(List<VirtualItemBundleMessage> messages, CallbackContext callbackContext, MunerisException exception);

		private onFindVirtualItemBundleMessagesDelegate m_onFindVirtualItemBundleMessagesDelegate;

		public IFindVirtualItemBundleMessagesCallbackDelegates(onFindVirtualItemBundleMessagesDelegate _onFindVirtualItemBundleMessagesDelegate)
		{
			m_onFindVirtualItemBundleMessagesDelegate = _onFindVirtualItemBundleMessagesDelegate;
		}

		public void onFindVirtualItemBundleMessages(List<VirtualItemBundleMessage> messages, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindVirtualItemBundleMessagesDelegate != null)
			{
				m_onFindVirtualItemBundleMessagesDelegate(messages, callbackContext, exception);
			}
		}
	}
}
