namespace Muneris.Messaging
{
	public class IReceiveAlertAcknowledgmentCallbackDelegates : ICallback, IReceiveAlertAcknowledgmentCallback
	{
		public delegate void onReceiveAlertAcknowledgmentDelegate(AlertAcknowledgment acknowledgment);

		private onReceiveAlertAcknowledgmentDelegate m_onReceiveAlertAcknowledgmentDelegate;

		public IReceiveAlertAcknowledgmentCallbackDelegates(onReceiveAlertAcknowledgmentDelegate _onReceiveAlertAcknowledgmentDelegate)
		{
			m_onReceiveAlertAcknowledgmentDelegate = _onReceiveAlertAcknowledgmentDelegate;
		}

		public void onReceiveAlertAcknowledgment(AlertAcknowledgment acknowledgment)
		{
			if (m_onReceiveAlertAcknowledgmentDelegate != null)
			{
				m_onReceiveAlertAcknowledgmentDelegate(acknowledgment);
			}
		}
	}
}
