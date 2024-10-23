using UnityEngine;

public class UITouch
{
	public int fingerId;

	public int tapCount;

	public float deltaTime;

	public Vector2 position;

	public Vector2 deltaPosition;

	public TouchPhase phase;

	public void ConvertTouch(Touch touch)
	{
		fingerId = touch.fingerId;
		position = touch.position;
		deltaPosition = touch.deltaPosition;
		deltaTime = touch.deltaTime;
		tapCount = touch.tapCount;
		phase = touch.phase;
	}
}
