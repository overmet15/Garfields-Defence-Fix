using System.Collections.Generic;

namespace Muneris.Virtualgood
{
	public class IRestoreVirtualGoodsCallbackDelegates : ICallback, IRestoreVirtualGoodsCallback
	{
		public delegate void onRestoreVirtualGoodsDelegate(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception);

		private onRestoreVirtualGoodsDelegate m_onRestoreVirtualGoodsDelegate;

		public IRestoreVirtualGoodsCallbackDelegates(onRestoreVirtualGoodsDelegate _onRestoreVirtualGoodsDelegate)
		{
			m_onRestoreVirtualGoodsDelegate = _onRestoreVirtualGoodsDelegate;
		}

		public void onRestoreVirtualGoods(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onRestoreVirtualGoodsDelegate != null)
			{
				m_onRestoreVirtualGoodsDelegate(virtualGoods, callbackContext, exception);
			}
		}
	}
}
