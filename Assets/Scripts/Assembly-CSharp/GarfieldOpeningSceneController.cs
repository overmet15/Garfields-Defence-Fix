using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarfieldOpeningSceneController : MonoBehaviour
{
	public GameObject[] shots;

	public GameObject camera;

	public AudioClip backgroundMusic;

	public AudioClip flipSFX;

	public AudioClip zoomSFX;

	private void Start()
	{
		SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic);
		iTween.MoveBy(camera, iTween.Hash("x", 9.6f, "time", 3f, "delay", 3f, "easetype", iTween.EaseType.easeInOutSine, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotTwo"));
	}

	private void MoveToShotTwo()
	{
		SingletonMonoBehaviour<AudioManager>.Instance.Play(flipSFX);
		iTween.MoveBy(camera, iTween.Hash("y", -6.4f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotThree"));
	}

	private void MoveToShotThree()
	{
		iTween.MoveBy(camera, iTween.Hash("x", -9.6f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "delay", 2f, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotFour"));
	}

	private void MoveToShotFour()
	{
		iTween.MoveBy(camera, iTween.Hash("x", 9.6f, "y", -6.4f, "time", 0.5f, "delay", 1f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotFive"));
	}

	private void MoveToShotFive()
	{
		iTween.ScaleTo(shots[3], iTween.Hash("scale", new Vector3(2f, 2f, 2f), "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
		iTween.FadeTo(shots[3], iTween.Hash("alpha", 0f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", zoomSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotSix"));
	}

	private void MoveToShotSix()
	{
		iTween.MoveBy(shots[5], iTween.Hash("y", -6.4f, "time", 0.5f, "delay", 1f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotSeven"));
	}

	private void MoveToShotSeven()
	{
		iTween.MoveBy(camera, iTween.Hash("x", -9.6f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "delay", 1.5f, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotEight"));
	}

	private void MoveToShotEight()
	{
		iTween.ShakePosition(camera, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 0.5f, "delay", 1.5f, "easetype", iTween.EaseType.linear));
		iTween.MoveBy(camera, iTween.Hash("x", 9.6f, "y", -6.4f, "time", 0.5f, "delay", 2f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotNine"));
	}

	private void MoveToShotNine()
	{
		iTween.MoveBy(camera, iTween.Hash("x", -9.6f, "time", 0.5f, "delay", 2f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", flipSFX, "oncompletetarget", base.gameObject, "oncomplete", "MoveToShotTen"));
	}

	private void MoveToShotTen()
	{
		iTween.ScaleTo(shots[9], iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "onstarttarget", base.gameObject, "onstart", "PlaySFX", "onstartparams", zoomSFX, "oncompletetarget", base.gameObject, "oncomplete", "LoadNextScene"));
	}

	private void PlaySFX(AudioClip clip)
	{
		SingletonMonoBehaviour<AudioManager>.Instance.Play(clip);
	}

	private void LoadNextScene()
	{
		Debug.Log("NextScene");
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (userProfileManagerInstance.getIsTutorial() == 1)
		{
			loadingManagerInstance.setSceneId("Mini_Game");
		}
		else
		{
			loadingManagerInstance.setSceneId("FD_OptionScene");
		}
		SceneManager.LoadScene("Loading");
	}

	private void Skip()
	{
		Debug.Log("NextScene");
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (userProfileManagerInstance.getIsTutorial() == 1)
		{
			loadingManagerInstance.setSceneId("Mini_Game");
		}
		else
		{
			loadingManagerInstance.setSceneId("FD_OptionScene");
		}
		SceneManager.LoadScene("Loading");
	}
}
