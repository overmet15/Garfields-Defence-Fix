namespace Muneris
{
	public interface IDetectGeoIpLocationChangeCallback : ICallback
	{
		void onDetectGeoIpLocationChange(GeoIpLocation location);
	}
}
