using Outblaze;
using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class HeroUpgradeShortDescription : MonoBehaviour
{
	[SerializeField]
	private SpriteText _levelLabel;

	[SerializeField]
	private SpriteText _probLabel;

	private void Awake()
	{
		base.gameObject.SetActive(false);
		Init();
	}

	private void Init()
	{
		CargoManager.HeroUpgradeData heroUpgradeData = CargoManager.GetHeroUpgradeData();
		base.gameObject.SetActive(heroUpgradeData.isEnableAndValid);
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		_levelLabel.Text = heroUpgradeData.shortDesc[userProfileManagerInstance.getLangCode()];
		_probLabel.Text = string.Format("({0}%)", (heroUpgradeData.prob * 100f).ToString());
	}

	private void OnEnable()
	{
		CargoManager.HeroUpgradeData heroUpgradeData = CargoManager.GetHeroUpgradeData();
		base.gameObject.SetActive(heroUpgradeData.isEnableAndValid);
	}
}
