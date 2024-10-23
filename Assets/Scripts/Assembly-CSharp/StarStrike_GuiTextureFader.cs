using UnityEngine;

internal class StarStrike_GuiTextureFader : MonoBehaviour
{
	public float fadeTime = 0.5f;

	private UnityEngine.UI.Image thisGuiTexture;

	private StarStrike_CountdownTimer fadeTimer;

	private float startingAlpha;

	private void Start()
	{
		thisGuiTexture = GetComponent<UnityEngine.UI.Image>();
		fadeTimer = new StarStrike_CountdownTimer(fadeTime);
		startingAlpha = thisGuiTexture.color.a;
	}

	private void Update()
	{
		fadeTimer.Update();
		if (fadeTimer.HasElapsed())
		{
			Object.Destroy(base.gameObject);
		}
		float a = startingAlpha + fadeTimer.GetRatio() * (0f - startingAlpha);
		Color color = thisGuiTexture.color;
		color.a = a;
		thisGuiTexture.color = color;
	}
}
