using UnityEngine;

public class StarStrike_MiscHud : MonoBehaviour, StarStrike_UiRenderer
{
	public Rect specialAttackTextRect;

	public GUIStyle specialAttackTextStyle;

	public Rect quitTextRect;

	public GUIStyle quitTextStyle;

	public Rect quitButtonRect;

	public GUIStyle quitButtonStyle;

	private StarStrike_GameStateManager gameStateManager;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		StarStrike_Assertion.AssertNotNull(gameStateManager, "gameStateManager");
	}

	public void RenderUI()
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, StarStrike_Constants.HUD_SCALE);
		GUI.Label(specialAttackTextRect, "Special Attack", specialAttackTextStyle);
		if (gameStateManager.IsCurrentState(StarStrike_GameState.QUIT))
		{
			GUI.Label(quitButtonRect, string.Empty, quitButtonStyle);
		}
		else if (GUI.Button(quitButtonRect, string.Empty, quitButtonStyle))
		{
			gameStateManager.Push(StarStrike_GameState.QUIT);
			StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.BUTTON_CLICKED);
			starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
			StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
		}
		GUI.Label(quitTextRect, "Quit", quitTextStyle);
	}
}
