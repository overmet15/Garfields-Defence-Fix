using UnityEngine;

internal class StarStrike_CameraShake : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const float TRANSITION_TIME = 0.4f;

	public float shakeSpeed = 2f;

	public Vector3 shakeRange = new Vector3(1f, 1f, 1f);

	private Vector3 halfShakeRange;

	private Transform thisTransform;

	private Transform parentTransform;

	private bool timedShake;

	private StarStrike_CountdownTimer shakeTimer;

	private bool transitionToParentPosition;

	private Vector3 lastPosition;

	private StarStrike_CountdownTimer transitionTimer;

	private void Start()
	{
		thisTransform = base.transform;
		parentTransform = base.transform.parent;
		StarStrike_Assertion.Assert(parentTransform != null, "parentTransform should not be null");
		timedShake = false;
		halfShakeRange = Vector3.Scale(shakeRange, new Vector3(0.5f, 0.5f, 0.5f));
		transitionToParentPosition = false;
		transitionTimer = new StarStrike_CountdownTimer(0.4f);
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		TimedShake();
		UpdateTransition();
	}

	private void TimedShake()
	{
		if (timedShake)
		{
			shakeTimer.Update();
			if (shakeTimer.HasElapsed())
			{
				timedShake = false;
				transitionToParentPosition = true;
				transitionTimer.Reset();
				lastPosition = thisTransform.position;
			}
			else
			{
				ShakePosition();
			}
		}
	}

	private void UpdateTransition()
	{
		if (transitionToParentPosition)
		{
			transitionTimer.Update();
			if (transitionTimer.HasElapsed())
			{
				thisTransform.position = parentTransform.position;
				transitionToParentPosition = false;
			}
			float t = StarStrike_InterpolationUtils.SmoothStep(transitionTimer.GetRatio());
			thisTransform.position = Vector3.Lerp(lastPosition, parentTransform.position, t);
		}
	}

	public void DoTimedShake(float shakeTime)
	{
		if (!timedShake)
		{
			shakeTimer = new StarStrike_CountdownTimer(shakeTime);
			timedShake = true;
			transitionToParentPosition = false;
		}
	}

	private void ShakePosition()
	{
		Vector3 vector = StarStrike_SmoothRandom.GetVector3(shakeSpeed);
		Vector3 vector2 = Vector3.Scale(vector, shakeRange) - halfShakeRange;
		thisTransform.position = parentTransform.position + vector2;
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.METEOR_HIT_GROUND:
			DoTimedShake(1f);
			break;
		case StarStrike_EventType.EMP_USED:
			DoTimedShake(0.6f);
			break;
		case StarStrike_EventType.ROCKET_GLOVE_FIRED:
			DoTimedShake(2f);
			break;
		case StarStrike_EventType.ROCKET_CANNON_USED:
			break;
		}
	}
}
