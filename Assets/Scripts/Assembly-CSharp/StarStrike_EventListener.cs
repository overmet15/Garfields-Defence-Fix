internal interface StarStrike_EventListener<TEventType, TAttachmentKey>
{
	void ProcessEvent(StarStrike_Event<TEventType, TAttachmentKey> gameEvent);
}
