using System.Collections.Generic;

namespace Muneris.Messaging
{
	public interface IFindChannelsCallback : ICallback
	{
		void onFindChannels(List<Channel> channels, CallbackContext callbackContext, MunerisException exception);
	}
}
