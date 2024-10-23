using System.Collections.Generic;

namespace Muneris.Appevent
{
	public interface IReportAppEventCallback : ICallback
	{
		void onReportAppEvent(string eventName, Dictionary<string, string> parameters, CallbackContext callbackContext);
	}
}
