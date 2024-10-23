using System;
using System.Text;
using Outblaze;
using UnityEngine;

[RequireComponent(typeof(SpriteText))]
public class CountDown : MonoBehaviour
{
	private SpriteText _label;

	private DateTime _endDate;

	private string _labelFormat;

	private UserProfileManager _userProfile;

	public void SetEndDate(DateTime dt)
	{
		_endDate = dt;
	}

	private void Awake()
	{
		_label = GetComponent<SpriteText>();
	}

	private void Start()
	{
		_userProfile = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
	}

	private void Update()
	{
		TimeSpan duration = _endDate.Subtract(DateTime.Now);
		string langCode = _userProfile.getLangCode();
		_label.Text = DurationFormat(duration, CargoManager.GetTimeLocalization().format[langCode], langCode);
	}

	private string GetDayString(string locale)
	{
		return CargoManager.GetTimeLocalization().days[locale];
	}

	private string GetHourString(string locale)
	{
		return CargoManager.GetTimeLocalization().hours[locale];
	}

	private string GetMinuteString(string locale)
	{
		return CargoManager.GetTimeLocalization().minutes[locale];
	}

	private string DurationFormat(TimeSpan duration, string formatProvider, string locale)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Length = 0;
		if (duration.Days > 0)
		{
			stringBuilder.AppendFormat(formatProvider, duration.Days, GetDayString(locale));
		}
		else if (duration.Hours > 0)
		{
			stringBuilder.AppendFormat(formatProvider, duration.Hours, GetHourString(locale));
		}
		else if (duration.Minutes > 0)
		{
			stringBuilder.AppendFormat(formatProvider, duration.Minutes, GetMinuteString(locale));
		}
		return stringBuilder.ToString();
	}
}
