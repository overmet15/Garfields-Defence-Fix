using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindVirtualItemBundleMessagesCallback : ICallback
	{
		void onFindVirtualItemBundleMessages(List<VirtualItemBundleMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
