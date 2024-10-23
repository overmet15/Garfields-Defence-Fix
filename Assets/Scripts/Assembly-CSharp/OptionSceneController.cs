using System;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionSceneController : MonoBehaviour
{
	public UIButton backButton;

	public UIButton introButton;

	public UIButton soundButton;

	public UIButton resetButton;

	public UIButton infoButton;

	public SpriteText versionLabel;

	public Material optionMaterial;

	public Material buttonMaterial;

	private UserProfileManager user_obj;

	private LoadingManager Lvl_obj;

	private LanguageManager langMan;

	private void Awake()
	{
		Lvl_obj = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		LoadTextures();
	}

	private void LoadTextures()
	{
		Texture2D texture2D = Resources.Load("OptionSceneMaterial-" + user_obj.getLangCode()) as Texture2D;
		if (texture2D == null)
		{
			optionMaterial.mainTexture = Resources.Load("OptionSceneMaterial") as Texture2D;
		}
		else
		{
			optionMaterial.mainTexture = texture2D;
		}
		texture2D = Resources.Load("ButtonsMaterial-" + user_obj.getLangCode()) as Texture2D;
		if (texture2D == null)
		{
			buttonMaterial.mainTexture = Resources.Load("ButtonsMaterial") as Texture2D;
		}
		else
		{
			buttonMaterial.mainTexture = texture2D;
		}
	}

	private void Start()
	{
		backButton.SetValueChangedDelegate(onButtonClick);
		resetButton.SetValueChangedDelegate(onButtonClick);
		introButton.SetValueChangedDelegate(onButtonClick);
		soundButton.SetValueChangedDelegate(onButtonClick);
		infoButton.SetValueChangedDelegate(onInfoButtonClick);
		if (user_obj.getAndroidMarket() == "google")
		{
			versionLabel.Text = "ver." + user_obj.getVersion() + " [GP]";
		}
		else if (user_obj.getAndroidMarket() == "amazon")
		{
			versionLabel.Text = "ver." + user_obj.getVersion() + " [A]";
		}
		user_obj.setCurrentScene("FD_Option");
		PopupManager.Instance.PopupCloseHandler += getConfirmationPopupResult;
		UIButton component = GameObject.Find("Leaf").GetComponent<UIButton>();
		if (user_obj.getSoundSetting() == 1)
		{
			component.SetSize(66f, 72f);
			AudioListener.volume = 1f;
		}
		else
		{
			component.SetSize(0f, 0f);
			AudioListener.volume = 0f;
		}
		//MunerisController.Instance.ReportEvent("menu");
	}

	private void OnDestroy()
	{
		PopupManager.Instance.PopupCloseHandler -= getConfirmationPopupResult;
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
			Lvl_obj.setSceneId("FD_TitleScene");
			SceneManager.LoadScene("Loading");
		}
	}

	private void getConfirmationPopupResult(PopupManager.PopupType type, bool choice)
	{
		if (choice)
		{
			user_obj.resetGameData();
		}
	}

	private void onInfoButtonClick(IUIObject button)
	{
		Debug.Log(">>> On Info Button Click >>> ");
		//MunerisController.Instance.ReportCustomerSupport();
	}

	private void onButtonClick(IUIObject obj)
	{
		if (obj == backButton)
		{
			Lvl_obj.setSceneId("FD_TitleScene");
			SceneManager.LoadScene("Loading");
		}
		else if (obj == resetButton)
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Confirmation, langMan.getLangData("OptionScene_ResetDataConfirmation"));
		}
		else if (obj == soundButton)
		{
			UIButton component = GameObject.Find("Leaf").GetComponent<UIButton>();
			int soundSetting = user_obj.getSoundSetting();
			if (soundSetting == 1)
			{
				soundSetting = 0;
				AudioListener.volume = 0f;
				component.SetSize(0f, 0f);
			}
			else
			{
				soundSetting = 1;
				AudioListener.volume = 1f;
				component.SetSize(66f, 72f);
			}
			Debug.Log("Sound Setting: " + soundSetting);
			user_obj.setSoundSetting(soundSetting);
		}
		else if (obj == introButton)
		{
			user_obj.resetTutorial(0);
			Lvl_obj.setSceneId("FD_Opening");
			SceneManager.LoadScene("Loading");
		}
	}

	public void resetGame_lvl10()
	{
		user_obj.resetGameData_lvl10();
	}

	public void resetGame_lvl20()
	{
		user_obj.resetGameData_lvl20();
	}

	public void resetGame_lvl30()
	{
		user_obj.resetGameData_lvl30();
	}

	public void resetGame_lvl40()
	{
		user_obj.resetGameData_lvl40();
	}

	public void resetManyPoints()
	{
		user_obj.resetManyPoints();
	}
}
