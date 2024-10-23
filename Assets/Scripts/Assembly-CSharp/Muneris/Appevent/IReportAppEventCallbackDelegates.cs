using System.Collections.Generic;

namespace Muneris.Appevent
{
	public class IReportAppEventCallbackDelegates : IReportAppEventCallback, ICallback
	{
		public delegate void onReportAppEventDelegate(string eventName, Dictionary<string, string> parameters, CallbackContext callbackContext);

		private onReportAppEventDelegate m_onReportAppEventDelegate;

		public IReportAppEventCallbackDelegates(onReportAppEventDelegate _onReportAppEventDelegate)
		{
			m_onReportAppEventDelegate = _onReportAppEventDelegate;
		}

		public void onReportAppEvent(string eventName, Dictionary<string, string> parameters, CallbackContext callbackContext)
		{
			if (m_onReportAppEventDelegate != null)
			{
				m_onReportAppEventDelegate(eventName, parameters, callbackContext);
			}
		}
	}
}
