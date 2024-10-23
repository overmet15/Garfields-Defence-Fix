namespace Muneris.Virtualgood
{
	public class IPurchaseVirtualGoodCallbackDelegates : ICallback, IPurchaseVirtualGoodCallback
	{
		public delegate void onPurchaseVirtualGoodDelegate(VirtualGood virtualGood, CallbackContext callbackContext, MunerisException exception);

		private onPurchaseVirtualGoodDelegate m_onPurchaseVirtualGoodDelegate;

		public IPurchaseVirtualGoodCallbackDelegates(onPurchaseVirtualGoodDelegate _onPurchaseVirtualGoodDelegate)
		{
			m_onPurchaseVirtualGoodDelegate = _onPurchaseVirtualGoodDelegate;
		}

		public void onPurchaseVirtualGood(VirtualGood virtualGood, CallbackContext callbackContext, MunerisException exception)
		{
			if (m_onPurchaseVirtualGoodDelegate != null)
			{
				m_onPurchaseVirtualGoodDelegate(virtualGood, callbackContext, exception);
			}
		}
	}
}
