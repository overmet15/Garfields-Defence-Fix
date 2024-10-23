namespace Muneris
{
	public interface IDetectNetworkConnectivityChangeCallback : ICallback
	{
		void onDetectNetworkConnectivityChange(bool online);
	}
}
