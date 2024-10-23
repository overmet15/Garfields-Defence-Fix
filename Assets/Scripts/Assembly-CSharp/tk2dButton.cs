using System.Collections;
using UnityEngine;

[AddComponentMenu("2D Toolkit/GUI/tk2dButton")]
[RequireComponent(typeof(tk2dSprite))]
public class tk2dButton : MonoBehaviour
{
	public Camera viewCamera;

	public string buttonDownSprite = "button_down";

	public string buttonUpSprite = "button_up";

	public string buttonPressedSprite = "button_up";

	private int buttonDownSpriteId = -1;

	private int buttonUpSpriteId = -1;

	private int buttonPressedSpriteId = -1;

	public AudioClip buttonDownSound;

	public AudioClip buttonUpSound;

	public AudioClip buttonPressedSound;

	public GameObject targetObject;

	public string messageName = string.Empty;

	private tk2dSprite sprite;

	private bool buttonDown;

	private float targetScale = 1.1f;

	private float scaleTime = 0.05f;

	private float pressedWaitTime = 0.3f;

	private void Start()
	{
		if (viewCamera == null)
		{
			Transform parent = base.transform;
			while ((bool)parent && parent.GetComponent<Camera>() == null)
			{
				parent = parent.parent;
			}
			if ((bool)parent && parent.GetComponent<Camera>() != null)
			{
				viewCamera = parent.GetComponent<Camera>();
			}
			if (viewCamera == null)
			{
				viewCamera = Camera.main;
			}
		}
		sprite = GetComponent<tk2dSprite>();
		buttonDownSpriteId = sprite.GetSpriteIdByName(buttonDownSprite);
		buttonUpSpriteId = sprite.GetSpriteIdByName(buttonUpSprite);
		buttonPressedSpriteId = sprite.GetSpriteIdByName(buttonPressedSprite);
		if (GetComponent<Collider>() == null)
		{
			BoxCollider boxCollider = base.gameObject.AddComponent<BoxCollider>();
			Vector3 size = boxCollider.size;
			size.z = 0.2f;
			boxCollider.size = size;
		}
	}

	private void PlaySound(AudioClip source)
	{
		if ((bool)GetComponent<AudioSource>() && (bool)source)
		{
			GetComponent<AudioSource>().PlayOneShot(source);
		}
	}

	private IEnumerator coScale(Vector3 defaultScale, float startScale, float endScale)
	{
		Vector3 scale2 = defaultScale;
		float s = 0f;
		while (s < scaleTime)
		{
			float t = Mathf.Clamp01(s / scaleTime);
			float scl = Mathf.Lerp(startScale, endScale, t);
			scale2 = defaultScale * scl;
			base.transform.localScale = scale2;
			s += Time.deltaTime;
			yield return 0;
		}
		base.transform.localScale = defaultScale * endScale;
	}

	private IEnumerator coHandleButtonPress()
	{
		buttonDown = true;
		bool buttonPressed = true;
		Vector3 defaultScale = base.transform.localScale;
		yield return StartCoroutine(coScale(defaultScale, 1f, targetScale));
		PlaySound(buttonDownSound);
		sprite.spriteId = buttonDownSpriteId;
		while (Input.GetMouseButton(0))
		{
			Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			bool colliderHit = GetComponent<Collider>().Raycast(ray, out hitInfo, 100000000f);
			if (buttonPressed && !colliderHit)
			{
				yield return StartCoroutine(coScale(defaultScale, targetScale, 1f));
				PlaySound(buttonUpSound);
				sprite.spriteId = buttonUpSpriteId;
				buttonPressed = false;
			}
			else if (!buttonPressed && colliderHit)
			{
				yield return StartCoroutine(coScale(defaultScale, 1f, targetScale));
				PlaySound(buttonDownSound);
				sprite.spriteId = buttonDownSpriteId;
				buttonPressed = true;
			}
			yield return 0;
		}
		if (buttonPressed)
		{
			yield return StartCoroutine(coScale(defaultScale, targetScale, 1f));
			PlaySound(buttonPressedSound);
			sprite.spriteId = buttonPressedSpriteId;
			if ((bool)targetObject)
			{
				targetObject.SendMessage(messageName);
			}
			yield return new WaitForSeconds(pressedWaitTime);
			sprite.spriteId = buttonUpSpriteId;
		}
		buttonDown = false;
	}

	private void Update()
	{
		if (!buttonDown && Input.GetMouseButtonDown(0))
		{
			Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (GetComponent<Collider>().Raycast(ray, out hitInfo, 100000000f))
			{
				StartCoroutine(coHandleButtonPress());
			}
		}
	}
}
