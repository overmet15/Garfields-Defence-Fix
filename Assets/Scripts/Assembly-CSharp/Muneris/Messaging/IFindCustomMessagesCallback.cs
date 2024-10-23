using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindCustomMessagesCallback : ICallback
	{
		void onFindCustomMessages(List<CustomMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
