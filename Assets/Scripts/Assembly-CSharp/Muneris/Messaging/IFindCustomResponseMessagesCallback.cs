using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindCustomResponseMessagesCallback : ICallback
	{
		void onFindCustomResponseMessages(List<CustomResponseMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
