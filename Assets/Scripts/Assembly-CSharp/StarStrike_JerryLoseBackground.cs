using UnityEngine;

public class StarStrike_JerryLoseBackground : MonoBehaviour
{
	public Texture2D[] backgrounds;

	public Color[] backgroundColors;

	private MeshRenderer meshRenderer;

	private void Start()
	{
		meshRenderer = base.transform.Find("Image").GetComponent<MeshRenderer>();
		StarStrike_Assertion.Assert(meshRenderer != null, "meshRenderer must not be null");
		GameObject gameObject = GameObject.Find("LevelManager");
		StarStrike_LevelManager starStrike_LevelManager = ((!(gameObject == null)) ? gameObject.GetComponent<StarStrike_LevelManager>() : null);
		int num = ((starStrike_LevelManager == null) ? 1 : starStrike_LevelManager.GetLevelNumber());
		int value = num - 1;
		value = StarStrike_Utils.Clamp(value, 0, backgrounds.Length - 1);
		meshRenderer.material.mainTexture = backgrounds[value];
		Camera.main.backgroundColor = backgroundColors[value];
	}
}
