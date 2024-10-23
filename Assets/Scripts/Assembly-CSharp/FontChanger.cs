using Outblaze;
using UnityEngine;

public class FontChanger : MonoBehaviour
{
	[SerializeField]
	private SpriteText _spriteText;

	private void Awake()
	{
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(_spriteText);
	}
}
