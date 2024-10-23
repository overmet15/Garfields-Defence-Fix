using System;
using System.Collections;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
	private const float tweeningTime = 0.6f;

	public AudioClip backgroundMusic;

	public GameObject logo;

	public UIButton playButton;

	public UIButton optionButton;

	public UIButton moreGamesButton;

	public UIButton garfieldDinerTabButton;

	public UIButton ppButton;

	public Material buttonMaterial;

	public Material backgroundMaterial;

	public static float expectedScreenRatio = 2f / 3f;

	public GameObject Glow;

	private UserProfileManager user_obj;

	private string locale = "en";

	private void Awake()
	{
		Debug.Log("Andy: this is log from TitleSceneController.cs");
		user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		locale = user_obj.getLangCode();
		Debug.Log(locale);
		LoadTextures();
	}

	private void OnAlertClick(bool result)
	{
		if (result)
		{
			Application.Quit();
		}
	}

	private void LoadTextures()
	{
		Debug.Log(Application.systemLanguage);
		Texture2D texture2D = Resources.Load("ButtonsMaterial-" + locale) as Texture2D;
		if (texture2D == null)
		{
			buttonMaterial.mainTexture = Resources.Load("ButtonsMaterial") as Texture2D;
		}
		else
		{
			buttonMaterial.mainTexture = texture2D;
		}
		texture2D = Resources.Load("TitleSceneBackgroundMaterial-" + locale) as Texture2D;
		if (texture2D == null)
		{
			if (user_obj.ChineseNewYear)
			{
				backgroundMaterial.mainTexture = Resources.Load("TitleSceneCNYBackgroundMaterial") as Texture2D;
			}
			else
			{
				backgroundMaterial.mainTexture = Resources.Load("TitleSceneBackgroundMaterial") as Texture2D;
			}
		}
		else
		{
			backgroundMaterial.mainTexture = texture2D;
		}
	}

	private void Awak()
	{
		playButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		optionButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		moreGamesButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		garfieldDinerTabButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		ppButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
	}

	private void Start()
	{
		StartCoroutine(WaitToCheckWallpaper());
		NativeAlertManager.OnClick += OnAlertClick;
		Debug.Log("+++ Title page start +++");
		iTween.MoveFrom(logo, iTween.Hash("x", logo.transform.position.x + 600f, "time", 0.4f, "easetype", iTween.EaseType.easeOutBack));
		iTween.MoveFrom(garfieldDinerTabButton.gameObject, iTween.Hash("x", garfieldDinerTabButton.transform.position.x - 360f, "time", 1f, "delay", 1.2f, "easetype", iTween.EaseType.easeOutBack, "oncompletetarget", base.gameObject, "oncomplete", "StartGlow"));
		StartGlow();
		playButton.SetValueChangedDelegate(MyDelegate);
		optionButton.SetValueChangedDelegate(MyDelegate);
		moreGamesButton.SetValueChangedDelegate(MyDelegate);
		garfieldDinerTabButton.SetValueChangedDelegate(MyDelegate);
		ppButton.SetValueChangedDelegate(MyDelegate);
		AdjustSize();
		user_obj.setCurrentScene("FD_Title");
		AudioListener.volume = user_obj.getSoundSetting();
		playButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		optionButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		moreGamesButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		garfieldDinerTabButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		ppButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic);
		if (user_obj.ShowedChallengeModePopup)
		{
			return;
		}
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		if (user_obj.getPurchasedCoins() > 0)
		{
			if (user_obj.getGameLevel() < 20)
			{
				PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, languageManagerInstance.getLangData("PaidUserBefore20"));
			}
			else
			{
				PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, languageManagerInstance.getLangData("PaidUserAfter20"));
			}
		}
		else if (user_obj.getGameLevel() < 20)
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, languageManagerInstance.getLangData("UnpaidUserBefore20"));
		}
		else
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, languageManagerInstance.getLangData("UnpaidUserAfter20"));
		}
		user_obj.ShowedChallengeModePopup = true;
	}

	private void HandleAlertButtonClickedEvent(string title)
	{
		if (title == "YES")
		{
			Application.Quit();
		}
	}

	private IEnumerator DisplayTakeover()
	{
		yield return new WaitForSeconds(0.5f);
		//MunerisController.Instance.ReportInterstitialAd();
	}

	private IEnumerator WaitToCheckWallpaper()
	{
		LiveWallpaperBridge.Balance = user_obj.getCoin();
		yield return new WaitForSeconds(0.5f);
		Debug.Log("check wall paper");
		user_obj.checkWallpaperBonus();
	}

	private void StartGlow()
	{
		iTween.ScaleTo(Glow, iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", 0.5f, "delay", 3, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "StopGlow"));
	}

	private void StopGlow()
	{
		iTween.ScaleTo(Glow, iTween.Hash("scale", new Vector3(0f, 0f, 1f), "time", 0.5f, "delay", 2, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "StartGlow"));
	}

	private void AdjustSize()
	{
		float num = (float)Screen.height / (float)Screen.width / expectedScreenRatio;
		Debug.Log(Screen.width * Screen.height);
		Debug.Log("size: " + num);
		if (num > 1f)
		{
			Camera[] allCameras = Camera.allCameras;
			foreach (Camera camera in allCameras)
			{
				Debug.Log("c.orthographicSize = " + camera.orthographicSize);
				camera.orthographic = true;
				camera.orthographicSize = num * 320f;
			}
		}
	}

	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (PopupManager.Instance.isPopping())
			{
				PopupManager.Instance.HidePopup();
				return;
			}
			EtceteraAndroidManager.alertButtonClickedEvent += alertCallback;
			EtceteraAndroid.showAlert("Warning", "Are you sure to quit the game?", "YES", "NO");
		}
	}

	private void alertCallback(string result)
	{
		HandleAlertButtonClickedEvent(result);
	}

	private void OnDisable()
	{
		NativeAlertManager.OnClick -= OnAlertClick;
	}

	private void MyDelegate(IUIObject obj)
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		if (obj == playButton)
		{
			playButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			if (user_obj.getGameLevel() > 1)
			{
				loadingManagerInstance.setSceneId("FD_Upgrade");
			}
			else
			{
				user_obj.resetTutorial(1);
				loadingManagerInstance.setSceneId("FD_Opening");
			}
			SceneManager.LoadScene("Loading");
		}
		else if (obj == moreGamesButton)
		{
			//if (!MunerisController.Instance.HasMoreGamesEvent())
			//{
			//	user_obj.setCurrentScene("FD_MoreApps");
			//	MunerisController.Instance.ReportMoreApps();
			//}
			//else
			//{
			//	MunerisController.Instance.ReportMoreGames();
			//}
			Application.OpenURL("https://www.youtube.com/watch?v=gyNtZeN_gTQ&list=PLameREsjcp75af75spbRZoHwC0ICqSeJ3");
		}
		else if (obj == optionButton)
		{
			optionButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			loadingManagerInstance.setSceneId("FD_OptionScene");
			SceneManager.LoadScene("Loading");
		}
		else if (obj == garfieldDinerTabButton)
		{
//			if (!MunerisController.Instance.HasCrossPromoEvent())
			//{
				Application.OpenURL("https://www.youtube.com/watch?v=gyNtZeN_gTQ&list=PLameREsjcp75af75spbRZoHwC0ICqSeJ3");
				user_obj.setCurrentScene("FD_MoreApps");
				//MunerisController.Instance.ReportMoreApps();
			//}
			//else
			//{
				//MunerisController.Instance.ReportCrossPromo();
			//}
		}
		else if (obj == ppButton)
		{
			Application.OpenURL("https://youtu.be/1OQbWpJeNc4");
			//MunerisController.Instance.ReportPrivacyPolicy();
		}
	}
}
