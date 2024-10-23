using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindAlertMessagesCallback : ICallback
	{
		void onFindAlertMessages(List<AlertMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
