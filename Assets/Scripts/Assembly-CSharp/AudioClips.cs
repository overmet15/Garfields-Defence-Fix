using UnityEngine;

public class AudioClips : MonoBehaviour
{
	public static int _SFXOn = 1;

	public static int _BGMOn = 1;

	public AudioClip _SfxButtonPress;

	public AudioClip _SfxExplosion;

	public AudioClip _SfxFireCracker;

	public AudioClip _SfxHammer;

	public AudioClip _SfxManScream;

	public AudioClip _SfxWomanScream;

	public AudioClip _SfxCamera;

	public AudioClip _SfxChatter;

	public AudioClip _SfxHitIcecream;

	public AudioClip _SfxHitSoda;

	public AudioClip _SfxHitPopcorn;

	public AudioClip _SfxPhoneRing;

	public AudioClip _SfxShockedCrowd;

	public AudioClip _SfxThrow;

	public AudioClip _SfxManAngry;

	public AudioClip _SfxWarning;

	public AudioClip _SfxCry;

	public AudioClip _SfxLifeChance;

	public AudioClip _SfxOrder;

	public AudioClip _BGM_Comedy;

	public AudioClip _BGM_Dramatic;

	public AudioClip _BGM_Action;

	public AudioClip _BGM_Horror;

	public AudioClip _BGM_Intro;

	public AudioClip _BGM_Pony;

	private static AudioClips _clips;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		_clips = this;
	}

	public void PlayComedy()
	{
	}

	public static void Play(AudioClip ac)
	{
		if (_SFXOn == 1)
		{
			MinigameAudioManager.PlayClipAtPoint(ac, Camera.main.transform.position);
		}
	}

	public static AudioClips Instance()
	{
		return _clips;
	}
}
