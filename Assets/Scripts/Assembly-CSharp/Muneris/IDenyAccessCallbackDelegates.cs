namespace Muneris
{
	public class IDenyAccessCallbackDelegates : ICallback, IDenyAccessCallback
	{
		public delegate void onDenyAccessDelegate(MunerisException exception);

		private onDenyAccessDelegate m_onDenyAccessDelegate;

		public IDenyAccessCallbackDelegates(onDenyAccessDelegate _onDenyAccessDelegate)
		{
			m_onDenyAccessDelegate = _onDenyAccessDelegate;
		}

		public void onDenyAccess(MunerisException exception)
		{
			if (m_onDenyAccessDelegate != null)
			{
				m_onDenyAccessDelegate(exception);
			}
		}
	}
}
