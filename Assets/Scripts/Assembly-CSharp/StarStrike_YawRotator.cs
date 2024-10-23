using UnityEngine;

internal class StarStrike_YawRotator : MonoBehaviour
{
	public float angularVelocity = 10f;

	private Transform selfTransform;

	private void Start()
	{
		selfTransform = base.transform;
	}

	private void Update()
	{
		selfTransform.Rotate(0f, 0f, angularVelocity);
	}
}
