using UnityEngine;

public class StarStrike_StartLevelPrompt : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	public GUIStyle textStyle;

	public Rect textRect;

	public Texture windowTexture;

	public Rect windowRect;

	private StarStrike_GameStateManager gameStateManager;

	private StarStrike_LevelManager levelManager;

	private StarStrike_GuidedMovement cameraMovement;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		levelManager = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.Assert(levelManager != null, "Level manager must not be null.");
		StarStrike_GameConfiguration component = GameObject.Find("GameConfiguration").GetComponent<StarStrike_GameConfiguration>();
		StarStrike_Assertion.Assert(component != null, "Game configuration must not be null.");
		float num = float.Parse(component.GetAttribute("startLevelPanTime"));
		float num2 = (StarStrike_CameraDragListener.RIGHTMOST_POSITION.x - StarStrike_CameraDragListener.LEFTMOST_POSITION.x) * 2f;
		Transform transform = GameObject.Find("CameraParent").transform;
		cameraMovement = new StarStrike_GuidedMovement(transform, num2 / num);
		Debug.Log("StarStrike_CameraDragListener.RIGHTMOST_POSITION: " + StarStrike_CameraDragListener.RIGHTMOST_POSITION);
		Debug.Log("StarStrike_CameraDragListener.LEFTMOST_POSITION: " + StarStrike_CameraDragListener.LEFTMOST_POSITION);
		cameraMovement.AddControlPoint(StarStrike_CameraDragListener.RIGHTMOST_POSITION);
		cameraMovement.AddControlPoint(StarStrike_CameraDragListener.LEFTMOST_POSITION);
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		if (!gameStateManager.IsCurrentState(StarStrike_GameState.LEVEL_START))
		{
			return;
		}
		cameraMovement.Update();
		if (cameraMovement.IsFinished())
		{
			gameStateManager.Pop();
			gameStateManager.Push(StarStrike_GameState.IN_GAME);
			cameraMovement.Restart();
			if (levelManager.GetLevelNumber() == 1)
			{
				Debug.Log("===========Tutorial Trigger=============");
			}
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		if (gameEvent.GetEventType() == StarStrike_EventType.TOM_BASE_DESTROYED)
		{
			cameraMovement.Restart();
		}
	}
}
