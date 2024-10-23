using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/tk2dTextMesh")]
[RequireComponent(typeof(MeshFilter))]
public class tk2dTextMesh : MonoBehaviour
{
	[Flags]
	private enum UpdateFlags
	{
		UpdateNone = 0,
		UpdateText = 1,
		UpdateColors = 2,
		UpdateBuffers = 4
	}

	[SerializeField]
	private tk2dFontData _font;

	[SerializeField]
	private string _text = string.Empty;

	[SerializeField]
	private Color _color = Color.white;

	[SerializeField]
	private Color _color2 = Color.white;

	[SerializeField]
	private bool _useGradient;

	[SerializeField]
	private int _textureGradient;

	[SerializeField]
	private TextAnchor _anchor = TextAnchor.LowerLeft;

	[SerializeField]
	private Vector3 _scale = new Vector3(1f, 1f, 1f);

	[SerializeField]
	private bool _kerning;

	[SerializeField]
	private int _maxChars = 16;

	[SerializeField]
	private bool _inlineStyling;

	public bool pixelPerfect;

	private Vector3[] vertices;

	private Vector2[] uvs;

	private Vector2[] uv2;

	private Color[] colors;

	private UpdateFlags updateFlags = UpdateFlags.UpdateBuffers;

	private Mesh mesh;

