internal class StarStrike_WaitAfterSpawnWave : StarStrike_LevelManagerStateAdapter
{
	private StarStrike_CountdownTimer waitTimer;

	public StarStrike_WaitAfterSpawnWave(float waitTimeBetweenWaves)
	{
		waitTimer = new StarStrike_CountdownTimer(waitTimeBetweenWaves);
	}

	public override void ProcessEvent(StarStrike_LevelManagerStateEvent stateEvent)
	{
		if (stateEvent == StarStrike_LevelManagerStateEvent.ON_PUSH)
		{
			UnmarkAsDone();
			waitTimer.Reset();
		}
	}

	public override void Update()
	{
		waitTimer.Update();
		if (waitTimer.HasElapsed())
		{
			MarkAsDone();
		}
	}
}
