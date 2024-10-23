using UnityEngine;

internal class StarStrike_MeshFader : MonoBehaviour
{
	public float fadeTime = 1f;

	private StarStrike_CountdownTimer fadeTimer;

	private Renderer[] renderers;

	private void Start()
	{
		renderers = base.gameObject.GetComponentsInChildren<Renderer>();
	}

	public void SetFadeTime(float newFadeTime)
	{
		fadeTimer = new StarStrike_CountdownTimer(newFadeTime);
	}

	private void Update()
	{
		if (fadeTimer == null)
		{
			fadeTimer = new StarStrike_CountdownTimer(fadeTime);
		}
		fadeTimer.Update();
		if (fadeTimer.HasElapsed())
		{
			Object.Destroy(base.gameObject);
		}
		float a = 1f - StarStrike_InterpolationUtils.SmoothStep(fadeTimer.GetRatio());
		Renderer[] array = renderers;
		foreach (Renderer renderer in array)
		{
			if (renderer.material.HasProperty("_Color"))
			{
				Color color = renderer.material.color;
				color.a = a;
				renderer.material.color = color;
			}
		}
	}
}
