using System.Collections.Generic;

namespace Muneris.Virtualgood
{
	public interface IRestoreVirtualGoodsCallback : ICallback
	{
		void onRestoreVirtualGoods(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception);
	}
}
