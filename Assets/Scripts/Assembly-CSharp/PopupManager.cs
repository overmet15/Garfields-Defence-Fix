using System.Runtime.CompilerServices;
using Outblaze;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
	public enum PopupType
	{
		None = -1,
		Confirmation = 0,
		Notification = 1,
		Warning = 2,
		UnlockedItem = 3,
		DailyAward = 4,
		GarfieldDinerAd = 5
	}

	public delegate void PopupHandler(PopupType type, bool choice);

	private static PopupManager instance;

	private ImageManager imageManager;

	private UserProfileManager user_obj;

	public UIPanel[] panels;

	private PopupType currentPopupType = PopupType.None;

	public static PopupManager Instance
	{
		get
		{
			return instance;
		}
	}

	[method: MethodImpl(32)]
	public event PopupHandler PopupCloseHandler;

	public bool isPopping()
	{
		return currentPopupType != PopupType.None;
	}

	private void Awake()
	{
		instance = this;
		imageManager = ImageManager.Instance;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		user_obj = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
	}

	private void OnLevelWasLoaded(int level)
	{
		HidePopup();
	}

	public void ShowPopup(PopupType type, string message, bool showCNY = false)
	{
		Debug.Log(">>> Ready to open popup: " + currentPopupType);
		Debug.Log(">>>> Current Ppopup : " + currentPopupType);
		if (currentPopupType >= PopupType.Confirmation)
		{
			return;
		}
		currentPopupType = type;
		panels[(int)type].BringIn();
		SpriteText component = panels[(int)type].transform.Find("Message").GetComponent<SpriteText>();
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetSmallFontMat(component);
		fontManagerInstance.SetFontColor(component, Color.black, component.color);
		component.Text = message;
		if (type == PopupType.Notification)
		{
			GameObject gameObject = GameObject.Find("CNYGarfield");
			GameObject gameObject2 = GameObject.Find("RedPocket");
			gameObject.SetActive(false);
			gameObject2.gameObject.SetActive(false);
			if (showCNY)
			{
				gameObject.SetActive(true);
				gameObject2.gameObject.SetActive(true);
			}
		}
	}

	public void ShowWallpaperPopup(PopupType type, int bonus)
	{
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		string text = languageManagerInstance.getLangData("TapjoyPointReward1") + bonus + languageManagerInstance.getLangData("TapjoyPointReward2");
		if (currentPopupType < PopupType.Confirmation)
		{
			currentPopupType = type;
			panels[(int)type].BringIn();
			SpriteText component = panels[(int)type].transform.Find("Message").GetComponent<SpriteText>();
			FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
			fontManagerInstance.SetSmallFontMat(component);
			fontManagerInstance.SetFontColor(component, Color.black, component.color);
			component.Text = text;
		}
	}

	public void ShowUnlockPopup(PopupType type, string unitName, string imgPath, string description, string title)
	{
		if (currentPopupType < PopupType.Confirmation)
		{
			currentPopupType = type;
			panels[(int)type].BringIn();
			SpriteText component = panels[(int)type].transform.Find("Message").GetComponent<SpriteText>();
			FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
			fontManagerInstance.SetSmallFontMat(component);
			component.Text = description;
			SpriteText component2 = panels[(int)type].transform.Find("ArmyName").GetComponent<SpriteText>();
			fontManagerInstance.SetSmallFontMat(component2);
			component2.Text = unitName;
			UIButton component3 = panels[(int)type].transform.Find("Icon").GetComponent<UIButton>();
			component3.SetTexture(imageManager.GetImage(imgPath));
			SpriteText component4 = panels[(int)type].transform.Find("Title").GetComponent<SpriteText>();
			fontManagerInstance.SetBigFontMat(component4);
			component4.Text = title;
		}
	}

	public void ShowDailyAwardPopup()
	{
		if (currentPopupType >= PopupType.Confirmation)
		{
			return;
		}
		currentPopupType = PopupType.DailyAward;
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		UIPanel uIPanel = panels[4];
		uIPanel.BringIn();
		SpriteText component = uIPanel.transform.Find("Panel/Title").GetComponent<SpriteText>();
		SpriteText component2 = uIPanel.transform.Find("Panel/Caption").GetComponent<SpriteText>();
		SpriteText component3 = uIPanel.transform.Find("Panel/Message").GetComponent<SpriteText>();
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(component);
		fontManagerInstance.SetSmallFontMat(component2);
		fontManagerInstance.SetSmallFontMat(component3);
		if (user_obj.getLangCode() == "zh-Hant")
		{
			component2.SetCharacterSize(28f);
		}
		component.Text = languageManagerInstance.getLangData("DailyGiftTitle");
		component2.Text = languageManagerInstance.getLangData("DailyGiftCaption");
		component3.Text = languageManagerInstance.getLangData("DailyGiftMessage");
		for (int i = 1; i <= 7; i += 2)
		{
			SpriteText component4 = uIPanel.transform.Find("Panel/Day " + i + "/Day").GetComponent<SpriteText>();
			SpriteText component5 = uIPanel.transform.Find("Panel/Day " + i + "/Gift_Cookies").GetComponent<SpriteText>();
			fontManagerInstance.SetSmallFontMat(component4);
			fontManagerInstance.SetSmallFontMat(component5);
			component4.Text = languageManagerInstance.getLangData("DailyGiftDayNum").Replace("#", i.ToString());
			component5.Text = languageManagerInstance.getLangData("DailyGift" + i + "Name").Replace("#", "5");
		}
		if (GiftManager.GiftDay >= 7)
		{
			return;
		}
		UIButton component6 = uIPanel.transform.Find("Panel/Day 7/Icon").GetComponent<UIButton>();
		component6.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		if (GiftManager.GiftDay >= 5)
		{
			return;
		}
		component6 = uIPanel.transform.Find("Panel/Day 5/Icon").GetComponent<UIButton>();
		component6.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		if (GiftManager.GiftDay < 3)
		{
			component6 = uIPanel.transform.Find("Panel/Day 3/Icon").GetComponent<UIButton>();
			component6.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			if (GiftManager.GiftDay > 1)
			{
				component6 = uIPanel.transform.Find("Panel/Day 1/Icon").GetComponent<UIButton>();
				component6.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
	}

	public void HidePopup()
	{
		Debug.Log(">>> Hide Popup: " + currentPopupType);
		if (currentPopupType > PopupType.None)
		{
			panels[(int)currentPopupType].Dismiss();
			currentPopupType = PopupType.None;
		}
	}

	private void Confirm()
	{
		HidePopup();
		if (this.PopupCloseHandler != null)
		{
			this.PopupCloseHandler(PopupType.Confirmation, true);
		}
	}

	private void Deny()
	{
		HidePopup();
		if (this.PopupCloseHandler != null)
		{
			this.PopupCloseHandler(PopupType.Confirmation, false);
		}
	}

	private void CloseNotification()
	{
		HidePopup();
		if (this.PopupCloseHandler != null)
		{
			this.PopupCloseHandler(currentPopupType, false);
		}
	}

	private void CloseWarning()
	{
		HidePopup();
		if (this.PopupCloseHandler != null)
		{
			this.PopupCloseHandler(PopupType.Warning, false);
		}
	}

	private void LoadGarfieldDinerAd()
	{
		HidePopup();
		if (Application.internetReachability != 0)
		{
			//MunerisController.Instance.ReportEvent("Download Garfield Diner Ad");
			if (user_obj.getAndroidMarket() == "google")
			{
				Debug.Log("url:market://details?id=com.webprancer.google.garfieldDefense2");
				Application.OpenURL("market://details?id=com.webprancer.google.garfieldDefense2");
			}
			else if (user_obj.getAndroidMarket() == "amazon")
			{
				Debug.Log("url:amzn://apps/android?p=com.webprancer.amazon.garfieldDefense2");
				Application.OpenURL("amzn://apps/android?p=com.webprancer.amazon.garfieldDefense2");
			}
		}
		else
		{
			LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
			ShowPopup(PopupType.Warning, languageManagerInstance.getLangData("NoInternetAccessError2"));
		}
	}
}
