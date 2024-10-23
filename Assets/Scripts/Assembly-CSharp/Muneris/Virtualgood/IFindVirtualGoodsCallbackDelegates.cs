using System.Collections.Generic;

namespace Muneris.Virtualgood
{
	public class IFindVirtualGoodsCallbackDelegates : ICallback, IFindVirtualGoodsCallback
	{
		public delegate void onFindVirtualGoodsDelegate(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception);

		private onFindVirtualGoodsDelegate m_onFindVirtualGoodsDelegate;

		public IFindVirtualGoodsCallbackDelegates(onFindVirtualGoodsDelegate _onFindVirtualGoodsDelegate)
		{
			m_onFindVirtualGoodsDelegate = _onFindVirtualGoodsDelegate;
		}

		public void onFindVirtualGoods(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onFindVirtualGoodsDelegate != null)
			{
				m_onFindVirtualGoodsDelegate(virtualGoods, callbackContext, exception);
			}
		}
	}
}
