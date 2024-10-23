using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindMessagesCallback : ICallback
	{
		void onFindMessages(List<Message> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
