using System;
using System.Collections;
using System.Collections.Generic;
using Muneris;
using UnityEngine;

public class UserProfileManager
{
	public const bool normalVersion = true;

	public const string androidMarket = "google";

	public const string bundleId = "com.webprancer.google.garfieldDefense";

	public const string appKey = "91ad6500ecb446b8bcb53ccf0cc09bb8";

	public const string PPA_Lvl2 = "1a9dc340-de41-454d-92cc-723aed7a4492";

	public const string PPA_Lvl4 = "801eb347-386d-4c17-bf28-c227568fd24a";

	public const string PPA_Lvl10 = "f62b928f-e832-40f9-a5b5-c0733fad99bd";

	public const string PPA_Laminaceph = "b1a75bc3-31fc-4d47-9866-502936e7c2d9";

	public const string PPA_Devout = "325f0d0c-15d4-40eb-8305-48bd40aa6b78";

	public const string PPA_ComBackDay2 = "c3f5a26b-5140-439c-acae-55fc44cdaf3d";

	public const string PPA_ComBackDay3 = "f4fe1deb-7f37-466d-a9e4-3e8374c9f81b";

	public const string PPA_ComBackDay4 = "247fc3d2-9b30-4bc2-b835-14f699889628";

	public const string PPA_ComBackDay5 = "59701b7e-12c3-41de-a2c2-ecac04819acb";

	public const string PPA_ComBackDay7 = "bbb75ed5-f8e2-46c6-ad47-2480d8f0fd24";

	public const string bundleId_amazon = "com.webprancer.amazon.garfieldDefense";

	public const string appKey_amazon = "7c295f07469047f0ae7e63752347abd9";

	public const string PPA_Lvl2_amazon = "e90f7f24-9193-46a2-abbf-7144214ed662";

	public const string PPA_Lvl4_amazon = "38a3af60-60be-4e0c-8119-8f0a10fdd82c";

	public const string PPA_Lvl10_amazon = "4457fc2f-f9f0-405c-81ed-acdc77d736cb";

	public const string PPA_Laminaceph_amazon = "2acc1336-2f11-45eb-b114-3b01d129298d";

	public const string PPA_Devout_amazon = "da3e5e25-3ac2-4e4c-b3c2-a814fd31d9b4";

	public const string PPA_ComBackDay2_amazon = "72549713-1299-4832-a4d1-78f89cec69dc";

	public const string PPA_ComBackDay3_amazon = "2ee36570-ccea-4a24-b8c1-df97573fe58f";

	public const string PPA_ComBackDay4_amazon = "fb166f4b-50f5-4178-9c42-3c8d774c4df1";

	public const string PPA_ComBackDay5_amazon = "9d09964f-d5c9-4adb-bb18-ffde3b6b1e07";

	public const string PPA_ComBackDay7_amazon = "bb46ae4d-0eed-42cc-bcd9-98b1f1e544e8";

	public const int maxLevel = 60;

	public const int defaultCoins = 0;

	public const int armySlot = 5;

	public const int NMode_maxLevel = 30;

	public const int HalloweenMode_maxLevel = 30;

	private int spAttackSlot = 2;

	private bool initialized;

	private string currentSceneId;

	private ArrayList selectedSpecialAttack;

	private InGamePlayMode currentPlayMode;

	private bool _chineseNewYear;

	private string lang = "en";

	private int requestStage;

	public bool ChineseNewYear
	{
		get
		{
			return _chineseNewYear;
		}
	}

	public static int Quantity
	{
		get
		{
			return PlayerPrefs.GetInt("Quantity", 0);
		}
		set
		{
			PlayerPrefs.SetInt("Quantity", value);
		}
	}

	public static string Message
	{
		get
		{
			return PlayerPrefs.GetString("Message", string.Empty);
		}
		set
		{
			PlayerPrefs.SetString("Message", value);
		}
	}

