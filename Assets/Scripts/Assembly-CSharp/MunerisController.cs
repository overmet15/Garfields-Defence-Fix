using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Muneris;
using Muneris.Appevent;
using Muneris.Pushnotification;
using Muneris.Takeover;
using Muneris.Virtualgood;
using Muneris.Virtualitem;
using Outblaze;
using UnityEngine;

public class MunerisController : MonoBehaviour
{
	private const string videoRewardEv = "video";

	private const string interstitialEv = "interstitial";

	private const string offerwallEv = "offerwall";

	private const string ppEv = "privacypolicy";

	private const string moreAppsEv = "moreapps";

	private const string customerSupEv = "customersupport";

	private const string crossPromoEv = "crosspromo";

	private const string moreGamesEv = "moregames";

	private const string weiboSharingCargoName = "WeiboSharing";

	private const string msgTargetsText = "msgTargets";

	private const string BOGOFPromo = "BuyOneGetOneFree_IAPPromo";

	private int weiboReward = 300;

	private Dictionary<string, VirtualGood> _mapVirtualGood;

	private bool _virtualGoodsReady;

	public bool ShowingTakeover;

	private UserProfileManager user_obj;

	private MunerisCallback _callback;

	private string[] productNames = new string[5] { "package1", "package2", "package3", "package4", "package5" };

	public static MunerisController Instance;

	public bool takeoverIsVideo;

	public MunerisCallback Callback
	{
		get
		{
			return _callback;
		}
	}

	public string InterstitialEventName
	{
		get
		{
			return "interstitial";
		}
	}

	public string MoreAppsEventName
	{
		get
		{
			return "moreapps";
		}
	}

	public string CrossPromoEventName
	{
		get
		{
			return "crosspromo";
		}
	}

	public string CustomerSupportEventName
	{
		get
		{
			return "customersupport";
		}
	}

	public string PrivacyPolicyEventName
	{
		get
		{
			return "privacypolicy";
		}
	}

	public JsonObject cargo { get; set; }

	private void Awake()
	{
		//Instance = this;
		//UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		//user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		//if (!Application.isEditor)
		//{
		//	_callback = new MunerisCallback();
		//	Callbacks.add(_callback);
		//}
		//PrepareVirtualGoods();
		//PreparePushNotifications();
		//Debug.Log("MunerisController - Awake() : Finished");
	}

	private void PreparePushNotifications()
	{
		//PushNotifications.registerDevice().execute();
	}

	public void CheckListVirtualGoods()
	{
		//if (_mapVirtualGood == null)
		//{
		//	_mapVirtualGood = new Dictionary<string, VirtualGood>();
		//}
		//Debug.Log("CheckListVirtualGoods: BEFORE calling VirtualGoods.find().setVirtualGoodIds");
		//VirtualGoods.find().setCallback(_callback).execute();
		//Debug.Log("CheckListVirtualGoods: AFTER calling VirtualGoods.find().setVirtualGoodIds");
	}

	private void PrepareVirtualGoods()
	{
		//Debug.Log("PrepareVirtualGoods: Enter");
		//_mapVirtualGood = new Dictionary<string, VirtualGood>();
		//Debug.Log("PrepareVirtualGoods: BEFORE calling VirtualGoods.find().setVirtualGoodIds");
		//VirtualGoods.find().setCallback(_callback).execute();
		//Debug.Log("PrepareVirtualGoods: AFTER calling VirtualGoods.find().setVirtualGoodIds");
		//Debug.Log("PrepareVirtualGoods: Exit");
	}

	public void AddVirtualGood(VirtualGood newvgood)
	{
		//if (newvgood != null)
		//{
		//	string virtualGoodId = newvgood.getVirtualGoodId();
		//	if (!_mapVirtualGood.ContainsKey(virtualGoodId) && !virtualGoodId.Equals(string.Empty))
		//	{
		//		_mapVirtualGood[virtualGoodId] = newvgood;
		//		Debug.Log("AddVirtualGood: VirtualGood Added: " + virtualGoodId);
		//	}
		//	else
		//	{
		//		_mapVirtualGood[virtualGoodId] = newvgood;
		//		Debug.Log("Note: This Virtual Good(" + virtualGoodId + ") is already on our list or the id is an empty string. We update it.");
		//	}
		//}
		//else
		//{
		//	Debug.LogWarning("Error: This Virtual Good is empty.");
		//}
	}

