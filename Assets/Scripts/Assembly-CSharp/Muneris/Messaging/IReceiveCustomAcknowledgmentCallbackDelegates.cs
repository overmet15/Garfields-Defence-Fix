namespace Muneris.Messaging
{
	public class IReceiveCustomAcknowledgmentCallbackDelegates : ICallback, IReceiveCustomAcknowledgmentCallback
	{
		public delegate void onReceiveCustomAcknowledgmentDelegate(CustomAcknowledgment acknowledgment);

		private onReceiveCustomAcknowledgmentDelegate m_onReceiveCustomAcknowledgmentDelegate;

		public IReceiveCustomAcknowledgmentCallbackDelegates(onReceiveCustomAcknowledgmentDelegate _onReceiveCustomAcknowledgmentDelegate)
		{
			m_onReceiveCustomAcknowledgmentDelegate = _onReceiveCustomAcknowledgmentDelegate;
		}

		public void onReceiveCustomAcknowledgment(CustomAcknowledgment acknowledgment)
		{
			if (m_onReceiveCustomAcknowledgmentDelegate != null)
			{
				m_onReceiveCustomAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
