using System;
using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.FindAlertMessagesCommand")]
	public class FindAlertMessagesCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.FindAlertMessagesCommandBridge";

		protected FindAlertMessagesCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindAlertMessagesCommand setCallback(IFindAlertMessagesCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindAlertMessagesCommand_FindAlertMessagesCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public IFindAlertMessagesCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindAlertMessagesCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindAlertMessagesCallback>(callbackId);
		}

		public FindAlertMessagesCommand addMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addMessageIds___FindAlertMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public FindAlertMessagesCommand setMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setMessageIds___FindAlertMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public List<string> getMessageIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessageIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindAlertMessagesCommand setStartDate(DateTime start)
		{
			long num = SerializationHelper.Serialize(start);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setStartDate___FindAlertMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public DateTime getStartDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getStartDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindAlertMessagesCommand setEndDate(DateTime end)
		{
			long num = SerializationHelper.Serialize(end);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setEndDate___FindAlertMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public DateTime getEndDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getEndDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindAlertMessagesCommand setSortBy(SortDescriptor sortDescriptor)
		{
			string text = JsonHelper.Serialize(sortDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSortBy___FindAlertMessagesCommand_SortDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public SortDescriptor getSortBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSortBy___SortDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SortDescriptor>(json);
		}

		public FindAlertMessagesCommand setGroupBy(GroupDescriptor groupDescriptor)
		{
			string text = JsonHelper.Serialize(groupDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setGroupBy___FindAlertMessagesCommand_GroupDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public GroupDescriptor getGroupBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getGroupBy___GroupDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<GroupDescriptor>(json);
		}

		public FindAlertMessagesCommand setOffset(int offset)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setOffset___FindAlertMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				offset
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public int getOffset()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getOffset___int", new object[1] { GetObjectId() });
		}

		public FindAlertMessagesCommand setLimit(int limit)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setLimit___FindAlertMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				limit
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public int getLimit()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getLimit___int", new object[1] { GetObjectId() });
		}

		public FindAlertMessagesCommand addSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceTypes___FindAlertMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public FindAlertMessagesCommand setSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceTypes___FindAlertMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public List<AddressType> getSourceTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSourceTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindAlertMessagesCommand addSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceIds___FindAlertMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public FindAlertMessagesCommand setSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceIds___FindAlertMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
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

		public FindAlertMessagesCommand addTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetTypes___FindAlertMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public FindAlertMessagesCommand setTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetTypes___FindAlertMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public List<AddressType> getTargetTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTargetTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindAlertMessagesCommand addTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetIds___FindAlertMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}

		public FindAlertMessagesCommand setTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetIds___FindAlertMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
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

		public FindAlertMessagesCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindAlertMessagesCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
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
