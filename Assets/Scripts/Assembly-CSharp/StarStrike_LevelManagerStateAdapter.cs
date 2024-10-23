internal class StarStrike_LevelManagerStateAdapter : StarStrike_LevelManagerState
{
	private bool done;

	public StarStrike_LevelManagerStateAdapter()
	{
		done = false;
	}

	protected void MarkAsDone()
	{
		done = true;
	}

	protected void UnmarkAsDone()
	{
		done = false;
	}

	public virtual void Update()
	{
	}

	public virtual bool IsDone()
	{
		return done;
	}

	public virtual void ProcessEvent(StarStrike_LevelManagerStateEvent stateEvent)
	{
	}
}
