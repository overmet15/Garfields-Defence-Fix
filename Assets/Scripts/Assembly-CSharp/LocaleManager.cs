using UnityEngine;

public class LocaleManager
{
	public static string DeviceLocale()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("java.util.Locale");
		AndroidJavaObject androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getDefault", new object[0]);
		return androidJavaObject.Call<string>("getLanguage", new object[0]);
	}
}
