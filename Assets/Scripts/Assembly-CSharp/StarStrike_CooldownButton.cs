using System.Collections;
using UnityEngine;

internal class StarStrike_CooldownButton : StarStrike_HudElement
{
	private GUIStyle buttonStyle;

	private GUIStyle disabledButtonStyle;

	private Rect rect;

	private Rect cooldownCoverRect;

	private StarStrike_CountdownTimer cooldownTimer;

	private StarStrike_ConditionalAction action;

	private Texture cooldownOverlay;

	private IList childElementList;

	public StarStrike_CooldownButton(GUIStyle buttonStyle, GUIStyle disabledButtonStyle, Texture cooldownOverlay, Rect rect, float cooldownTime, StarStrike_ConditionalAction action)
	{
		this.buttonStyle = buttonStyle;
		this.disabledButtonStyle = disabledButtonStyle;
		this.rect = rect;
		cooldownCoverRect = new Rect(this.rect);
		cooldownTimer = new StarStrike_CountdownTimer(cooldownTime);
		this.cooldownOverlay = cooldownOverlay;
		childElementList = new ArrayList();
		this.action = action;
		StarStrike_Assertion.Assert(this.action != null, "Action can't be null.");
	}

	public void AddChildElement(StarStrike_HudElement element)
	{
		childElementList.Add(element);
	}

	public void Update()
	{
		if (cooldownTimer.HasElapsed())
		{
			return;
		}
		cooldownTimer.Update();
		foreach (StarStrike_HudElement childElement in childElementList)
		{
			childElement.Update();
		}
	}

	public void OnGUI()
	{
		RenderButton();
		foreach (StarStrike_HudElement childElement in childElementList)
		{
			childElement.OnGUI();
		}
	}

	private void RenderButton()
	{
		if (IsClickable())
		{
			if (GUI.Button(rect, string.Empty, buttonStyle))
			{
				action.Execute();
				cooldownTimer.Reset();
				StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.BUTTON_CLICKED);
				StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
				return;
			}
		}
		else
		{
			GUI.Box(rect, string.Empty, disabledButtonStyle);
		}
		if (!cooldownTimer.HasElapsed())
		{
			float num = StarStrike_InterpolationUtils.SmoothStep(cooldownTimer.GetRatio());
			cooldownCoverRect.height = rect.height * (1f - num);
			GUI.DrawTexture(cooldownCoverRect, cooldownOverlay);
		}
	}

	public bool IsClickable()
	{
		return cooldownTimer.HasElapsed() && action.CanBeExecuted();
	}

	public void ResetCooldown()
	{
		cooldownTimer.Reset();
	}

	public Rect GetRect()
	{
		return rect;
	}
}
