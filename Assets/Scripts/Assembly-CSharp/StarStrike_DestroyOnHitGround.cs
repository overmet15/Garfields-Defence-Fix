using UnityEngine;

internal class StarStrike_DestroyOnHitGround : MonoBehaviour
{
	private static string GROUND_NAME = "Ground";

	private void OnTriggerEnter(Collider collider)
	{
		if (StarStrike_Utils.ContainsObjectWithName(collider.transform, GROUND_NAME))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
