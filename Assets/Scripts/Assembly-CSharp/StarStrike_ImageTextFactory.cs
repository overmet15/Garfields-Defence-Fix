using System;
using System.Collections;
using UnityEngine;

public class StarStrike_ImageTextFactory : MonoBehaviour
{
	public Texture[] numbers;

	public Texture[] upperCaseLetters;

	public Texture[] lowerCaseLetters;

	public Texture plusSign;

	public Texture minusSign;

	private Hashtable characterMap;

	private static Vector3 defaultScale = new Vector3(1f, 1f, 1f);

	private static Vector3 defaultRotation = new Vector3(-90f, 0f, 0f);

	private void Awake()
	{
		StarStrike_Assertion.Assert(numbers.Length == 10, "[ImageTextFactory] Invalid number of textures for numeric characters.");
		characterMap = new Hashtable();
		int num = 0;
		for (num = 0; num < numbers.Length; num++)
		{
			Texture value = numbers[num];
			characterMap.Add(num.ToString(), value);
		}
		for (num = 0; num < upperCaseLetters.Length; num++)
		{
			int value2 = Convert.ToInt32('A') + num;
			string key = string.Empty + Convert.ToChar(value2);
			characterMap.Add(key, upperCaseLetters[num]);
		}
		for (num = 0; num < lowerCaseLetters.Length; num++)
		{
			int value3 = Convert.ToInt32('a') + num;
			string key2 = string.Empty + Convert.ToChar(value3);
			characterMap.Add(key2, lowerCaseLetters[num]);
		}
		characterMap.Add("+", plusSign);
		characterMap.Add("-", minusSign);
	}

	public void CreateText(string text, Vector3 position)
	{
		CreateText(text, position, Quaternion.Euler(defaultRotation), defaultScale);
	}

	public void CreateText(string text, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		Vector3 position2 = new Vector3(position.x, position.y, position.z);
		GameObject gameObject = new GameObject("ImageText - " + text);
		gameObject.transform.position = Vector3.zero;
		foreach (char c in text)
		{
			Texture texture = (Texture)characterMap[c + string.Empty];
			float num = scale.x;
			float y = scale.y;
			if (texture != null)
			{
				float num2 = Mathf.Max(texture.width, texture.height);
				num *= (float)texture.width / num2;
				y *= (float)texture.height / num2;
				GameObject gameObject2 = CreateCharacter(scale: new Vector3(num, y, 1f), charTexture: texture, position: position2, rotation: rotation);
				gameObject2.transform.parent = gameObject.transform;
			}
			else
			{
				Debug.Log("[ImageText] Texture not found for '" + c + "'.");
			}
			position2.x += num;
		}
		Vector3 position3 = gameObject.transform.position;
		position3.x -= (position2.x - position.x) / 2f;
		gameObject.transform.position = position3;
		gameObject.AddComponent<StarStrike_ImageText>();
	}

	private GameObject CreateCharacter(Texture charTexture, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		GameObject gameObject = CreatePlane(position, rotation, scale);
		Renderer component = gameObject.GetComponent<Renderer>();
		Shader shader = Shader.Find("Transparent/Cutout/Diffuse");
		Material material = new Material(shader);
		material.mainTexture = charTexture;
		component.material = material;
		return gameObject;
	}

	private GameObject CreatePlane(Vector3 position, Quaternion rotation, Vector3 scale)
	{
		GameObject gameObject = new GameObject("TextPlane");
		Vector3[] vertices = new Vector3[4]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(scale.x, 0f, 0f),
			new Vector3(0f, 0f, scale.y),
			new Vector3(scale.x, 0f, scale.y)
		};
		int[] triangles = new int[6] { 0, 2, 3, 0, 3, 1 };
		Vector3[] array = new Vector3[4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = Vector3.up;
		}
		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = array;
		mesh.uv = uv;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = mesh;
		gameObject.AddComponent<MeshRenderer>();
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;
		return gameObject;
	}
}
