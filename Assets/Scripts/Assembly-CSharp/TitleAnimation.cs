using System.Collections;
using Outblaze;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
	private enum Character
	{
		Garfield = 0,
		Odie = 1,
		Jon = 2,
		E01 = 3,
		E06 = 4,
		E18 = 5,
		E19 = 6,
		E24 = 7,
		Fork = 8
	}

	public GameObject[] characters;

	public GameObject hitEffect;

	public Material garfieldMaterial;

	public Material odieMaterial;

	public Material jonMaterial;

	public Material E01Material;

	public Material E06Material;

	public Material E18Material;

	public Material E19Material;

	public Material E24Material;

	private void Start()
	{
		garfieldMaterial.color = Color.white;
		if (SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance.ChineseNewYear)
		{
			garfieldMaterial.mainTexture = Resources.Load("Textures/CNY/G01_Garfield") as Texture2D;
			odieMaterial.mainTexture = Resources.Load("Textures/CNY/G07_Odie") as Texture2D;
			jonMaterial.mainTexture = Resources.Load("Textures/Normal/SP06_Jon") as Texture2D;
			E01Material.mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
			E06Material.mainTexture = Resources.Load("Textures/Normal/E06", typeof(Texture2D)) as Texture2D;
			E18Material.mainTexture = Resources.Load("Textures/Normal/E18", typeof(Texture2D)) as Texture2D;
			E19Material.mainTexture = Resources.Load("Textures/Normal/E19", typeof(Texture2D)) as Texture2D;
			E24Material.mainTexture = Resources.Load("Textures/Normal/E24", typeof(Texture2D)) as Texture2D;
		}
		else
		{
			garfieldMaterial.mainTexture = Resources.Load("Textures/Normal/G01_Garfield") as Texture2D;
			odieMaterial.mainTexture = Resources.Load("Textures/Normal/G07_Odie") as Texture2D;
			jonMaterial.mainTexture = Resources.Load("Textures/Normal/SP06_Jon") as Texture2D;
			E01Material.mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
			E06Material.mainTexture = Resources.Load("Textures/Normal/E06", typeof(Texture2D)) as Texture2D;
			E18Material.mainTexture = Resources.Load("Textures/Normal/E18", typeof(Texture2D)) as Texture2D;
			E19Material.mainTexture = Resources.Load("Textures/Normal/E19", typeof(Texture2D)) as Texture2D;
			E24Material.mainTexture = Resources.Load("Textures/Normal/E24", typeof(Texture2D)) as Texture2D;
		}
		characters[0].transform.localPosition = new Vector3(-200f, 0f, 0f);
		characters[1].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[1].transform.localScale = new Vector3(200f, 200f, 1f);
		characters[1].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		characters[2].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[3].transform.localPosition = new Vector3(700f, 0f, 0f);
		characters[3].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		iTween.FadeTo(characters[3], 1f, 0f);
		characters[4].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[4].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		iTween.FadeTo(characters[4], 1f, 0f);
		characters[5].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[5].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		iTween.FadeTo(characters[5], 1f, 0f);
		characters[6].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[6].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		iTween.FadeTo(characters[6], 1f, 0f);
		characters[7].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[7].transform.Find("Shadow").gameObject.SetActiveRecursivelyLegacy(true);
		iTween.FadeTo(characters[7], 1f, 0f);
		characters[8].transform.localPosition = new Vector3(0f, 0f, -10000f);
		characters[0].GetComponent<Animation>().Play("Idle");
		characters[3].GetComponent<Animation>().Play("Walk");
		iTween.MoveTo(characters[0], iTween.Hash("x", 0, "time", 1f, "delay", 1f, "onstart", "PlayAnimation", "onstarttarget", base.gameObject, "onstartparams", new object[2]
		{
			characters[0],
			"Walk"
		}, "easetype", iTween.EaseType.linear));
		iTween.MoveTo(characters[3], iTween.Hash("x", 140, "time", 2f, "oncomplete", "GarfieldAttackE01", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator GarfieldAttackE01()
	{
		Transform transform = characters[3].transform;
		Vector3 position = new Vector3(transform.position.x, transform.position.y - 20f, -1f);
		GameObject shadow = transform.Find("Shadow").gameObject;
		characters[3].GetComponent<Animation>().CrossFade("Idle");
		characters[0].GetComponent<Animation>().CrossFade("Attack");
		yield return new WaitForSeconds(0.5f);
		Object.Instantiate(hitEffect, position, Quaternion.identity);
		characters[3].GetComponent<Animation>().CrossFade("Damage");
		yield return new WaitForSeconds(0.5f);
		characters[0].GetComponent<Animation>().CrossFade("Attack");
		yield return new WaitForSeconds(0.5f);
		Object.Instantiate(hitEffect, position, Quaternion.identity);
		characters[3].GetComponent<Animation>().CrossFade("Damage");
		yield return new WaitForSeconds(0.5f);
		characters[0].GetComponent<Animation>().CrossFade("SP01");
		yield return new WaitForSeconds(0.5f);
		Object.Instantiate(hitEffect, position, Quaternion.identity);
		characters[3].GetComponent<Animation>().CrossFade("Destroyed");
		shadow.SetActiveRecursivelyLegacy(false);
		yield return new WaitForSeconds(0.5f);
		characters[0].GetComponent<Animation>().CrossFade("Idle");
		yield return new WaitForSeconds(0.5f);
		shadow.SetActiveRecursivelyLegacy(true);
		E01Escape();
	}

	private void E01Escape()
	{
		characters[3].transform.localPosition = new Vector3(360f, 0f, 0f);
		characters[3].transform.localScale = new Vector3(-200f, 200f, 1f);
		characters[3].GetComponent<Animation>().Play("Walk");
		characters[0].GetComponent<Animation>().Play("Walk");
		iTween.MoveTo(characters[3], iTween.Hash("x", 700, "time", 1f, "oncomplete", string.Empty, "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
		iTween.MoveTo(characters[0], iTween.Hash("x", 200, "time", 1f, "oncomplete", "GarfieldThrowFork", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator GarfieldThrowFork()
	{
		GameObject garfield = characters[0];
		garfield.GetComponent<Animation>().CrossFade("RangeAttack");
		Transform transform = garfield.transform;
		yield return new WaitForSeconds(0.55f);
		GameObject fork = characters[8];
		fork.transform.localPosition = new Vector3(transform.localPosition.x + 100f, transform.localPosition.y + 70f, 1f);
		iTween.MoveTo(fork, iTween.Hash("x", 700, "time", 0.5f, "easetype", iTween.EaseType.linear));
		yield return new WaitForSeconds(0.45f);
		garfield.GetComponent<Animation>().CrossFade("Walk");
		iTween.MoveTo(garfield, iTween.Hash("x", 300, "time", 0.5f, "oncomplete", "GarfieldJump", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator GarfieldJump()
	{
		GameObject garfield = characters[0];
		garfield.GetComponent<Animation>().CrossFade("SP");
		yield return new WaitForSeconds(1f);
		garfield.GetComponent<Animation>().CrossFade("SP");
		yield return new WaitForSeconds(1f);
		garfield.GetComponent<Animation>().CrossFade("SP");
		E01WalkIn();
		yield return new WaitForSeconds(1f);
	}

	private void E01WalkIn()
	{
		GameObject gameObject = characters[3];
		gameObject.transform.localScale = new Vector3(200f, 200f, 1f);
		iTween.MoveTo(gameObject, iTween.Hash("x", 400, "time", 1f, "oncomplete", "GarfieldEscape", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator GarfieldEscape()
	{
		GameObject e = characters[3];
		e.GetComponent<Animation>().Play("Idle");
		GameObject garfield = characters[0];
		garfield.transform.localScale = new Vector3(-200f, 200f, 1f);
		garfield.GetComponent<Animation>().CrossFade("Walk");
		iTween.MoveTo(garfield, iTween.Hash("x", -700, "time", 2f, "easetype", iTween.EaseType.linear));
		yield return new WaitForSeconds(1f);
		EnemiesChaseGarfield();
	}

	private void EnemiesChaseGarfield()
	{
		GameObject gameObject = characters[3];
		gameObject.GetComponent<Animation>().CrossFade("Walk");
		iTween.MoveTo(gameObject, iTween.Hash("x", -300, "time", 2f, "oncomplete", "GarfieldChaseEnemies", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
		gameObject = characters[6];
		gameObject.GetComponent<Animation>().CrossFade("Walk");
		gameObject.transform.localPosition = new Vector3(600f, 0f, 0f);
		iTween.MoveTo(gameObject, iTween.Hash("x", -100, "time", 2f, "easetype", iTween.EaseType.linear));
		gameObject = characters[4];
		gameObject.GetComponent<Animation>().CrossFade("Walk");
		gameObject.transform.localPosition = new Vector3(740f, 0f, 1f);
		iTween.MoveTo(gameObject, iTween.Hash("x", 40, "time", 2f, "easetype", iTween.EaseType.linear));
		gameObject = characters[5];
		gameObject.GetComponent<Animation>().CrossFade("Walk");
		gameObject.transform.localPosition = new Vector3(880f, 0f, 2f);
		iTween.MoveTo(gameObject, iTween.Hash("x", 180, "time", 2f, "easetype", iTween.EaseType.linear));
		gameObject = characters[7];
		gameObject.GetComponent<Animation>().CrossFade("Walk");
		gameObject.transform.localPosition = new Vector3(1020f, 0f, 3f);
		iTween.MoveTo(gameObject, iTween.Hash("x", 320, "time", 2f, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator GarfieldChaseEnemies()
	{
		GameObject e = characters[3];
		e.GetComponent<Animation>().CrossFade("Idle");
		GameObject e2 = characters[4];
		e2.GetComponent<Animation>().CrossFade("Idle");
		GameObject e3 = characters[5];
		e3.GetComponent<Animation>().CrossFade("Idle");
		GameObject e4 = characters[6];
		e4.GetComponent<Animation>().CrossFade("Idle");
		GameObject e5 = characters[7];
		e5.GetComponent<Animation>().CrossFade("Idle");
		GameObject jon = characters[2];
		jon.transform.position = new Vector3(-800f, -200f, -1f);
		iTween.MoveTo(jon, iTween.Hash("x", 800, "time", 2f, "easetype", iTween.EaseType.linear));
		yield return new WaitForSeconds(0.3f);
		e.GetComponent<Animation>().CrossFade("Destroyed");
		Transform transform5 = e.transform;
		GameObject shadow5 = transform5.Find("Shadow").gameObject;
		shadow5.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform5.position.x, transform5.position.y - 20f, -2f), original: hitEffect, rotation: Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		GameObject garfield = characters[0];
		garfield.transform.localPosition = new Vector3(-850f, 0f, 0f);
		garfield.transform.localScale = new Vector3(200f, 200f, 1f);
		garfield.GetComponent<Animation>().CrossFade("Walk");
		GameObject odie = characters[1];
		odie.transform.localPosition = new Vector3(-700f, 0f, -1f);
		odie.GetComponent<Animation>().CrossFade("Walk");
		iTween.MoveTo(garfield, iTween.Hash("x", -200, "time", 1f, "oncomplete", "Hurray", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
		iTween.MoveTo(odie, iTween.Hash("x", -50, "time", 1f, "easetype", iTween.EaseType.linear));
		e4.GetComponent<Animation>().CrossFade("Destroyed");
		transform5 = e4.transform;
		shadow5 = transform5.Find("Shadow").gameObject;
		shadow5.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform5.position.x, transform5.position.y, -2f), original: hitEffect, rotation: Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		e2.GetComponent<Animation>().CrossFade("Destroyed");
		transform5 = e2.transform;
		shadow5 = transform5.Find("Shadow").gameObject;
		shadow5.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform5.position.x, transform5.position.y, -2f), original: hitEffect, rotation: Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		e3.GetComponent<Animation>().CrossFade("Destroyed");
		transform5 = e3.transform;
		shadow5 = transform5.Find("Shadow").gameObject;
		shadow5.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform5.position.x, transform5.position.y, -2f), original: hitEffect, rotation: Quaternion.identity);
		yield return new WaitForSeconds(0.2f);
		e5.GetComponent<Animation>().CrossFade("Destroyed");
		transform5 = e5.transform;
		shadow5 = transform5.Find("Shadow").gameObject;
		shadow5.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform5.position.x, transform5.position.y, -2f), original: hitEffect, rotation: Quaternion.identity);
		yield return new WaitForSeconds(1f);
		iTween.FadeTo(e, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.linear));
		iTween.FadeTo(e2, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.linear));
		iTween.FadeTo(e3, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.linear));
		iTween.FadeTo(e4, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.linear));
		iTween.FadeTo(e5, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.linear));
	}

	private IEnumerator Hurray()
	{
		GameObject garfield = characters[0];
		garfield.GetComponent<Animation>().CrossFade("SP");
		GameObject odie = characters[1];
		odie.GetComponent<Animation>().Play("Hurray");
		yield return new WaitForSeconds(1f);
		garfield.GetComponent<Animation>().CrossFade("SP");
		odie.GetComponent<Animation>().Play("Hurray");
		yield return new WaitForSeconds(1f);
		garfield.GetComponent<Animation>().CrossFade("SP");
		odie.transform.localScale = new Vector3(-200f, 200f, 1f);
		odie.GetComponent<Animation>().Play("Hurray");
		yield return new WaitForSeconds(1f);
		odie.GetComponent<Animation>().Play("Attack");
		yield return new WaitForSeconds(0.5f);
		garfield.GetComponent<Animation>().CrossFade("Damage");
		yield return new WaitForSeconds(0.5f);
		odie.GetComponent<Animation>().Play("Idle");
		yield return new WaitForSeconds(0.5f);
		garfield.GetComponent<Animation>().CrossFade("Idle");
		yield return new WaitForSeconds(1f);
		odie.GetComponent<Animation>().Play("Hurray");
		yield return new WaitForSeconds(1f);
		garfield.GetComponent<Animation>().CrossFade("SP01");
		yield return new WaitForSeconds(0.2f);
		odie.GetComponent<Animation>().Play("Damage");
		Transform transform = odie.transform;
		GameObject shadow = transform.Find("Shadow").gameObject;
		shadow.SetActiveRecursivelyLegacy(false);
		Object.Instantiate(position: new Vector3(transform.position.x, transform.position.y, -2f), original: hitEffect, rotation: Quaternion.identity);
		iTween.MoveTo(odie, iTween.Hash("y", 500, "time", 0.2f, "oncomplete", string.Empty, "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
		yield return new WaitForSeconds(0.8f);
		garfield.GetComponent<Animation>().CrossFade("Idle");
		yield return new WaitForSeconds(1f);
		Start();
	}

	private void PlayAnimation(object[] objects)
	{
		for (int i = 0; i < objects.Length; i += 2)
		{
			((GameObject)objects[i]).GetComponent<Animation>().CrossFade((string)objects[i + 1]);
		}
	}
}
