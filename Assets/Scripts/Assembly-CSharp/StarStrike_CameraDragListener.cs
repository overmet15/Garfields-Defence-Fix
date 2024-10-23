using UnityEngine;

internal class StarStrike_CameraDragListener : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const float RIGHTMOST_X = 76f;

	private const float LEFTMOST_X = 52f;

	private const float CAMERA_X_MOVEMENT_PER_MOUSE_DISPLACEMENT = 0.03f;

	public static Vector3 LEFTMOST_POSITION = new Vector3(52f, 13f, -18.89943f);

	public static Vector3 RIGHTMOST_POSITION = new Vector3(76f, LEFTMOST_POSITION.y, LEFTMOST_POSITION.z);

	private bool mouseWasDown;

	private float prevMouseX;

	private Transform thisTransform;

	private StarStrike_GameStateManager gameStateManager;

	private void Start()
	{
		thisTransform = base.transform;
		thisTransform.position = RIGHTMOST_POSITION;
		mouseWasDown = false;
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
		gameStateManager = StarStrike_GameStateManager.GetInstance();
	}

	private void Update()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.GAME_COMPLETED))
		{
			thisTransform.position = LEFTMOST_POSITION;
		}
		else if (gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME))
		{
			CheckInput();
		}
	}

	private void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseWasDown = true;
			prevMouseX = Input.mousePosition.x;
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			mouseWasDown = false;
		}
		if (mouseWasDown)
		{
			MoveCamera(Input.mousePosition.x - prevMouseX);
			prevMouseX = Input.mousePosition.x;
		}
	}

	private void MoveCamera(float mouseXDisplacement)
	{
		float value = thisTransform.position.x + 0.03f * (0f - mouseXDisplacement);
		value = StarStrike_Utils.Clamp(value, 52f, 76f);
		Vector3 position = thisTransform.position;
		position.x = value;
		thisTransform.position = position;
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_EventType eventType = gameEvent.GetEventType();
		if (eventType == StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED)
		{
			thisTransform.position = RIGHTMOST_POSITION;
		}
	}
}
