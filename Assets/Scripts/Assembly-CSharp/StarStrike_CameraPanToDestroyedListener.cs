using UnityEngine;

internal class StarStrike_CameraPanToDestroyedListener : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const float PANNING_TIME = 0.25f;

	private StarStrike_CountdownTimer panningTimer;

	private bool panning;

	private Vector3 startingPoint;

	private Vector3 destination;

	private Transform cameraTransform;

	private void Start()
	{
		panningTimer = new StarStrike_CountdownTimer(0.25f);
		panning = false;
		cameraTransform = base.transform;
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		if (panning)
		{
			if (panningTimer.HasElapsed())
			{
				cameraTransform.position = destination;
				panning = false;
			}
			else
			{
				panningTimer.Update();
				float t = StarStrike_InterpolationUtils.SmoothStep(panningTimer.GetRatio());
				cameraTransform.position = Vector3.Lerp(startingPoint, destination, t);
			}
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.JERRY_BASE_DESTROYED:
			StartPanning(StarStrike_CameraDragListener.LEFTMOST_POSITION);
			break;
		case StarStrike_EventType.TOM_BASE_DESTROYED:
			StartPanning(StarStrike_CameraDragListener.RIGHTMOST_POSITION);
			break;
		}
	}

	private void StartPanning(Vector3 destination)
	{
		panning = true;
		startingPoint = cameraTransform.position;
		this.destination = destination;
		panningTimer.Reset();
	}
}
