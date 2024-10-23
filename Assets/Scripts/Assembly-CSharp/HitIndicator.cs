using UnityEngine;

public class HitIndicator : MonoBehaviour
{
	public float DestroyTime;

	public Animation animation;

	public AnimationClip clip;

	private void Update()
	{
		if (!animation.IsPlaying(clip.name))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
