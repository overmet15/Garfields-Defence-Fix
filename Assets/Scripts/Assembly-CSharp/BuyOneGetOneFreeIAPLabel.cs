using Outblaze;
using UnityEngine;

public class BuyOneGetOneFreeIAPLabel : MonoBehaviour
{
	[SerializeField]
	private SpriteText _textLabel;

	private void OnEnable()
	{
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		CargoManager.BOGOFData buyOneGetOneFreeData = CargoManager.GetBuyOneGetOneFreeData();
		base.gameObject.SetActive(buyOneGetOneFreeData.periodData.isEnableAndValid);
		if (buyOneGetOneFreeData.periodData.isEnableAndValid)
		{
			_textLabel.Text = buyOneGetOneFreeData.desc[userProfileManagerInstance.getLangCode()];
		}
	}
}
