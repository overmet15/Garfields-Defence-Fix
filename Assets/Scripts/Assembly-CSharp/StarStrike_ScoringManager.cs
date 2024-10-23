internal class StarStrike_ScoringManager : StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private int score;

	private static StarStrike_ScoringManager ONLY_INSTANCE;

	private StarStrike_ScoringManager()
	{
		score = 0;
	}

	public static StarStrike_ScoringManager GetInstance()
	{
		if (ONLY_INSTANCE == null)
		{
			ONLY_INSTANCE = new StarStrike_ScoringManager();
		}
		return ONLY_INSTANCE;
	}

	public static void DeleteInstance()
	{
		ONLY_INSTANCE = null;
	}

	public int GetScore()
	{
		return score;
	}

	public void ResetScore()
	{
		score = 0;
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		if (gameEvent.GetEventType() == StarStrike_EventType.UNIT_DESTROYED)
		{
			StarStrike_ArmyUnit attachment = gameEvent.GetAttachment<StarStrike_ArmyUnit>(StarStrike_AttachmentKey.ARMY_UNIT);
			if (attachment.GetOwner() == Owner.TOM)
			{
			}
		}
	}
}
