using UnityEngine;

internal class StarStrike_Rise : MonoBehaviour
{
	public float riseVelocity = 1f;

	public float maxHeight = 50f;

	private Transform cachedTransform;

	private void Awake()
	{
		cachedTransform = base.transform;
	}

	private void Update()
	{
		cachedTransform.Translate(0f, riseVelocity * Time.deltaTime, 0f);
		if (StarStrike_Comparison.TolerantGreaterThanOrEquals(cachedTransform.position.y, maxHeight))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
