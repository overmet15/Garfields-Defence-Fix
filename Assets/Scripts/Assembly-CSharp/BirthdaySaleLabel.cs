public class BirthdaySaleLabel : CountDownHolder
{
	private void OnEnable()
	{
		CargoManager.PeriodData birthdayIAPData = CargoManager.GetBirthdayIAPData();
		base.gameObject.SetActive(birthdayIAPData.isEnableAndValid);
		if (birthdayIAPData.isEnableAndValid)
		{
			_countDownLabel.SetEndDate(birthdayIAPData.endDate);
		}
	}
}
