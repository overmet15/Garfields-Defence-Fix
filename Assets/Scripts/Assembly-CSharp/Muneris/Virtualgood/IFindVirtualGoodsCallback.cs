using System.Collections.Generic;

namespace Muneris.Virtualgood
{
	public interface IFindVirtualGoodsCallback : ICallback
	{
		void onFindVirtualGoods(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception);
	}
}
