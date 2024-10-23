using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Backend/tk2dFont")]
public class tk2dFont : MonoBehaviour
{
	public UnityEngine.Object bmFont;

	public Material material;

	public Texture texture;

	public Texture2D gradientTexture;

	public bool dupeCaps;

	public bool flipTextureY;

	public int targetHeight = 640;

	public float targetOrthoSize = 1f;

	public int gradientCount = 1;

	[NonSerialized]
	[HideInInspector]
	public int numCharacters = 256;

	public bool manageMaterial;

	public tk2dFontData data;
}
