using UnityEngine;

public class AutoDestroyAfterAnimation : MonoBehaviour
{
	public Animation animation;

	private void Update()
	{
		if (!animation.IsPlaying(animation.clip.name))
		{
			Object.Destroy(base.gameObject);
		}
	}
}
