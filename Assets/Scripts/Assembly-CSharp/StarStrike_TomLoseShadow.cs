using UnityEngine;

public class StarStrike_TomLoseShadow : MonoBehaviour
{
	public Transform followTransform;

	private Transform selfTransform;

	private void Start()
	{
		selfTransform = base.transform;
	}

	private void Update()
	{
		Vector3 position = followTransform.position;
		position.y = 0f;
		selfTransform.position = position;
	}
}
