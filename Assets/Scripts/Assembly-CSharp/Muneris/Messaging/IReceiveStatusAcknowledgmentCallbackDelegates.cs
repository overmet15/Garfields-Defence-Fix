namespace Muneris.Messaging
{
	public class IReceiveStatusAcknowledgmentCallbackDelegates : ICallback, IReceiveStatusAcknowledgmentCallback
	{
		public delegate void onReceiveStatusAcknowledgmentDelegate(StatusAcknowledgment acknowledgment);

		private onReceiveStatusAcknowledgmentDelegate m_onReceiveStatusAcknowledgmentDelegate;

		public IReceiveStatusAcknowledgmentCallbackDelegates(onReceiveStatusAcknowledgmentDelegate _onReceiveStatusAcknowledgmentDelegate)
		{
			m_onReceiveStatusAcknowledgmentDelegate = _onReceiveStatusAcknowledgmentDelegate;
		}

		public void onReceiveStatusAcknowledgment(StatusAcknowledgment acknowledgment)
		{
			if (m_onReceiveStatusAcknowledgmentDelegate != null)
			{
				m_onReceiveStatusAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