	public void SetVirtualGoodReady()
	{
		_virtualGoodsReady = true;
	}

	public bool IsVirtualGoodReady()
	{
		//return _virtualGoodsReady;
		return false;
	}

	public VirtualGood GetVirtualGood(string virtualGoodId)
	{
		//if (_mapVirtualGood.ContainsKey(virtualGoodId))
		//{
		//	return _mapVirtualGood[virtualGoodId];
		//}
		//Debug.LogWarning("This virtual Good doesn't exist or RequestVirtualGoods has not been called or the server has not respond yet.");
		return null;
	}

	public VirtualGood GetVirtualGoodById(int id)
	{
		//if (id <= productNames.Length)
		//{
		//	return GetVirtualGood(productNames[id]);
		//}
		//Debug.LogWarning("This Virtual Good Id doesn't exist.");
		return null;
	}

	public string GetVirtualGoodPackageNameById(int id)
	{
		//if (id <= productNames.Length)
		//{
		//	return productNames[id];
		//}
		//Debug.LogWarning("This Virtual Good Id doesn't exist.");
		return string.Empty;
	}

	public int GetVirtualGoodQuantity(string virtualGoodId)
	{
		//VirtualGood virtualGood = GetVirtualGood(virtualGoodId);
		//int num = 0;
		//if (virtualGoodId != null)
		//{
		//	List<VirtualItemAndQuantity> virtualItemAndQuantities = virtualGood.getVirtualItemBundle().getVirtualItemAndQuantities();
		//	for (int i = 0; i < virtualItemAndQuantities.Count; i++)
		//	{
		//		num += virtualItemAndQuantities[i].getQuantity();
		//	}
		//}
		//return num;
		return 0;
	}

	public int GetVirtualGoodCount()
	{
		//if (_mapVirtualGood != null)
		//{
		//	return _mapVirtualGood.Count;
		//}
		return 0;
	}

	public bool Purchase(string packageID)
	{
		//Debug.Log("Purchase package: " + packageID);
		//VirtualGood virtualGood = GetVirtualGood(packageID);
		//if (virtualGood != null)
		//{
		//	Debug.Log("Call to Purchase " + packageID);
		//	virtualGood.purchase().execute();
		//	return true;
		//}
		//Debug.Log("Error trying to Purchase " + packageID);
		return false;
	}

	public bool IsMunerisReady()
	{
		//return MunerisClient.isReady();
		return false;
	}

	public void ReportEvent(string eventName)
	{
		//Debug.Log(">>>MUN 5 -Event reported: " + eventName);
		//if (!Application.isEditor)
		//{
		//	AppEvents.report(eventName).execute();
		//}
	}

	public void ReportEventWithInternetCheck(string eventName)
	{
		//Debug.Log(">>>MUN 5 -Event reported: " + eventName);
		//if (Application.internetReachability != 0)
		//{
		//	if (!Application.isEditor)
		//	{
		//		AppEvents.report(eventName).execute();
		//	}
		//}
		//else
		//{
		//	LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		//	PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance.getLangData("NoInternetAccessError2"));
		//}
	}

	public void ReportEventWithEventCheck(string eventName)
	{
		//Debug.Log(">>>MUN 5 -Event reported: " + eventName);
		//if (Application.internetReachability != 0)
		//{
		//	if (HasEvent(eventName))
		//	{
		//		if (!Application.isEditor)
		//		{
		//			AppEvents.report(eventName).execute();
		//		}
		//	}
		//	else
		//	{
		//		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		//		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance.getLangData("NoEventAccessError"));
		//	}
		//}
		//else
		//{
		//	LanguageManager languageManagerInstance2 = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		//	PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance2.getLangData("NoInternetAccessError2"));
		//}
	}

	public void ReportEvent(string eventName, Hashtable param)
	{
		//Debug.Log(">>>MUN 5 -Event reported: " + eventName);
		//if (Application.isEditor)
		//{
		//	return;
		//}
		//ReportAppEventCommand reportAppEventCommand = AppEvents.report(eventName);
		//foreach (DictionaryEntry item in param)
		//{
		//	reportAppEventCommand.addParameter(item.Key.ToString(), item.Value.ToString());
		//}
		//reportAppEventCommand.execute();
	}

