using UnityEngine;

public class StarStrike_Background : MonoBehaviour
{
	public Texture2D[] backgrounds;

	private StarStrike_LevelManager levelManager;

	private MeshRenderer meshRenderer;

	private int previousIndex;

	private void Start()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.Assert(levelManager != null, "levelManager must not be null");
		meshRenderer = GetComponent<MeshRenderer>();
		StarStrike_Assertion.Assert(meshRenderer != null, "meshRenderer must not be null");
		previousIndex = -1;
	}

	private void Update()
	{
		int num = levelManager.GetLevelNumber() - 1;
		if (previousIndex != num)
		{
			meshRenderer.material.mainTexture = backgrounds[num];
			previousIndex = num;
		}
	}
}
