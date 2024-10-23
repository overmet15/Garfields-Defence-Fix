internal class StarStrike_RandomWaveResolver : StarStrike_WaveResolver
{
	public StarStrike_WaveDefinition ResolveNextWave(StarStrike_LevelDefinition levelDefinition)
	{
		return levelDefinition.GetRandomWave();
	}
}
