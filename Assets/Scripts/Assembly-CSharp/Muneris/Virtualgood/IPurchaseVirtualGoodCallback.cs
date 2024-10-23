namespace Muneris.Virtualgood
{
	public interface IPurchaseVirtualGoodCallback : ICallback
	{
		void onPurchaseVirtualGood(VirtualGood virtualGood, CallbackContext callbackContext, MunerisException exception);
	}
}
