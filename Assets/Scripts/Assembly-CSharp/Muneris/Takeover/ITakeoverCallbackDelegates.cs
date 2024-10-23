namespace Muneris.Takeover
{
	public class ITakeoverCallbackDelegates : ICallback, ITakeoverCallback
	{
		public delegate void onStartTakeoverRequestDelegate(TakeoverEvent takeoverEvent);

		public delegate void onLoadTakeoverDelegate(TakeoverEvent takeoverEvent, TakeoverResponse takeoverResponse);

		public delegate void onDismissTakeoverDelegate(TakeoverEvent takeoverEvent);

		public delegate void onFailTakeoverDelegate(TakeoverEvent takeoverEvent, MunerisException exception);

		public delegate void onEndTakeoverRequestDelegate(TakeoverEvent takeoverEvent);

		private onStartTakeoverRequestDelegate m_onStartTakeoverRequestDelegate;

		private onLoadTakeoverDelegate m_onLoadTakeoverDelegate;

		private onDismissTakeoverDelegate m_onDismissTakeoverDelegate;

		private onFailTakeoverDelegate m_onFailTakeoverDelegate;

		private onEndTakeoverRequestDelegate m_onEndTakeoverRequestDelegate;

		public ITakeoverCallbackDelegates(onStartTakeoverRequestDelegate _onStartTakeoverRequestDelegate, onLoadTakeoverDelegate _onLoadTakeoverDelegate, onDismissTakeoverDelegate _onDismissTakeoverDelegate, onFailTakeoverDelegate _onFailTakeoverDelegate, onEndTakeoverRequestDelegate _onEndTakeoverRequestDelegate)
		{
			m_onStartTakeoverRequestDelegate = _onStartTakeoverRequestDelegate;
			m_onLoadTakeoverDelegate = _onLoadTakeoverDelegate;
			m_onDismissTakeoverDelegate = _onDismissTakeoverDelegate;
			m_onFailTakeoverDelegate = _onFailTakeoverDelegate;
			m_onEndTakeoverRequestDelegate = _onEndTakeoverRequestDelegate;
		}

		public void onStartTakeoverRequest(TakeoverEvent takeoverEvent)
		{
			if (m_onStartTakeoverRequestDelegate != null)
			{
				m_onStartTakeoverRequestDelegate(takeoverEvent);
			}
		}

		public void onLoadTakeover(TakeoverEvent takeoverEvent, TakeoverResponse takeoverResponse)
		{
			if (m_onLoadTakeoverDelegate != null)
			{
				m_onLoadTakeoverDelegate(takeoverEvent, takeoverResponse);
			}
		}

		public void onDismissTakeover(TakeoverEvent takeoverEvent)
		{
			if (m_onDismissTakeoverDelegate != null)
			{
				m_onDismissTakeoverDelegate(takeoverEvent);
			}
		}

		public void onFailTakeover(TakeoverEvent takeoverEvent, MunerisException exception)
		{
			if (m_onFailTakeoverDelegate != null)
			{
				m_onFailTakeoverDelegate(takeoverEvent, exception);
			}
		}

		public void onEndTakeoverRequest(TakeoverEvent takeoverEvent)
		{
			if (m_onEndTakeoverRequestDelegate != null)
			{
				m_onEndTakeoverRequestDelegate(takeoverEvent);
			}
		}
	}
}
