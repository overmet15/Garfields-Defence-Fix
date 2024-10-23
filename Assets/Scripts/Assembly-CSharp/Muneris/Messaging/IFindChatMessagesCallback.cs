using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindChatMessagesCallback : ICallback
	{
		void onFindChatMessages(List<ChatMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
