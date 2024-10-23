using Outblaze;
using UnityEngine;

public class HeroUpgradeLongDescription : MonoBehaviour
{
	[SerializeField]
	private SpriteText _spriteText;

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
		_spriteText.Text = heroUpgradeData.longDesc[userProfileManagerInstance.getLangCode()];
	}

	private void OnEnable()
	{
		CargoManager.HeroUpgradeData heroUpgradeData = CargoManager.GetHeroUpgradeData();
		base.gameObject.SetActive(heroUpgradeData.isEnableAndValid);
	}
}
