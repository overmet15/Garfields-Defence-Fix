using System.Collections;

internal class StarStrike_WaveDefinition
{
	private IList armyList;

	private int cursor;

	public StarStrike_WaveDefinition()
	{
		armyList = new ArrayList();
	}

	public void AddArmy(StarStrike_ArmyDefinition army)
	{
		armyList.Add(army);
	}

	public bool HasNext()
	{
		return cursor < armyList.Count;
	}

	public StarStrike_ArmyDefinition MoveToNext()
	{
		StarStrike_ArmyDefinition result = (StarStrike_ArmyDefinition)armyList[cursor];
		cursor++;
		return result;
	}

	public void ResetIteration()
	{
		cursor = 0;
	}

	public int ArmyCount()
	{
		return armyList.Count;
	}
}
