using UnityEngine;

public class StarStrike_Follower : MonoBehaviour
{
	public Transform transformToFollow;

	private Transform selfTransform;

	private void Start()
	{
		selfTransform = base.transform;
	}

	private void Update()
	{
		selfTransform.position = transformToFollow.position;
	}
}
