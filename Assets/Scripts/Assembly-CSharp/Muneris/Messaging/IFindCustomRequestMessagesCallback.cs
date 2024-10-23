using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindCustomRequestMessagesCallback : ICallback
	{
		void onFindCustomRequestMessages(List<CustomRequestMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
