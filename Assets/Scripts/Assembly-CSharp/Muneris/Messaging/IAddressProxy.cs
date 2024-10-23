using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Address")]
	public class IAddressProxy : BridgeObject, IAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.AddressBridge";

		protected IAddressProxy(ObjectId objectId)
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
