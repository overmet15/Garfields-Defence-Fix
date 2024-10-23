using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class NativeAlertManager : MonoBehaviour
{
	[method: MethodImpl(32)]
	public static event Action<bool> OnClick;

	private void Awake()
	{
		base.gameObject.name = GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void DidClick(string result)
	{
		Debug.Log("!!!!~~~~~~~~~~~ DidClick");
		if (NativeAlertManager.OnClick != null)
		{
			NativeAlertManager.OnClick(bool.Parse(result));
		}
	}
}
