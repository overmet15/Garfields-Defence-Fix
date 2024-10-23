using UnityEngine;

public class D01_Squirrel : MonoBehaviour
{
	public GameObject _Bullet;

	private void Start()
	{
		HideBullet();
	}

	public void HideBullet()
	{
		_Bullet.SetActiveRecursivelyLegacy(false);
	}

	public void ShowBullet()
	{
		Invoke("ShowBulletOffSet", 0.8f);
		Invoke("HideBullet", 2f);
	}

	private void ShowBulletOffSet()
	{
		_Bullet.SetActiveRecursivelyLegacy(true);
	}
}
