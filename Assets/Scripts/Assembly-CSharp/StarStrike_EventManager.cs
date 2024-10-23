using System.Collections;

internal class StarStrike_EventManager<TEventType, TAttachmentKey>
{
	private ArrayList listenerList;

	private bool eventPostingLock;

	public StarStrike_EventManager()
	{
		listenerList = new ArrayList();
		UnlockEventPosting();
	}

	public bool ContainsListener(StarStrike_EventListener<TEventType, TAttachmentKey> listener)
	{
		return listenerList.Contains(listener);
	}

	public void AddListener(StarStrike_EventListener<TEventType, TAttachmentKey> listener)
	{
		StarStrike_Assertion.Assert(!listenerList.Contains(listener), "Can't add listener. The manager already contains the specified listener.");
		listenerList.Add(listener);
	}

	private void VerifyLock()
	{
		StarStrike_Assertion.Assert(!eventPostingLock, "Can't continue with operation. Event posting is still mutating.");
	}

	private void LockEventPosting()
	{
		eventPostingLock = true;
	}

	private void UnlockEventPosting()
	{
		eventPostingLock = false;
	}

	public void PostEvent(StarStrike_Event<TEventType, TAttachmentKey> gameEvent)
	{
		VerifyLock();
		LockEventPosting();
		foreach (StarStrike_EventListener<TEventType, TAttachmentKey> listener in listenerList)
		{
			listener.ProcessEvent(gameEvent);
		}
		UnlockEventPosting();
	}

	public void RemoveAllListeners()
	{
		listenerList.Clear();
	}
}
