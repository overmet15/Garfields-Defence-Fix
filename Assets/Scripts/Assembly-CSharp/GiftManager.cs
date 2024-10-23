using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
	private const string GiftKey = "gift";

	private const string DayKey = "day";

	private const string CookieKey = "cookie";

	private const string ItemTypeKey = "itemType";

	private const string PotionKey = "potion";

	private const string FirstVisitYearKey = "FirstVisitYear";

	private const string FirstVisitMonthKey = "FirstVisitMonth";

	private const string FirstVisitDayKey = "FirstVisitDay";

	private const string LastGiftDayKey = "LastGiftDay";

	public TextAsset configFile;

	private static Dictionary<int, Dictionary<string, int>> gifts;

	public static int GiftDay
	{
		get
		{
			DateTime today = DateTime.Today;
			if (!PlayerPrefs.HasKey("FirstVisitYear") || !PlayerPrefs.HasKey("FirstVisitMonth") || !PlayerPrefs.HasKey("FirstVisitDay"))
			{
				PlayerPrefs.SetInt("FirstVisitYear", today.Year);
				PlayerPrefs.SetInt("FirstVisitMonth", today.Month);
				PlayerPrefs.SetInt("FirstVisitDay", today.Day);
			}
			DateTime dateTime = new DateTime(PlayerPrefs.GetInt("FirstVisitYear"), PlayerPrefs.GetInt("FirstVisitMonth"), PlayerPrefs.GetInt("FirstVisitDay"));
			return (today - dateTime).Days + 1;
		}
	}

	public static bool CanGetGift
	{
		get
		{
			int @int = PlayerPrefs.GetInt("LastGiftDay", 0);
			int giftDay = GiftDay;
			return giftDay > @int && gifts.ContainsKey(giftDay);
		}
	}

	public static int TodayCookieGift
	{
		get
		{
			int giftDay = GiftDay;
			if (gifts.ContainsKey(giftDay))
			{
				return gifts[giftDay]["cookie"];
			}
			return 0;
		}
	}

	public static int TodayItemTypeGift
	{
		get
		{
			int giftDay = GiftDay;
			if (gifts.ContainsKey(giftDay))
			{
				return gifts[giftDay]["itemType"];
			}
			return 0;
		}
	}

	public static int TodayPotionGift
	{
		get
		{
			int giftDay = GiftDay;
			if (gifts.ContainsKey(giftDay))
			{
				return gifts[giftDay]["potion"];
			}
			return 0;
		}
	}

	private void Awake()
	{
		base.gameObject.name = GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(this);
		gifts = new Dictionary<int, Dictionary<string, int>>();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(configFile.text);
		XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("gift");
		foreach (XmlNode item in elementsByTagName)
		{
			int key = 0;
			XmlNodeList childNodes = item.ChildNodes;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (XmlNode item2 in childNodes)
			{
				switch (item2.Name)
				{
				case "day":
					key = int.Parse(item2.InnerText);
					break;
				case "cookie":
					dictionary.Add("cookie", int.Parse(item2.InnerText));
					break;
				case "itemType":
					dictionary.Add("itemType", int.Parse(item2.InnerText));
					break;
				case "potion":
					dictionary.Add("potion", int.Parse(item2.InnerText));
					break;
				}
			}
			gifts.Add(key, dictionary);
		}
	}

	public static void Reset()
	{
		PlayerPrefs.DeleteKey("FirstVisitYear");
		PlayerPrefs.DeleteKey("FirstVisitMonth");
		PlayerPrefs.DeleteKey("FirstVisitDay");
		PlayerPrefs.DeleteKey("LastGiftDay");
	}

	public static void GetGift()
	{
		if (CanGetGift)
		{
			PlayerPrefs.SetInt("LastGiftDay", GiftDay);
		}
	}

	public static int GetGiftDay()
	{
		return GiftDay;
	}
}
