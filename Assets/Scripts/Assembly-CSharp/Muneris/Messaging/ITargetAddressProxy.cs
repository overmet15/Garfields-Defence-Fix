using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.TargetAddress")]
	public class ITargetAddressProxy : BridgeObject, IAddress, ITargetAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.TargetAddressBridge";

		protected ITargetAddressProxy(ObjectId objectId)
			: base(objectId)
		{
		}

		public AddressType getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___AddressType", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<AddressType>(num);
		}
	}
}
