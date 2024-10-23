using System;
using System.Collections.Generic;
using Muneris;
using Muneris.MiniJSON;
using UnityEngine;

public static class CargoManager
{
	public class HeroUpgradeData
	{
		public float prob;

		public int extraLevel;

		public int maxLevel;

		private bool enabled;

		public DateTime startDate;

		public DateTime endDate;

		public Dictionary<string, string> longDesc = new Dictionary<string, string>();

		public Dictionary<string, string> shortDesc = new Dictionary<string, string>();

		public bool isEnableAndValid
		{
			get
			{
				return enabled && DateTime.Compare(DateTime.Now, endDate) <= 0 && DateTime.Compare(startDate, DateTime.Now) <= 0;
			}
		}

		public HeroUpgradeData(Dictionary<string, object> jsonData)
		{
			prob = (float)(double)jsonData["prob"];
			extraLevel = (int)(long)jsonData["extraLevel"];
			maxLevel = (int)(long)jsonData["maxLevel"];
			enabled = (bool)jsonData["enabled"];
			startDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((long)jsonData["startDate"]);
			endDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((long)jsonData["endDate"]);
			foreach (KeyValuePair<string, object> item in (Dictionary<string, object>)jsonData["longDesc"])
			{
				longDesc.Add(item.Key, item.Value as string);
			}
			foreach (KeyValuePair<string, object> item2 in (Dictionary<string, object>)jsonData["shortDesc"])
			{
				shortDesc.Add(item2.Key, item2.Value as string);
			}
		}
	}

	public class BOGOFData
	{
		public Dictionary<string, string> desc = new Dictionary<string, string>();

		public PeriodData periodData;

		public BOGOFData(Dictionary<string, object> jsonData)
		{
			foreach (KeyValuePair<string, object> item in (Dictionary<string, object>)jsonData["desc"])
			{
				desc.Add(item.Key, item.Value as string);
			}
			periodData = new PeriodData((Dictionary<string, object>)jsonData["periodData"]);
		}
	}

	public class VideoRewardData
	{
		public string desc;

		public PeriodData periodData;

		public VideoRewardData(Dictionary<string, object> jsonData)
		{
			desc = jsonData["desc"] as string;
			periodData = new PeriodData((Dictionary<string, object>)jsonData["periodData"]);
		}
	}

	public class PeriodData
	{
		private bool enabled;

		public DateTime startDate;

		public DateTime endDate;

		public bool isEnableAndValid
		{
			get
			{
				return enabled && DateTime.Compare(DateTime.Now, endDate) <= 0 && DateTime.Compare(startDate, DateTime.Now) <= 0;
			}
		}

		public PeriodData(Dictionary<string, object> jsonData)
		{
			enabled = (bool)jsonData["enabled"];
			startDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((long)jsonData["startDate"]);
			endDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((long)jsonData["endDate"]);
		}
	}

	public class TimeLocalization
	{
		public Dictionary<string, string> format = new Dictionary<string, string>();

		public Dictionary<string, string> days = new Dictionary<string, string>();

		public Dictionary<string, string> hours = new Dictionary<string, string>();

		public Dictionary<string, string> minutes = new Dictionary<string, string>();

		public TimeLocalization(Dictionary<string, object> jsonData)
		{
			foreach (KeyValuePair<string, object> item in (Dictionary<string, object>)jsonData["format"])
			{
				format.Add(item.Key, item.Value as string);
			}
			foreach (KeyValuePair<string, object> item2 in (Dictionary<string, object>)jsonData["days"])
			{
				days.Add(item2.Key, item2.Value as string);
			}
			foreach (KeyValuePair<string, object> item3 in (Dictionary<string, object>)jsonData["hours"])
			{
				hours.Add(item3.Key, item3.Value as string);
			}
			foreach (KeyValuePair<string, object> item4 in (Dictionary<string, object>)jsonData["minutes"])
			{
				minutes.Add(item4.Key, item4.Value as string);
			}
		}
	}

