using UnityEngine;

internal class StarStrike_TextureHudElement : StarStrike_HudElement
{
	private Texture texture;

	private Rect rect;

	public StarStrike_TextureHudElement(Texture texture, Rect rect)
	{
		this.texture = texture;
		this.rect = rect;
	}

	public void Update()
	{
	}

	public void OnGUI()
	{
		GUI.DrawTexture(rect, texture);
	}
}
