using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoAnimController")]
public class tk2dDemoAnimController : MonoBehaviour
{
	private tk2dAnimatedSprite animSprite;

	public tk2dTextMesh popupTextMesh;

	private void Start()
	{
		animSprite = GetComponent<tk2dAnimatedSprite>();
		animSprite.animationEventDelegate = AnimationEventDelegate;
		popupTextMesh.gameObject.SetActive(false);
	}

	private void AnimationEventDelegate(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum)
	{
		string text = sprite.name + "\n" + clip.name + "\nINFO: " + frame.eventInfo;
		StartCoroutine(PopupText(text));
	}

	private IEnumerator PopupText(string text)
	{
		popupTextMesh.text = text;
		popupTextMesh.Commit();
		popupTextMesh.gameObject.SetActive(true);
		float fadeTime = 1f;
		Color c1 = popupTextMesh.color;
		Color c2 = popupTextMesh.color2;
		for (float f = 0f; f < fadeTime; f += Time.deltaTime)
		{
			c2.a = (c1.a = Mathf.Clamp01(2f * (1f - f / fadeTime)));
			popupTextMesh.color = c1;
			popupTextMesh.color2 = c2;
			popupTextMesh.Commit();
			yield return 0;
		}
		popupTextMesh.gameObject.SetActive(false);
	}

	private void OnGUI()
	{
		GUILayout.BeginVertical();
		GUILayout.Label("Animation wrap modes");
		if (GUILayout.Button("Loop", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_loop");
		}
		GUILayout.Label("  This animation will play indefinitely");
		if (GUILayout.Button("LoopSection", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_loopsection");
		}
		GUILayout.Label("  This animation has been set up to loop from frame 3.\nIt will play 0 1 2 3 4 2 3 4 2 3 4 indefinitely");
		if (GUILayout.Button("Once", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_once");
		}
		GUILayout.Label("  This animation will play once and stop at the last frame");
		if (GUILayout.Button("Ping pong", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_pingpong");
		}
		GUILayout.Label("  This animation will play once forward, and then reverse, repeating indefinitely");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Single", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_single1");
		}
		if (GUILayout.Button("Single", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_single2");
		}
		GUILayout.EndHorizontal();
		GUILayout.Label("  Use this for non-animated states and placeholders.");
		GUILayout.Space(20f);
		GUILayout.Label("Animation delegate example");
		if (GUILayout.Button("Delegate", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_once");
			animSprite.animationCompleteDelegate = _003COnGUI_003Em__43;
		}
		GUILayout.Label("Play demo_once, then immediately play demo_pingpong after that");
		if (GUILayout.Button("Message", GUILayout.MaxWidth(100f)))
		{
			animSprite.Play("demo_message");
		}
		GUILayout.Label("Plays demo_message once, will trigger an event when frame 3 is hit.\nLook at how this animation is set up.");
		GUILayout.EndVertical();
	}

	[CompilerGenerated]
	private void _003COnGUI_003Em__43(tk2dAnimatedSprite sprite, int clipId)
	{
		animSprite.Play("demo_pingpong");
		animSprite.animationCompleteDelegate = null;
	}
}
