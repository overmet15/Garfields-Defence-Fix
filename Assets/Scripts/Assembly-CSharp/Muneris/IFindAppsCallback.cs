using System.Collections.Generic;

namespace Muneris
{
	public interface IFindAppsCallback : ICallback
	{
		void onFindApps(List<IApp> apps, FindAppsCommand more, CallbackContext callbackContext, MunerisException munerisException);
	}
}
