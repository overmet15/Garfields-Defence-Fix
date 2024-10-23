using UnityEngine;

public sealed class NativeAlertBinding
{
	public static void ShowAlert(string title, string message, string negativeText, string positiveText)
	{
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.outblaze.util.UnityNativeAlert", title, message, negativeText, positiveText);
		androidJavaObject.Call("show");
	}
}
