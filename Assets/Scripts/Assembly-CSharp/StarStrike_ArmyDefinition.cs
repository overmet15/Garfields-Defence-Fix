internal class StarStrike_ArmyDefinition
{
	private int model;

	private int count;

	public StarStrike_ArmyDefinition(int model, int count)
	{
		this.model = model;
		this.count = count;
	}

	public int GetModel()
	{
		return model;
	}

	public int GetCount()
	{
		return count;
	}
}
