using UnityEngine;

internal class StarStrike_JerryLose : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = GameObject.Find("JerryBase");
		if (gameObject != null)
		{
			StarStrike_Utils.HideObject(gameObject);
		}
		GameObject gameObject2 = GameObject.Find("TomBase");
		if (gameObject2 != null)
		{
			StarStrike_Utils.HideObject(gameObject2);
		}
	}

	public void PushGameOverState()
	{
		StarStrike_GameStateManager.GetInstance().Push(StarStrike_GameState.GAME_OVER);
	}
}
