namespace Muneris
{
	public class IDetectNetworkConnectivityChangeCallbackDelegates : ICallback, IDetectNetworkConnectivityChangeCallback
	{
		public delegate void onDetectNetworkConnectivityChangeDelegate(bool online);

		private onDetectNetworkConnectivityChangeDelegate m_onDetectNetworkConnectivityChangeDelegate;

		public IDetectNetworkConnectivityChangeCallbackDelegates(onDetectNetworkConnectivityChangeDelegate _onDetectNetworkConnectivityChangeDelegate)
		{
			m_onDetectNetworkConnectivityChangeDelegate = _onDetectNetworkConnectivityChangeDelegate;
		}

		public void onDetectNetworkConnectivityChange(bool online)
		{
			if (m_onDetectNetworkConnectivityChangeDelegate != null)
			{
				m_onDetectNetworkConnectivityChangeDelegate(online);
			}
		}
	}
}
