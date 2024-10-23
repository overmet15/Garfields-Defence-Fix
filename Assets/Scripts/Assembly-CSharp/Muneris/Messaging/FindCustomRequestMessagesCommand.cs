using System;
using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.FindCustomRequestMessagesCommand")]
	public class FindCustomRequestMessagesCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.FindCustomRequestMessagesCommandBridge";

		protected FindCustomRequestMessagesCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindCustomRequestMessagesCommand setCallback(IFindCustomRequestMessagesCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindCustomRequestMessagesCommand_FindCustomRequestMessagesCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public IFindCustomRequestMessagesCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindCustomRequestMessagesCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindCustomRequestMessagesCallback>(callbackId);
		}

		public FindCustomRequestMessagesCommand addMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addMessageIds___FindCustomRequestMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public FindCustomRequestMessagesCommand setMessageIds(List<string> messageIds)
		{
			string text = JsonHelper.Serialize(messageIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setMessageIds___FindCustomRequestMessagesCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public List<string> getMessageIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessageIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindCustomRequestMessagesCommand setStartDate(DateTime start)
		{
			long num = SerializationHelper.Serialize(start);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setStartDate___FindCustomRequestMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public DateTime getStartDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getStartDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindCustomRequestMessagesCommand setEndDate(DateTime end)
		{
			long num = SerializationHelper.Serialize(end);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setEndDate___FindCustomRequestMessagesCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public DateTime getEndDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getEndDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public FindCustomRequestMessagesCommand setSortBy(SortDescriptor sortDescriptor)
		{
			string text = JsonHelper.Serialize(sortDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSortBy___FindCustomRequestMessagesCommand_SortDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public SortDescriptor getSortBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSortBy___SortDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SortDescriptor>(json);
		}

		public FindCustomRequestMessagesCommand setGroupBy(GroupDescriptor groupDescriptor)
		{
			string text = JsonHelper.Serialize(groupDescriptor);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setGroupBy___FindCustomRequestMessagesCommand_GroupDescriptor", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public GroupDescriptor getGroupBy()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getGroupBy___GroupDescriptor", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<GroupDescriptor>(json);
		}

		public FindCustomRequestMessagesCommand setOffset(int offset)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setOffset___FindCustomRequestMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				offset
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public int getOffset()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getOffset___int", new object[1] { GetObjectId() });
		}

		public FindCustomRequestMessagesCommand setLimit(int limit)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setLimit___FindCustomRequestMessagesCommand_int", new object[2]
			{
				GetObjectId(),
				limit
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public int getLimit()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getLimit___int", new object[1] { GetObjectId() });
		}

		public FindCustomRequestMessagesCommand addSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceTypes___FindCustomRequestMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public FindCustomRequestMessagesCommand setSourceTypes(List<AddressType> sourceTypes)
		{
			string text = JsonHelper.Serialize(sourceTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceTypes___FindCustomRequestMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public List<AddressType> getSourceTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSourceTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindCustomRequestMessagesCommand addSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSourceIds___FindCustomRequestMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public FindCustomRequestMessagesCommand setSourceIds(AddressType sourceType, List<string> sourceIds)
		{
			int num = SerializationHelper.Serialize(sourceType);
			string text = JsonHelper.Serialize(sourceIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSourceIds___FindCustomRequestMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
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

		public FindCustomRequestMessagesCommand addTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetTypes___FindCustomRequestMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public FindCustomRequestMessagesCommand setTargetTypes(List<AddressType> targetTypes)
		{
			string text = JsonHelper.Serialize(targetTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetTypes___FindCustomRequestMessagesCommand_AddressType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public List<AddressType> getTargetTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTargetTypes___AddressType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<AddressType>>(json);
		}

		public FindCustomRequestMessagesCommand addTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addTargetIds___FindCustomRequestMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}

		public FindCustomRequestMessagesCommand setTargetIds(AddressType targetType, List<string> targetIds)
		{
			int num = SerializationHelper.Serialize(targetType);
			string text = JsonHelper.Serialize(targetIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setTargetIds___FindCustomRequestMessagesCommand_AddressType_String", new object[3]
			{
				GetObjectId(),
				num,
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
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

		public FindCustomRequestMessagesCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindCustomRequestMessagesCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
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
