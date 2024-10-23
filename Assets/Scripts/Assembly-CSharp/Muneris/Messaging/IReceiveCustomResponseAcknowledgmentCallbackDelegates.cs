namespace Muneris.Messaging
{
	public class IReceiveCustomResponseAcknowledgmentCallbackDelegates : ICallback, IReceiveCustomResponseAcknowledgmentCallback
	{
		public delegate void onReceiveCustomResponseAcknowledgmentDelegate(CustomResponseAcknowledgment acknowledgment);

		private onReceiveCustomResponseAcknowledgmentDelegate m_onReceiveCustomResponseAcknowledgmentDelegate;

		public IReceiveCustomResponseAcknowledgmentCallbackDelegates(onReceiveCustomResponseAcknowledgmentDelegate _onReceiveCustomResponseAcknowledgmentDelegate)
		{
			m_onReceiveCustomResponseAcknowledgmentDelegate = _onReceiveCustomResponseAcknowledgmentDelegate;
		}

		public void onReceiveCustomResponseAcknowledgment(CustomResponseAcknowledgment acknowledgment)
		{
			if (m_onReceiveCustomResponseAcknowledgmentDelegate != null)
			{
				m_onReceiveCustomResponseAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
