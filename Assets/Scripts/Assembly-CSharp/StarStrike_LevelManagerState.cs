internal interface StarStrike_LevelManagerState
{
	void Update();

	bool IsDone();

	void ProcessEvent(StarStrike_LevelManagerStateEvent stateEvent);
}
