using System.Collections;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneController : MonoBehaviour
{
	public GameObject camera;

	public GameObject overlay;

	public GameObject[] subtitles;

	public float[] durations;

	public UIButton skipBtn;

	private int subtitleIndex;

	private int count;

	private void Awake()
	{
		skipBtn.SetControlState(UIButton.CONTROL_STATE.DISABLED);
	}

	private void Start()
	{
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		userProfileManagerInstance.setCurrentScene("FD_Opening");
		count = 0;
		skipBtn.SetValueChangedDelegate(SkipButtonDelegate);
		SingletonMonoBehaviour<AudioManager>.Instance.StopBackgroundMusic();
		FadeOutOverlay();
		PanCamera();
		StartCoroutine(StartSubtitle());
	}

	private void Update()
	{
		count++;
		if (count == 50)
		{
			skipBtn.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		}
	}

	private IEnumerator StartSubtitle()
	{
		yield return new WaitForSeconds(1.2f);
		FadeInSubtitle();
	}

	private void FadeOutOverlay()
	{
		overlay.GetComponent<Renderer>().material.color = Color.black;
		iTween.FadeTo(overlay, iTween.Hash("alpha", 0, "time", 2f, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "FadeInOverlay"));
	}

	private void FadeInOverlay()
	{
		overlay.GetComponent<Renderer>().enabled = false;
		iTween.FadeTo(overlay, iTween.Hash("alpha", 1, "time", 2f, "delay", 57f, "onstarttarget", base.gameObject, "onstart", "OnFadeInStart", "easetype", iTween.EaseType.linear));
	}

	private void OnFadeInStart()
	{
		overlay.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);
		overlay.GetComponent<Renderer>().enabled = true;
	}

	private void PanCamera()
	{
		iTween.MoveTo(camera, iTween.Hash("x", 960, "time", 58, "delay", 2f, "easetype", iTween.EaseType.easeInOutSine));
	}

	private void FadeInSubtitle()
	{
		iTween.FadeFrom(subtitles[subtitleIndex], iTween.Hash("alpha", 0, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "oncompletetarget", base.gameObject, "oncomplete", "FadeOutSubtitle"));
		subtitles[subtitleIndex].SetActive(true);
	}

	private void FadeOutSubtitle()
	{
		iTween.FadeTo(subtitles[subtitleIndex], iTween.Hash("alpha", 0, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "delay", durations[subtitleIndex] - 2f, "oncompletetarget", base.gameObject, "oncomplete", "HideSubtitle"));
	}

	private void HideSubtitle()
	{
		subtitles[subtitleIndex].SetActive(false);
		subtitleIndex++;
		if (subtitleIndex < subtitles.Length)
		{
			FadeInSubtitle();
		}
		else
		{
			LoadNextScene();
		}
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

	private void SkipButtonDelegate(IUIObject obj)
	{
		LoadNextScene();
	}
}
