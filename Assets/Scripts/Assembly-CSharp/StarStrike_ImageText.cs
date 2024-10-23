using UnityEngine;

internal class StarStrike_ImageText : MonoBehaviour
{
	private const float MAX_LIFE_TIME = 2f;

	private const float MAX_JUMP_HEIGHT = 0.5f;

	private Vector3 origPosition;

	private float lifeCtr;

	private void Start()
	{
		origPosition = base.transform.position;
		lifeCtr = 0f;
	}

	private void Update()
	{
		lifeCtr += Time.deltaTime;
		if (lifeCtr >= 2f)
		{
			BroadcastMessage("Destruct");
		}
		else if (lifeCtr < 0.2f)
		{
			float num = lifeCtr / 0.2f;
			base.transform.position = new Vector3(origPosition.x, origPosition.y + 0.5f * num, origPosition.z);
		}
	}

	private void Destruct()
	{
		Object.Destroy(base.gameObject);
	}
}
