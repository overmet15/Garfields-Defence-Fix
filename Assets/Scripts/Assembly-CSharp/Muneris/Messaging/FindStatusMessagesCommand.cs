using System;
using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.FindStatusMessagesCommand")]
	public class FindStatusMessagesCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.FindStatusMessagesCommandBridge";

		protected FindStatusMessagesCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindStatusMessagesCommand setCallback(IFindStatusMessagesCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindStatusMessagesCommand_FindStatusMessagesCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public IFindStatusMessagesCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindStatusMessagesCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindStatusMessagesCallback>(callbackId);
		}

		public FindStatusMessagesCommand addMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addMessageIds___FindStatusMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public FindStatusMessagesCommand setMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setMessageIds___FindStatusMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public List<string> getMessageIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessageIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindStatusMessagesCommand setStartDate(DateTime start)
		{
			long num = SerializationHelper.Serialize(start);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setStartDate___FindStatusMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public DateTime getStartDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getStartDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindStatusMessagesCommand setEndDate(DateTime end)
		{
			long num = SerializationHelper.Serialize(end);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setEndDate___FindStatusMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public DateTime getEndDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getEndDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindStatusMessagesCommand setSortBy(SortDescriptor sortDescriptor)
		{
			string text = JsonHelper.Serialize(sortDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSortBy___FindStatusMessagesCommand_SortDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public SortDescriptor getSortBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSortBy___SortDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SortDescriptor>(json);
		}

		public FindStatusMessagesCommand setGroupBy(GroupDescriptor groupDescriptor)
		{
			string text = JsonHelper.Serialize(groupDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setGroupBy___FindStatusMessagesCommand_GroupDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public GroupDescriptor getGroupBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getGroupBy___GroupDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<GroupDescriptor>(json);
		}

		public FindStatusMessagesCommand setOffset(int offset)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setOffset___FindStatusMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				offset
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public int getOffset()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getOffset___int", new object[1] { GetObjectId() });
		}

		public FindStatusMessagesCommand setLimit(int limit)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setLimit___FindStatusMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				limit
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public int getLimit()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getLimit___int", new object[1] { GetObjectId() });
		}

		public FindStatusMessagesCommand addSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceTypes___FindStatusMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public FindStatusMessagesCommand setSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceTypes___FindStatusMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public List<AddressType> getSourceTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSourceTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindStatusMessagesCommand addSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceIds___FindStatusMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public FindStatusMessagesCommand setSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceIds___FindStatusMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public List<string> getSourceIds(AddressType sourceType)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSourceIds___String_AddressType", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindStatusMessagesCommand addTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetTypes___FindStatusMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public FindStatusMessagesCommand setTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetTypes___FindStatusMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public List<AddressType> getTargetTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTargetTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindStatusMessagesCommand addTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetIds___FindStatusMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public FindStatusMessagesCommand setTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetIds___FindStatusMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public List<string> getTargetIds(AddressType targetType)
		{
			int num = SerializationHelper.Serialize(targetType);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTargetIds___String_AddressType", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public FindStatusMessagesCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindStatusMessagesCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}

		public void validate()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "validate___void", new object[1] { GetObjectId() });
			BridgeResult<object> bridgeResult = JsonHelper.DeserializeBridgeResult<object>(json);
			if (bridgeResult.Kind == BridgeResult<object>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
		}
	}
}
