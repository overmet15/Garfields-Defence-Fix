namespace Muneris.Takeover
{
	public interface ITakeoverCallback : ICallback
	{
		void onStartTakeoverRequest(TakeoverEvent takeoverEvent);

		void onLoadTakeover(TakeoverEvent takeoverEvent, TakeoverResponse takeoverResponse);

		void onDismissTakeover(TakeoverEvent takeoverEvent);

		void onFailTakeover(TakeoverEvent takeoverEvent, MunerisException exception);

		void onEndTakeoverRequest(TakeoverEvent takeoverEvent);
	}
}
