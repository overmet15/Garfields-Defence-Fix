using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	public Transform targetTransform;

	public bool faceForward;

	private Transform thisTransform;

	public float speed = 10f;

	private void Start()
	{
		thisTransform = base.transform;
		thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, targetTransform.position.z);
	}

	private void Update()
	{
		if (thisTransform != null && targetTransform != null)
		{
			Vector3 position = thisTransform.position;
			Vector3 position2 = targetTransform.position;
			float num = Vector3.Distance(position, position2);
			float num2 = ((!(targetTransform.position.x >= 32f)) ? targetTransform.position.x : 32f);
			if (num2 <= -54f)
			{
				num2 = -54f;
			}
			float x = Mathf.MoveTowards(position.x, num2, num * speed * Time.deltaTime);
			thisTransform.position = new Vector3(x, position2.y, position2.z);
			if (faceForward)
			{
				thisTransform.forward = targetTransform.forward;
			}
		}
	}
}
