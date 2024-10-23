using System.Collections;

public class StarStrike_Event<TEventType, TAttachmentKey>
{
	private TEventType eventType;

	private Hashtable attachmentMap;

	public StarStrike_Event(TEventType eventType)
	{
		this.eventType = eventType;
	}

	public TEventType GetEventType()
	{
		return eventType;
	}

	public void Attach(TAttachmentKey key, object attachment)
	{
		if (attachmentMap == null)
		{
			attachmentMap = new Hashtable();
		}
		attachmentMap.Add(key, attachment);
	}

	public T GetAttachment<T>(TAttachmentKey key)
	{
		StarStrike_Assertion.Assert(attachmentMap.ContainsKey(key), "Attachment not found for key: " + key);
		return (T)attachmentMap[key];
	}

	public bool ContainsAttachment(TAttachmentKey key)
	{
		if (attachmentMap == null)
		{
			return false;
		}
		return attachmentMap.ContainsKey(key);
	}
}
