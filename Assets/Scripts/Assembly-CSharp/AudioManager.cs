using Outblaze;
using UnityEngine;

public sealed class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	private AudioSource backgroundMusic;

	public AudioSource BackgroundMusic
	{
		get
		{
			return backgroundMusic;
		}
	}

	public void StopBackgroundMusic()
	{
		if (backgroundMusic != null)
		{
			backgroundMusic.Stop();
		}
	}

	public AudioSource PlayBackgroundMusic(AudioClip clip)
	{
		if (backgroundMusic == null)
		{
			GameObject gameObject = new GameObject("Background Music");
			Object.DontDestroyOnLoad(gameObject);
			backgroundMusic = gameObject.AddComponent<AudioSource>();
			backgroundMusic.loop = true;
		}
		if (backgroundMusic.clip != clip)
		{
			backgroundMusic.clip = clip;
			backgroundMusic.volume = 0.6f;
			backgroundMusic.loop = true;
		}
		if (!backgroundMusic.isPlaying)
		{
			backgroundMusic.Play();
		}
		return backgroundMusic;
	}

	public AudioSource Play(AudioClip clip)
	{
		return Play(clip, false);
	}

	public AudioSource Play(AudioClip clip, bool loop)
	{
		return Play(clip, loop, Vector3.zero);
	}

	public AudioSource Play(AudioClip clip, bool loop, Vector3 position)
	{
		return Play(clip, loop, position, 1f);
	}

	public AudioSource Play(AudioClip clip, bool loop, Vector3 position, float volume)
	{
		return Play(clip, loop, position, volume, 1f);
	}

	public AudioSource Play(AudioClip clip, bool loop, Vector3 position, float volume, float pitch)
	{
		GameObject gameObject = new GameObject("Audio: " + clip.name);
		gameObject.transform.position = position;
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.Play();
		if (loop)
		{
			audioSource.loop = loop;
		}
		else
		{
			Object.Destroy(gameObject, clip.length);
		}
		return audioSource;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		StopBackgroundMusic();
	}
}
