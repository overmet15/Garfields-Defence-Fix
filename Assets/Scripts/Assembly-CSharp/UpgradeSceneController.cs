using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSceneController : MonoBehaviour
{
	private enum WarningPopupType
	{
		None = 0,
		NotEnoughCoins = 1,
		ChallengeMode = 2
	}

	private enum ConfirmationPopupType
	{
		None = 0,
		BuyNightMode = 1,
		ResetNightMode = 2,
		BuyHalloweenMode = 3,
		ResetHalloweenMode = 4,
		FreeUnlock = 5,
		WeiboShare = 6
	}

	[CompilerGenerated]
	private sealed class _003CprocessUpgrade_003Ec__AnonStorey1F
	{
		internal CargoManager.HeroUpgradeData heroUpgradeData;

		internal int nextLevel;

		internal int maxLevel;

		internal void _003C_003Em__3()
		{
			int num = UnityEngine.Random.Range(1, heroUpgradeData.extraLevel);
			nextLevel = Mathf.Clamp(nextLevel + num, 0, maxLevel);
			Debug.Log("Randomized level = " + nextLevel);
		}
	}

	private ImageManager imageManager;

	public AudioClip backgroundMusic;

	public GameObject challengeModeButton;

	public GameObject challengeModePopup;

	public GameObject challengeModeFreeUnlockButton;

	public GameObject heroUpgradeListItem;

	public GameObject spellUpgradeListItem;

	public GameObject armyUpgradeListItem;

	public GameObject itemUpgradeListItem;

	public UIScrollList heroScrollList;

	public UIScrollList numenScrollList;

	public UIScrollList forestScrollList;

	public UIPanelManager panelManager;

	public SpriteText coinLabel;

	public SpriteText SeasonNum;

	public UIPanel popupPanel;

	public SpriteText upgradeTitleLable;

	public AudioSource purchaseSound;

	public GameObject ThanksgivingBtn;

	private UserProfileManager user_obj;

	private StarStrike_ArmyUnitConfiguration armyUnitConfiguration;

	private StarStrike_SpecialAttackConfiguration spAttackConfiguration;

	private FD_ForrestConfiguration forrestConfiguration;

	private LanguageManager langMan;

	public static float expectedScreenRatio = 2f / 3f;

	private Vector3 popupIcon_OriginalPos;

	private Vector3 popupCurrentDemage_OriginalPos;

	private Vector3 popupNextDemage_OriginalPos;

	private Vector3 popupCurrentHP_OriginalPos;

	private Vector3 popupNextHP_OriginalPos;

	private bool gotPopupOriginalPos;

	private int challengeModePrice = 5000;

	public TextAsset SChi_BigFont;

	public Material SChi_BigFontMat;

	public TextAsset SChi_SmallFont;

	public Material SChi_SmallFontMat;

	public TextAsset TChi_BigFont;

	public Material TChi_BigFontMat;

	public TextAsset TChi_SmallFont;

	public Material TChi_SmallFontMat;

	public TextAsset KO_BigFont;

	public Material KO_BigFontMat;

	public TextAsset KO_SmallFont;

	public Material KO_SmallFontMat;

	public TextAsset JA_BigFont;

	public Material JA_BigFontMat;

	public TextAsset JA_SmallFont;

	public Material JA_SmallFontMat;

	public TextAsset FR_BigFont;

	public Material FR_BigFontMat;

	public TextAsset FR_SmallFont;

	public Material FR_SmallFontMat;

	public TextAsset DE_BigFont;

	public Material DE_BigFontMat;

	public TextAsset DE_SmallFont;

	public Material DE_SmallFontMat;

	public Material buttonMaterial;

	public Material upgradeSceneMaterial;

	public Material inAppsPurchaseMaterial;

	private WarningPopupType warningPopupType;

	private ConfirmationPopupType confirmationPopupType;

	private void Awake()
	{
		user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (user_obj.getPurchasedCoins() > 0)
		{
			if (!user_obj.isUnlocked_NMode())
			{
				user_obj.setUnlocked_NMode();
				//MunerisController.Instance.ReportEvent("Unlocked Night Mode");
			}
			if (!user_obj.isUnlocked_HalloweenMode())
			{
				user_obj.setUnlocked_HalloweenMode();
				//MunerisController.Instance.ReportEvent("Unlocked Halloween Mode");
			}
		}
		imageManager = ImageManager.Instance;
		popupPanel.Dismiss();
		LoadTextures();
	}

	private void Start()
	{
		gotPopupOriginalPos = false;
		LoadUserData();
		LoadArmyUnitDefinition();
		BuildNumenList();
		StartCoroutine(WaitAndPlay(1f));
		AdjustSize();
		PopupManager.Instance.PopupCloseHandler += getConfirmationPopupResult;
		SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic);
		int num = UnityEngine.Random.Range(0, 9);
		Debug.Log(">>>>> random: " + num);
		if (num > 6)
		{
			Debug.Log(">>>>SHOW INTERSTITIAL AD - Shown.");
			//MunerisController.Instance.ReportInterstitialAd();
		}
		if (user_obj.getCurrentPlayMode() != 0)
		{
			ShowChallengeModePopup();
		}
		if (!user_obj.isUnlocked_NMode() || !user_obj.isUnlocked_HalloweenMode())
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("time", 0.3f);
			hashtable.Add("x", 1.1f);
			hashtable.Add("y", 1.1f);
			hashtable.Add("z", 1.1f);
			hashtable.Add("looptype", iTween.LoopType.pingPong);
			iTween.ScaleTo(ThanksgivingBtn, hashtable);
		}
		else
		{
			ThanksgivingBtn.transform.localScale = new Vector3(0f, 0f, 0f);
		}
		challengeModeButton.SetActive(true);
	}

	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
		if (Input.GetKeyUp(KeyCode.Escape) && PopupManager.Instance.isPopping())
		{
			PopupManager.Instance.HidePopup();
		}
	}

	public IEnumerator WaitAndPlay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (GiftManager.CanGetGift)
		{
			int cookie = GiftManager.TodayCookieGift;
			int itemType = GiftManager.TodayItemTypeGift;
			int potion = GiftManager.TodayPotionGift;
			user_obj.addCoin(cookie);
			Debug.Log("ItemType:" + itemType);
			user_obj.addItemCount("item0" + itemType + "_lvl", potion);
			GiftManager.GetGift();
			PopupManager.Instance.ShowDailyAwardPopup();
		}
		else
		{
			CheckIsNewLevel();
		}
		BuildForestList();
	}

	private void OnDestroy()
	{
		PopupManager.Instance.PopupCloseHandler -= getConfirmationPopupResult;
	}

	private void LoadTextures()
	{
		Texture2D texture2D = Resources.Load("ButtonsMaterial-" + user_obj.getLangCode()) as Texture2D;
		if (texture2D == null)
		{
			buttonMaterial.mainTexture = Resources.Load("ButtonsMaterial") as Texture2D;
		}
		else
		{
			buttonMaterial.mainTexture = texture2D;
		}
	}

	private void AdjustSize()
	{
		float num = (float)Screen.height / (float)Screen.width / expectedScreenRatio;
		if (num > 1f)
		{
			Camera[] allCameras = Camera.allCameras;
			foreach (Camera camera in allCameras)
			{
				camera.orthographic = true;
				camera.orthographicSize = num * 320f;
			}
		}
	}

	private void LoadUserData()
	{
		armyUnitConfiguration = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		spAttackConfiguration = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		forrestConfiguration = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
		AudioListener.volume = user_obj.getSoundSetting();
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		if (user_obj.getLangCode() == "ko")
		{
			SeasonNum.SetFont(KO_BigFont, KO_BigFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			SeasonNum.SetFont(JA_BigFont, JA_BigFontMat);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			SeasonNum.SetFont(FR_BigFont, FR_BigFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			SeasonNum.SetFont(DE_BigFont, DE_BigFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			SeasonNum.SetFont(SChi_BigFont, SChi_BigFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			SeasonNum.SetFont(TChi_BigFont, TChi_BigFontMat);
		}
		string langData = langMan.getLangData("UpgradeScene_Season_Txt");
		Debug.Log("Game Level:" + user_obj.getGameLevel());
		SeasonNum.Text = langData.Replace("#", user_obj.getGameLevel().ToString());
		if (user_obj.getLangCode() == "ko")
		{
			upgradeTitleLable.SetFont(KO_BigFont, KO_BigFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			upgradeTitleLable.SetFont(JA_BigFont, JA_BigFontMat);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			upgradeTitleLable.SetFont(FR_BigFont, FR_BigFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			upgradeTitleLable.SetFont(DE_BigFont, DE_BigFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			upgradeTitleLable.SetFont(SChi_BigFont, SChi_BigFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			upgradeTitleLable.SetFont(TChi_BigFont, TChi_BigFontMat);
		}
		upgradeTitleLable.Text = langMan.getLangData("UpgradeScene_Title_Txt");
		updateCoinLabel();
	}

	public void updateCoinLabel()
	{
		if (user_obj == null)
		{
			user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		}
		int num = user_obj.getCoin();
		if (num < 0)
		{
			num = 0;
		}
		coinLabel.Text = num.ToString();
	}

	private void CheckIsNewLevel()
	{
		Debug.Log("+++++ Check is New Level!!!! ++++++");
		if (user_obj == null)
		{
			user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		}
		if (user_obj.getIsNewLevel() == 1)
		{
			showNewUnlockItems(user_obj.getGameLevel());
			user_obj.resetIsNewLevel();
		}
	}

	private void showNewUnlockItems(int currentLvl)
	{
		bool flag = false;
		ArrayList specialAttackArray = spAttackConfiguration.GetSpecialAttackArray();
		foreach (string item in specialAttackArray)
		{
			StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(item);
			if (int.Parse(definition.GetAttributeValue("UnlockLevel")) == currentLvl && !flag)
			{
				flag = true;
				string imgPath = "Images/" + definition.GetAttributeValue("icon");
				string attributeValue;
				string attributeValue2;
				if (user_obj.getLangCode() != "en")
				{
					attributeValue = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
					attributeValue2 = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
				}
				else
				{
					attributeValue = definition.GetAttributeValue("UnitName");
					attributeValue2 = definition.GetAttributeValue("Description");
				}
				PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, attributeValue, imgPath, attributeValue2, langMan.getLangData("UnlockNotification_Title"));
				break;
			}
		}
		if (!flag)
		{
			ArrayList goodGuy = armyUnitConfiguration.GetGoodGuy();
			foreach (string item2 in goodGuy)
			{
				StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(item2);
				if (int.Parse(unitDefinition.GetAttributeValue("UnlockLevel")) == currentLvl && !flag)
				{
					flag = true;
					string imgPath2 = "Images/" + unitDefinition.GetAttributeValue("icon");
					string attributeValue3;
					string attributeValue4;
					if (user_obj.getLangCode() != "en")
					{
						attributeValue3 = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
						attributeValue4 = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_Description");
					}
					else
					{
						attributeValue3 = unitDefinition.GetAttributeValue("UnitName");
						attributeValue4 = unitDefinition.GetAttributeValue("Description");
					}
					PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, attributeValue3, imgPath2, attributeValue4, langMan.getLangData("UnlockNotification_Title"));
					break;
				}
			}
		}
		if (!flag)
		{
			ArrayList forrestArray = forrestConfiguration.GetForrestArray();
			foreach (string item3 in forrestArray)
			{
				StarStrike_ObjectDefinition definition2 = forrestConfiguration.GetDefinition(item3);
				if (int.Parse(definition2.GetAttributeValue("UnlockLevel")) == currentLvl && !flag)
				{
					flag = true;
					string imgPath3 = "Images/" + definition2.GetAttributeValue("icon");
					string attributeValue5;
					string attributeValue6;
					if (user_obj.getLangCode() != "en")
					{
						attributeValue5 = definition2.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
						attributeValue6 = definition2.GetAttributeValue(user_obj.getLangCode() + "_Description");
					}
					else
					{
						attributeValue5 = definition2.GetAttributeValue("UnitName");
						attributeValue6 = definition2.GetAttributeValue("Description");
					}
					PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, attributeValue5, imgPath3, attributeValue6, langMan.getLangData("UnlockNotification_Title"));
					break;
				}
			}
		}
		if (!flag && user_obj.getGameLevel() == 20)
		{
			if (user_obj.getPurchasedCoins() > 0)
			{
				PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, langMan.getLangData("PaidUserAfter20"));
			}
			else
			{
				PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, langMan.getLangData("UnpaidUserAfter20"));
			}
		}
	}

	private void showItemInfo(string name, string imagePath, string description)
	{
		PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, name, imagePath, description, string.Empty);
	}

	private void onPopupBackButtonClicked(IUIObject button)
	{
		popupPanel.Dismiss();
	}

	private void processUpgrade(IUIObject obj)
	{
		_003CprocessUpgrade_003Ec__AnonStorey1F _003CprocessUpgrade_003Ec__AnonStorey1F = new _003CprocessUpgrade_003Ec__AnonStorey1F();
		Hashtable hashtable = (Hashtable)obj.Data;
		int num = int.Parse(hashtable["updatePrice"].ToString());
		_003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel = int.Parse(hashtable["nextLevel"].ToString());
		StarStrike_ObjectDefinition starStrike_ObjectDefinition = armyUnitConfiguration.GetUnitDefinition(hashtable["objName"].ToString()) ?? spAttackConfiguration.GetDefinition(hashtable["objName"].ToString()) ?? forrestConfiguration.GetDefinition(hashtable["objName"].ToString());
		ArrayList levelArray = starStrike_ObjectDefinition.GetLevelArray("level");
		_003CprocessUpgrade_003Ec__AnonStorey1F.maxLevel = 0;
		if (levelArray.Count > 0)
		{
			_003CprocessUpgrade_003Ec__AnonStorey1F.maxLevel = levelArray.Count - 1;
			Debug.LogFormat("Hero = {0} | max Level = {1}", starStrike_ObjectDefinition.Name(), _003CprocessUpgrade_003Ec__AnonStorey1F.maxLevel);
		}
		_003CprocessUpgrade_003Ec__AnonStorey1F.heroUpgradeData = CargoManager.GetHeroUpgradeData();
		if (_003CprocessUpgrade_003Ec__AnonStorey1F.heroUpgradeData.isEnableAndValid)
		{
			RandomUtils.RandomAction(_003CprocessUpgrade_003Ec__AnonStorey1F.heroUpgradeData.prob, _003CprocessUpgrade_003Ec__AnonStorey1F._003C_003Em__3, null);
		}
		string text = hashtable["key"].ToString();
		if (user_obj.getCoin() >= num)
		{
			user_obj.addCoin(-1 * num);
			user_obj.addCoinsSpent(num);
			updateCoinLabel();
			Debug.Log(string.Concat("+++++ UPGRADED: ", hashtable["objName"], ", ", text, " to ", _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel, ", ", user_obj.getTotalCoinsSpent()));
			Debug.Log("++++ ItemType: " + hashtable["itemType"].ToString() + "++++ Item: " + hashtable["type"].ToString());
			if (hashtable["itemType"].ToString() == "item")
			{
				StarStrike_ObjectDefinition definition = forrestConfiguration.GetDefinition(hashtable["objName"].ToString());
				user_obj.addItemCount(definition.GetAttributeValue("key"), 1);
			}
			else if (hashtable["type"].ToString() == "Army")
			{
				Debug.Log("+++ Army Upgrade +++ ");
				PlayerPrefs.SetInt(text, _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel);
			}
			else if (hashtable["objName"].ToString() != "Weapon1" && hashtable["objName"].ToString() != "Weapon2")
			{
				Debug.Log("+++ Not weapon +++ ");
				PlayerPrefs.SetInt(text, _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel);
			}
			else
			{
				Debug.Log("+++ Hero Upgrade +++ ");
				StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition("Hero");
				int @int = PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key"));
				@int++;
				PlayerPrefs.SetInt(text, @int);
				if (hashtable["objName"].ToString() == "Weapon2")
				{
					PlayerPrefs.SetInt("Weapon1_lvl", @int);
					RefreshForestList(0, "Weapon1");
				}
			}
			if (hashtable["objName"].ToString() == "06NewSpecialAttackSlot")
			{
				user_obj.setTotalSpAttackSlot(3);
			}
			if (hashtable["objName"].ToString() == "Hero")
			{
				if (_003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel == 1 && !PlayerPrefs.HasKey("PPA_Devout"))
				{
					Debug.Log(">>> Action Complete: PPA Devout");
					//MunerisController.Instance.ReportEvent(user_obj.getPPA_Devout());
					PlayerPrefs.SetInt("PPA_Devout", 1);
				}
				if (PlayerPrefs.GetInt("Weapon1_lvl") > 0)
				{
					PlayerPrefs.SetInt("Weapon1_lvl", _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel + 1);
				}
				if (PlayerPrefs.GetInt("Weapon2_lvl") > 0)
				{
					PlayerPrefs.SetInt("Weapon2_lvl", _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel + 1);
				}
			}
			if (hashtable["type"].ToString() == "Hero")
			{
				RefreshHero();
			}
			else if (hashtable["type"].ToString() == "SpecialAttack")
			{
				RefreshSpecialAttack(int.Parse(hashtable["index"].ToString()), hashtable["objName"].ToString());
			}
			else if (hashtable["type"].ToString() == "Army")
			{
				RefreshNumenList(int.Parse(hashtable["index"].ToString()), hashtable["objName"].ToString());
			}
			else
			{
				RefreshForestList(int.Parse(hashtable["index"].ToString()), hashtable["objName"].ToString());
			}
			Debug.Log("=++++++ Obj Name: " + hashtable["objName"]);
			if (hashtable["objName"].ToString() == "Weapon1" || hashtable["objName"].ToString() == "Weapon2")
			{
				RefreshHero();
			}
			Debug.Log("---- Dictionary ----");
			string value = string.Concat(hashtable["objName"], "_", _003CprocessUpgrade_003Ec__AnonStorey1F.nextLevel);
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add("Item Data", value);
			//MunerisController.Instance.ReportEvent("Item Update", hashtable2);
			popupPanel.Dismiss();
		}
		else
		{
			popupPanel.Dismiss();
			notEnoughPointOnClick(null);
		}
	}

	private void UpdateChallengeModePopup()
	{
		if (user_obj.isUnlocked_NMode())
		{
			challengeModePopup.transform.Find("Panel/Night Mode/BuyButton").gameObject.SetActiveRecursivelyLegacy(false);
			if (user_obj.getIsReachedMaxLevel_NMode() > 0)
			{
				challengeModePopup.transform.Find("Panel/Night Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(false);
			}
			else
			{
				challengeModePopup.transform.Find("Panel/Night Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(true);
			}
			if (user_obj.getIsReachedMaxLevel_NMode() > 0)
			{
				challengeModePopup.transform.Find("Panel/Night Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(true);
				challengeModePopup.transform.Find("Panel/Night Mode/Progress").GetComponent<SpriteText>().Text = "CLEARED";
			}
			else
			{
				challengeModePopup.transform.Find("Panel/Night Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(false);
				challengeModePopup.transform.Find("Panel/Night Mode/Progress").GetComponent<SpriteText>().Text = user_obj.getGameLevel_NMode() + "/" + user_obj.getMaxLevel_NMode();
			}
		}
		else
		{
			challengeModePopup.transform.Find("Panel/Night Mode/BuyButton").gameObject.SetActiveRecursivelyLegacy(true);
			challengeModePopup.transform.Find("Panel/Night Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(false);
			challengeModePopup.transform.Find("Panel/Night Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(false);
			challengeModePopup.transform.Find("Panel/Night Mode/Progress").GetComponent<SpriteText>().Text = "LOCKED";
		}
		if (user_obj.isUnlocked_HalloweenMode())
		{
			challengeModePopup.transform.Find("Panel/Halloween Mode/BuyButton").gameObject.SetActiveRecursivelyLegacy(false);
			if (user_obj.getIsReachedMaxLevel_HalloweenMode() > 0)
			{
				challengeModePopup.transform.Find("Panel/Halloween Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(false);
			}
			else
			{
				challengeModePopup.transform.Find("Panel/Halloween Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(true);
			}
			if (user_obj.getIsReachedMaxLevel_HalloweenMode() > 0)
			{
				challengeModePopup.transform.Find("Panel/Halloween Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(true);
				challengeModePopup.transform.Find("Panel/Halloween Mode/Progress").GetComponent<SpriteText>().Text = "CLEARED";
			}
			else
			{
				challengeModePopup.transform.Find("Panel/Halloween Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(false);
				challengeModePopup.transform.Find("Panel/Halloween Mode/Progress").GetComponent<SpriteText>().Text = user_obj.getGameLevel_HalloweenMode() + "/" + user_obj.getMaxLevel_HalloweenMode();
			}
		}
		else
		{
			challengeModePopup.transform.Find("Panel/Halloween Mode/BuyButton").gameObject.SetActiveRecursivelyLegacy(true);
			challengeModePopup.transform.Find("Panel/Halloween Mode/Play Button").gameObject.SetActiveRecursivelyLegacy(false);
			challengeModePopup.transform.Find("Panel/Halloween Mode/Reset Button").gameObject.SetActiveRecursivelyLegacy(false);
			challengeModePopup.transform.Find("Panel/Halloween Mode/Progress").GetComponent<SpriteText>().Text = "LOCKED";
		}
		if (user_obj.getPurchasedCoins() > 0)
		{
			challengeModeFreeUnlockButton.SetActiveRecursivelyLegacy(false);
		}
	}

	public void ShowChallengeModePopup()
	{
		int num = 20;
		if (Application.isEditor)
		{
			num = 1;
		}
		if (user_obj.getGameLevel() < num)
		{
			warningPopupType = WarningPopupType.ChallengeMode;
			if (user_obj.getPurchasedCoins() > 0)
			{
				PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, string.Empty, "ChallengeModeIcon", langMan.getLangData("PaidUserBefore20"), string.Empty);
			}
			else
			{
				PopupManager.Instance.ShowUnlockPopup(PopupManager.PopupType.UnlockedItem, string.Empty, "ChallengeModeIcon", langMan.getLangData("UnpaidUserBefore20"), string.Empty);
			}
		}
		else if (challengeModePopup != null && !challengeModePopup.activeInHierarchy)
		{
			challengeModePopup.SetActiveRecursivelyLegacy(true);
			Debug.Log("elvgjejlejkvhekvkwjk");
			UpdateChallengeModePopup();
		}
	}

	public void CloseChallengeModePopup()
	{
		if (challengeModePopup != null && challengeModePopup.activeInHierarchy)
		{
			user_obj.setCurrentPlayMode(InGamePlayMode.NORMAL);
			challengeModePopup.SetActiveRecursivelyLegacy(false);
		}
	}

	private void BuyNightMode()
	{
		confirmationPopupType = ConfirmationPopupType.BuyNightMode;
		if (user_obj.getCoin() < challengeModePrice)
		{
			notEnoughPointOnClick(null);
		}
		else
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Confirmation, langMan.getLangData("NightModeWarning").Replace("#", challengeModePrice.ToString()));
		}
	}

	private void PlayNightMode()
	{
		user_obj.setCurrentPlayMode(InGamePlayMode.NIGHT_MODE);
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		loadingManagerInstance.setSceneId("FD_Preparation");
		SceneManager.LoadScene("Loading");
	}

	private void ResetNightMode()
	{
		confirmationPopupType = ConfirmationPopupType.ResetNightMode;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Confirmation, langMan.getLangData("NightModeResetWarning"));
	}

	private void BuyHalloweenMode()
	{
		confirmationPopupType = ConfirmationPopupType.BuyHalloweenMode;
		if (user_obj.getCoin() < challengeModePrice)
		{
			notEnoughPointOnClick(null);
		}
		else
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Confirmation, langMan.getLangData("NightModeWarning").Replace("#", challengeModePrice.ToString()));
		}
	}

	private void PlayHalloweenMode()
	{
		user_obj.setCurrentPlayMode(InGamePlayMode.HALLOWEEN);
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		loadingManagerInstance.setSceneId("FD_Preparation");
		SceneManager.LoadScene("Loading");
	}

	private void ResetHalloweenMode()
	{
		confirmationPopupType = ConfirmationPopupType.ResetHalloweenMode;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Confirmation, langMan.getLangData("NightModeResetWarning"));
	}

	private void ShowItemInfo(IUIObject obj)
	{
		Hashtable hashtable = (Hashtable)obj.Data;
		string imagePath = "Images/" + hashtable["icon"];
		showItemInfo(hashtable["name"].ToString(), imagePath, hashtable["description"].ToString());
	}

	private void OnUpgradeButtonClicked(IUIObject obj)
	{
		Hashtable hashtable = (Hashtable)obj.Data;
		popupPanel.BringIn();
		int num = int.Parse(hashtable["currentLevel"].ToString());
		int num2 = num + 1;
		if (num < 0)
		{
			num = 0;
		}
		bool flag = false;
		string text = string.Empty;
		StarStrike_ObjectDefinition starStrike_ObjectDefinition;
		ArrayList levelArray;
		if (hashtable["type"].ToString() == "Hero" || hashtable["type"].ToString() == "Army")
		{
			starStrike_ObjectDefinition = armyUnitConfiguration.GetUnitDefinition(hashtable["objName"].ToString());
			levelArray = starStrike_ObjectDefinition.GetLevelArray("level");
		}
		else if (hashtable["type"].ToString() == "SpecialAttack")
		{
			starStrike_ObjectDefinition = spAttackConfiguration.GetDefinition(hashtable["objName"].ToString());
			levelArray = starStrike_ObjectDefinition.GetLevelArray("level");
		}
		else
		{
			starStrike_ObjectDefinition = forrestConfiguration.GetDefinition(hashtable["objName"].ToString());
			if (int.Parse(starStrike_ObjectDefinition.GetAttributeValue("needToBuy")) == 1)
			{
				flag = true;
			}
			levelArray = starStrike_ObjectDefinition.GetLevelArray("level");
			text = ((!starStrike_ObjectDefinition.HasAttribute("type")) ? "-" : starStrike_ObjectDefinition.GetAttributeValue("type"));
		}
		if (text == "item")
		{
			num2 = num + 1;
		}
		else if (num2 >= levelArray.Count)
		{
			num2 = num;
		}
		SpriteText component = popupPanel.transform.Find("Name").GetComponent<SpriteText>();
		if (user_obj.getLangCode() == "ko")
		{
			component.SetFont(KO_BigFont, KO_BigFontMat);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else if (user_obj.getLangCode() == "ja")
		{
			component.SetFont(JA_BigFont, JA_BigFontMat);
			component.SetCharacterSize(40f);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else if (user_obj.getLangCode() == "fr")
		{
			component.SetFont(FR_BigFont, FR_BigFontMat);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else if (user_obj.getLangCode() == "de")
		{
			component.SetFont(DE_BigFont, DE_BigFontMat);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			component.SetFont(SChi_BigFont, SChi_BigFontMat);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			component.SetFont(TChi_BigFont, TChi_BigFontMat);
			component.SetCharacterSize(30f);
			component.SetLineSpacing(1f);
			component.Text = starStrike_ObjectDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
		}
		else
		{
			component.Text = starStrike_ObjectDefinition.GetAttributeValue("UnitName").ToUpper();
		}
		UIButton component2 = popupPanel.transform.Find("Icon").GetComponent<UIButton>();
		component2.SetTexture(imageManager.GetImage("Images/" + starStrike_ObjectDefinition.GetAttributeValue("icon")));
		UIButton component3 = popupPanel.transform.Find("Icon Background").GetComponent<UIButton>();
		if (!gotPopupOriginalPos)
		{
			popupIcon_OriginalPos = new Vector3(component2.transform.position.x, component2.transform.position.y, component2.transform.position.z);
		}
		if (hashtable["type"].ToString() == "Forest" && (num == 0 || text == "item" || text == "goldenSlot"))
		{
			component2.transform.position = new Vector3(popupIcon_OriginalPos.x + 190f, popupIcon_OriginalPos.y, component2.transform.position.z);
			component3.transform.position = new Vector3(popupIcon_OriginalPos.x + 190f, popupIcon_OriginalPos.y, component3.transform.position.z);
		}
		else
		{
			component2.transform.position = new Vector3(popupIcon_OriginalPos.x, popupIcon_OriginalPos.y, component2.transform.position.z);
			component3.transform.position = new Vector3(popupIcon_OriginalPos.x, popupIcon_OriginalPos.y, component3.transform.position.z);
		}
		UIButton component4 = popupPanel.transform.Find("CurrentDamage").GetComponent<UIButton>();
		UIButton component5 = popupPanel.transform.Find("CurrentHP").GetComponent<UIButton>();
		UIButton component6 = popupPanel.transform.Find("CurrentHeal").GetComponent<UIButton>();
		if (!gotPopupOriginalPos)
		{
			popupCurrentDemage_OriginalPos = component4.transform.position;
			popupCurrentHP_OriginalPos = component5.transform.position;
		}
		FD_ObjectLevelDefinition fD_ObjectLevelDefinition = ((!(text == "item")) ? (levelArray[num] as FD_ObjectLevelDefinition) : (levelArray[0] as FD_ObjectLevelDefinition));
		int num3 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice").ToString());
		int num4;
		int num6;
		if (hashtable["type"].ToString() == "Forest" && (num == 0 || text == "item" || text == "goldenSlot"))
		{
			SpriteText component7 = popupPanel.transform.Find("CurrentLevel").GetComponent<SpriteText>();
			component7.Text = string.Empty;
			component4.Text = string.Empty;
			component4.SetSize(0f, 0f);
			component5.Text = string.Empty;
			component5.SetSize(0f, 0f);
			component6.Text = string.Empty;
			component6.SetSize(0f, 0f);
		}
		else
		{
			SpriteText component8 = popupPanel.transform.Find("CurrentLevel").GetComponent<SpriteText>();
			if (user_obj.getLangCode() == "ko")
			{
				component8.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				component8.SetFont(JA_SmallFont, JA_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				component8.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				component8.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				component8.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				component8.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (flag)
			{
				component8.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", num.ToString());
			}
			else
			{
				component8.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num + 1).ToString());
			}
			num4 = ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")));
			if (hashtable["type"].ToString() == "Hero")
			{
				FD_ObjectLevelDefinition currentLevel = forrestConfiguration.GetCurrentLevel("Weapon2");
				if (currentLevel == null)
				{
					currentLevel = forrestConfiguration.GetCurrentLevel("Weapon1");
				}
				if (currentLevel != null)
				{
					num4 = int.Parse(currentLevel.GetAttributeValue("attack"));
				}
			}
			if (num4 > -1)
			{
				if (hashtable["objName"].ToString() == "Army_Owl")
				{
					component6.Text = Mathf.Floor((float)num4 * 1.2f).ToString();
					component6.SetSize(25f, 25f);
					component4.Text = string.Empty;
					component4.SetSize(0f, 0f);
				}
				else
				{
					float num5 = Mathf.Floor((float)num4 * 1.2f);
					if (hashtable["objName"].ToString() == "Rein")
					{
						component4.Text = num4 + "%";
					}
					else
					{
						component4.Text = num5.ToString();
					}
					component4.SetSize(55f, 57f);
					component6.Text = string.Empty;
					component6.SetSize(0f, 0f);
				}
			}
			else
			{
				component4.Text = string.Empty;
				component4.SetSize(0f, 0f);
			}
			if (hashtable["type"].ToString() == "Forest")
			{
				component4.transform.position = new Vector3(popupCurrentDemage_OriginalPos.x + 20f, popupCurrentDemage_OriginalPos.y, popupCurrentDemage_OriginalPos.z);
			}
			else
			{
				component4.transform.position = popupCurrentDemage_OriginalPos;
			}
			num6 = ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")));
			if (num6 > -1)
			{
				component5.Text = Mathf.Floor((float)num6 * 1.2f).ToString();
				component5.SetSize(42f, 37f);
				if (num4 == -1)
				{
					component5.transform.position = component4.transform.position;
				}
				else
				{
					component5.transform.position = popupCurrentHP_OriginalPos;
				}
			}
			else
			{
				component5.Text = string.Empty;
				component5.SetSize(0f, 0f);
			}
		}
		fD_ObjectLevelDefinition = ((!(text == "item")) ? (levelArray[num2] as FD_ObjectLevelDefinition) : (levelArray[0] as FD_ObjectLevelDefinition));
		SpriteText component9 = popupPanel.transform.Find("NewLevel").GetComponent<SpriteText>();
		SpriteText component10 = popupPanel.transform.Find("NewWeapon").GetComponent<SpriteText>();
		if (user_obj.getLangCode() == "ko")
		{
			component10.SetFont(KO_SmallFont, KO_SmallFontMat);
			component9.SetFont(KO_SmallFont, KO_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			component10.SetFont(JA_SmallFont, JA_SmallFontMat);
			component10.SetCharacterSize(25f);
			Vector3 position = component10.transform.position;
			component10.transform.position = new Vector3(position.x, -16f, position.z);
			component9.SetFont(JA_SmallFont, JA_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			component10.SetFont(FR_SmallFont, FR_SmallFontMat);
			component9.SetFont(FR_SmallFont, FR_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			component10.SetFont(DE_SmallFont, DE_SmallFontMat);
			component9.SetFont(DE_SmallFont, DE_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			component10.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			component9.SetFont(SChi_SmallFont, SChi_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			component10.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			component9.SetFont(TChi_SmallFont, TChi_SmallFontMat);
		}
		if (hashtable["objName"].ToString() == "Weapon1")
		{
			component10.Text = langMan.getLangData("UnlockWeapon1");
			component9.Text = string.Empty;
		}
		else if (hashtable["objName"].ToString() == "Weapon2")
		{
			component10.Text = langMan.getLangData("UnlockWeapon2");
			component9.Text = string.Empty;
		}
		else if (hashtable["objName"].ToString() == "05LittleTreePlants")
		{
			component10.Text = langMan.getLangData("SmithLevelUpgrade");
			component9.Text = string.Empty;
		}
		else if (hashtable["objName"].ToString() == "06NewSpecialAttackSlot")
		{
			component10.Text = langMan.getLangData("ExtraSpecialAttackSlot");
			component9.Text = string.Empty;
		}
		else if (text == "item")
		{
			component10.Text = "x 1";
			component9.Text = string.Empty;
		}
		else if (text == "goldenSlot")
		{
			component10.Text = langMan.getLangData("GoldenArmySlot").Replace("#", num2.ToString());
			component9.Text = string.Empty;
		}
		else
		{
			if (flag)
			{
				component9.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", num2.ToString());
			}
			else if (CargoManager.GetHeroUpgradeData().isEnableAndValid)
			{
				component9.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", "?");
			}
			else
			{
				component9.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num2 + 1).ToString());
			}
			component10.Text = string.Empty;
		}
		UIButton component11 = popupPanel.transform.Find("NewDamage").GetComponent<UIButton>();
		UIButton component12 = popupPanel.transform.Find("NewHP").GetComponent<UIButton>();
		UIButton component13 = popupPanel.transform.Find("NewHeal").GetComponent<UIButton>();
		if (!gotPopupOriginalPos)
		{
			popupNextDemage_OriginalPos = component11.transform.position;
			popupNextHP_OriginalPos = component12.transform.position;
			gotPopupOriginalPos = true;
		}
		num4 = ((text == "item") ? (-1) : ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack"))));
		if (hashtable["type"].ToString() == "Hero")
		{
			FD_ObjectLevelDefinition currentLevel2 = forrestConfiguration.GetCurrentLevel("Weapon2");
			if (currentLevel2 == null)
			{
				currentLevel2 = forrestConfiguration.GetCurrentLevel("Weapon1");
				if (currentLevel2 != null)
				{
					StarStrike_ObjectDefinition definition = forrestConfiguration.GetDefinition("Weapon1");
					int @int = PlayerPrefs.GetInt(definition.GetAttributeValue("key"));
					ArrayList levelArray2 = definition.GetLevelArray("level");
					@int++;
					if (@int < 1)
					{
						@int = 1;
					}
					if (@int >= levelArray2.Count)
					{
						@int = levelArray2.Count - 1;
					}
					currentLevel2 = levelArray2[@int] as FD_ObjectLevelDefinition;
				}
			}
			else
			{
				StarStrike_ObjectDefinition definition = forrestConfiguration.GetDefinition("Weapon2");
				int @int = PlayerPrefs.GetInt(definition.GetAttributeValue("key"));
				ArrayList levelArray2 = definition.GetLevelArray("level");
				@int++;
				if (@int < 1)
				{
					@int = 1;
				}
				if (@int >= levelArray2.Count)
				{
					@int = levelArray2.Count - 1;
				}
				currentLevel2 = levelArray2[@int] as FD_ObjectLevelDefinition;
			}
			if (currentLevel2 != null)
			{
				num4 = int.Parse(currentLevel2.GetAttributeValue("attack"));
			}
		}
		num6 = ((text == "item") ? (-1) : ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health"))));
		if (num4 > -1 && hashtable["objName"].ToString() != "Weapon1" && hashtable["objName"].ToString() != "Weapon2")
		{
			if (hashtable["objName"].ToString() == "Army_Owl")
			{
				component13.Text = Mathf.Floor((float)num4 * 1.2f).ToString();
				component13.SetSize(25f, 25f);
				component11.Text = string.Empty;
				component11.SetSize(0f, 0f);
			}
			else
			{
				if (CargoManager.GetHeroUpgradeData().isEnableAndValid)
				{
					component11.Text = "?????";
				}
				else
				{
					float num5 = Mathf.Floor((float)num4 * 1.2f);
					if (hashtable["objName"].ToString() == "Rein")
					{
						component11.Text = num4 + "%";
					}
					else
					{
						component11.Text = num5.ToString();
					}
				}
				component11.SetSize(55f, 57f);
				component13.Text = string.Empty;
				component13.SetSize(0f, 0f);
			}
		}
		else
		{
			component13.Text = string.Empty;
			component13.SetSize(0f, 0f);
			component11.Text = string.Empty;
			component11.SetSize(0f, 0f);
		}
		if (hashtable["type"].ToString() == "Forest")
		{
			component11.transform.position = new Vector3(popupNextDemage_OriginalPos.x + 20f, popupNextDemage_OriginalPos.y, popupNextDemage_OriginalPos.z);
		}
		else
		{
			component11.transform.position = popupNextDemage_OriginalPos;
		}
		if (num6 > -1 && hashtable["objName"].ToString() != "Weapon1" && hashtable["objName"].ToString() != "Weapon2")
		{
			if (CargoManager.GetHeroUpgradeData().isEnableAndValid)
			{
				component12.Text = "??????";
			}
			else
			{
				component12.Text = Mathf.Floor((float)num6 * 1.2f).ToString();
			}
			component12.SetSize(42f, 37f);
		}
		else
		{
			component6.Text = string.Empty;
			component6.SetSize(0f, 0f);
			component12.Text = string.Empty;
			component12.SetSize(0f, 0f);
		}
		if (num4 == -1)
		{
			component12.transform.position = component11.transform.position;
		}
		else
		{
			component12.transform.position = popupNextHP_OriginalPos;
		}
		UIButton component14 = popupPanel.transform.Find("Button").GetComponent<UIButton>();
		UIButton component15 = popupPanel.transform.Find("BuyButton").GetComponent<UIButton>();
		if ((hashtable["type"].ToString() == "Forest" && flag && num == 0) || text == "item" || text == "goldenSlot")
		{
			component15.Text = num3.ToString();
			component15.SetSize(153f, 120f);
			component14.Text = string.Empty;
			component14.SetSize(0f, 0f);
		}
		else
		{
			component14.Text = num3.ToString();
			component14.SetSize(153f, 120f);
			component15.Text = string.Empty;
			component15.SetSize(0f, 0f);
		}
		UIButton component16 = popupPanel.transform.Find("BackButton").GetComponent<UIButton>();
		component16.SetValueChangedDelegate(onPopupBackButtonClicked);
		Debug.Log("i have: " + user_obj.getCoin() + ", upgradePrice: " + num3);
		if (user_obj.getCoin() >= num3)
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add("key", starStrike_ObjectDefinition.GetAttributeValue("key"));
			hashtable2.Add("objName", hashtable["objName"].ToString());
			hashtable2.Add("index", hashtable["index"].ToString());
			hashtable2.Add("nextLevel", num2);
			hashtable2.Add("updatePrice", num3);
			hashtable2.Add("type", hashtable["type"].ToString());
			if (text != string.Empty)
			{
				hashtable2.Add("itemType", text);
			}
			else
			{
				hashtable2.Add("itemType", hashtable["type"].ToString());
			}
			component14.Data = hashtable2;
			component15.Data = hashtable2;
			if ((hashtable["type"].ToString() == "Forest" && flag && num == 0) || text == "item" || text == "goldenSlot")
			{
				component15.soundOnClick = purchaseSound;
				component15.SetValueChangedDelegate(processUpgrade);
				component15.SetControlState(UIButton.CONTROL_STATE.NORMAL);
				component14.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			else
			{
				component14.soundOnClick = purchaseSound;
				component14.SetValueChangedDelegate(processUpgrade);
				component14.SetControlState(UIButton.CONTROL_STATE.NORMAL);
				component15.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
		else if ((hashtable["type"].ToString() == "Forest" && flag && num == 0) || text == "item" || text == "goldenSlot")
		{
			component15.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			component15.SetValueChangedDelegate(notEnoughPointOnClick);
			component15.soundOnClick = null;
			component14.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		}
		else
		{
			Debug.Log(" ++++ Upgrade");
			component14.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			component14.SetValueChangedDelegate(notEnoughPointOnClick);
			component14.soundOnClick = null;
			component15.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		}
	}

	private void notEnoughPointOnClick(IUIObject button)
	{
		warningPopupType = WarningPopupType.NotEnoughCoins;
		Debug.Log("not enough coins: " + langMan.getLangData("UpgradeScene_NotEnoughPoint_Err"));
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, langMan.getLangData("UpgradeScene_NotEnoughPoint_Err"));
	}

	private void RefreshHero()
	{
		Debug.Log("++++++++++++ Refresh Hero ++++++++++++++");
		UIListItemContainer uIListItemContainer = (UIListItemContainer)heroScrollList.GetItem(0);
		StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition("Hero");
		ArrayList levelArray = unitDefinition.GetLevelArray("level");
		if (levelArray.Count > 0)
		{
			int num = (unitDefinition.HasAttribute("key") ? (PlayerPrefs.HasKey(unitDefinition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key")) : 0) : 0);
			int num2 = -1;
			FD_ObjectLevelDefinition currentLevel = forrestConfiguration.GetCurrentLevel("Weapon2");
			if (currentLevel == null)
			{
				currentLevel = forrestConfiguration.GetCurrentLevel("Weapon1");
				Debug.Log("------- Weapon1 ------");
			}
			else
			{
				Debug.Log("------- Weapon2 ------");
			}
			if (currentLevel != null)
			{
				num2 = int.Parse(currentLevel.GetAttributeValue("attack"));
			}
			Debug.Log("------- attack = " + num2);
			int num3 = ((num >= levelArray.Count - 1) ? num : (num + 1));
			SpriteText textElement = uIListItemContainer.GetTextElement("Level");
			if (user_obj.getLangCode() == "ko")
			{
				textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement.SetCharacterSize(19f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (num < num3)
			{
				textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num + 1).ToString());
			}
			else
			{
				textElement.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
			}
			if (num < 0)
			{
				num = 0;
			}
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num] as FD_ObjectLevelDefinition;
			UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Attack");
			if (num2 > -1)
			{
				uIButton.Text = Mathf.Floor((float)num2 * 1.2f).ToString();
			}
			else
			{
				uIButton.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")) * 1.2f).ToString();
			}
			UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("HP");
			uIButton2.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")) * 1.2f).ToString();
			UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("Button");
			uIButton3.Text = fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("type", "Hero");
			hashtable.Add("objName", "Hero");
			hashtable.Add("index", 1);
			hashtable.Add("key", unitDefinition.GetAttributeValue("key"));
			hashtable.Add("currentLevel", num);
			hashtable.Add("nextLevelPrice", fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			hashtable.Add("unlockLevel", "0");
			if (user_obj.getLangCode() != "en")
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue("UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue("Description"));
			}
			hashtable.Add("icon", unitDefinition.GetAttributeValue("icon"));
			uIButton3.data = hashtable;
			uIButton3.SetValueChangedDelegate(OnUpgradeButtonClicked);
			if (num == num3)
			{
				uIButton3.Text = string.Empty;
				uIButton3.SetSize(0f, 0f);
				uIButton3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("Icon");
			uIButton4.SetTexture(imageManager.GetImage("Images/" + unitDefinition.GetAttributeValue("icon")));
		}
	}

	private void RefreshSpecialAttack(int index, string unit)
	{
		UIListItemContainer uIListItemContainer = (UIListItemContainer)heroScrollList.GetItem(index);
		StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(unit);
		ArrayList levelArray = definition.GetLevelArray("level");
		if (levelArray.Count <= 0)
		{
			return;
		}
		int num = 0;
		num = (definition.HasAttribute("key") ? (PlayerPrefs.HasKey(definition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(definition.GetAttributeValue("key")) : 0) : 0);
		int num2 = ((num >= levelArray.Count - 1) ? num : (num + 1));
		SpriteText textElement = uIListItemContainer.GetTextElement("Level");
		if (user_obj.getLangCode() == "ko")
		{
			textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
			textElement.SetCharacterSize(19f);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
		}
		if (num < num2)
		{
			textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num + 1).ToString());
		}
		else
		{
			textElement.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
		}
		if (num < 0)
		{
			num = 0;
		}
		FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num] as FD_ObjectLevelDefinition;
		UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Damage");
		float num3 = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")) * 1.2f);
		if (unit == "Rein")
		{
			uIButton.Text = fD_ObjectLevelDefinition.GetAttributeValue("attack") + "%";
		}
		else
		{
			uIButton.Text = num3.ToString();
		}
		UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("HP");
		uIButton2.Text = string.Empty;
		uIButton2.SetSize(0f, 0f);
		UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("Button");
		uIButton3.Text = fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice");
		if (num == num2)
		{
			uIButton3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			uIButton3.Text = string.Empty;
			uIButton3.SetSize(0f, 0f);
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("type", "SpecialAttack");
		hashtable.Add("objName", unit);
		hashtable.Add("index", index);
		hashtable.Add("key", definition.GetAttributeValue("key"));
		hashtable.Add("currentLevel", num);
		hashtable.Add("nextLevelPrice", fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
		hashtable.Add("unlockLevel", definition.GetAttributeValue("UnlockLevel"));
		if (user_obj.getLangCode() != "en")
		{
			hashtable.Add("name", definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
			hashtable.Add("description", definition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
		}
		else
		{
			hashtable.Add("name", definition.GetAttributeValue("UnitName"));
			hashtable.Add("description", definition.GetAttributeValue("Description"));
		}
		hashtable.Add("icon", definition.GetAttributeValue("icon"));
		uIButton3.data = hashtable;
		uIButton3.SetValueChangedDelegate(OnUpgradeButtonClicked);
	}

	private void LoadArmyUnitDefinition()
	{
		int num = 0;
		UIListItemContainer uIListItemContainer = (UIListItemContainer)heroScrollList.CreateItem(heroUpgradeListItem);
		StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition("Hero");
		ArrayList levelArray = unitDefinition.GetLevelArray("level");
		if (levelArray.Count > 0)
		{
			int num2 = (unitDefinition.HasAttribute("key") ? (PlayerPrefs.HasKey(unitDefinition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key")) : 0) : 0);
			int num3 = -1;
			FD_ObjectLevelDefinition currentLevel = forrestConfiguration.GetCurrentLevel("Weapon2");
			if (currentLevel == null)
			{
				currentLevel = forrestConfiguration.GetCurrentLevel("Weapon1");
				Debug.Log("------- Weapon1 ------");
			}
			else
			{
				Debug.Log("------- Weapon2 ------");
			}
			if (currentLevel != null)
			{
				num3 = int.Parse(currentLevel.GetAttributeValue("attack"));
			}
			Debug.Log("------- attack = " + num3);
			int num4 = ((num2 >= levelArray.Count - 1) ? num2 : (num2 + 1));
			SpriteText textElement = uIListItemContainer.GetTextElement("Level");
			if (user_obj.getLangCode() == "ko")
			{
				textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement.SetCharacterSize(19f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (num2 < num4)
			{
				textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num2 + 1).ToString());
			}
			else
			{
				textElement.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
			}
			SpriteText textElement2 = uIListItemContainer.GetTextElement("Name");
			if (user_obj.getLangCode() == "ko")
			{
				textElement2.SetFont(KO_SmallFont, KO_SmallFontMat);
				textElement2.Text = unitDefinition.GetAttributeValue("ko_UnitName");
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement2.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement2.SetCharacterSize(25f);
				textElement2.SetLineSpacing(1f);
				textElement2.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement2.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement2.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement2.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement2.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement2.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement2.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement2.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement2.SetCharacterSize(30f);
				textElement2.SetLineSpacing(1f);
				textElement2.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else
			{
				textElement2.Text = unitDefinition.GetAttributeValue("UnitName");
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num2] as FD_ObjectLevelDefinition;
			UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Attack");
			if (num3 > -1)
			{
				uIButton.Text = Mathf.Floor((float)num3 * 1.2f).ToString();
			}
			else
			{
				uIButton.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")) * 1.2f).ToString();
			}
			UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("HP");
			uIButton2.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")) * 1.2f).ToString();
			UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("Button");
			uIButton3.Text = fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("type", "Hero");
			hashtable.Add("objName", "Hero");
			hashtable.Add("index", num);
			hashtable.Add("key", unitDefinition.GetAttributeValue("key"));
			hashtable.Add("currentLevel", num2);
			hashtable.Add("nextLevelPrice", fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			hashtable.Add("unlockLevel", "0");
			if (user_obj.getLangCode() != "en")
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue("UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue("Description"));
			}
			hashtable.Add("icon", unitDefinition.GetAttributeValue("icon"));
			uIButton3.data = hashtable;
			uIButton3.SetValueChangedDelegate(OnUpgradeButtonClicked);
			if (num2 == num4)
			{
				uIButton3.Text = string.Empty;
				uIButton3.SetSize(0f, 0f);
				uIButton3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("Icon");
			uIButton4.SetTexture(imageManager.GetImage("Images/" + unitDefinition.GetAttributeValue("icon")));
			uIButton4.Data = hashtable;
			uIButton4.SetValueChangedDelegate(ShowItemInfo);
			num++;
		}
		ArrayList specialAttackArray = spAttackConfiguration.GetSpecialAttackArray();
		foreach (string item in specialAttackArray)
		{
			UIListItemContainer uIListItemContainer2 = (UIListItemContainer)heroScrollList.CreateItem(spellUpgradeListItem);
			StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(item);
			ArrayList levelArray2 = definition.GetLevelArray("level");
			if (levelArray2.Count <= 0)
			{
				continue;
			}
			int num5 = 0;
			num5 = (definition.HasAttribute("key") ? (PlayerPrefs.HasKey(definition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(definition.GetAttributeValue("key")) : 0) : 0);
			int num6 = ((num5 >= levelArray2.Count - 1) ? num5 : (num5 + 1));
			SpriteText textElement3 = uIListItemContainer2.GetTextElement("Name");
			if (user_obj.getLangCode() == "ko")
			{
				textElement3.SetFont(KO_SmallFont, KO_SmallFontMat);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement3.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement3.SetCharacterSize(25f);
				textElement3.SetLineSpacing(1f);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement3.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement3.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement3.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement3.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement3.SetCharacterSize(30f);
				textElement3.SetLineSpacing(1f);
				textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else
			{
				textElement3.Text = definition.GetAttributeValue("UnitName");
			}
			SpriteText textElement4 = uIListItemContainer2.GetTextElement("Level");
			if (user_obj.getLangCode() == "ko")
			{
				textElement4.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement4.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement4.SetCharacterSize(19f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement4.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement4.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement4.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement4.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (num5 < num6)
			{
				textElement4.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num5 + 1).ToString());
			}
			else
			{
				textElement4.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition2 = levelArray2[num5] as FD_ObjectLevelDefinition;
			UIButton uIButton5 = (UIButton)uIListItemContainer2.GetElement("Damage");
			float num7 = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition2.GetAttributeValue("attack")) * 1.2f);
			if (item == "Rein")
			{
				uIButton5.Text = fD_ObjectLevelDefinition2.GetAttributeValue("attack") + "%";
			}
			else
			{
				uIButton5.Text = num7.ToString();
			}
			UIButton uIButton6 = (UIButton)uIListItemContainer2.GetElement("HP");
			uIButton6.Text = string.Empty;
			uIButton6.SetSize(0f, 0f);
			UIButton uIButton7 = (UIButton)uIListItemContainer2.GetElement("Button");
			uIButton7.Text = fD_ObjectLevelDefinition2.GetAttributeValue("upgradePrice");
			UIButton uIButton8 = (UIButton)uIListItemContainer2.GetElement("BuyButton");
			uIButton8.Text = string.Empty;
			uIButton8.SetSize(0f, 0f);
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add("type", "SpecialAttack");
			hashtable2.Add("objName", item);
			hashtable2.Add("index", num);
			hashtable2.Add("key", definition.GetAttributeValue("key"));
			hashtable2.Add("currentLevel", num5);
			hashtable2.Add("nextLevelPrice", fD_ObjectLevelDefinition2.GetAttributeValue("upgradePrice"));
			hashtable2.Add("unlockLevel", definition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getLangCode() != "en")
			{
				hashtable2.Add("name", definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable2.Add("description", definition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable2.Add("name", definition.GetAttributeValue("UnitName"));
				hashtable2.Add("description", definition.GetAttributeValue("Description"));
			}
			hashtable2.Add("icon", definition.GetAttributeValue("icon"));
			if (num5 == num6)
			{
				uIButton7.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton7.Text = string.Empty;
				uIButton7.SetSize(0f, 0f);
			}
			else
			{
				uIButton7.data = hashtable2;
				uIButton7.SetValueChangedDelegate(OnUpgradeButtonClicked);
			}
			UIButton uIButton9 = (UIButton)uIListItemContainer2.GetElement("Icon");
			uIButton9.Data = hashtable2;
			uIButton9.SetValueChangedDelegate(ShowItemInfo);
			SpriteText textElement5 = uIListItemContainer2.GetTextElement("UnlockText");
			if (user_obj.getLangCode() == "ko")
			{
				textElement5.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement5.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement5.SetCharacterSize(21f);
				textElement5.SetLineSpacing(1f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement5.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement5.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement5.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement5.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement5.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement5.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement5.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement5.SetCharacterSize(22f);
			}
			if (definition.HasAttribute("UnlockLevel"))
			{
				int num8 = int.Parse(definition.GetAttributeValue("UnlockLevel"));
				if (user_obj.getGameLevel() >= num8)
				{
					uIButton9.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon")));
					textElement5.Text = string.Empty;
					if (uIButton6.Text != string.Empty)
					{
						uIButton6.SetSize(55f, 57f);
					}
					uIButton5.SetSize(55f, 57f);
				}
				else
				{
					uIButton7.SetControlState(UIButton.CONTROL_STATE.DISABLED);
					uIButton9.SetTexture(imageManager.GetImage("Images/lock_2"));
					textElement5.Text = langMan.getLangData("UpgradeScene_UnlockLevel").Replace("#", num8.ToString());
					uIButton6.Text = string.Empty;
					uIButton6.SetSize(0f, 0f);
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
				}
			}
			num++;
		}
	}

	private void RefreshNumenList(int index, string unit)
	{
		UIListItemContainer uIListItemContainer = (UIListItemContainer)numenScrollList.GetItem(index);
		StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(unit);
		ArrayList levelArray = unitDefinition.GetLevelArray("level");
		if (levelArray.Count <= 0)
		{
			return;
		}
		int num = 0;
		num = (unitDefinition.HasAttribute("key") ? (PlayerPrefs.HasKey(unitDefinition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key")) : 0) : 0);
		int num2 = ((num >= levelArray.Count - 1) ? num : (num + 1));
		SpriteText textElement = uIListItemContainer.GetTextElement("Level");
		if (user_obj.getLangCode() == "ko")
		{
			textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
			textElement.SetCharacterSize(19f);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
		}
		if (num < num2)
		{
			textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num + 1).ToString());
		}
		else
		{
			textElement.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
		}
		if (num < 0)
		{
			num = 0;
		}
		FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num] as FD_ObjectLevelDefinition;
		UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Damage");
		float num3 = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")) * 1.2f);
		uIButton.Text = num3.ToString();
		UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("Heal");
		if (unit == "Army_Owl")
		{
			uIButton2.Text = num3.ToString();
			uIButton.Text = string.Empty;
			uIButton.SetSize(0f, 0f);
		}
		else
		{
			uIButton2.Text = string.Empty;
			uIButton2.SetSize(0f, 0f);
		}
		UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("HP");
		uIButton3.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")) * 1.2f).ToString();
		UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("Button");
		uIButton4.Text = fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice");
		if (num == num2)
		{
			uIButton4.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			uIButton4.Text = string.Empty;
			uIButton4.SetSize(0f, 0f);
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("type", "Army");
		hashtable.Add("objName", unit);
		hashtable.Add("index", index);
		hashtable.Add("key", unitDefinition.GetAttributeValue("key"));
		hashtable.Add("currentLevel", num);
		hashtable.Add("nextLevelPrice", fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
		hashtable.Add("unlockLevel", unitDefinition.GetAttributeValue("UnlockLevel"));
		if (user_obj.getLangCode() != "en")
		{
			hashtable.Add("name", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
			hashtable.Add("description", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
		}
		else
		{
			hashtable.Add("name", unitDefinition.GetAttributeValue("UnitName"));
			hashtable.Add("description", unitDefinition.GetAttributeValue("Description"));
		}
		hashtable.Add("icon", unitDefinition.GetAttributeValue("icon"));
		uIButton4.data = hashtable;
		uIButton4.SetValueChangedDelegate(OnUpgradeButtonClicked);
	}

	private void BuildNumenList()
	{
		StarStrike_ArmyUnitConfiguration component = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		int num = 0;
		ArrayList goodGuy = component.GetGoodGuy();
		foreach (string item in goodGuy)
		{
			if (item == "Hero")
			{
				continue;
			}
			UIListItemContainer uIListItemContainer = (UIListItemContainer)numenScrollList.CreateItem(armyUpgradeListItem);
			StarStrike_ObjectDefinition unitDefinition = component.GetUnitDefinition(item);
			ArrayList levelArray = unitDefinition.GetLevelArray("level");
			if (levelArray.Count <= 0)
			{
				continue;
			}
			int num2 = 0;
			num2 = (unitDefinition.HasAttribute("key") ? (PlayerPrefs.HasKey(unitDefinition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key")) : 0) : 0);
			Debug.Log(item + ": Level=" + num2);
			int num3 = ((num2 >= levelArray.Count - 1) ? num2 : (num2 + 1));
			SpriteText textElement = uIListItemContainer.GetTextElement("Name");
			if (user_obj.getLangCode() == "ko")
			{
				textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement.SetCharacterSize(25f);
				textElement.SetLineSpacing(1f);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement.SetCharacterSize(30f);
				textElement.SetLineSpacing(1f);
				textElement.Text = unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else
			{
				textElement.Text = unitDefinition.GetAttributeValue("UnitName");
			}
			SpriteText textElement2 = uIListItemContainer.GetTextElement("Level");
			if (user_obj.getLangCode() == "ko")
			{
				textElement2.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement2.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement2.SetCharacterSize(19f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement2.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement2.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement2.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement2.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (num2 < num3)
			{
				textElement2.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num2 + 1).ToString());
			}
			else
			{
				textElement2.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num2] as FD_ObjectLevelDefinition;
			UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Damage");
			float num4 = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")) * 1.2f);
			uIButton.Text = num4.ToString();
			UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("Heal");
			if (item == "Army_Owl")
			{
				uIButton2.Text = num4.ToString();
				uIButton.Text = string.Empty;
				uIButton.SetSize(0f, 0f);
			}
			else
			{
				uIButton2.Text = string.Empty;
				uIButton2.SetSize(0f, 0f);
			}
			UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("HP");
			uIButton3.Text = Mathf.Floor((float)int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")) * 1.2f).ToString();
			UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("SpawnPrice");
			uIButton4.Text = unitDefinition.GetAttributeValue("price");
			UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("Button");
			uIButton5.Text = fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("type", "Army");
			hashtable.Add("objName", item);
			hashtable.Add("index", num);
			hashtable.Add("key", unitDefinition.GetAttributeValue("key"));
			hashtable.Add("currentLevel", num2);
			hashtable.Add("nextLevelPrice", fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			hashtable.Add("unlockLevel", unitDefinition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getLangCode() != "en")
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable.Add("name", unitDefinition.GetAttributeValue("UnitName"));
				hashtable.Add("description", unitDefinition.GetAttributeValue("Description"));
			}
			hashtable.Add("icon", unitDefinition.GetAttributeValue("icon"));
			if (num2 == num3)
			{
				uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton5.Text = string.Empty;
				uIButton5.SetSize(0f, 0f);
			}
			else
			{
				uIButton5.data = hashtable;
				uIButton5.SetValueChangedDelegate(OnUpgradeButtonClicked);
			}
			num++;
			UIButton uIButton6 = (UIButton)uIListItemContainer.GetElement("Icon");
			uIButton6.Data = hashtable;
			uIButton6.SetValueChangedDelegate(ShowItemInfo);
			SpriteText textElement3 = uIListItemContainer.GetTextElement("UnlockText");
			if (user_obj.getLangCode() == "ko")
			{
				textElement3.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement3.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement3.SetCharacterSize(21f);
				textElement3.SetLineSpacing(1f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement3.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement3.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement3.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement3.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement3.SetCharacterSize(22f);
			}
			if (!unitDefinition.HasAttribute("UnlockLevel"))
			{
				continue;
			}
			int num5 = int.Parse(unitDefinition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getGameLevel() >= num5)
			{
				string path = ((!unitDefinition.HasAttribute("icon")) ? "Images/small_heart" : ("Images/" + unitDefinition.GetAttributeValue("icon")));
				uIButton6.SetTexture(imageManager.GetImage(path));
				textElement3.Text = string.Empty;
				if (uIButton3.Text != string.Empty)
				{
					uIButton3.SetSize(42f, 37f);
				}
				if (uIButton.Text != string.Empty)
				{
					uIButton.SetSize(55f, 57f);
				}
			}
			else
			{
				uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton6.SetTexture(imageManager.GetImage("Images/lock_2"));
				textElement3.Text = langMan.getLangData("UpgradeScene_UnlockLevel").Replace("#", num5.ToString());
				uIButton.Text = string.Empty;
				uIButton.SetSize(0f, 0f);
				uIButton3.Text = string.Empty;
				uIButton3.SetSize(0f, 0f);
				uIButton2.Text = string.Empty;
				uIButton2.SetSize(0f, 0f);
			}
		}
	}

	private void RefreshForestList(int index, string unit)
	{
		UIListItemContainer uIListItemContainer = (UIListItemContainer)forestScrollList.GetItem(index);
		StarStrike_ObjectDefinition definition = forrestConfiguration.GetDefinition(unit);
		ArrayList levelArray = definition.GetLevelArray("level");
		string text = ((!definition.HasAttribute("type")) ? "-" : definition.GetAttributeValue("type"));
		if (levelArray.Count <= 0)
		{
			return;
		}
		int num = 0;
		num = (definition.HasAttribute("key") ? (PlayerPrefs.HasKey(definition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(definition.GetAttributeValue("key")) : 0) : 0);
		int num2 = ((text == "generator") ? ((num >= levelArray.Count - 2) ? num : (num + 1)) : ((num >= levelArray.Count - 1) ? num : (num + 1)));
		if ((text == "weapon" || text == "slot") && num > 0)
		{
			num2 = num;
		}
		bool flag = int.Parse(definition.GetAttributeValue("needToBuy")) == 1;
		SpriteText textElement = uIListItemContainer.GetTextElement("Level");
		if (user_obj.getLangCode() == "ko")
		{
			textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
			textElement.SetCharacterSize(19f);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "de")
		{
			textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
		}
		if (text == "item")
		{
			textElement.Text = "x " + num;
		}
		else if (flag && num == 0)
		{
			textElement.Text = string.Empty;
		}
		else if (num < num2)
		{
			if (flag)
			{
				textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", num.ToString());
			}
			else
			{
				textElement.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num + 1).ToString());
			}
		}
		else
		{
			textElement.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
		}
		UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Damage");
		UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("HP");
		int num4;
		int num5;
		int num3;
		if (text == "item")
		{
			num3 = -1;
			num4 = -1;
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[0] as FD_ObjectLevelDefinition;
			num5 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
		}
		else if (flag && num == 0)
		{
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num2] as FD_ObjectLevelDefinition;
			num3 = ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")));
			num4 = ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")));
			fD_ObjectLevelDefinition = levelArray[num] as FD_ObjectLevelDefinition;
			num5 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
		}
		else
		{
			FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num] as FD_ObjectLevelDefinition;
			num3 = ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")));
			num4 = ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")));
			num5 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
		}
		if (num3 > 0 && num != num2)
		{
			uIButton.Text = Mathf.Floor((float)num3 * 1.2f).ToString();
			uIButton.SetSize(55f, 57f);
		}
		else
		{
			uIButton.Text = string.Empty;
			uIButton.SetSize(0f, 0f);
		}
		if (num4 > 0 && num != num2)
		{
			uIButton2.Text = Mathf.Floor((float)num4 * 1.2f).ToString();
			uIButton2.SetSize(42f, 37f);
		}
		else
		{
			uIButton2.Text = string.Empty;
			uIButton2.SetSize(0f, 0f);
		}
		UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("Icon");
		bool flag2 = true;
		SpriteText textElement2 = uIListItemContainer.GetTextElement("UnlockText");
		if (user_obj.getLangCode() == "ko")
		{
			textElement2.SetFont(KO_SmallFont, KO_SmallFontMat);
		}
		else if (user_obj.getLangCode() == "ja")
		{
			textElement2.SetFont(JA_SmallFont, JA_SmallFontMat);
			textElement2.SetCharacterSize(21f);
			textElement2.SetLineSpacing(1f);
		}
		else if (user_obj.getLangCode() == "fr")
		{
			textElement2.SetFont(FR_SmallFont, FR_SmallFontMat);
			textElement2.SetCharacterSize(25f);
		}
		else if (user_obj.getLangCode() == "de")
		{
			textElement2.SetFont(DE_SmallFont, DE_SmallFontMat);
			textElement2.SetCharacterSize(25f);
		}
		else if (user_obj.getLangCode() == "zh-Hans")
		{
			textElement2.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			textElement2.SetCharacterSize(25f);
		}
		else if (user_obj.getLangCode() == "zh-Hant")
		{
			textElement2.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			textElement2.SetCharacterSize(22f);
		}
		if (definition.HasAttribute("UnlockLevel"))
		{
			int num6 = int.Parse(definition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getGameLevel() >= num6)
			{
				flag2 = false;
				if (text != "weapon")
				{
					uIButton3.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon")));
					if (text == "generator" && num != num2)
					{
						textElement2.Text = langMan.getLangData("SmithLevelUpgrade");
						num3 = -1;
						uIButton.Text = string.Empty;
						uIButton.SetSize(0f, 0f);
					}
					else if (text == "slot" && num != num2)
					{
						textElement2.Text = langMan.getLangData("ExtraSpecialAttackSlot");
						num3 = -1;
						uIButton.Text = string.Empty;
						uIButton.SetSize(0f, 0f);
					}
					else if (text == "item")
					{
						uIButton.Text = string.Empty;
						uIButton.SetSize(0f, 0f);
						uIButton2.Text = string.Empty;
						uIButton2.SetSize(0f, 0f);
						if (user_obj.getLangCode() != "en")
						{
							textElement2.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
						}
						else
						{
							textElement2.Text = definition.GetAttributeValue("Description");
						}
					}
					else if (text == "goldenSlot")
					{
						uIButton.Text = string.Empty;
						uIButton.SetSize(0f, 0f);
						uIButton2.Text = string.Empty;
						uIButton2.SetSize(0f, 0f);
						if (user_obj.getLangCode() != "en")
						{
							textElement2.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
						}
						else
						{
							textElement2.Text = definition.GetAttributeValue("Description");
						}
					}
					else
					{
						textElement2.Text = string.Empty;
					}
					if (uIButton2.Text != string.Empty)
					{
						uIButton2.SetSize(55f, 57f);
					}
					if (uIButton.Text != string.Empty)
					{
						uIButton.SetSize(55f, 57f);
					}
				}
				else
				{
					if (unit == "Weapon1" && num != num2)
					{
						textElement2.Text = langMan.getLangData("UnlockWeapon1");
					}
					else if (unit == "Weapon2" && num != num2)
					{
						textElement2.Text = langMan.getLangData("UnlockWeapon2");
					}
					else
					{
						textElement2.Text = string.Empty;
					}
					uIButton3.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon")));
					uIButton.Text = string.Empty;
					uIButton.SetSize(0f, 0f);
					uIButton2.Text = string.Empty;
					uIButton2.SetSize(0f, 0f);
				}
			}
			else
			{
				flag2 = true;
				uIButton3.SetTexture(imageManager.GetImage("Images/lock_2"));
				textElement2.Text = langMan.getLangData("UpgradeScene_UnlockLevel").Replace("#", num6.ToString());
				uIButton.Text = string.Empty;
				uIButton.SetSize(0f, 0f);
				uIButton2.Text = string.Empty;
				uIButton2.SetSize(0f, 0f);
			}
		}
		UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("Button");
		UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("BuyButton");
		if (text == "item" || (flag && num == 0))
		{
			uIButton4.Text = string.Empty;
			uIButton4.SetSize(0f, 0f);
			uIButton5.Text = num5.ToString();
			uIButton5.SetSize(153f, 120f);
			if (flag2)
			{
				uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				return;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("type", "Forest");
			hashtable.Add("objName", unit);
			hashtable.Add("index", index);
			hashtable.Add("key", definition.GetAttributeValue("key"));
			hashtable.Add("currentLevel", num);
			hashtable.Add("nextLevelPrice", num5);
			hashtable.Add("unlockLevel", definition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getLangCode() != "en")
			{
				hashtable.Add("name", definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable.Add("description", definition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable.Add("name", definition.GetAttributeValue("UnitName"));
				hashtable.Add("description", definition.GetAttributeValue("Description"));
			}
			hashtable.Add("icon", definition.GetAttributeValue("icon"));
			uIButton5.data = hashtable;
			uIButton5.SetValueChangedDelegate(OnUpgradeButtonClicked);
			return;
		}
		uIButton5.Text = string.Empty;
		uIButton5.SetSize(0f, 0f);
		uIButton4.SetSize(153f, 120f);
		if (num == num2)
		{
			if (uIButton4 != null)
			{
				uIButton4.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton4.Text = string.Empty;
				uIButton4.SetSize(0f, 0f);
				uIButton5.Text = string.Empty;
				uIButton5.SetSize(0f, 0f);
				uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			return;
		}
		uIButton4.Text = num5.ToString();
		Hashtable hashtable2 = new Hashtable();
		hashtable2.Add("type", "Forest");
		hashtable2.Add("objName", unit);
		hashtable2.Add("index", index);
		hashtable2.Add("key", definition.GetAttributeValue("key"));
		hashtable2.Add("currentLevel", num);
		hashtable2.Add("nextLevelPrice", num5);
		hashtable2.Add("unlockLevel", definition.GetAttributeValue("UnlockLevel"));
		if (user_obj.getLangCode() != "en")
		{
			hashtable2.Add("name", definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
			hashtable2.Add("description", definition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
		}
		else
		{
			hashtable2.Add("name", definition.GetAttributeValue("UnitName"));
			hashtable2.Add("description", definition.GetAttributeValue("Description"));
		}
		hashtable2.Add("icon", definition.GetAttributeValue("icon"));
		uIButton4.data = hashtable2;
		uIButton4.SetValueChangedDelegate(OnUpgradeButtonClicked);
	}

	private void BuildForestList()
	{
		int num = 0;
		ArrayList forrestArray = forrestConfiguration.GetForrestArray();
		foreach (string item in forrestArray)
		{
			UIListItemContainer uIListItemContainer = (UIListItemContainer)forestScrollList.CreateItem(itemUpgradeListItem);
			StarStrike_ObjectDefinition definition = forrestConfiguration.GetDefinition(item);
			string text2 = ((!definition.HasAttribute("type")) ? "-" : definition.GetAttributeValue("type"));
			ArrayList levelArray = definition.GetLevelArray("level");
			Debug.Log(" >>>> Unit : " + item + ", type: " + text2);
			if (levelArray.Count <= 0)
			{
				continue;
			}
			int num2 = 0;
			num2 = ((text2 == "goldenSlot") ? user_obj.getGoldenSlotCount(definition.GetAttributeValue("key")) : ((text2 == "item") ? user_obj.getItemCount(definition.GetAttributeValue("key")) : ((!definition.HasAttribute("key")) ? (-1) : ((!PlayerPrefs.HasKey(definition.GetAttributeValue("key"))) ? (-1) : PlayerPrefs.GetInt(definition.GetAttributeValue("key"))))));
			int num3 = ((text2 == "generator") ? ((num2 >= levelArray.Count - 2) ? num2 : (num2 + 1)) : ((num2 >= levelArray.Count - 1) ? num2 : (num2 + 1)));
			if (text2 == "weapon" && num2 > 0)
			{
				num3 = num2;
			}
			bool flag = int.Parse(definition.GetAttributeValue("needToBuy")) == 1;
			SpriteText textElement = uIListItemContainer.GetTextElement("Name");
			if (user_obj.getLangCode() == "ko")
			{
				textElement.SetFont(KO_SmallFont, KO_SmallFontMat);
				textElement.Text = definition.GetAttributeValue("ko_UnitName");
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement.SetCharacterSize(25f);
				textElement.SetLineSpacing(1f);
				textElement.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement.SetCharacterSize(30f);
				textElement.SetLineSpacing(1f);
				textElement.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName");
			}
			else
			{
				textElement.Text = definition.GetAttributeValue("UnitName");
			}
			SpriteText textElement2 = uIListItemContainer.GetTextElement("Level");
			if (user_obj.getLangCode() == "ko")
			{
				textElement2.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement2.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement2.SetCharacterSize(19f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement2.SetFont(FR_SmallFont, FR_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement2.SetFont(DE_SmallFont, DE_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement2.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement2.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			}
			if (text2 == "item")
			{
				textElement2.Text = "x " + num2;
			}
			else if (flag && num2 == 0)
			{
				textElement2.Text = string.Empty;
			}
			else if (num2 < num3)
			{
				if (flag)
				{
					textElement2.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", num2.ToString());
				}
				else
				{
					textElement2.Text = langMan.getLangData("UpgradeScene_ItemLevel_Txt").Replace("#", (num2 + 1).ToString());
				}
			}
			else
			{
				textElement2.Text = langMan.getLangData("UpgradeScene_MaxLvl_Txt");
			}
			UIButton uIButton = (UIButton)uIListItemContainer.GetElement("Damage");
			UIButton uIButton2 = (UIButton)uIListItemContainer.GetElement("HP");
			int num5;
			int num6;
			int num4;
			if (text2 == "item")
			{
				num4 = -1;
				num5 = -1;
				FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[0] as FD_ObjectLevelDefinition;
				num6 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			}
			else if (flag && num2 == 0)
			{
				FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num3] as FD_ObjectLevelDefinition;
				num4 = ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")));
				num5 = ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")));
				fD_ObjectLevelDefinition = levelArray[num2] as FD_ObjectLevelDefinition;
				num6 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			}
			else
			{
				FD_ObjectLevelDefinition fD_ObjectLevelDefinition = levelArray[num2] as FD_ObjectLevelDefinition;
				num4 = ((!fD_ObjectLevelDefinition.HasAttribute("attack")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("attack")));
				num5 = ((!fD_ObjectLevelDefinition.HasAttribute("health")) ? (-1) : int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("health")));
				num6 = int.Parse(fD_ObjectLevelDefinition.GetAttributeValue("upgradePrice"));
			}
			if (num4 > 0)
			{
				uIButton.Text = Mathf.Floor((float)num4 * 1.2f).ToString();
				uIButton.SetSize(55f, 57f);
			}
			else
			{
				uIButton.Text = string.Empty;
				uIButton.SetSize(0f, 0f);
			}
			if (num5 > 0)
			{
				uIButton2.Text = Mathf.Floor((float)num5 * 1.2f).ToString();
				uIButton2.SetSize(55f, 57f);
			}
			else
			{
				uIButton2.Text = string.Empty;
				uIButton2.SetSize(0f, 0f);
			}
			UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("Icon");
			bool flag2 = true;
			SpriteText textElement3 = uIListItemContainer.GetTextElement("UnlockText");
			if (user_obj.getLangCode() == "ko")
			{
				textElement3.SetFont(KO_SmallFont, KO_SmallFontMat);
			}
			else if (user_obj.getLangCode() == "ja")
			{
				textElement3.SetFont(JA_SmallFont, JA_SmallFontMat);
				textElement3.SetCharacterSize(21f);
				textElement3.SetLineSpacing(1f);
			}
			else if (user_obj.getLangCode() == "fr")
			{
				textElement3.SetFont(FR_SmallFont, FR_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "de")
			{
				textElement3.SetFont(DE_SmallFont, DE_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hans")
			{
				textElement3.SetFont(SChi_SmallFont, SChi_SmallFontMat);
				textElement3.SetCharacterSize(25f);
			}
			else if (user_obj.getLangCode() == "zh-Hant")
			{
				textElement3.SetFont(TChi_SmallFont, TChi_SmallFontMat);
				textElement3.SetCharacterSize(22f);
			}
			if (definition.HasAttribute("UnlockLevel"))
			{
				int num7 = int.Parse(definition.GetAttributeValue("UnlockLevel"));
				if (user_obj.getGameLevel() >= num7)
				{
					flag2 = false;
					if (text2 != "weapon")
					{
						uIButton3.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon")));
						if (text2 == "generator" && num2 != num3)
						{
							textElement3.Text = langMan.getLangData("SmithLevelUpgrade");
							num4 = -1;
							uIButton.Text = string.Empty;
							uIButton.SetSize(0f, 0f);
						}
						else if (text2 == "slot" && num2 != num3)
						{
							textElement3.Text = langMan.getLangData("ExtraSpecialAttackSlot");
							num4 = -1;
							uIButton.Text = string.Empty;
							uIButton.SetSize(0f, 0f);
						}
						else if (text2 == "item")
						{
							uIButton.Text = string.Empty;
							uIButton.SetSize(0f, 0f);
							uIButton2.Text = string.Empty;
							uIButton2.SetSize(0f, 0f);
							if (user_obj.getLangCode() != "en")
							{
								textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
							}
							else
							{
								textElement3.Text = definition.GetAttributeValue("Description");
							}
						}
						else if (text2 == "goldenSlot")
						{
							uIButton.Text = string.Empty;
							uIButton.SetSize(0f, 0f);
							uIButton2.Text = string.Empty;
							uIButton2.SetSize(0f, 0f);
							if (user_obj.getLangCode() != "en")
							{
								textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
							}
							else
							{
								textElement3.Text = definition.GetAttributeValue("Description");
							}
						}
						else
						{
							textElement3.Text = string.Empty;
						}
						if (uIButton2.Text != string.Empty)
						{
							uIButton2.SetSize(55f, 57f);
						}
						if (uIButton.Text != string.Empty)
						{
							uIButton.SetSize(55f, 57f);
						}
					}
					else
					{
						if (item == "Weapon1" && num2 != num3)
						{
							textElement3.Text = langMan.getLangData("UnlockWeapon1");
						}
						else if (item == "Weapon2" && num2 != num3)
						{
							textElement3.Text = langMan.getLangData("UnlockWeapon2");
						}
						else
						{
							textElement3.Text = string.Empty;
						}
						uIButton3.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon")));
						uIButton.Text = string.Empty;
						uIButton.SetSize(0f, 0f);
						uIButton2.Text = string.Empty;
						uIButton2.SetSize(0f, 0f);
					}
				}
				else
				{
					flag2 = true;
					uIButton3.SetTexture(imageManager.GetImage("Images/lock_2"));
					textElement3.Text = langMan.getLangData("UpgradeScene_UnlockLevel").Replace("#", num7.ToString());
					uIButton.Text = string.Empty;
					uIButton.SetSize(0f, 0f);
					uIButton2.Text = string.Empty;
					uIButton2.SetSize(0f, 0f);
				}
			}
			UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("Button");
			UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("BuyButton");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("type", "Forest");
			hashtable.Add("objName", item);
			hashtable.Add("index", num);
			hashtable.Add("key", definition.GetAttributeValue("key"));
			hashtable.Add("currentLevel", num2);
			hashtable.Add("nextLevelPrice", num6);
			hashtable.Add("unlockLevel", definition.GetAttributeValue("UnlockLevel"));
			if (user_obj.getLangCode() != "en")
			{
				hashtable.Add("name", definition.GetAttributeValue(user_obj.getLangCode() + "_UnitName"));
				hashtable.Add("description", definition.GetAttributeValue(user_obj.getLangCode() + "_Description"));
			}
			else
			{
				hashtable.Add("name", definition.GetAttributeValue("UnitName"));
				hashtable.Add("description", definition.GetAttributeValue("Description"));
			}
			hashtable.Add("icon", definition.GetAttributeValue("icon"));
			uIButton3.Data = hashtable;
			uIButton3.SetValueChangedDelegate(ShowItemInfo);
			if (text2 == "item")
			{
				if (user_obj.getLangCode() != "en")
				{
					textElement3.Text = definition.GetAttributeValue(user_obj.getLangCode() + "_Description");
				}
				else
				{
					textElement3.Text = definition.GetAttributeValue("Description");
				}
				uIButton4.Text = string.Empty;
				uIButton4.SetSize(0f, 0f);
				uIButton5.Text = num6.ToString();
				if (flag2)
				{
					uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				}
				else
				{
					uIButton5.data = hashtable;
					uIButton5.SetValueChangedDelegate(OnUpgradeButtonClicked);
				}
			}
			else if (flag && num2 == 0)
			{
				uIButton4.Text = string.Empty;
				uIButton4.SetSize(0f, 0f);
				uIButton5.Text = num6.ToString();
				if (flag2)
				{
					uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				}
				else
				{
					uIButton5.data = hashtable;
					uIButton5.SetValueChangedDelegate(OnUpgradeButtonClicked);
				}
			}
			else
			{
				uIButton5.Text = string.Empty;
				uIButton5.SetSize(0f, 0f);
				if (num2 == num3)
				{
					uIButton4.SetControlState(UIButton.CONTROL_STATE.DISABLED);
					uIButton4.Text = string.Empty;
					uIButton4.SetSize(0f, 0f);
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
					uIButton5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				}
				else
				{
					uIButton4.Text = num6.ToString();
					if (!flag2)
					{
						uIButton4.data = hashtable;
						uIButton4.SetValueChangedDelegate(OnUpgradeButtonClicked);
					}
					else
					{
						uIButton4.SetControlState(UIButton.CONTROL_STATE.DISABLED);
					}
				}
			}
			num++;
		}
	}

	private void getConfirmationPopupResult(PopupManager.PopupType type, bool choice)
	{
		Debug.Log("++++ DISMISS ++++: " + type);
		popupPanel.Dismiss();
		switch (type)
		{
		case PopupManager.PopupType.None:
			CheckIsNewLevel();
			break;
		case PopupManager.PopupType.Warning:
		{
			WarningPopupType warningPopupType = this.warningPopupType;
			if (warningPopupType == WarningPopupType.NotEnoughCoins)
			{
				InAppsController component2 = GameObject.Find("InAppsPurchaseControllers").GetComponent<InAppsController>();
				component2.ShowPurchasePanel();
			}
			this.warningPopupType = WarningPopupType.None;
			break;
		}
		case PopupManager.PopupType.Confirmation:
			if (choice)
			{
				switch (confirmationPopupType)
				{
				case ConfirmationPopupType.BuyNightMode:
					user_obj.addCoin(-challengeModePrice);
					user_obj.addCoinsSpent(challengeModePrice);
					user_obj.setUnlocked_NMode();
					UpdateChallengeModePopup();
					//MunerisController.Instance.ReportEvent("Unlocked Night Mode");
					break;
				case ConfirmationPopupType.ResetNightMode:
					user_obj.resetGameLevel_NMode();
					UpdateChallengeModePopup();
					break;
				case ConfirmationPopupType.BuyHalloweenMode:
					user_obj.addCoin(-challengeModePrice);
					user_obj.addCoinsSpent(challengeModePrice);
					user_obj.setUnlocked_HalloweenMode();
					UpdateChallengeModePopup();
					//MunerisController.Instance.ReportEvent("Unlocked Halloween Mode");
					break;
				case ConfirmationPopupType.ResetHalloweenMode:
					user_obj.resetGameLevel_HalloweenMode();
					UpdateChallengeModePopup();
					break;
				case ConfirmationPopupType.FreeUnlock:
				{
					InAppsController component = GameObject.Find("InAppsPurchaseControllers").GetComponent<InAppsController>();
					component.ShowPurchasePanel();
					break;
				}
				}
			}
			confirmationPopupType = ConfirmationPopupType.None;
			break;
		}
	}
}
