using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerAAA : UIViewController
{
	private const float RIGHTMOST_X = 5.608241f;

	private const float LEFTMOST_X = -6.219444f;

	private UITouch currentTouch;

	private GameObject _RefButtonPress;

	private Vector3 _ScaleUp;

	private int ButtonClickedIndex;

	private StarStrike_ArmyUnit StarStrike_ArmyUnit;

	public GameObject[] _ButtonCollection;

	public Camera _HUDCam;

	private Vector3? raycastCollider(Collider c, Vector2 screenPos)
	{
		Vector3? result = null;
		Ray ray = _HUDCam.ScreenPointToRay(screenPos);
		RaycastHit hitInfo;
		if (c.Raycast(ray, out hitInfo, 100f))
		{
			result = hitInfo.point;
		}
		return result;
	}

	public override void OnTouchBegan(UITouch touch)
	{
		if (currentTouch != null)
		{
			return;
		}
		currentTouch = touch;
		int num = 0;
		GameObject[] buttonCollection = _ButtonCollection;
		foreach (GameObject gameObject in buttonCollection)
		{
			if (raycastCollider(gameObject.GetComponent<Collider>(), touch.position).HasValue)
			{
				if (ButtonClickedIndex == -1 && num < 2)
				{
					ButtonClickedIndex = num;
					ButtonPress(gameObject);
				}
				break;
			}
			num++;
		}
	}

	public override void OnTouchMoved(UITouch touch)
	{
	}

	public override void OnTouchEnded(UITouch touch)
	{
		currentTouch = null;
	}

	private void ButtonCollectionAction(int id)
	{
		Debug.Log("button " + id + " is clicked");
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		switch (id)
		{
		case 0:
			loadingManagerInstance.setSceneId("JerryLose");
			SceneManager.LoadScene("Loading");
			break;
		case 1:
			loadingManagerInstance.setSceneId("Mini_Game");
			SceneManager.LoadScene("Loading");
			break;
		case 2:
			break;
		}
	}

	private void ButtonScaleBack()
	{
		_RefButtonPress.transform.localScale = _ScaleUp;
		if (ButtonClickedIndex != -1)
		{
			ButtonCollectionAction(ButtonClickedIndex);
			ButtonClickedIndex = -1;
		}
	}

	private void ButtonPress(GameObject obj)
	{
		_RefButtonPress = obj;
		_ScaleUp = obj.transform.localScale;
		Vector3 localScale = new Vector3(obj.transform.localScale.x * 1.7f, obj.transform.localScale.y * 1.7f, obj.transform.localScale.z * 1.7f);
		obj.transform.localScale = localScale;
		Invoke("ButtonScaleBack", 0.1f);
	}

	private void Start()
	{
		ButtonClickedIndex = -1;
	}
}
