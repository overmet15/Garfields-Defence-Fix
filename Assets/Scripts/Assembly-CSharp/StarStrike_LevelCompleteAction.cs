internal class StarStrike_LevelCompleteAction : StarStrike_ActionBehaviour
{
	private StarStrike_GameStateManager gameStateManager;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
	}

	public override void ExecuteAction()
	{
		gameStateManager.Clear();
		gameStateManager.Push(StarStrike_GameState.LEVEL_START);
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
	}
}
