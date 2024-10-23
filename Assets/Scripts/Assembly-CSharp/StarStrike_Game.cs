using UnityEngine;

internal class StarStrike_Game : MonoBehaviour
{
	public float timeScale = 1f;

	private StarStrike_GameStateManager gameStateManager;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		gameStateManager.Clear();
		gameStateManager.Push(StarStrike_GameState.LEVEL_START);
		Time.timeScale = timeScale;
		StarStrike_ScoringManager.GetInstance().ResetScore();
		StarStrike_EventManagerInstance.GetInstance().AddListener(StarStrike_ScoringManager.GetInstance());
	}

	private void OnLevelWasLoaded(int level)
	{
		StarStrike_EventManagerInstance.GetInstance().RemoveAllListeners();
	}
}
