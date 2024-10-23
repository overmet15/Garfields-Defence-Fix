using UnityEngine;

internal class StarStrike_TextHudElement : StarStrike_HudElement
{
	private Rect rect;

	private string text;

	private GUIStyle style;

	public StarStrike_TextHudElement(Rect rect, string text, GUIStyle style)
	{
		this.rect = rect;
		this.text = text;
		this.style = style;
	}

	public void Update()
	{
	}

	public void OnGUI()
	{
		GUI.Label(rect, text, style);
	}
}
