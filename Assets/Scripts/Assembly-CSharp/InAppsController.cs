using System;
using System.Collections;
using System.Collections.Generic;
using Muneris.MiniJSON;
using Muneris.Virtualgood;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InAppsController : MonoBehaviour
{
	private string defaultPackageJson = "{\"package1\":{\"coins\":1000,\"price\":\"US$ 0.99\"},\"package2\":{\"coins\":2500,\"price\":\"US$ 1.99\"},\"package3\":{\"coins\":10000,\"price\":\"US$ 6.99\"},\"package4\":{\"coins\":25000,\"price\":\"US$ 15.99\"},\"package5\":{\"coins\":50000,\"price\":\"US$ 29.99\"},\"package6\":{\"coins\":20000,\"price\":\"US$ 4.99\"}}";

	private Dictionary<string, object> defaultPackageData;

	private int[] packageCoins = new int[5] { 1000, 2500, 10000, 25000, 50000 };

	private string[] packagePrice = new string[5] { "US $0.99", "US $1.99", "US $6.99", "US $15.99", "US $29.99" };

	private UserProfileManager user_obj;

	private LanguageManager langMan;

	private MunerisDelegatesBehaviour m2;

	public UIPanel purchasePanel;

	public UIButton[] purchaseButtons;

	public UIButton tapjoyOfferWallBtn;

	public UIButton videoRewardBtn;

	public SpriteText videoLabel;

	public SpriteText offerLabel;

	public UIButton woodBoard;

	public float videoRewardYpPos;

	private int GetDefaultCoinValue(string packageName)
	{
		//Discarded unreachable code: IL_002e
		int num = 0;
		try
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)defaultPackageData[packageName];
			return (int)(long)dictionary["coins"];
		}
		catch (Exception)
		{
			throw;
		}
	}

	private string GetDefaultPriceValue(string packageName)
	{
		//Discarded unreachable code: IL_0031
		string empty = string.Empty;
		try
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)defaultPackageData[packageName];
			return (string)dictionary["price"];
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void Awake()
	{
		user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
	}

	private void Start()
	{
		woodBoard.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		videoRewardYpPos = videoRewardBtn.transform.localPosition.y;
		purchasePanel.Dismiss();
		tapjoyOfferWallBtn.SetValueChangedDelegate(ShowTJOfferWall);
		videoRewardBtn.SetValueChangedDelegate(ShowVideoReward);
		UIButton[] array = purchaseButtons;
		foreach (UIButton uIButton in array)
		{
			uIButton.AddValueChangedDelegate(Purchase);
		}
		tapjoyOfferWallBtn.AddValueChangedDelegate(ShowTJOfferWall);
		defaultPackageData = (Dictionary<string, object>)Json.Deserialize(defaultPackageJson);
	}

	private void OnEnable()
	{
		MunerisDelegatesBehaviour.PurchaseSucceededHandler += HandlePurchaseSucceededEvent;
		MunerisDelegatesBehaviour.PurchaseFailedHandler += HandlePurchaseFailedEvent;
		MunerisDelegatesBehaviour.PurchaseCancelledHandler += HandlePurchaseFailedEvent;
	}

	private void OnDisable()
	{
		MunerisDelegatesBehaviour.PurchaseSucceededHandler -= HandlePurchaseSucceededEvent;
		MunerisDelegatesBehaviour.PurchaseFailedHandler -= HandlePurchaseFailedEvent;
		MunerisDelegatesBehaviour.PurchaseCancelledHandler -= HandlePurchaseFailedEvent;
	}

	public void ShowPurchasePanel()
	{
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		purchasePanel.BringIn();
		UIPanel component = purchasePanel.transform.Find("Loading").GetComponent<UIPanel>();
		component.Dismiss();
		//if (MunerisController.Instance.GetVirtualGoodCount() != purchaseButtons.Length)
		//{
		//	Debug.LogWarning("ERROR: The amount of IAP in the server is different than in the app. This could casue problems");
		//}
		for (int i = 0; i < purchaseButtons.Length; i++)
		{
			Transform transform = purchaseButtons[i].transform;
			SpriteText component2 = transform.Find("Coin").GetComponent<SpriteText>();
			SpriteText component3 = transform.Find("Price").GetComponent<SpriteText>();
			IAPPackage component4 = purchaseButtons[i].GetComponent<IAPPackage>();
			fontManagerInstance.SetBigFontMat(component2);
			if (user_obj.getLangCode() == "zh-Hant")
			{
				component2.SetCharacterSize(40f);
			}
			bool isSpecial = false;
			VirtualGood virtualGood = IdentifyVirtualGood(component4, out isSpecial);
			if (virtualGood != null)
			{
				component2.Text = MunerisController.Instance.GetVirtualGoodQuantity(virtualGood.getVirtualGoodId()).ToString();
				component3.Text = ((virtualGood.getAppStoreLocalizedData() == null) ? GetDefaultPriceValue(virtualGood.getVirtualGoodId()) : virtualGood.getAppStoreLocalizedData().getPriceAndCurrency().getPrice());
			}
			else
			{
				Debug.LogWarning("ERROR: There was an error requested data from Muneris server. We show default data for package id: " + i);
				component2.Text = packageCoins[i].ToString();
				component3.Text = packagePrice[i];
			}
			if (isSpecial)
			{
				Color color = Color.white;
				ColorUtility.TryParseHtmlString("#00FF90", out color);
				component2.SetColor(color);
				component3.SetColor(color);
			}
			else
			{
				Color white = Color.white;
				component2.SetColor(white);
				component3.SetColor(white);
			}
		}
		enableAllButtons();
		SpriteText component5 = purchasePanel.transform.Find("inAppsPurchaseLabel").GetComponent<SpriteText>();
		SpriteText component6 = purchasePanel.transform.Find("inAppsPurchaseLabel2").GetComponent<SpriteText>();
		fontManagerInstance.SetBigFontMat(component5);
		fontManagerInstance.SetBigFontMat(component6);
		fontManagerInstance.SetBigFontMat(offerLabel);
		fontManagerInstance.SetBigFontMat(videoLabel);
		fontManagerInstance.SetFontColor(offerLabel, Color.white, offerLabel.color);
		fontManagerInstance.SetFontColor(videoLabel, Color.white, offerLabel.color);
		Debug.Log("Shop title:" + component6.Text);
		offerLabel.Text = langMan.getLangData("offerStr");
		videoLabel.Text = langMan.getLangData("videoStr");
		component5.Text = langMan.getLangData("ShopTitle1");
		component6.Text = langMan.getLangData("ShopTitle2");
		bool flag = MunerisController.Instance.HasVideoRewardEvent();
		bool flag2 = MunerisController.Instance.HasOfferwallEvent();
		flag2 = false;
		if (flag2)
		{
			tapjoyOfferWallBtn.gameObject.SetActive(true);
			tapjoyOfferWallBtn.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			offerLabel.enabled = true;
		}
		else
		{
			tapjoyOfferWallBtn.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			tapjoyOfferWallBtn.gameObject.SetActive(false);
			offerLabel.enabled = false;
		}
		if (flag)
		{
			videoRewardBtn.gameObject.SetActive(true);
			videoRewardBtn.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			videoLabel.enabled = true;
		}
		else
		{
			videoRewardBtn.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			videoRewardBtn.gameObject.SetActive(false);
			videoLabel.enabled = false;
		}
		if (!flag2 && flag)
		{
			float y = (videoRewardBtn.transform.localPosition.y + tapjoyOfferWallBtn.transform.localPosition.y) / 2f;
			videoRewardBtn.transform.localPosition = new Vector3(videoRewardBtn.transform.position.x, y, videoRewardBtn.transform.localPosition.z);
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (purchasePanel.gameObject.activeInHierarchy)
			{
				purchasePanel.Dismiss();
				return;
			}
			LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
			loadingManagerInstance.setSceneId("FD_TitleScene");
			SceneManager.LoadScene("Loading");
		}
	}

	private void enableAllButtons()
	{
		for (int i = 0; i < purchaseButtons.Length; i++)
		{
			purchaseButtons[i].SetControlState(UIButton.CONTROL_STATE.NORMAL);
		}
	}

	private void HidePurchasePanel()
	{
		purchasePanel.Dismiss();
		videoRewardBtn.transform.localPosition = new Vector3(videoRewardBtn.transform.position.x, videoRewardYpPos, videoRewardBtn.transform.localPosition.z);
	}

	private void Purchase(IUIObject button)
	{
		if (Application.internetReachability != 0)
		{
			bool flag = false;
			for (int i = 0; i < purchaseButtons.Length; i++)
			{
				if (button == purchaseButtons[i])
				{
					IAPPackage component = purchaseButtons[i].transform.GetComponent<IAPPackage>();
					bool isSpecial;
					VirtualGood virtualGood = IdentifyVirtualGood(component, out isSpecial);
					string virtualGoodId = virtualGood.getVirtualGoodId();
					Debug.Log("+++++++++++ Going to buy: " + virtualGoodId);
					if (MunerisController.Instance.Purchase(virtualGoodId))
					{
						GameObject gameObject = GameObject.Find("Loading Panel");
						UIPanel component2 = gameObject.transform.Find("Loading").GetComponent<UIPanel>();
						component2.BringIn();
					}
					else
					{
						PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, langMan.getLangData("InAppsPurchaseFailed"));
					}
				}
				purchaseButtons[i].SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
		else
		{
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, langMan.getLangData("NoInternetAccessError"));
		}
	}

	public void HandlePurchaseSucceededEvent(string productId)
	{
		Debug.Log("+++++ PurchaseSucceeded: " + productId);
		string text = string.Empty;
		int num = 1;
		Hashtable hashtable = MunerisController.Instance.BOGOFBonus();
		if (hashtable != null)
		{
			if (hashtable.ContainsKey("bonus"))
			{
				num = (int)hashtable["bonus"];
			}
			if (hashtable.ContainsKey("plusMessage"))
			{
				text = (string)hashtable["plusMessage"];
			}
		}
		int virtualGoodQuantity = MunerisController.Instance.GetVirtualGoodQuantity(productId);
		int num2 = virtualGoodQuantity * num;
		Debug.Log("Product: " + productId);
		user_obj.addCoin(num2);
		user_obj.addPurchasedCoins(num2);
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		string message = languageManagerInstance.getLangData("InAppsPurchaseSuccess1") + num2 + languageManagerInstance.getLangData("InAppsPurchaseSuccess2") + text;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Notification, message);
		Hashtable hashtable2 = new Hashtable();
		hashtable2.Add("package_id", productId);
		//MunerisController.Instance.ReportEvent("In App Purchase", hashtable2);
		enableAllButtons();
		closeLoading();
		UpgradeSceneController component = GameObject.Find("Controllers").GetComponent<UpgradeSceneController>();
		component.updateCoinLabel();
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

	public void closeLoading()
	{
		GameObject gameObject = GameObject.Find("Loading Panel");
		UIPanel component = gameObject.transform.Find("Loading").GetComponent<UIPanel>();
		component.Dismiss();
	}

	public void HandlePurchaseFailedEvent(string productId)
	{
		Debug.Log("+++++ PurchaseFailed: " + productId);
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance.getLangData("InAppsPurchaseFailed"));
		closeLoading();
		enableAllButtons();
	}

	public void HandlePurchaseCancelledEvent(string productId)
	{
		Debug.Log("+++++ PurchaseCancelled: " + productId);
		GameObject gameObject = GameObject.Find("Loading Panel");
		UIPanel component = gameObject.transform.Find("Loading").GetComponent<UIPanel>();
		component.Dismiss();
		enableAllButtons();
	}

	private void ShowVideoReward(IUIObject button)
	{
		if (Application.internetReachability != 0)
		{
			MunerisController.Instance.ReportVideoReward();
			return;
		}
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance.getLangData("NoInternetAccessError2"));
	}

	private void ShowTJOfferWall(IUIObject button)
	{
		if (Application.internetReachability != 0)
		{
			//MunerisController.Instance.ReportOfferWall();
			return;
		}
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, languageManagerInstance.getLangData("NoInternetAccessError2"));
	}

	private VirtualGood IdentifyVirtualGood(IAPPackage package, out bool isSpecial)
	{
		isSpecial = false;
		VirtualGood virtualGood = null;
		virtualGood = MunerisController.Instance.GetVirtualGood(package.replacablePackageName);
		CargoManager.PeriodData periodData = null;
		if (virtualGood != null && virtualGood.getCargo().Count > 0)
		{
			periodData = CargoManager.ParsePeriodData(virtualGood.getCargo());
		}
		else
		{
			Debug.LogFormat("********No Cargo for {0}", package.replacablePackageName);
		}
		if (periodData != null)
		{
			if (!periodData.isEnableAndValid || periodData.endDate.Subtract(DateTime.Now) < TimeSpan.FromMilliseconds(0.0))
			{
				virtualGood = MunerisController.Instance.GetVirtualGood(package.defaultPackageName);
			}
			else
			{
				isSpecial = true;
			}
		}
		else
		{
			Debug.LogFormat("********No Period for {0}", package.replacablePackageName);
		}
		return virtualGood;
	}
}
