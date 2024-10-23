using System.Collections;
using UnityEngine;

public class StarStrike_UI : MonoBehaviour
{
	private IList uiRendererList;

	private void Start()
	{
		uiRendererList = new ArrayList();
		StarStrike_UiRenderer component = GameObject.Find("JerryUnitControl").GetComponent<StarStrike_JerryUnitControl>();
		StarStrike_Assertion.AssertNotNull(component, "jerryUnitControl");
		uiRendererList.Add(component);
		StarStrike_UiRenderer component2 = GameObject.Find("ScoreHUD").GetComponent<StarStrike_ScoreHud>();
		StarStrike_Assertion.AssertNotNull(component2, "scoreHud");
		uiRendererList.Add(component2);
		StarStrike_UiRenderer component3 = GameObject.Find("MiscHUD").GetComponent<StarStrike_MiscHud>();
		StarStrike_Assertion.AssertNotNull(component3, "miscHud");
		uiRendererList.Add(component3);
		StarStrike_UiRenderer component4 = GameObject.Find("LevelNumberHUD").GetComponent<StarStrike_LevelNumberHud>();
		StarStrike_Assertion.AssertNotNull(component4, "levelNumberHud");
		uiRendererList.Add(component4);
	}

	private void OnGUI()
	{
		foreach (StarStrike_UiRenderer uiRenderer in uiRendererList)
		{
			uiRenderer.RenderUI();
		}
	}
}
