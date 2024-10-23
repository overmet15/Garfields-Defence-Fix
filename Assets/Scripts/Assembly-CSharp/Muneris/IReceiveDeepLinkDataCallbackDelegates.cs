namespace Muneris
{
	public class IReceiveDeepLinkDataCallbackDelegates : ICallback, IReceiveDeepLinkDataCallback
	{
		public delegate void onDeepLinkDataReceiveDelegate(JsonObject data);

		private onDeepLinkDataReceiveDelegate m_onDeepLinkDataReceiveDelegate;

		public IReceiveDeepLinkDataCallbackDelegates(onDeepLinkDataReceiveDelegate _onDeepLinkDataReceiveDelegate)
		{
			m_onDeepLinkDataReceiveDelegate = _onDeepLinkDataReceiveDelegate;
		}

		public void onDeepLinkDataReceive(JsonObject data)
		{
			if (m_onDeepLinkDataReceiveDelegate != null)
			{
				m_onDeepLinkDataReceiveDelegate(data);
			}
		}
	}
}
