using UnityEngine;

internal class StarStrike_StartScreen : MonoBehaviour
{
	private void Start()
	{
		Object.DontDestroyOnLoad(GameObject.Find("BackgroundMusic"));
	}
}