	public bool ShowedChallengeModePopup
	{
		get
		{
			if (isChallengeModeOpened())
			{
				return true;
			}
			return PlayerPrefs.GetInt("ShowedChallengeModePopup") > 0;
		}
		set
		{
			if (!isChallengeModeOpened())
			{
				PlayerPrefs.SetInt("ShowedChallengeModePopup", value ? 1 : 0);
			}
		}
	}

	public UserProfileManager(string hack_lang)
	{
		Debug.Log("UserProfileManager Awake");
		requestStage = -1;
		string text = string.Empty;
		string text2 = string.Empty;
		//using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("java.util.Locale"))
		//{
		//	using (AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getDefault", new object[0]))
		//	{
		//		text2 = androidJavaObject.Call<string>("getCountry", new object[0]);
		//		text = androidJavaObject.Call<string>("getISO3Language", new object[0]);
		//	}
		//}
		switch (text)
		{
		case "zh":
		case "zho":
			if (text2 == "CN")
			{
				lang = "zh-Hans";
			}
			else
			{
				lang = "zh-Hant";
			}
			break;
		case "kor":
			lang = "ko";
			break;
		case "jpn":
			lang = "ja";
			break;
		case "fre":
		case "fra":
			lang = "fr";
			break;
		case "ger":
		case "deu":
			lang = "de";
			break;
		default:
			lang = "en";
			break;
		}
		Debug.Log("lang = " + lang);
	}

	[ContextMenu("DelPref")]
	private void delPref()
	{
		PlayerPrefs.DeleteAll();
	}

	public void Start()
	{
		Debug.Log("UserProfileManager start");
		CheckGift_PPA();
		currentSceneId = "FD_Title";
		int pie = getPie();
		addItemCount("item01_lvl", pie);
		addPie(-1 * pie);
		if (!PlayerPrefs.HasKey("gameLevel") || !PlayerPrefs.HasKey("coin"))
		{
			Debug.Log("Andy: First time launch. reset game data");
			PlayerPrefs.SetInt("firstLaunch", 0);
			PlayerPrefs.Save();
			resetGameData();
		}
		initialized = true;
	}

	public void Update()
	{
		switch (getCurrentScene())
		{
		case "FD_TitleScene":
			CheckMessage();
			break;
		case "FD_Upgrade":
			CheckMessage();
			break;
		}
	}

