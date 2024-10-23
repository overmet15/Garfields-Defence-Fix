namespace Muneris.Messaging
{
	public class IReceiveCustomRequestAcknowledgmentCallbackDelegates : ICallback, IReceiveCustomRequestAcknowledgmentCallback
	{
		public delegate void onReceiveCustomRequestAcknowledgmentDelegate(CustomRequestAcknowledgment acknowledgment);

		private onReceiveCustomRequestAcknowledgmentDelegate m_onReceiveCustomRequestAcknowledgmentDelegate;

		public IReceiveCustomRequestAcknowledgmentCallbackDelegates(onReceiveCustomRequestAcknowledgmentDelegate _onReceiveCustomRequestAcknowledgmentDelegate)
		{
			m_onReceiveCustomRequestAcknowledgmentDelegate = _onReceiveCustomRequestAcknowledgmentDelegate;
		}

		public void onReceiveCustomRequestAcknowledgment(CustomRequestAcknowledgment acknowledgment)
		{
			if (m_onReceiveCustomRequestAcknowledgmentDelegate != null)
			{
				m_onReceiveCustomRequestAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
