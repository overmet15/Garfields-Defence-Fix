using UnityEngine;

public class StarStrike_TomLoseUi : MonoBehaviour
{
	public GUIStyle skipButtonStyle;

	public Rect skipButtonRect;

	private StarStrike_GameStateManager gameStateManager;

	private StarStrike_TomLose tomLoseComponent;

	private bool skipped;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		tomLoseComponent = GetComponent<StarStrike_TomLose>();
		StarStrike_Assertion.AssertNotNull(tomLoseComponent, "tomLoseComponent");
		skipped = false;
	}

	private void Update()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.GAME_COMPLETED) && !skipped && Input.GetKeyUp(KeyCode.Space))
		{
			SkipScene();
		}
	}

	private void OnGUI()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.GAME_COMPLETED) && !skipped && GUI.Button(skipButtonRect, string.Empty, skipButtonStyle))
		{
			SkipScene();
		}
	}

	private void SkipScene()
	{
		Time.timeScale = 0f;
		tomLoseComponent.GameEnd();
		skipped = true;
	}
}
