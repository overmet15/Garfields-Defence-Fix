using UnityEngine;

public class UIViewController : MonoBehaviour
{
	private DeviceOrientation currentOrientation;

	private Vector3 _lastMousePosition;

	private bool _bCanInteract = true;

	public virtual void Awake()
	{
	}

	public virtual void Update()
	{
		detectTouch();
	}

	public static DeviceOrientation iphoneScreenOrientationToDeviceOrientation(ScreenOrientation io)
	{
		switch (io)
		{
		case ScreenOrientation.Portrait:
			return DeviceOrientation.Portrait;
		case ScreenOrientation.PortraitUpsideDown:
			return DeviceOrientation.PortraitUpsideDown;
		case ScreenOrientation.LandscapeLeft:
			return DeviceOrientation.LandscapeLeft;
		case ScreenOrientation.LandscapeRight:
			return DeviceOrientation.LandscapeRight;
		default:
			return DeviceOrientation.Portrait;
		}
	}

	public static bool isPortraitUpSideDown(DeviceOrientation io)
	{
		return io == DeviceOrientation.PortraitUpsideDown;
	}

	public static bool isPortrait(DeviceOrientation io)
	{
		return io == DeviceOrientation.Portrait || io == DeviceOrientation.PortraitUpsideDown;
	}

	public static bool isLandscape(DeviceOrientation io)
	{
		return io == DeviceOrientation.LandscapeLeft || io == DeviceOrientation.LandscapeRight;
	}

	private void initOrientationDetection()
	{
		currentOrientation = iphoneScreenOrientationToDeviceOrientation(Screen.orientation);
	}

	public virtual bool shouldAutorotateToInterfaceOrientation(DeviceOrientation orientation)
	{
		return true;
	}

	private void detectOrientation()
	{
		if (Input.deviceOrientation != currentOrientation)
		{
			switch (Input.deviceOrientation)
			{
			case DeviceOrientation.Portrait:
				onDeviceUpSideDown(Input.deviceOrientation);
				break;
			case DeviceOrientation.PortraitUpsideDown:
				onDeviceUpSideDown(Input.deviceOrientation);
				break;
			}
			currentOrientation = Input.deviceOrientation;
			currentOrientation = Input.deviceOrientation;
		}
	}

	private void detectTouch()
	{
		if (!_bCanInteract)
		{
			return;
		}
		if (Application.isMobilePlatform && Input.touchCount > 0 /*|| !Application.isMobilePlatform && InputHelper.GetTouches().Count > 0*/)
		{
			for (int i = 0; (Application.isMobilePlatform) ? i < Input.touchCount : i < InputHelper.GetTouches().Count; i++)
			{
				UITouch uITouch = new UITouch();
				uITouch.ConvertTouch((Application.isMobilePlatform) ? Input.GetTouch(i) : InputHelper.GetTouches()[0]);
				switch (uITouch.phase)
				{
				case TouchPhase.Began:
					Debug.Log("began");
					OnTouchBegan(uITouch);
					break;
				case TouchPhase.Moved:
					Debug.Log("moved");
					OnTouchMoved(uITouch);
					break;
				case TouchPhase.Ended:
					Debug.Log("ended");
					OnTouchEnded(uITouch);
					break;
				case TouchPhase.Canceled:
					Debug.Log("canceled");
					OnTouchCanceled(uITouch);
					break;
				}
			}
		}
		else if (!Application.isMobilePlatform)
		{
			Debug.LogError("returned true");
			if (Input.GetMouseButtonDown(0))
			{
				UITouch uITouch2 = new UITouch();
				uITouch2.fingerId = 0;
				uITouch2.position = Input.mousePosition;
				uITouch2.phase = TouchPhase.Began;
				OnTouchBegan(uITouch2);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				UITouch uITouch3 = new UITouch();
				uITouch3.fingerId = 0;
				uITouch3.position = Input.mousePosition;
				uITouch3.phase = TouchPhase.Ended;
				OnTouchEnded(uITouch3);
			}
			else if (Input.GetMouseButton(0) && (Input.mousePosition - _lastMousePosition).magnitude > 0f)
			{
				UITouch uITouch4 = new UITouch();
				uITouch4.fingerId = 0;
				uITouch4.position = Input.mousePosition;
				uITouch4.deltaPosition = Input.mousePosition - _lastMousePosition;
				uITouch4.phase = TouchPhase.Ended;
				uITouch4.deltaTime = Time.deltaTime;
				OnTouchMoved(uITouch4);
			}
			_lastMousePosition = Input.mousePosition;
		}
	}

	public virtual void OnTouchBegan(UITouch touch)
	{
	}

	public virtual void OnTouchMoved(UITouch touch)
	{
	}

	public virtual void OnTouchEnded(UITouch touch)
	{
	}

	public virtual void OnTouchCanceled(UITouch touch)
	{
	}

	public virtual void onDeviceUpSideDown(DeviceOrientation io)
	{
	}

	public void SetInteractEnable(bool enable)
	{
		_bCanInteract = enable;
	}
}
