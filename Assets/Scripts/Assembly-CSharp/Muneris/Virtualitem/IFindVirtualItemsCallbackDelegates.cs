using System.Collections.Generic;

namespace Muneris.Virtualitem
{
	public class IFindVirtualItemsCallbackDelegates : ICallback, IFindVirtualItemsCallback
	{
		public delegate void onFindVirtualItemsDelegate(List<VirtualItem> virtualItems, CallbackContext callbackContext, MunerisException exception);

		private onFindVirtualItemsDelegate m_onFindVirtualItemsDelegate;

		public IFindVirtualItemsCallbackDelegates(onFindVirtualItemsDelegate _onFindVirtualItemsDelegate)
		{
			m_onFindVirtualItemsDelegate = _onFindVirtualItemsDelegate;
		}

		public void onFindVirtualItems(List<VirtualItem> virtualItems, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindVirtualItemsDelegate != null)
			{
				m_onFindVirtualItemsDelegate(virtualItems, callbackContext, exception);
			}
		}
	}
}
