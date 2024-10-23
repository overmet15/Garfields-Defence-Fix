using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverResponse")]
	public class TakeoverResponse : BridgeObject
	{
		public enum State
		{
			INIT = 0,
			SHOW = 1,
			WAITING = 2,
			IGNORE = 3,
			CANCELLED = 4
		}

		private static string _bridgeClassName = "muneris.bridge.takeover.TakeoverResponseBridge";

		protected TakeoverResponse(ObjectId objectId)
			: base(objectId)
		{
		}

		public State getTakeoverState()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getTakeoverState___TakeoverResponse_State", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<State>(num);
		}

		public void showBuiltInView()
		{
			JniHelper.CallStatic(_bridgeClassName, "showBuiltInView___void", GetObjectId());
		}

		public void setTakeoverState(State state)
		{
			int num = SerializationHelper.Serialize(state);
			JniHelper.CallStatic(_bridgeClassName, "setTakeoverState___void_TakeoverResponse_State", GetObjectId(), num);
		}

		public bool isShowBuiltInView()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isShowBuiltInView___boolean", new object[1] { GetObjectId() });
		}

		public void dismiss()
		{
			JniHelper.CallStatic(_bridgeClassName, "dismiss___void", GetObjectId());
		}
	}
}
