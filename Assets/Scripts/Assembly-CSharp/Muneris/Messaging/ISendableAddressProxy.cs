using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendableAddress")]
	public class ISendableAddressProxy : BridgeObject, IAddress, ISendableAddress, ITargetAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendableAddressBridge";

		protected ISendableAddressProxy(ObjectId objectId)
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
