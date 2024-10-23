using Outblaze;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
	public enum ActionType
	{
		Spawn = 0,
		Spell = 1
	}

	private static bool showedTutorial;

	private StarStrike_CountdownTimer cooldownTimer;

	private UIButton button;

	private UIButton icon;

	private UIButton top;

	private StarStrike_ConditionalAction action;

	private UIButton overlay;

	private bool disabled;

	public bool disableOnInvoke;

	public int summonChildIndex;

	public GameObject summonChildPrefab;

	public ActionType type;

	private TutorialManager tutorialManager;

	public float CooldownTime
	{
		set
		{
			cooldownTimer = new StarStrike_CountdownTimer(value);
		}
	}

	public UIButton Button
	{
		set
		{
			button = value;
			button.scriptWithMethodToInvoke = this;
			button.methodToInvoke = "Invoke";
			icon = button.transform.Find("Icon").GetComponent<UIButton>();
			switch (type)
			{
			case ActionType.Spawn:
				icon.scriptWithMethodToInvoke = this;
				icon.methodToInvoke = "Invoke";
				break;
			case ActionType.Spell:
				top = button.transform.Find("Top").GetComponent<UIButton>();
				top.scriptWithMethodToInvoke = this;
				top.methodToInvoke = "Invoke";
				break;
			}
			Transform transform = base.transform.Find("Overlay");
			if (transform != null)
			{
				overlay = transform.GetComponent<UIButton>();
			}
		}
	}

	public StarStrike_ConditionalAction Action
	{
		set
		{
			action = value;
		}
	}

	private bool Disabled
	{
		get
		{
			return disabled;
		}
		set
		{
			if (value == disabled)
			{
				return;
			}
			disabled = value;
			if (disabled)
			{
				if (button.controlState != UIButton.CONTROL_STATE.DISABLED)
				{
					button.SetControlState(UIButton.CONTROL_STATE.DISABLED);
					button.transform.Find("Normal").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.DISABLED);
					button.transform.Find("Golden").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.DISABLED);
					button.Text = string.Empty;
				}
				if (icon.controlState != UIButton.CONTROL_STATE.DISABLED)
				{
					icon.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				}
				if (top != null && top.controlState != UIButton.CONTROL_STATE.DISABLED)
				{
					top.SetControlState(UIButton.CONTROL_STATE.DISABLED);
					button.transform.Find("Normal").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.DISABLED);
					button.transform.Find("Golden").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.DISABLED);
				}
				if (overlay != null)
				{
					overlay.Hide(false);
				}
			}
			else
			{
				if (button.controlState == UIButton.CONTROL_STATE.DISABLED)
				{
					button.SetControlState(UIButton.CONTROL_STATE.NORMAL);
					button.transform.Find("Normal").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.NORMAL);
					button.transform.Find("Golden").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.NORMAL);
					button.Text = string.Empty;
				}
				if (icon.controlState == UIButton.CONTROL_STATE.DISABLED)
				{
					icon.SetControlState(UIButton.CONTROL_STATE.NORMAL);
				}
				if (top != null && top.controlState == UIButton.CONTROL_STATE.DISABLED)
				{
					top.SetControlState(UIButton.CONTROL_STATE.NORMAL);
					button.transform.Find("Normal").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.NORMAL);
					button.transform.Find("Golden").GetComponent<UIButton>().SetControlState(UIButton.CONTROL_STATE.NORMAL);
				}
				if (overlay != null)
				{
					overlay.Hide(true);
				}
			}
		}
	}

	private void Awake()
	{
		tutorialManager = GameObject.Find("Tutorial Panels").GetComponent<TutorialManager>();
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (userProfileManagerInstance.getGameLevel() == 2)
		{
			showedTutorial = false;
		}
		else
		{
			showedTutorial = true;
		}
	}

	private void Update()
	{
		if (!cooldownTimer.HasElapsed())
		{
			Disabled = true;
			cooldownTimer.Update();
			float num = StarStrike_InterpolationUtils.SmoothStep(cooldownTimer.GetRatio());
			if (overlay != null)
			{
				overlay.gameObject.transform.localScale = new Vector3(1f, 1f - num, 1f);
			}
		}
		else
		{
			if (action == null)
			{
				return;
			}
			if (action.CanBeExecuted())
			{
				Disabled = false;
				if (type == ActionType.Spawn && !showedTutorial)
				{
					showedTutorial = true;
					if (tutorialManager != null)
					{
						tutorialManager.Hide();
						tutorialManager.Insert(6);
						tutorialManager.ShowNext();
					}
				}
			}
			else
			{
				Disabled = true;
			}
		}
	}

	private void Invoke()
	{
		Debug.Log(">>>>>> Invoke:");
		if (action != null)
		{
			Debug.Log(">>>>>> Action not null");
			action.Execute();
			if (summonChildPrefab != null)
			{
				StarStrike_UnitCreatorAction.Summon(summonChildPrefab, new Vector3(-75f - Random.Range(0f, 5f), 0f, 0f), summonChildIndex);
				StarStrike_UnitCreatorAction.Summon(summonChildPrefab, new Vector3(-75f - Random.Range(0f, 5f), 0f, 0f), summonChildIndex);
			}
			if (disableOnInvoke)
			{
				((StarStrike_UnitCreatorAction)action).disabled = true;
			}
			cooldownTimer.Reset();
			if (tutorialManager != null && tutorialManager.IsShowing)
			{
				if (tutorialManager.CurrentPanelIndex == 3 || tutorialManager.CurrentPanelIndex == 6)
				{
					tutorialManager.ShowNext();
				}
				else if (type == ActionType.Spawn)
				{
					tutorialManager.Remove(3);
				}
			}
		}
		else
		{
			Debug.Log(">>>>>> Action is null");
		}
	}
}
