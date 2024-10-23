using UnityEngine;

internal class StarStrike_ProjectileMotion : MonoBehaviour
{
	private float accelerationDueToGravity = -9.8f;

	private Vector3 currentVelocity;

	private Transform thisTransform;

	private void Start()
	{
		Initialize();
	}

	private void Update()
	{
		DoUpdate();
	}

	protected virtual void Initialize()
	{
		SetAccelerationDueToGravity(-9.8f);
		thisTransform = base.transform;
	}

	protected void SetInitialVelocity(float x, float y)
	{
		currentVelocity = new Vector3(x, y, 0f);
	}

	protected void SetAccelerationDueToGravity(float acceleration)
	{
		accelerationDueToGravity = acceleration;
	}

	protected virtual void DoUpdate()
	{
		thisTransform.Translate(currentVelocity * Time.deltaTime);
	}

	protected float GetAccelerationDueToGravity()
	{
		return accelerationDueToGravity;
	}

	protected Transform GetTransform()
	{
		return thisTransform;
	}
}
