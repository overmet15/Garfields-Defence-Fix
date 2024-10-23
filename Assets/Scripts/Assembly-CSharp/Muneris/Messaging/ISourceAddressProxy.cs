using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SourceAddress")]
	public class ISourceAddressProxy : BridgeObject, IAddress, ISourceAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SourceAddressBridge";

		protected ISourceAddressProxy(ObjectId objectId)
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
