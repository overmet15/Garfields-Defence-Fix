using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MinigameAudioManager : MonoBehaviour
{
	private static float musicVolume;

	private static float soundVolumeScale;

	private static AudioSource musicAudioSource;

	public void Awake()
	{
		musicAudioSource = GetComponent<AudioSource>();
		fetchMusicVolume();
		fetchSoundVolumeScale();
		base.transform.position = Camera.main.transform.position;
	}

	public static float getSoundVolumeScale()
	{
		return soundVolumeScale;
	}

	public static void fetchMusicVolume()
	{
		GameObject gameObject = GameObject.FindWithTag("MusicManager");
		musicAudioSource.volume = ((!gameObject) ? 1f : gameObject.GetComponent<AudioSource>().volume);
	}

	public static void fetchSoundVolumeScale()
	{
		GameObject gameObject = GameObject.FindWithTag("SoundManager");
		soundVolumeScale = ((!gameObject) ? 1f : gameObject.GetComponent<AudioSource>().volume);
	}

	public static void PlayMusic()
	{
		musicAudioSource.Play();
	}

	public static void StopMusic()
	{
		musicAudioSource.Stop();
	}

	public static void PauseMusic()
	{
		musicAudioSource.Pause();
	}

	public static void PlaySound(AudioSource audioSource)
	{
		audioSource.PlayOneShot(audioSource.clip, audioSource.volume * soundVolumeScale);
	}

	public static void PlaySound(AudioSource audioSource, float audioVolume)
	{
		audioSource.PlayOneShot(audioSource.clip, audioVolume * soundVolumeScale);
	}

	public static void PlaySound(AudioSource audioSource, AudioClip ac)
	{
		audioSource.PlayOneShot(ac, audioSource.volume * soundVolumeScale);
	}

	public static void PlaySound(AudioSource audioSource, AudioClip ac, float audioVolume)
	{
		audioSource.PlayOneShot(ac, audioVolume * soundVolumeScale);
	}

	public static void PlayClipAtPoint(AudioClip ac, Vector3 pos, float audioVolume)
	{
		AudioSource.PlayClipAtPoint(ac, pos, audioVolume * soundVolumeScale);
	}

	public static void PlayClipAtPoint(AudioClip ac, Vector3 pos)
	{
		AudioSource.PlayClipAtPoint(ac, pos, soundVolumeScale);
	}
}
