using UnityEngine;

public class VideoRewardLabel : CountDownHolder
{
	[SerializeField]
	private SpriteText _descLabel;

	private void OnEnable()
	{
		CargoManager.VideoRewardData videoRewardData = CargoManager.GetVideoRewardData();
		base.gameObject.SetActive(videoRewardData.periodData.isEnableAndValid);
		if (videoRewardData.periodData.isEnableAndValid)
		{
			_descLabel.Text = videoRewardData.desc;
			_countDownLabel.SetEndDate(videoRewardData.periodData.endDate);
		}
	}
}