	private static string _defaultCargo = "{\"msgTargets\":{\"BuyOneGetOneFree_IAPPromo\":{\"enabled\":false,\"type\":\"iap\",\"iapMappings\":{\"package1\":{\"productId\":\"cookies\",\"credits\":1000},\"package2\":{\"productId\":\"cookies\",\"credits\":2500},\"package3\":{\"productId\":\"cookies\",\"credits\":10000},\"package4\":{\"productId\":\"cookies\",\"credits\":25000},\"package5\":{\"productId\":\"cookies\",\"credits\":50000}},\"text\":{\"TH\":\"ขอบค\u0e38ณสำหร\u0e31บการส\u0e31\u0e48งซ\u0e37\u0e49อ! ค\u0e38ณเพ\u0e34\u0e48งได\u0e49ร\u0e31บคะแนนเพ\u0e34\u0e48มอ\u0e35ก 100%!\",\"MY\":\"Terima kasih atas pembelian anda! Anda baru sahaja mendapat 100% lebih Mata!\",\"PH\":\"Salamat sa iyong pagbili! Nakatanggap ka ng kadagdagang 100% Cookies!\",\"ES\":\"¡Gracias por tu compra! ¡Acabas de recibir un 100% más de puntos!\",\"PT\":\"Agradecemos a sua compra! Acabou de receber mais 100% de Pontos!\",\"IT\":\"Grazie per il tuo acquisto! Hai appena ricevuto il 100% in più dei Punti!\",\"en\":\"Thank you for your purchase! You have just received 100% more Cookies!\",\"ko\":\"구매에 감사합니다! 추가로 100%보너스를 받으셨습니다!\",\"ja\":\"ご購入ありがとうございます！100%のボーナスをもらいました！\",\"zh-Hant\":\"謝謝你! 你剛獲得了200%曲奇! \",\"zh-Hans\":\"谢谢你! 你刚获得了200%曲奇!\",\"fr\":\"Merci pour ton achat! Tu viens de recevoir 100% de Cookies en plus !\",\"de\":\"Danke für deinen Einkauf! Du hast gerade 100% mehr Punkte erhalten!\"}}},\"heroCriticalUpgrade\":{\"prob\":0.3,\"extraLevel\":1,\"maxLevel\":10,\"enabled\":true,\"startDate\":1297744000,\"endDate\":1497784000,\"longDesc\":{\"en\":\"30% chance of DOUBLE UPGRADE during 18-24/6\",\"ko\":\"\",\"ja\":\"\",\"fr\":\"\",\"de\":\"\",\"zh-Hans\":\"\",\"zh-Hant\":\"\"},\"shortDesc\":{\"en\":\"+2Lv\",\"ko\":\"+2Lv\",\"ja\":\"+2Lv\",\"fr\":\"+2Lv\",\"de\":\"+2Lv\",\"zh-Hans\":\"+2Lv\",\"zh-Hant\":\"+2Lv\"}},\"birthdayIAP\":{\"enabled\":true,\"startDate\":1397744000,\"endDate\":1497794000},\"buyOneGetOneFree\":{\"periodData\":{\"enabled\":false,\"startDate\":1494404128,\"endDate\":1497744000},\"desc\":{\"en\":\"Buy 1 Get 1 Free\",\"ko\":\"Buy 1 Get 1 Free\",\"ja\":\"Buy 1 Get 1 Free\",\"fr\":\"Buy 1 Get 1 Free\",\"de\":\"Buy 1 Get 1 Free\",\"zh-Hans\":\"Buy 1 Get 1 Free\",\"zh-Hant\":\"Buy 1 Get 1 Free\"}},\"videoReward\":{\"desc\":\"100x\",\"periodData\":{\"enabled\":true,\"startDate\":1497744000,\"endDate\":1497794000}},\"timeLocalization\":{\"format\":{\"en\":\"{0} {1} left\",\"ko\":\"\",\"ja\":\"\",\"fr\":\"\",\"de\":\"\",\"zh-Hans\":\"\",\"zh-Hant\":\"\"},\"days\":{\"en\":\"day\",\"ko\":\"\",\"ja\":\"\",\"fr\":\"\",\"de\":\"\",\"zh-Hans\":\"\",\"zh-Hant\":\"\"},\"hours\":{\"en\":\"hour\",\"ko\":\"\",\"ja\":\"\",\"fr\":\"\",\"de\":\"\",\"zh-Hans\":\"\",\"zh-Hant\":\"\"},\"minutes\":{\"en\":\"minute\",\"ko\":\"\",\"ja\":\"\",\"fr\":\"\",\"de\":\"\",\"zh-Hans\":\"\",\"zh-Hant\":\"\"}}}";

	private static HeroUpgradeData _heroUpgradeData;

	private static PeriodData _birthdayIAPData;

	private static BOGOFData _buyOneGetOneFreeData;

	private static VideoRewardData _videoRewardData;

	private static TimeLocalization _timeLocalization;

	public static void OnCargoChange(JsonObject cargoObject)
	{
		Debug.Log("On Cargo Change");
		Parse(cargoObject);
	}

	public static HeroUpgradeData GetHeroUpgradeData()
	{
		CheckData();
		return _heroUpgradeData;
	}

	public static PeriodData GetBirthdayIAPData()
	{
		CheckData();
		return _birthdayIAPData;
	}

	public static VideoRewardData GetVideoRewardData()
	{
		CheckData();
		return _videoRewardData;
	}

	public static TimeLocalization GetTimeLocalization()
	{
		CheckData();
		return _timeLocalization;
	}

	public static PeriodData ParsePeriodData(JsonObject jsonObject)
	{
		if (jsonObject.ContainsKey("enabled"))
		{
			return new PeriodData(jsonObject);
		}
		return null;
	}

	public static BOGOFData GetBuyOneGetOneFreeData()
	{
		CheckData();
		return _buyOneGetOneFreeData;
	}

	private static void Parse(string jsonString)
	{
		Dictionary<string, object> jsonData = (Dictionary<string, object>)Json.Deserialize(jsonString);
		Parse(jsonData);
	}

	private static void Parse(Dictionary<string, object> jsonData)
	{
		_heroUpgradeData = new HeroUpgradeData((Dictionary<string, object>)jsonData["heroCriticalUpgrade"]);
		_birthdayIAPData = new PeriodData((Dictionary<string, object>)jsonData["birthdayIAP"]);
		_buyOneGetOneFreeData = new BOGOFData((Dictionary<string, object>)jsonData["buyOneGetOneFree"]);
		_videoRewardData = new VideoRewardData((Dictionary<string, object>)jsonData["videoReward"]);
		_timeLocalization = new TimeLocalization((Dictionary<string, object>)jsonData["timeLocalization"]);
	}

	private static void CheckData()
	{
		if (_heroUpgradeData == null || _birthdayIAPData == null || _buyOneGetOneFreeData == null || _videoRewardData == null || _timeLocalization == null)
		{
			Parse(((Dictionary<string, object>)Json.Deserialize(_defaultCargo)));
		}
	}
}
