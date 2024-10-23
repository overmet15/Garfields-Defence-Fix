using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
	public string command;

	private UnityEngine.UI.Text titleText;

	private bool held;

	private Color oldColor;

	private void Start()
	{
		titleText = base.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
		titleText.material.color = new Color(0f, 0.25f, 0.1f, 1f);
		oldColor = GetComponent<Renderer>().material.color;
	}

	private void DoPick(Vector3 pos, bool down)
	{
		if (!base.enabled)
		{
			return;
		}
		Camera component = GameObject.Find("Camera").GetComponent<Camera>();
		Ray ray = component.ScreenPointToRay(pos);
		RaycastHit hitInfo = default(RaycastHit);
		if (!Physics.Raycast(ray, out hitInfo) || !(hitInfo.collider.gameObject == base.gameObject))
		{
			return;
		}
		if (!down)
		{
			if (held)
			{
				base.gameObject.BroadcastMessage(command);
				GetComponent<Renderer>().material.color = oldColor;
				held = false;
			}
		}
		else if (down)
		{
			base.gameObject.BroadcastMessage("OnPress", SendMessageOptions.DontRequireReceiver);
			GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
			held = true;
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			DoPick(Input.mousePosition, false);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			DoPick(Input.mousePosition, true);
		}
		Touch[] touches = Input.touches;
		if (!Application.isMobilePlatform)
		{
			touches = InputHelper.GetTouches().ToArray();
		}
		for (int i = 0; i < touches.Length; i++)
		{
			Touch touch = touches[i];
			if (touch.phase == TouchPhase.Ended)
			{
				DoPick(new Vector3(touch.position.x, touch.position.y, 0f), false);
			}
			else if (touch.phase == TouchPhase.Began)
			{
				DoPick(new Vector3(touch.position.x, touch.position.y, 0f), true);
			}
		}
	}
}
