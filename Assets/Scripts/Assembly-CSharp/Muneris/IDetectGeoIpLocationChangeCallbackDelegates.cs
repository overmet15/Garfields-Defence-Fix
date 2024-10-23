namespace Muneris
{
	public class IDetectGeoIpLocationChangeCallbackDelegates : ICallback, IDetectGeoIpLocationChangeCallback
	{
		public delegate void onDetectGeoIpLocationChangeDelegate(GeoIpLocation location);

		private onDetectGeoIpLocationChangeDelegate m_onDetectGeoIpLocationChangeDelegate;

		public IDetectGeoIpLocationChangeCallbackDelegates(onDetectGeoIpLocationChangeDelegate _onDetectGeoIpLocationChangeDelegate)
		{
			m_onDetectGeoIpLocationChangeDelegate = _onDetectGeoIpLocationChangeDelegate;
		}

		public void onDetectGeoIpLocationChange(GeoIpLocation location)
		{
			if (m_onDetectGeoIpLocationChangeDelegate != null)
			{
				m_onDetectGeoIpLocationChangeDelegate(location);
			}
		}
	}
}
