using UnityEngine;

public class StarStrike_InstructionPanel
{
	private Rect sourceRect;

	private Rect displayRect;

	private Texture image;

	public StarStrike_InstructionPanel(Texture image, Rect rect)
	{
		this.image = image;
		sourceRect = rect;
		displayRect = new Rect(sourceRect.x, sourceRect.y, image.width, image.height);
	}

	public void setRefPoint(Vector2 refPoint)
	{
		displayRect.x = refPoint.x + sourceRect.x;
		displayRect.y = refPoint.y + sourceRect.y;
	}

	public Rect getDisplayRect()
	{
		return displayRect;
	}

	public Texture getImage()
	{
		return image;
	}
}