	public void ReportVideoReward()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithEventCheck("video");
		//	takeoverIsVideo = true;
		//}
	}

	public void ReportInterstitialAd()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEvent("interstitial");
		//}
	}

	public void ReportOfferWall()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithEventCheck("offerwall");
		//}
	}

	public void ReportMoreApps()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithInternetCheck("moreapps");
		//}
	}

	public void ReportPrivacyPolicy()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithInternetCheck("privacypolicy");
		//}
	}

	public void ReportCustomerSupport()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithInternetCheck("customersupport");
		//}
	}

	public void ReportCrossPromo()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithInternetCheck("crosspromo");
		//}
	}

	public void ReportMoreGames()
	{
		//if (!ShowingTakeover)
		//{
		//	ReportEventWithInternetCheck("moregames");
		//}
	}

	public bool HasOfferwallEvent()
	{
		//return HasEvent("offerwall");
		return false;
	}

	public bool HasVideoRewardEvent()
	{
		//return HasEvent("video");
		return false;
	}

	public bool HasCrossPromoEvent()
	{
		//return HasEvent("crosspromo");
		return false;
	}

	public bool HasMoreGamesEvent()
	{
		//return HasEvent("moregames");
		return false;
	}

	public bool HasOffers()
	{
		//return HasEvent("offers");
		return false;
	}

	public bool HasEvent(string eventName)
	{
		//TakeoverAvailability takeoverAvailability = Takeovers.checkAvailability(eventName);
		//int availableCount = takeoverAvailability.getAvailableCount();
		//if (!takeoverAvailability.isPrecise() || (availableCount > 0 && takeoverAvailability.isPrecise()))
		//{
		//	Debug.Log(availableCount + " takeovers available for " + eventName);
		//	return true;
		//}
		//Debug.Log("no takeovers available for " + eventName);
		return false;
	}

	public JsonObject GetCargo()
	{
		//return Instance.cargo;
		return null;
	}

	public bool GetWeiboSharing()
	{
		//bool result = true;
		//if (cargo != null && cargo.ContainsKey("WeiboSharing"))
		//{
		//	float result2 = 0f;
		//	if (!float.TryParse(cargo["WeiboSharing"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out result2))
		//	{
		//		Debug.Log("WARN: There were some errors reading the HasPromotion from the cargo. We use default value.");
		//		result2 = 0f;
		//	}
		//	result2 = Mathf.Clamp01(result2);
		//	result = result2 != 1f;
		//	Debug.Log("weiboSharing " + result2);
		//}
		//return result;
		return false;
	}

	public Hashtable BOGOFBonus()
	{
		//int num = 1;
		//string text = string.Empty;
		//Hashtable hashtable = new Hashtable();
		//try
		//{
		//	if (cargo != null && cargo.ContainsKey("msgTargets"))
		//	{
		//		Debug.Log("cargo contain msgTargets");
		//		Dictionary<string, object> dictionary = cargo["msgTargets"] as Dictionary<string, object>;
		//		if (dictionary.ContainsKey("BuyOneGetOneFree_IAPPromo"))
		//		{
		//			Debug.Log("cargo contain BOGOFPromo");
		//			Dictionary<string, object> dictionary2 = dictionary["BuyOneGetOneFree_IAPPromo"] as Dictionary<string, object>;
		//			if ((bool)dictionary2["enabled"])
		//			{
		//				num = 2;
		//				Dictionary<string, object> dictionary3 = dictionary2["text"] as Dictionary<string, object>;
		//				text = "\n" + dictionary3[user_obj.getLangCode()].ToString();
		//				Debug.Log("bonus = " + num + ", plusMessage = " + text);
		//			}
		//		}
		//	}
		//}
		//catch (Exception ex)
		//{
		//	Debug.Log(ex.Message);
		//}
		//hashtable.Add("bonus", num);
		//hashtable.Add("plusMessage", text);
		//return hashtable;
		return null;
	}

	public void RewardForWeiboPost(string message)
	{
		//LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		//UserProfileManager.Quantity += weiboReward;
		//UserProfileManager.Message = languageManagerInstance.getLangData("TapjoyPointReward1") + weiboReward + languageManagerInstance.getLangData("TapjoyPointReward2");
		//PlayerPrefs.SetInt("WeiboPosted", 1);
	}

	private void OnDestroy()
	{
//		PushNotifications.unregisterDevice().execute();
	}
}