	public void CheckMessage()
	{
		if (Quantity > 0)
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, Message, !MunerisController.Instance.takeoverIsVideo && _chineseNewYear);
			addCoin(Quantity);
			Quantity = 0;
			Message = string.Empty;
			//MunerisController.Instance.takeoverIsVideo = false;
		}
	}

	public bool getIsNormalVersion()
	{
		return true;
	}

	public string getAndroidMarket()
	{
		return "google";
	}

	private bool IsGoogleAndroidMarket()
	{
		return true;
	}

	public string getBundleId()
	{
		string empty = string.Empty;
		return (!IsGoogleAndroidMarket()) ? "com.webprancer.amazon.garfieldDefense" : "com.webprancer.google.garfieldDefense";
	}

	public string getAppKey()
	{
		string empty = string.Empty;
		return (!IsGoogleAndroidMarket()) ? "7c295f07469047f0ae7e63752347abd9" : "91ad6500ecb446b8bcb53ccf0cc09bb8";
	}

	public string getAppID()
	{
		return string.Empty;
	}

	public string getVersion()
	{
		string empty = string.Empty;
		return Application.version;
	}

	public string getPPA_Devout()
	{
		string empty = string.Empty;
		return (!IsGoogleAndroidMarket()) ? "da3e5e25-3ac2-4e4c-b3c2-a814fd31d9b4" : "325f0d0c-15d4-40eb-8305-48bd40aa6b78";
	}

	public int getGameLevel()
	{
		return GetAndSetPlayerPrefsIntValue("gameLevel", 1);
	}

	public void setGameLevel(int gLevel)
	{
		switch (gLevel)
		{
		case 2:
			resetTutorial(0);
			if (!PlayerPrefs.HasKey("PPA_Lvl2"))
			{
				Debug.Log(">>> Action Complete: PPA Lvl2");
				string empty2 = string.Empty;
				empty2 = ((!IsGoogleAndroidMarket()) ? "e90f7f24-9193-46a2-abbf-7144214ed662" : "1a9dc340-de41-454d-92cc-723aed7a4492");
				//MunerisController.Instance.ReportEvent(empty2);
				PlayerPrefs.SetInt("PPA_Lvl2", 1);
			}
			break;
		case 4:
			resetTutorial(0);
			if (!PlayerPrefs.HasKey("PPA_Lvl4"))
			{
				Debug.Log(">>> Action Complete: PPA Lvl4");
				string empty3 = string.Empty;
				empty3 = ((!IsGoogleAndroidMarket()) ? "38a3af60-60be-4e0c-8119-8f0a10fdd82c" : "801eb347-386d-4c17-bf28-c227568fd24a");
				//MunerisController.Instance.ReportEvent(empty3);
				PlayerPrefs.SetInt("PPA_Lvl4", 1);
			}
			break;
		case 10:
			resetTutorial(0);
			if (!PlayerPrefs.HasKey("PPA_Lvl10"))
			{
				Debug.Log(">>> Action Complete: PPA Lvl10");
				string empty = string.Empty;
				empty = ((!IsGoogleAndroidMarket()) ? "4457fc2f-f9f0-405c-81ed-acdc77d736cb" : "f62b928f-e832-40f9-a5b5-c0733fad99bd");
				//MunerisController.Instance.ReportEvent(empty);
				PlayerPrefs.SetInt("PPA_Lvl10", 1);
			}
			break;
		}
		if (gLevel == 14 && !PlayerPrefs.HasKey("PPA_Laminaceph"))
		{
			Debug.Log(">>> Action Complete: PPA Laminaceph");
			PlayerPrefs.SetInt("PPA_Laminaceph", 1);
			string empty4 = string.Empty;
			empty4 = ((!IsGoogleAndroidMarket()) ? "2acc1336-2f11-45eb-b114-3b01d129298d" : "b1a75bc3-31fc-4d47-9866-502936e7c2d9");
			//MunerisController.Instance.ReportEvent(empty4);
		}
		if (gLevel >= 60)
		{
			gLevel = 60;
			PlayerPrefs.SetInt("reachedMaxLevel", 1);
		}
		if (gLevel < requestStage)
		{
			gLevel = requestStage;
			requestStage = -1;
		}
		Debug.Log("New Level");
		PlayerPrefs.SetInt("gameLevel", gLevel);
		PlayerPrefs.SetInt("isNewLevel", 1);
	}

	public int getMaxLevel()
	{
		return 60;
	}

	public int getIsReachedMaxLevel()
	{
		return GetAndSetPlayerPrefsIntValue("reachedMaxLevel", 0);
	}

	public int getTotalSpAttackSlot()
	{
		string key = "spAttackSlot";
		if (!PlayerPrefs.HasKey(key))
		{
			setTotalSpAttackSlot(2);
		}
		spAttackSlot = PlayerPrefs.GetInt(key);
		return spAttackSlot;
	}

	public void setTotalSpAttackSlot(int totalSlot)
	{
		Debug.Log("++++ TOTOAL SLOT: " + totalSlot + " ++++++");
		PlayerPrefs.SetInt("spAttackSlot", totalSlot);
		spAttackSlot = totalSlot;
	}

	public int getIsNewLevel()
	{
		if (!PlayerPrefs.HasKey("isNewLevel"))
		{
			resetIsNewLevel();
		}
		return PlayerPrefs.GetInt("isNewLevel");
	}

	public void resetIsNewLevel()
	{
		PlayerPrefs.SetInt("isNewLevel", 0);
	}

	public int getIsTutorial()
	{
		return PlayerPrefs.GetInt("isTutorial");
	}

	public void resetTutorial(int isTutorial)
	{
		PlayerPrefs.SetInt("isTutorial", isTutorial);
	}

	public int getSoundSetting()
	{
		return PlayerPrefs.GetInt("soundMute", 1);
	}

	public void setSoundSetting(int sound)
	{
		PlayerPrefs.SetInt("soundMute", sound);
	}

	public int getPurchasedCoins()
	{
		if (!PlayerPrefs.HasKey("purchasedCoins"))
		{
			addPurchasedCoins(0);
		}
		return PlayerPrefs.GetInt("purchasedCoins");
	}

	public void addPurchasedCoins(int coins)
	{
		string key = "purchasedCoins";
		int @int = PlayerPrefs.GetInt(key, 0);
		PlayerPrefs.SetInt(key, @int + coins);
	}

	public int GetAndSetPlayerPrefsIntValue(string key, int valueDefault)
	{
		if (!PlayerPrefs.HasKey(key))
		{
			PlayerPrefs.SetInt(key, valueDefault);
		}
		return PlayerPrefs.GetInt(key);
	}

	public int getTotalCoinsSpent()
	{
		return GetAndSetPlayerPrefsIntValue("coinSpent", 0);
	}

	public void addCoinsSpent(int coin)
	{
		int num = getTotalCoinsSpent() + coin;
		if (num < 0)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("coinSpent", num);
	}

	public int getPie()
	{
		return GetAndSetPlayerPrefsIntValue("pie", 0);
	}

	public void addPie(int num)
	{
		int pie = getPie();
		Debug.Log("=++++++ Add Pie +=======");
		Debug.Log("Original: " + pie);
		pie += num;
		if (pie < 0)
		{
			pie = 0;
		}
		Debug.Log("After: " + pie);
		PlayerPrefs.SetInt("pie", pie);
	}

	public int getCoin()
	{
		return GetAndSetPlayerPrefsIntValue("coin", 0);
	}

	public void addCoin(int num)
	{
		int coin = getCoin();
		Debug.Log("++++++ Add Coin ++++++ ");
		Debug.Log("++++ current scene: " + getCurrentScene());
		Debug.Log("original: " + coin);
		coin += num;
		if (coin < 0)
		{
			coin = 0;
		}
		Debug.Log("new: " + coin);
		PlayerPrefs.SetInt("coin", coin);
		if (getCurrentScene() == "FD_Upgrade")
		{
			Debug.Log(">>>> In Upgrade Scene >>>> ");
			UpgradeSceneController component = GameObject.Find("Controllers").GetComponent<UpgradeSceneController>();
			component.updateCoinLabel();
		}
	}

	public ArrayList getSelectedArmy()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < 5; i++)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("UnitName", PlayerPrefs.GetString("slot" + i + "_unitName"));
			dictionary.Add("id", PlayerPrefs.GetString("slot" + i + "_id"));
			dictionary.Add("price", PlayerPrefs.GetString("slot" + i + "_price"));
			dictionary.Add("cooldown", PlayerPrefs.GetString("slot" + i + "_cooldown"));
			Debug.Log("++++ Get Army: " + PlayerPrefs.GetString("slot" + i + "_unitName") + " | " + PlayerPrefs.GetString("slot" + i + "_id") + " | " + PlayerPrefs.GetString("slot" + i + "_price") + " | " + PlayerPrefs.GetString("slot" + i + "_cooldown"));
			arrayList.Add(dictionary);
		}
		return arrayList;
	}

	public void setSelectedArmy(ArrayList armyList)
	{
		Debug.Log("++++ update special army+++++ " + armyList.Count);
		for (int i = 0; i < 5; i++)
		{
			if (i < armyList.Count)
			{
				Dictionary<string, string> dictionary = (Dictionary<string, string>)armyList[i];
				PlayerPrefs.SetString("slot" + i + "_unitName", dictionary["UnitName"]);
				PlayerPrefs.SetString("slot" + i + "_id", dictionary["id"]);
				PlayerPrefs.SetString("slot" + i + "_price", dictionary["price"]);
				PlayerPrefs.SetString("slot" + i + "_cooldown", dictionary["cooldown"]);
			}
			else
			{
				PlayerPrefs.SetString("slot" + i + "_unitName", string.Empty);
				PlayerPrefs.SetString("slot" + i + "_id", string.Empty);
				PlayerPrefs.SetString("slot" + i + "_price", string.Empty);
				PlayerPrefs.SetString("slot" + i + "_cooldown", string.Empty);
			}
		}
	}

	public ArrayList getSelectedSpecialAttack()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < getTotalSpAttackSlot(); i++)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("UnitName", PlayerPrefs.GetString("sp_slot" + i + "_unitName"));
			dictionary.Add("id", PlayerPrefs.GetString("sp_slot" + i + "_id"));
			dictionary.Add("cooldown", PlayerPrefs.GetString("sp_slot" + i + "_cooldown"));
			arrayList.Add(dictionary);
			Debug.Log("++++ Get SelectedSpecialAttack: " + PlayerPrefs.GetString("sp_slot" + i + "_unitName") + " | " + PlayerPrefs.GetString("sp_slot" + i + "_id") + " | " + PlayerPrefs.GetString("sp_slot" + i + "_cooldown"));
		}
		return arrayList;
	}

	public void setSelectedSpecialAttack(ArrayList spList)
	{
		Debug.Log("++++ update special attack+++++: " + spList.Count);
		for (int i = 0; i < getTotalSpAttackSlot(); i++)
		{
			if (i < spList.Count)
			{
				Dictionary<string, string> dictionary = (Dictionary<string, string>)spList[i];
				Debug.Log("+++ UnitName: " + dictionary["UnitName"]);
				PlayerPrefs.SetString("sp_slot" + i + "_unitName", dictionary["UnitName"]);
				PlayerPrefs.SetString("sp_slot" + i + "_id", dictionary["id"]);
				PlayerPrefs.SetString("sp_slot" + i + "_cooldown", dictionary["cooldown"]);
			}
			else
			{
				PlayerPrefs.SetString("sp_slot" + i + "_unitName", string.Empty);
				PlayerPrefs.SetString("sp_slot" + i + "_id", string.Empty);
				PlayerPrefs.SetString("sp_slot" + i + "_cooldown", string.Empty);
			}
		}
	}

	public void resetGameData()
	{
		PlayerPrefs.SetInt("gameLevel", 1);
		PlayerPrefs.SetInt("reachedMaxLevel", 0);
		if (getGameLevel_HalloweenMode() > 0)
		{
			setGameLevel_HalloweenMode(1);
		}
		if (getGameLevel_NMode() > 0)
		{
			setGameLevel_NMode(1);
		}
		int num = 0 + getPurchasedCoins();
		Debug.Log("++++  Reset Game: Coins:" + num);
		PlayerPrefs.SetInt("coin", num);
		PlayerPrefs.SetInt("coinSpent", 0);
		resetFinishedLevel_NMode();
		resetFinishedLevel_HalloweenMode();
		resetTutorial(1);
		setSoundSetting(1);
		resetIsNewLevel();
		GiftManager.Reset();
		for (int i = 0; i < getTotalSpAttackSlot(); i++)
		{
			PlayerPrefs.SetString("sp_slot" + i + "_unitName", string.Empty);
			PlayerPrefs.SetString("sp_slot" + i + "_id", string.Empty);
		}
		for (int j = 0; j < 5; j++)
		{
			PlayerPrefs.SetString("slot" + j + "_unitName", string.Empty);
			PlayerPrefs.SetString("slot" + j + "_id", string.Empty);
			PlayerPrefs.SetString("slot" + j + "_price", string.Empty);
		}
		setTotalSpAttackSlot(2);
		StarStrike_ArmyUnitConfiguration component = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		ArrayList goodGuy = component.GetGoodGuy();
		foreach (string item in goodGuy)
		{
			StarStrike_ObjectDefinition unitDefinition = component.GetUnitDefinition(item);
			if (unitDefinition.HasAttribute("key"))
			{
				PlayerPrefs.SetInt(unitDefinition.GetAttributeValue("key"), 0);
			}
		}
		StarStrike_SpecialAttackConfiguration component2 = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		ArrayList specialAttackArray = component2.GetSpecialAttackArray();
		foreach (string item2 in specialAttackArray)
		{
			StarStrike_ObjectDefinition definition = component2.GetDefinition(item2);
			if (definition.HasAttribute("key"))
			{
				PlayerPrefs.SetInt(definition.GetAttributeValue("key"), 0);
			}
		}
		FD_ForrestConfiguration component3 = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
		ArrayList forrestArray = component3.GetForrestArray();
		foreach (string item3 in forrestArray)
		{
			StarStrike_ObjectDefinition definition2 = component3.GetDefinition(item3);
			PlayerPrefs.SetInt(definition2.GetAttributeValue("key"), 0);
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("UnitName", "FireBall");
		dictionary.Add("id", "0");
		dictionary.Add("cooldown", "10");
		ArrayList arrayList = new ArrayList();
		arrayList.Add(dictionary);
		setSelectedSpecialAttack(arrayList);
	}

	public void resetGameData_lvl10()
	{
		resetGameData();
		PlayerPrefs.SetInt("gameLevel", 20);
		addCoin(2400);
	}

	public void resetGameData_lvl20()
	{
		resetGameData();
		PlayerPrefs.SetInt("gameLevel", 30);
		addCoin(750000);
	}

	public void resetGameData_lvl30()
	{
		resetGameData();
		PlayerPrefs.SetInt("gameLevel", 40);
		addCoin(1700000);
	}

	public void resetGameData_lvl40()
	{
		resetGameData();
		PlayerPrefs.SetInt("gameLevel", 50);
		addCoin(3300000);
	}

	public void resetManyPoints()
	{
		addCoin(100000);
	}

	public void setRequestStage(int stage)
	{
		requestStage = stage;
	}

	public int getRequestStage()
	{
		return requestStage;
	}

	public void setCurrentScene(string sceneId)
	{
		currentSceneId = sceneId;
	}

	public string getCurrentScene()
	{
		return currentSceneId;
	}

	public string getLangCode()
	{
		return lang;
	}

	private void OnApplicationPause(bool b)
	{
		if (!b && getCurrentScene() != "Mini_Game" && getCurrentScene() != "FD_Title" && IsGoogleAndroidMarket())
		{
			checkWallpaperBonus();
		}
	}

	private void OnApplicationFocus(bool state)
	{
		OnApplicationPause(state);
	}

	private void CheckGift_PPA()
	{
		switch (GiftManager.GetGiftDay())
		{
		case 2:
		{
			string eventName = ((!IsGoogleAndroidMarket()) ? "72549713-1299-4832-a4d1-78f89cec69dc" : "c3f5a26b-5140-439c-acae-55fc44cdaf3d");
			if (!PlayerPrefs.HasKey("PPA_ComBackDay2"))
			{
				PlayerPrefs.SetInt("PPA_ComBackDay2", 1);
				//MunerisController.Instance.ReportEvent(eventName);
				Debug.Log(">>>> PPA Action Complete: Come back at day 2");
			}
			break;
		}
		case 3:
		{
			string eventName = "f4fe1deb-7f37-466d-a9e4-3e8374c9f81b";
			if (!PlayerPrefs.HasKey("PPA_ComBackDay3"))
			{
				PlayerPrefs.SetInt("PPA_ComBackDay3", 1);
				//MunerisController.Instance.ReportEvent(eventName);
				Debug.Log(">>>> PPA Action Complete: Come back at day 3");
			}
			break;
		}
		case 4:
		{
			string eventName = "247fc3d2-9b30-4bc2-b835-14f699889628";
			if (!PlayerPrefs.HasKey("PPA_ComBackDay4"))
			{
				PlayerPrefs.SetInt("PPA_ComBackDay4", 1);
				//MunerisController.Instance.ReportEvent(eventName);
				Debug.Log(">>>> PPA Action Complete: Come back at day 4");
			}
			break;
		}
		case 5:
		{
			string eventName = "59701b7e-12c3-41de-a2c2-ecac04819acb";
			if (!PlayerPrefs.HasKey("PPA_ComBackDay5"))
			{
				PlayerPrefs.SetInt("PPA_ComBackDay5", 1);
				//MunerisController.Instance.ReportEvent(eventName);
				Debug.Log(">>>> PPA Action Complete: Come back at day 5");
			}
			break;
		}
		case 7:
		{
			string eventName = "bbb75ed5-f8e2-46c6-ad47-2480d8f0fd24";
			if (!PlayerPrefs.HasKey("PPA_ComBackDay7"))
			{
				PlayerPrefs.SetInt("PPA_ComBackDay7", 1);
				//MunerisController.Instance.ReportEvent(eventName);
				Debug.Log(">>>> PPA Action Complete: Come back at day 7");
			}
			break;
		}
		case 6:
			break;
		}
	}

	public int getItemCount(string itemKey)
	{
		return GetAndSetPlayerPrefsIntValue(itemKey, 0);
	}

	public void addItemCount(string itemKey, int quantity)
	{
		int itemCount = getItemCount(itemKey);
		itemCount += quantity;
		if (itemCount < 0)
		{
			itemCount = 0;
		}
		PlayerPrefs.SetInt(itemKey, itemCount);
	}

	public int getGoldenSlotCount(string itemKey)
	{
		return GetAndSetPlayerPrefsIntValue(itemKey, 0);
	}

	public void setGoldenSlotCount(string itemKey, int quantity)
	{
		PlayerPrefs.SetInt(itemKey, quantity);
	}

	public int getBirthdayGiftFlag()
	{
		return GetAndSetPlayerPrefsIntValue("gotBithdayGift", 0);
	}

	public void setBirthdayGiftFlag(int flag)
	{
		PlayerPrefs.SetInt("gotBithdayGift", flag);
	}

	public int getGameLevel_NMode()
	{
		return GetAndSetPlayerPrefsIntValue("gameLevel_NMode", 0);
	}

	public void setGameLevel_NMode(int gLevel)
	{
		if (gLevel > 30)
		{
			gLevel = 30;
			PlayerPrefs.SetInt("reachedMaxLevel_NMode", 1);
		}
		PlayerPrefs.SetInt("gameLevel_NMode", gLevel);
		PlayerPrefs.SetInt("isNewLevel_NMode", 1);
	}

	public void resetGameLevel_NMode()
	{
		PlayerPrefs.SetInt("reachedMaxLevel_NMode", 0);
		PlayerPrefs.SetInt("gameLevel_NMode", 1);
		PlayerPrefs.SetInt("isNewLevel_NMode", 1);
	}

	public int getMaxLevel_NMode()
	{
		return 30;
	}

	public int getIsReachedMaxLevel_NMode()
	{
		return GetAndSetPlayerPrefsIntValue("reachedMaxLevel_NMode", 0);
	}

	public bool isUnlocked_NMode()
	{
		return getGameLevel_NMode() > 0;
	}

	public void setUnlocked_NMode()
	{
		setGameLevel_NMode(1);
	}

	public int getFinishedLevel_NMode()
	{
		return GetAndSetPlayerPrefsIntValue("finishedLevel_NMode", 0);
	}

	public void setFinishedLevel_NMode(int gLevel)
	{
		if (gLevel > getFinishedLevel_NMode())
		{
			if (gLevel > 30)
			{
				gLevel = 30;
			}
			PlayerPrefs.SetInt("finishedLevel_NMode", gLevel);
		}
	}

	public void resetFinishedLevel_NMode()
	{
		PlayerPrefs.SetInt("finishedLevel_NMode", 0);
	}

	public InGamePlayMode getCurrentPlayMode()
	{
		return currentPlayMode;
	}

	public void setCurrentPlayMode(InGamePlayMode playMode)
	{
		currentPlayMode = playMode;
	}

	public bool isChallengeModeOpened()
	{
		if (Application.isEditor)
		{
			return true;
		}
		bool result = false;
		try
		{
			JsonObject cargo = /*MunerisController.Instance.GetCargo()*/null;
			if (cargo.ContainsKey("ChallengeMode"))
			{
				string text = cargo["ChallengeMode"].ToString();
				result = text.Equals("on");
				return result;
			}
			return result;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			return result;
		}
	}

	public int getGameLevel_HalloweenMode()
	{
		int num = GetAndSetPlayerPrefsIntValue("gameLevel_HalloweenMode", 0);
		if (isChallengeModeOpened() && num == 0)
		{
			num = 1;
		}
		return num;
	}

	public void setGameLevel_HalloweenMode(int gLevel)
	{
		if (gLevel > 30)
		{
			gLevel = 30;
			PlayerPrefs.SetInt("reachedMaxLevel_HalloweenMode", 1);
		}
		PlayerPrefs.SetInt("gameLevel_HalloweenMode", gLevel);
		PlayerPrefs.SetInt("isNewLevel_HalloweenMode", 1);
	}

	public void resetGameLevel_HalloweenMode()
	{
		PlayerPrefs.SetInt("reachedMaxLevel_HalloweenMode", 0);
		PlayerPrefs.SetInt("gameLevel_HalloweenMode", 1);
		PlayerPrefs.SetInt("isNewLevel_HalloweenMode", 1);
	}

	public int getMaxLevel_HalloweenMode()
	{
		return 30;
	}

	public int getIsReachedMaxLevel_HalloweenMode()
	{
		return GetAndSetPlayerPrefsIntValue("reachedMaxLevel_HalloweenMode", 0);
	}

	public bool isUnlocked_HalloweenMode()
	{
		return getGameLevel_HalloweenMode() > 0 || isChallengeModeOpened();
	}

	public void setUnlocked_HalloweenMode()
	{
		setGameLevel_HalloweenMode(1);
	}

	public int getFinishedLevel_HalloweenMode()
	{
		return GetAndSetPlayerPrefsIntValue("finishedLevel_HalloweenMode", 0);
	}

	public void setFinishedLevel_HalloweenMode(int gLevel)
	{
		if (gLevel > getFinishedLevel_HalloweenMode())
		{
			if (gLevel > 30)
			{
				gLevel = 30;
			}
			PlayerPrefs.SetInt("finishedLevel_HalloweenMode", gLevel);
		}
	}

	public void resetFinishedLevel_HalloweenMode()
	{
		PlayerPrefs.SetInt("finishedLevel_HalloweenMode", 0);
	}

	public void checkWallpaperBonus()
	{
		Debug.Log("++++++++ checking wallpaper bonus =+++++++ ");
		int bonus = LiveWallpaperBridge.Bonus;
		Debug.Log("bonus= " + bonus);
		if (bonus > 0)
		{
			PopupManager.Instance.ShowWallpaperPopup(PopupManager.PopupType.Notification, bonus);
		}
		Debug.Log("add bonus= " + bonus);
		addCoin(bonus);
		int bonus2 = 0;
		if (!Application.isEditor)
		{
			LiveWallpaperBridge.Bonus = bonus2;
		}
	}
}
