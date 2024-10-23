internal class StarStrike_TimedWaitState : StarStrike_LevelManagerStateAdapter
{
	private StarStrike_CountdownTimer waitTimer;

	public StarStrike_TimedWaitState(float waitTime)
	{
		StarStrike_Assertion.Assert(StarStrike_Comparison.TolerantGreaterThanOrEquals(waitTime, 0f), "waitTime should greater than or equal to zero.");
		waitTimer = new StarStrike_CountdownTimer(waitTime);
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
