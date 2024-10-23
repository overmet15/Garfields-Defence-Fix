using UnityEngine;

internal class StarStrike_GameOverAction : StarStrike_ActionBehaviour
{
	public override void ExecuteAction()
	{
		StarStrike_LevelManager component = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.Assert(component != null, "levelManager should not be null");
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.GAME_OVER_CONFIRMED);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
		int score = StarStrike_ScoringManager.GetInstance().GetScore();
		MinigameBridge.gameEnd(score, component.GetLevelNumber());
		StarStrike_GameStateManager.GetInstance().Pop();
	}
}
