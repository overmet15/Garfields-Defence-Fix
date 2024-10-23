using System.Collections;
using UnityEngine;

public class StarStrike_SmoothBar
{
	private Texture texture;

	private Rect rect;

	private Rect valueRect;

	private int currentWidth;

	private int maxWidth;

	private float currentValue;

	public float maxValue;

	private StarStrike_CountdownTimer updateBarTimer;

	private Stack updateValueStack;

	private bool updating;

	private float currentUpdateValue;

	private float polledValue;

	private Texture backgroundTexture;

	private SmoothBarDirection increasingDirection;

	public StarStrike_SmoothBar(Texture barTexture, Texture backgroundTexture, Rect rect, float maxValue, float updateBarTime, SmoothBarDirection direction)
	{
		texture = barTexture;
		this.rect = rect;
		currentValue = 0f;
		this.maxValue = maxValue;
		updateBarTimer = new StarStrike_CountdownTimer(updateBarTime);
		updateValueStack = new Stack();
		valueRect = new Rect(rect);
		updating = false;
		increasingDirection = direction;
		this.backgroundTexture = backgroundTexture;
	}

	public void SetValue(int value)
	{
		currentValue = value;
		UpdateValueRect(currentValue);
	}

	public void UpdateValue(float increment)
	{
		updateValueStack.Push(increment);
	}

	public void Update()
	{
		if (updating)
		{
			UpdatePolledValue();
		}
		else if (updateValueStack.Count > 0)
		{
			updateBarTimer.Reset();
			polledValue = 0f;
			currentUpdateValue = (float)updateValueStack.Peek();
			updateValueStack.Pop();
			updating = true;
		}
	}

	private void UpdatePolledValue()
	{
		if (updateBarTimer.HasElapsed())
		{
			polledValue = 0f;
			currentValue += currentUpdateValue;
			UpdateValueRect(currentValue);
			updating = false;
		}
		else
		{
			updateBarTimer.Update();
			float num = StarStrike_InterpolationUtils.SmoothStep(updateBarTimer.GetRatio());
			polledValue = num * currentUpdateValue;
			float value = currentValue + polledValue;
			UpdateValueRect(value);
		}
	}

	private void UpdateValueRect(float value)
	{
		float num = StarStrike_Utils.Clamp(value, 0f, maxValue);
		float num2 = num / maxValue;
		valueRect.width = (int)(num2 * rect.width);
	}

	public void OnGUI()
	{
		GUI.DrawTexture(rect, backgroundTexture);
		switch (increasingDirection)
		{
		case SmoothBarDirection.RIGHT:
			GUI.DrawTexture(valueRect, texture);
			break;
		case SmoothBarDirection.LEFT:
			valueRect.x = rect.x + rect.width - valueRect.width;
			GUI.DrawTexture(valueRect, texture);
			break;
		}
	}

	public bool IsUpdating()
	{
		return updating;
	}

	public float GetCurrentValue()
	{
		return currentValue;
	}

	public float GetMaxValue()
	{
		return maxValue;
	}

	public void ClearUpdateValues()
	{
		updateValueStack.Clear();
	}
}
