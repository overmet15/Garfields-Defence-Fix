using UnityEngine;

public class IAPPackage : MonoBehaviour
{
	[SerializeField]
	private string _replacablePackageName;

	[SerializeField]
	private string _defaultPackageName;

	public string replacablePackageName
	{
		get
		{
			return _replacablePackageName;
		}
	}

	public string defaultPackageName
	{
		get
		{
			return _defaultPackageName;
		}
	}
}
