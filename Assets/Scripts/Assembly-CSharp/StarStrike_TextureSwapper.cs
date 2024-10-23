using UnityEngine;

public class StarStrike_TextureSwapper : MonoBehaviour
{
	public Texture textureToSwap;

	private Renderer[] renderers;

	private bool swapped;

	private void Start()
	{
		renderers = GetComponentsInChildren<Renderer>();
	}

	public void SwapTexture()
	{
		if (!swapped)
		{
			Renderer[] array = renderers;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = textureToSwap;
			}
			swapped = true;
		}
	}
}
