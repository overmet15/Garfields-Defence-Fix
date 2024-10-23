using System;
using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.FindChatMessagesCommand")]
	public class FindChatMessagesCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.FindChatMessagesCommandBridge";

		protected FindChatMessagesCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindChatMessagesCommand setCallback(IFindChatMessagesCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindChatMessagesCommand_FindChatMessagesCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public IFindChatMessagesCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindChatMessagesCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindChatMessagesCallback>(callbackId);
		}

		public FindChatMessagesCommand addMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addMessageIds___FindChatMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public FindChatMessagesCommand setMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setMessageIds___FindChatMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public List<string> getMessageIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessageIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindChatMessagesCommand setStartDate(DateTime start)
		{
			long num = SerializationHelper.Serialize(start);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setStartDate___FindChatMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public DateTime getStartDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getStartDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindChatMessagesCommand setEndDate(DateTime end)
		{
			long num = SerializationHelper.Serialize(end);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setEndDate___FindChatMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public DateTime getEndDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getEndDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindChatMessagesCommand setSortBy(SortDescriptor sortDescriptor)
		{
			string text = JsonHelper.Serialize(sortDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSortBy___FindChatMessagesCommand_SortDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public SortDescriptor getSortBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSortBy___SortDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SortDescriptor>(json);
		}

		public FindChatMessagesCommand setGroupBy(GroupDescriptor groupDescriptor)
		{
			string text = JsonHelper.Serialize(groupDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setGroupBy___FindChatMessagesCommand_GroupDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public GroupDescriptor getGroupBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getGroupBy___GroupDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<GroupDescriptor>(json);
		}

		public FindChatMessagesCommand setOffset(int offset)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setOffset___FindChatMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				offset
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public int getOffset()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getOffset___int", new object[1] { GetObjectId() });
		}

		public FindChatMessagesCommand setLimit(int limit)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setLimit___FindChatMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				limit
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public int getLimit()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getLimit___int", new object[1] { GetObjectId() });
		}

		public FindChatMessagesCommand addSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceTypes___FindChatMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public FindChatMessagesCommand setSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceTypes___FindChatMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public List<AddressType> getSourceTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSourceTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindChatMessagesCommand addSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceIds___FindChatMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public FindChatMessagesCommand setSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceIds___FindChatMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
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

		public FindChatMessagesCommand addTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetTypes___FindChatMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public FindChatMessagesCommand setTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetTypes___FindChatMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public List<AddressType> getTargetTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTargetTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindChatMessagesCommand addTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetIds___FindChatMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}

		public FindChatMessagesCommand setTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetIds___FindChatMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
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

		public FindChatMessagesCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindChatMessagesCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
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
