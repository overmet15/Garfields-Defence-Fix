using UnityEngine;

public class UIButton2 : UIViewController
{
	public float leftBorder;

	public float rightBorder;

	public float topBorder;

	public float bottomBorder;

	public bool hightlightEffect = true;

	public bool disableEffect = true;

	private bool _enable = true;

	private int _curFingerId = -1;

	private Color keyUpColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	private Color keyDownColor = new Color(0.3f, 0.3f, 0.3f, 1f);

	private Color disableColor = new Color(0.25f, 0.25f, 0.25f, 1f);

	private void Start()
	{
	}

	public override void Update()
	{
		if (_enable)
		{
			base.Update();
		}
	}

	public override void OnTouchBegan(UITouch touch)
	{
		if (_enable && UIKit.CheckPointInsideRect(touch.position, getRect()))
		{
			_curFingerId = touch.fingerId;
			keyDown();
		}
	}

	public override void OnTouchMoved(UITouch touch)
	{
		if (_enable && touch.fingerId == _curFingerId)
		{
			if (UIKit.CheckPointInsideRect(touch.position, getRect()))
			{
				keyDown();
			}
			else
			{
				keyUp();
			}
		}
	}

	public override void OnTouchEnded(UITouch touch)
	{
		if (_enable && touch.fingerId == _curFingerId)
		{
			keyUp();
			_curFingerId = -1;
		}
	}

	private void keyUp()
	{
		base.gameObject.GetComponent<GUITexture>().color = keyUpColor;
	}

	private void keyDown()
	{
		if (hightlightEffect)
		{
			base.gameObject.GetComponent<GUITexture>().color = keyDownColor;
		}
	}

	private void disable()
	{
		if (disableEffect)
		{
			base.gameObject.GetComponent<GUITexture>().color = disableColor;
		}
		_curFingerId = -1;
	}

	public Rect getRect()
	{
		return UIKit.GetScreenRectFromObject(base.gameObject, leftBorder, rightBorder, topBorder, bottomBorder);
	}

	public void setEnable(bool enable)
	{
		_enable = enable;
		if (_enable)
		{
			keyUp();
		}
		else
		{
			disable();
		}
	}

	public bool isEnable()
	{
		return _enable;
	}

	public static UIButton2 getButton(GameObject obj)
	{
		return (UIButton2)obj.GetComponent("UIButton");
	}

	public static void enableButton(GameObject obj)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			button.setEnable(true);
		}
	}

	public static void disableButton(GameObject obj)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			button.setEnable(false);
		}
	}

	public static bool isEnableButton(GameObject obj)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			return button.isEnable();
		}
		return false;
	}

	public static void setButtonBorder(GameObject obj, float lBorder, float rBorder, float tBorder, float bBorder)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			button.leftBorder = lBorder;
			button.rightBorder = rBorder;
			button.topBorder = tBorder;
			button.bottomBorder = bBorder;
		}
	}

	public static void setButtonHighlightEffect(GameObject obj, bool enable)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			button.hightlightEffect = enable;
		}
	}

	public static void setButtonDisableEffect(GameObject obj, bool enable)
	{
		UIButton2 button = getButton(obj);
		if (button != null)
		{
			button.disableEffect = enable;
		}
	}
}
