using System.Collections;
using UnityEngine;

public class StarStrike_Base : MonoBehaviour, StarStrike_Targetable, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	public Owner owner;

	private int maxLife;

	private int currentLife;

	public GameObject damageIndicatorPrefab;

	private Transform thisTransform;

	public Texture lifeBarTexture;

	public Rect lifeBarRect;

	public Texture lifeValueTexture;

	public Rect currentLifeRect;

	public SmoothBarDirection smoothBarDirection;

	private StarStrike_SmoothBar smoothBar;

	public Texture backgroundTexture;

	public Animation viewAnimation;

	public GUIStyle lifeTextStyle;

	public Rect lifeTextRect;

	private string lifeText;

	public GameObject EZGUI;

	public UIPanel treeInDangerPanel;

	private bool attackShow;

	public Material material;

	public GameObject effect1;

	public GameObject effect2;

	private GameObject hpBarObject;

	private GameObject hpBar;

	private int timerCount;

	private void Start()
	{
		material.color = Color.white;
		treeInDangerPanel.Dismiss();
		getMaxLife();
		thisTransform = base.transform;
		UpdateLifeView();
		smoothBar = new StarStrike_SmoothBar(lifeValueTexture, backgroundTexture, currentLifeRect, maxLife, 0.01f, smoothBarDirection);
		smoothBar.SetValue(maxLife);
		Transform transform = thisTransform.Find("View");
		StarStrike_Assertion.Assert(transform != null, "viewTransform must not be null");
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
		createHP();
	}

	private void Update()
	{
		if (attackShow)
		{
			if (timerCount >= 100)
			{
				treeInDangerPanel.Dismiss();
			}
			else
			{
				timerCount++;
			}
		}
		smoothBar.Update();
	}

	private void UpdateLifeView()
	{
		float num = (float)currentLife * 1f / (float)maxLife * 1f;
		Debug.Log("---- Tree Life: " + currentLife + ", " + num);
		if (num <= 0f)
		{
			Debug.Log("Hide Effect");
			effect1.SetActiveRecursivelyLegacy(false);
			effect2.SetActiveRecursivelyLegacy(false);
			if (viewAnimation != null)
			{
				viewAnimation.Play("Destroyed");
			}
		}
		else if (num <= 0.5f && viewAnimation != null && !viewAnimation.IsPlaying("Half"))
		{
			viewAnimation.Play("Half");
		}
		SetHP(num);
	}

	private void getMaxLife()
	{
		FD_ForrestConfiguration component = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
		FD_ObjectLevelDefinition currentLevel = component.GetCurrentLevel("01Trees");
		maxLife = int.Parse(currentLevel.GetAttributeValue("health"));
		currentLife = maxLife;
	}

	public Owner GetOwner()
	{
		return owner;
	}

	public void ReceiveDamage(int damage)
	{
		Vector3 position = thisTransform.Find("Mesh").position;
		position.z -= 0.5f;
		Object.Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
		if (currentLife > 0)
		{
			currentLife -= damage;
			if (currentLife < 0)
			{
				currentLife = 0;
			}
			smoothBar.UpdateValue(-damage);
			UpdateLifeView();
			if (currentLife <= 0)
			{
				PostOnDestroyEvent();
			}
			StopAllCoroutines();
			material.color = Color.red;
			StartCoroutine(ResetColor());
		}
	}

	private IEnumerator ResetColor()
	{
		yield return new WaitForSeconds(0.1f);
		material.color = Color.white;
	}

	private void PostOnDestroyEvent()
	{
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = null;
		if (GetOwner() == Owner.TOM)
		{
			starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.TOM_BASE_DESTROYED);
		}
		else if (GetOwner() == Owner.JERRY)
		{
			starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.JERRY_BASE_DESTROYED);
		}
		starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	public bool IsAlive()
	{
		return currentLife > 0;
	}

	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_EventType eventType = gameEvent.GetEventType();
		if (eventType == StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED)
		{
			currentLife = maxLife;
			smoothBar.ClearUpdateValues();
			smoothBar.SetValue(maxLife);
			UpdateLifeView();
		}
	}

	private void createHP()
	{
		hpBarObject = (GameObject)Object.Instantiate(Resources.Load("Base HP Bar"));
		hpBar = hpBarObject.transform.Find("Bar").gameObject;
		hpBarObject.transform.parent = base.transform;
		hpBarObject.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		hpBarObject.transform.eulerAngles = Vector3.zero;
		hpBarObject.transform.position = base.transform.TransformPoint(Vector3.zero) + new Vector3(3f, 7f, -10f);
	}

	private void SetHP(float value)
	{
		if (hpBarObject != null)
		{
			if (!attackShow && value < 1f)
			{
				Debug.Log("TREE IN DANGER PANEL SHOW");
				treeInDangerPanel.BringIn();
				attackShow = true;
				timerCount = 0;
			}
			Debug.Log("trrr HP:" + value);
			hpBar.transform.localScale = new Vector3(1f, value, 1f);
			InGameUIDemo component = EZGUI.GetComponent<InGameUIDemo>();
			component.updateTreeHealth(value);
		}
	}

	public IEnumerator WaitAndPrint(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		treeInDangerPanel.Dismiss();
		Debug.Log("------------- > WaitAndPrint " + Time.time);
	}
}
