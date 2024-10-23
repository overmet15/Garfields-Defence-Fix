using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindStatusMessagesCallback : ICallback
	{
		void onFindStatusMessages(List<StatusMessage> messages, CallbackContext callbackContext, MunerisException exception);
	}
}
