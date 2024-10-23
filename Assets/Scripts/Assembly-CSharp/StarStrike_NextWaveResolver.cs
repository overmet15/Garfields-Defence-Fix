internal class StarStrike_NextWaveResolver : StarStrike_WaveResolver
{
	public StarStrike_WaveDefinition ResolveNextWave(StarStrike_LevelDefinition levelDefinition)
	{
		StarStrike_Assertion.Assert(levelDefinition.HasNextWave(), "Level definition should have a next wave at this point.");
		return levelDefinition.MoveToNextWave();
	}
}
