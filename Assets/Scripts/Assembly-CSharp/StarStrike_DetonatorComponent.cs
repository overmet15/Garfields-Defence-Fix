using UnityEngine;

public abstract class StarStrike_DetonatorComponent : MonoBehaviour
{
	public bool on = true;

	public bool detonatorControlled = true;

	[HideInInspector]
	public float startSize = 1f;

	public float size = 1f;

	public float explodeDelayMin;

	public float explodeDelayMax;

	[HideInInspector]
	public float startDuration = 2f;

	public float duration = 2f;

	[HideInInspector]
	public float timeScale = 1f;

	[HideInInspector]
	public float startDetail = 1f;

	public float detail = 1f;

	[HideInInspector]
	public Color startColor = Color.white;

	public Color color = Color.white;

	[HideInInspector]
	public Vector3 startLocalPosition = Vector3.zero;

	public Vector3 localPosition = Vector3.zero;

	public Vector3 force = Vector3.zero;

	public float detailThreshold;

	public abstract void Explode();

	public abstract void Init();

	public void SetStartValues()
	{
		startSize = size;
		startDuration = duration;
		startDetail = detail;
		startColor = color;
		startLocalPosition = localPosition;
	}

	public StarStrike_Detonator MyDetonator()
	{
		return GetComponent<StarStrike_Detonator>();
	}
}
