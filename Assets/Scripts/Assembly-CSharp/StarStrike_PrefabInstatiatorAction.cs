using UnityEngine;

internal class StarStrike_PrefabInstatiatorAction : StarStrike_ConditionalAction
{
	private Vector3 position;

	private GameObject prefab;

	private StarStrike_GameStateManager gameStateManager;

	public StarStrike_PrefabInstatiatorAction(GameObject prefab, Vector3 position)
	{
		this.prefab = prefab;
		this.position = position;
		gameStateManager = StarStrike_GameStateManager.GetInstance();
	}

	public bool CanBeExecuted()
	{
		return gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME);
	}

	public void Execute()
	{
		Object.Instantiate(prefab, position, prefab.transform.rotation);
	}
}
