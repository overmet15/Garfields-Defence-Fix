using UnityEngine;

public static class UIKit
{
	public static Rect GetScreenRectFromObject(GameObject obj)
	{
		return GetScreenRectFromObject(obj, 0f, 0f, 0f, 0f);
	}

	public static Rect GetScreenRectFromObject(GameObject obj, float leftBorder, float rightBorder, float topBorder, float bottomBorder)
	{
		float num = obj.transform.position.x - obj.transform.lossyScale.x * 0.5f + leftBorder;
		float num2 = obj.transform.position.y - obj.transform.lossyScale.y * 0.5f + bottomBorder;
		float num3 = obj.transform.lossyScale.x - leftBorder - rightBorder;
		float num4 = obj.transform.lossyScale.y - topBorder - bottomBorder;
		return new Rect(num * (float)Screen.width, num2 * (float)Screen.height, num3 * (float)Screen.width, num4 * (float)Screen.height);
	}

	public static bool CheckPointInsideRect(Vector2 point, Rect rect)
	{
		return CheckPointInsideRect(point, rect, 0f, 0f);
	}

	public static bool CheckPointInsideRect(Vector2 point, Rect rect, float toleranceX, float toleranceY)
	{
		if (point.x >= rect.x - toleranceX && point.x < rect.x + rect.width + toleranceX && point.y >= rect.y - toleranceY && point.y < rect.y + rect.height + toleranceY)
		{
			return true;
		}
		return false;
	}

	public static bool CheckPointInsideButton(Vector2 point, GameObject obj)
	{
		return CheckPointInsideButton(point, obj, 0f, 0f);
	}

	public static bool CheckPointInsideButton(Vector2 point, GameObject obj, float toleranceX, float toleranceY)
	{
		UIButton2 button = UIButton2.getButton(obj);
		if (button != null)
		{
			return CheckPointInsideRect(point, button.getRect(), toleranceX, toleranceY);
		}
		return CheckPointInsideRect(point, GetScreenRectFromObject(obj), toleranceX, toleranceY);
	}

	public static GameObject CreateGUITextureObject(string path)
	{
		GameObject gameObject = new GameObject();
		GUITexture gUITexture = gameObject.AddComponent<GUITexture>();
		gUITexture.texture = (Texture2D)Resources.Load(path);
		return gameObject;
	}

	public static GameObject CreateUIButton(string path)
	{
		GameObject gameObject = CreateGUITextureObject(path);
		gameObject.AddComponent<UIButton>();
		return gameObject;
	}
}
