using System.Collections;
using System.Collections.Generic;
using Outblaze;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public UIPanel[] panels;

	public bool[] shouldPause;

	private List<int> tutorialQueue;

	private List<float> tutorialDuration;

	private bool showing;

	private int currentPanelIndex;

	private float currentDuration;

	private UserProfileManager up;

	private LanguageManager langMan;

	public bool IsShowing
	{
		get
		{
			return showing;
		}
	}

	public int CurrentPanelIndex
	{
		get
		{
			return currentPanelIndex;
		}
	}

	private void Awake()
	{
		tutorialQueue = new List<int>();
		tutorialDuration = new List<float>();
		up = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
	}

	public void Push(int index)
	{
		Push(index, 0f);
	}

	public void Insert(int index)
	{
		Insert(index, 0f);
	}

	public void Insert(int index, float duration)
	{
		if (!tutorialQueue.Contains(index))
		{
			StopAllCoroutines();
			if (showing)
			{
				tutorialQueue.Insert(0, CurrentPanelIndex);
				tutorialDuration.Insert(0, currentDuration);
			}
			tutorialQueue.Insert(0, index);
			tutorialDuration.Insert(0, duration);
			if (showing)
			{
				ShowNext();
			}
		}
	}

	public void Push(int index, float duration)
	{
		if (!tutorialQueue.Contains(index))
		{
			tutorialQueue.Add(index);
			tutorialDuration.Add(duration);
		}
	}

	public void Remove(int index)
	{
		if (tutorialQueue.Contains(index))
		{
			int index2 = tutorialQueue.IndexOf(index);
			tutorialQueue.RemoveAt(index2);
			tutorialDuration.RemoveAt(index2);
		}
	}

	public void Show()
	{
		if (showing)
		{
			return;
		}
		showing = true;
		panels[currentPanelIndex].BringIn();
		SpriteText component = panels[currentPanelIndex].transform.Find("Caption").GetComponent<SpriteText>();
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		if (component != null)
		{
			fontManagerInstance.SetSmallFontMat(component);
			if (up.getLangCode() == "ja")
			{
				if (currentPanelIndex == 5)
				{
					component.transform.localPosition = new Vector3(-80f, 99f, -2f);
				}
				else if (currentPanelIndex == 11)
				{
					component.transform.localPosition = new Vector3(-6f, -100f, -2f);
				}
			}
			else if (up.getLangCode() == "fr" || up.getLangCode() == "de")
			{
				if (currentPanelIndex == 5)
				{
					component.SetCharacterSize(25f);
					component.transform.localPosition = new Vector3(-80f, 97f, -2f);
				}
				else if (currentPanelIndex == 11)
				{
					component.SetCharacterSize(25f);
					component.transform.localPosition = new Vector3(-6f, -100f, -2f);
				}
			}
			component.Text = langMan.getLangData("Tutorial" + currentPanelIndex);
		}
		if (panels[currentPanelIndex].transform.Find("Message") != null)
		{
			component = panels[currentPanelIndex].transform.Find("Message").GetComponent<SpriteText>();
			fontManagerInstance.SetSmallFontMat(component);
			if (up.getLangCode() == "fr" || up.getLangCode() == "de")
			{
				component.SetCharacterSize(35f);
			}
			component.Text = langMan.getLangData("Tutorial" + currentPanelIndex + "b");
		}
		if (shouldPause[currentPanelIndex])
		{
			Time.timeScale = 0f;
		}
		if (currentDuration > 0f)
		{
			StartCoroutine(ShowNext(currentDuration));
		}
	}

	public void ShowNext()
	{
		if (tutorialQueue.Count > 0)
		{
			if (showing)
			{
				if (shouldPause[currentPanelIndex])
				{
					Time.timeScale = 1f;
				}
				panels[currentPanelIndex].Dismiss();
			}
			currentPanelIndex = tutorialQueue[0];
			currentDuration = tutorialDuration[0];
			tutorialQueue.RemoveAt(0);
			tutorialDuration.RemoveAt(0);
			showing = false;
			Show();
		}
		else
		{
			Hide();
		}
	}

	private IEnumerator ShowNext(float delay)
	{
		float pauseEndTime = Time.realtimeSinceStartup + delay;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		if (showing)
		{
			ShowNext();
		}
	}

	public void Hide()
	{
		StopAllCoroutines();
		if (showing)
		{
			showing = false;
			if (shouldPause[currentPanelIndex])
			{
				Time.timeScale = 1f;
			}
			panels[currentPanelIndex].Dismiss();
		}
	}

	public void Clear()
	{
		Hide();
		tutorialQueue.Clear();
		tutorialDuration.Clear();
		currentPanelIndex = 0;
		currentDuration = 0f;
	}
}
