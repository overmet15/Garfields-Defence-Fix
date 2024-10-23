using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.Callback")]
	public class ICallbackProxy : BridgeObject, ICallback
	{
		protected ICallbackProxy(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
