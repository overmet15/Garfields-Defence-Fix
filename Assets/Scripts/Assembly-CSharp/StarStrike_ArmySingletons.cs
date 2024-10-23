internal class StarStrike_ArmySingletons
{
	private StarStrike_ArmyStatistics armyStatistics;

	private static StarStrike_ArmySingletons ONLY_INSTANCE;

	private StarStrike_ArmySingletons()
	{
		armyStatistics = new StarStrike_ArmyStatistics();
		StarStrike_EventManagerInstance.GetInstance().AddListener(armyStatistics);
	}

	public static StarStrike_ArmySingletons GetInstance()
	{
		if (ONLY_INSTANCE == null)
		{
			ONLY_INSTANCE = new StarStrike_ArmySingletons();
		}
		return ONLY_INSTANCE;
	}

	public static void DeleteInstance()
	{
		ONLY_INSTANCE = null;
	}

	public StarStrike_ArmyStatistics GetArmyStatistics()
	{
		return armyStatistics;
	}
}
