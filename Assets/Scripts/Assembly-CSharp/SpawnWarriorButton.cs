using UnityEngine;

public class SpawnWarriorButton : MonoBehaviour
{
	private StarStrike_CountdownTimer cooldownTimer;

	private UIButton button;

	private StarStrike_ConditionalAction action;

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
		}
	}

	public StarStrike_ConditionalAction Action
	{
		set
		{
			action = value;
		}
	}

	private void Update()
	{
		if (!cooldownTimer.HasElapsed())
		{
			cooldownTimer.Update();
			button.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			float num = StarStrike_InterpolationUtils.SmoothStep(cooldownTimer.GetRatio());
			button.Text = (int)(num * 100f) + "%";
		}
		else if (action != null && action.CanBeExecuted() && button.controlState == UIButton.CONTROL_STATE.DISABLED)
		{
			button.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			button.Text = string.Empty;
		}
	}

	private void Invoke()
	{
		if (action != null)
		{
			action.Execute();
			cooldownTimer.Reset();
		}
	}
}
