using System.Collections.Generic;

namespace Muneris
{
	public class IFindAppsCallbackDelegates : ICallback, IFindAppsCallback
	{
		public delegate void onFindAppsDelegate(List<IApp> apps, FindAppsCommand more, CallbackContext callbackContext, MunerisException munerisException);

		private onFindAppsDelegate m_onFindAppsDelegate;

		public IFindAppsCallbackDelegates(onFindAppsDelegate _onFindAppsDelegate)
		{
			m_onFindAppsDelegate = _onFindAppsDelegate;
		}

		public void onFindApps(List<IApp> apps, FindAppsCommand more, CallbackContext callbackContext, MunerisException munerisException)
		{
			if (m_onFindAppsDelegate != null)
			{
				m_onFindAppsDelegate(apps, more, callbackContext, munerisException);
			}
		}
	}
}
