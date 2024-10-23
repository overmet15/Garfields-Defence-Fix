internal class StarStrike_EventManagerInstance
{
	private static StarStrike_EventManager<StarStrike_EventType, StarStrike_AttachmentKey> ONLY_INSTANCE;

	private StarStrike_EventManagerInstance()
	{
	}

	public static StarStrike_EventManager<StarStrike_EventType, StarStrike_AttachmentKey> GetInstance()
	{
		if (ONLY_INSTANCE == null)
		{
			ONLY_INSTANCE = new StarStrike_EventManager<StarStrike_EventType, StarStrike_AttachmentKey>();
		}
		return ONLY_INSTANCE;
	}

	public static void DeleteInstance()
	{
		ONLY_INSTANCE = null;
	}
}
