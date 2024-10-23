using System.Collections.Generic;

namespace Muneris.Virtualitem
{
	public interface IFindVirtualItemsCallback : ICallback
	{
		void onFindVirtualItems(List<VirtualItem> virtualItems, CallbackContext callbackContext, MunerisException exception);
	}
}