	public tk2dFontData font
	{
		get
		{
			return _font;
		}
		set
		{
			_font = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public string text
	{
		get
		{
			return _text;
		}
		set
		{
			_text = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public Color color
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
			updateFlags |= UpdateFlags.UpdateColors;
		}
	}

	public Color color2
	{
		get
		{
			return _color2;
		}
		set
		{
			_color2 = value;
			updateFlags |= UpdateFlags.UpdateColors;
		}
	}

	public bool useGradient
	{
		get
		{
			return _useGradient;
		}
		set
		{
			_useGradient = value;
			updateFlags |= UpdateFlags.UpdateColors;
		}
	}

	public TextAnchor anchor
	{
		get
		{
			return _anchor;
		}
		set
		{
			_anchor = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public Vector3 scale
	{
		get
		{
			return _scale;
		}
		set
		{
			_scale = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public bool kerning
	{
		get
		{
			return _kerning;
		}
		set
		{
			_kerning = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public int maxChars
	{
		get
		{
			return _maxChars;
		}
		set
		{
			_maxChars = value;
			updateFlags |= UpdateFlags.UpdateBuffers;
		}
	}

	public int textureGradient
	{
		get
		{
			return _textureGradient;
		}
		set
		{
			_textureGradient = value % font.gradientCount;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	public bool inlineStyling
	{
		get
		{
			return _inlineStyling;
		}
		set
		{
			_inlineStyling = value;
			updateFlags |= UpdateFlags.UpdateText;
		}
	}

	private void Awake()
	{
		if (pixelPerfect)
		{
			MakePixelPerfect();
		}
		Init();
	}

	public void Init(bool force)
	{
		if (force)
		{
			updateFlags |= UpdateFlags.UpdateBuffers;
		}
		Init();
	}

	public int NumDrawnCharacters()
	{
		bool useInlineStyling = inlineStyling && _font.textureGradients;
		float offsetX = 0f;
		float offsetY = 0f;
		int num = _maxChars;
		_maxChars = int.MaxValue;
		int result = CalcAnchor(useInlineStyling, out offsetX, out offsetY);
		_maxChars = num;
		return result;
	}

	private int FillTextData()
	{
		Vector2 vector = new Vector2((float)_textureGradient / (float)font.gradientCount, 0f);
		bool flag = inlineStyling && _font.textureGradients;
		float offsetX;
		float offsetY;
		CalcAnchor(flag, out offsetX, out offsetY);
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		for (int i = 0; i < _text.Length && num3 < _maxChars; i++)
		{
			int num4 = _text[i];
			if (num4 >= _font.chars.Length)
			{
				num4 = 0;
			}
			tk2dFontChar tk2dFontChar2 = _font.chars[num4];
			if (num4 == 10)
			{
				num = 0f;
				num2 -= _font.lineHeight * _scale.y;
				continue;
			}
			if (flag && num4 == 94 && i + 1 < _text.Length)
			{
				i++;
				if (_text[i] != '^')
				{
					int num5 = _text[i] - 48;
					vector = new Vector2((float)num5 / (float)font.gradientCount, 0f);
					continue;
				}
			}
			vertices[num3 * 4] = new Vector3(offsetX + num + tk2dFontChar2.p0.x * _scale.x, offsetY + num2 + tk2dFontChar2.p0.y * _scale.y, 0f);
			vertices[num3 * 4 + 1] = new Vector3(offsetX + num + tk2dFontChar2.p1.x * _scale.x, offsetY + num2 + tk2dFontChar2.p0.y * _scale.y, 0f);
			vertices[num3 * 4 + 2] = new Vector3(offsetX + num + tk2dFontChar2.p0.x * _scale.x, offsetY + num2 + tk2dFontChar2.p1.y * _scale.y, 0f);
			vertices[num3 * 4 + 3] = new Vector3(offsetX + num + tk2dFontChar2.p1.x * _scale.x, offsetY + num2 + tk2dFontChar2.p1.y * _scale.y, 0f);
			uvs[num3 * 4] = new Vector2(tk2dFontChar2.uv0.x, tk2dFontChar2.uv0.y);
			uvs[num3 * 4 + 1] = new Vector2(tk2dFontChar2.uv1.x, tk2dFontChar2.uv0.y);
			uvs[num3 * 4 + 2] = new Vector2(tk2dFontChar2.uv0.x, tk2dFontChar2.uv1.y);
			uvs[num3 * 4 + 3] = new Vector2(tk2dFontChar2.uv1.x, tk2dFontChar2.uv1.y);
			if (_font.textureGradients)
			{
				uv2[num3 * 4] = vector + tk2dFontChar2.gradientUv[0];
				uv2[num3 * 4 + 1] = vector + tk2dFontChar2.gradientUv[1];
				uv2[num3 * 4 + 2] = vector + tk2dFontChar2.gradientUv[2];
				uv2[num3 * 4 + 3] = vector + tk2dFontChar2.gradientUv[3];
			}
			num += tk2dFontChar2.advance * _scale.x;
			if (_kerning && i < _text.Length - 1)
			{
				tk2dFontKerning[] array = _font.kerning;
				foreach (tk2dFontKerning tk2dFontKerning2 in array)
				{
					if (tk2dFontKerning2.c0 == _text[i] && tk2dFontKerning2.c1 == _text[i + 1])
					{
						num += tk2dFontKerning2.amount * _scale.x;
						break;
					}
				}
			}
			num3++;
		}
		return num3;
	}

	public void Init()
	{
		if (!_font || (updateFlags & UpdateFlags.UpdateBuffers) == 0)
		{
			return;
		}
		MeshFilter component = GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		Color color = _color;
		Color color2 = ((!_useGradient) ? _color : _color2);
		vertices = new Vector3[_maxChars * 4];
		uvs = new Vector2[_maxChars * 4];
		colors = new Color[_maxChars * 4];
		if (_font.textureGradients)
		{
			uv2 = new Vector2[_maxChars * 4];
		}
		int[] array = new int[_maxChars * 6];
		int num = FillTextData();
		for (int i = 0; i < num; i++)
		{
			colors[i * 4] = (colors[i * 4 + 1] = color);
			colors[i * 4 + 2] = (colors[i * 4 + 3] = color2);
			array[i * 6] = i * 4;
			array[i * 6 + 1] = i * 4 + 1;
			array[i * 6 + 2] = i * 4 + 3;
			array[i * 6 + 3] = i * 4 + 2;
			array[i * 6 + 4] = i * 4;
			array[i * 6 + 5] = i * 4 + 3;
		}
		for (int j = num; j < _maxChars; j++)
		{
			vertices[j * 4] = (vertices[j * 4 + 1] = (vertices[j * 4 + 2] = (vertices[j * 4 + 3] = Vector3.zero)));
			uvs[j * 4] = (uvs[j * 4 + 1] = (uvs[j * 4 + 2] = (uvs[j * 4 + 3] = Vector2.zero)));
			if (_font.textureGradients)
			{
				uv2[j * 4] = (uv2[j * 4 + 1] = (uv2[j * 4 + 2] = (uv2[j * 4 + 3] = Vector2.zero)));
			}
			colors[j * 4] = (colors[j * 4 + 1] = color);
			colors[j * 4 + 2] = (colors[j * 4 + 3] = color2);
			array[j * 6] = j * 4;
			array[j * 6 + 1] = j * 4 + 1;
			array[j * 6 + 2] = j * 4 + 3;
			array[j * 6 + 3] = j * 4 + 2;
			array[j * 6 + 4] = j * 4;
			array[j * 6 + 5] = j * 4 + 3;
		}
		mesh.vertices = vertices;
		mesh.uv = uvs;
		if (font.textureGradients)
		{
			mesh.uv2 = uv2;
		}
		mesh.triangles = array;
		mesh.colors = colors;
		mesh.RecalculateBounds();
		component.mesh = mesh;
		this.mesh = component.sharedMesh;
		updateFlags = UpdateFlags.UpdateNone;
	}

	public void Commit()
	{
		if ((updateFlags & UpdateFlags.UpdateBuffers) != 0)
		{
			Init();
		}
		else
		{
			if ((updateFlags & UpdateFlags.UpdateText) != 0)
			{
				int num = FillTextData();
				for (int i = num; i < _maxChars; i++)
				{
					vertices[i * 4] = (vertices[i * 4 + 1] = (vertices[i * 4 + 2] = (vertices[i * 4 + 3] = Vector3.zero)));
				}
				mesh.vertices = vertices;
				mesh.uv = uvs;
				if (font.textureGradients)
				{
					mesh.uv2 = uv2;
				}
			}
			if ((updateFlags & UpdateFlags.UpdateColors) != 0)
			{
				Color color = _color;
				Color color2 = ((!_useGradient) ? _color : _color2);
				for (int j = 0; j < colors.Length; j += 4)
				{
					colors[j] = (colors[j + 1] = color);
					colors[j + 2] = (colors[j + 3] = color2);
				}
				mesh.colors = colors;
			}
		}
		updateFlags = UpdateFlags.UpdateNone;
	}

	private int CalcAnchor(bool useInlineStyling, out float offsetX, out float offsetY)
	{
		int num = 0;
		if (_font != null)
		{
			float a = 0f;
			float num2 = 0f;
			int num3 = 1;
			int num4 = 0;
			for (int i = 0; num4 < _maxChars && i < _text.Length; i++)
			{
				int num5 = _text[i];
				if (num5 >= _font.chars.Length)
				{
					num5 = 0;
				}
				tk2dFontChar tk2dFontChar2 = _font.chars[num5];
				if (num5 == 10)
				{
					num3++;
					a = Mathf.Max(a, num2);
					num2 = 0f;
					continue;
				}
				if (useInlineStyling && num5 == 94 && i + 1 < _text.Length)
				{
					i++;
					if (_text[i] != '^')
					{
						continue;
					}
				}
				num2 += tk2dFontChar2.advance * _scale.x;
				num++;
				num4++;
			}
			a = Mathf.Max(a, num2);
			float num6 = _font.lineHeight * _scale.y;
			float num7 = num6 * (float)num3;
			switch (_anchor)
			{
			case TextAnchor.LowerLeft:
				offsetX = 0f;
				offsetY = num7 - num6;
				break;
			case TextAnchor.MiddleLeft:
				offsetX = 0f;
				offsetY = num7 / 2f - num6;
				break;
			case TextAnchor.UpperLeft:
				offsetX = 0f;
				offsetY = 0f - num6;
				break;
			case TextAnchor.LowerCenter:
				offsetX = (0f - a) / 2f;
				offsetY = num7 - num6;
				break;
			case TextAnchor.MiddleCenter:
				offsetX = (0f - a) / 2f;
				offsetY = num7 / 2f - num6;
				break;
			case TextAnchor.UpperCenter:
				offsetX = (0f - a) / 2f;
				offsetY = 0f - num6;
				break;
			case TextAnchor.LowerRight:
				offsetX = 0f - a;
				offsetY = num7 - num6;
				break;
			case TextAnchor.MiddleRight:
				offsetX = 0f - a;
				offsetY = num7 / 2f - num6;
				break;
			case TextAnchor.UpperRight:
				offsetX = 0f - a;
				offsetY = 0f - num6;
				break;
			default:
				offsetX = 0f;
				offsetY = 0f;
				break;
			}
		}
		else
		{
			offsetX = 0f;
			offsetY = 0f;
		}
		return num;
	}

	public void MakePixelPerfect()
	{
		float num = 1f;
		tk2dPixelPerfectHelper inst = tk2dPixelPerfectHelper.inst;
		if ((bool)inst)
		{
			num = ((!inst.CameraIsOrtho) ? (inst.scaleK + inst.scaleD * base.transform.position.z) : inst.scaleK);
		}
		else if ((bool)Camera.main)
		{
			if (Camera.main.orthographic)
			{
				num = Camera.main.orthographicSize;
			}
			else
			{
				float zdist = base.transform.position.z - Camera.main.transform.position.z;
				num = tk2dPixelPerfectHelper.CalculateScaleForPerspectiveCamera(Camera.main.fieldOfView, zdist);
			}
		}
		scale = new Vector3(Mathf.Sign(scale.x) * num, Mathf.Sign(scale.y) * num, Mathf.Sign(scale.z) * num);
	}
}
