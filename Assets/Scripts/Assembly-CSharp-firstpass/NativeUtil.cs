using UnityEngine;

public class NativeUtil : MonoBehaviour
{
	private static AndroidJavaObject plugin;

	static NativeUtil()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	plugin = new AndroidJavaObject("com.outblaze.nativeutil.NativeUtil");
		//}
	}

	public static int getVersionCode()
	{
		//Debug.Log("getVersionCode()");
		//return plugin.Call<int>("getVersionCode", new object[0]);
		return 0;
	}

	public static string getVersionName()
	{
		//Debug.Log("getVersionName()");
		//string text = plugin.Call<string>("getVersionName", new object[0]);
		//if (text == null)
		//{
		//	text = string.Empty;
		//}
		//return text;
		return "";
	}

	public static string getPackageName()
	{
		//return plugin.Call<string>("getPackageName", new object[0]);
		return "";
	}
}
