using Outblaze;
using UnityEngine;

public class FontManager : SingletonMonoBehaviour<FontManager>
{
	public TextAsset KO_BigFont;

	public Material KO_BigFontMat;

	public TextAsset KO_SmallFont;

	public Material KO_SmallFontMat;

	public TextAsset JA_BigFont;

	public Material JA_BigFontMat;

	public TextAsset JA_SmallFont;

	public Material JA_SmallFontMat;

	public TextAsset FR_BigFont;

	public Material FR_BigFontMat;

	public TextAsset FR_SmallFont;

	public Material FR_SmallFontMat;

	public TextAsset DE_BigFont;

	public Material DE_BigFontMat;

	public TextAsset DE_SmallFont;

	public Material DE_SmallFontMat;

	public TextAsset SChi_BigFont;

	public Material SChi_BigFontMat;

	public TextAsset SChi_SmallFont;

	public Material SChi_SmallFontMat;

	public TextAsset TChi_BigFont;

	public Material TChi_BigFontMat;

	public TextAsset TChi_SmallFont;

	public Material TChi_SmallFontMat;

	public void SetBigFontMat(SpriteText text)
	{
		switch (SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance.getLangCode())
		{
		case "ko":
			text.SetFont(KO_BigFont, KO_BigFontMat);
			break;
		case "ja":
			text.SetFont(JA_BigFont, JA_BigFontMat);
			break;
		case "fr":
			text.SetFont(FR_BigFont, FR_BigFontMat);
			break;
		case "de":
			text.SetFont(DE_BigFont, DE_BigFontMat);
			break;
		case "zh-Hans":
			text.SetFont(SChi_BigFont, SChi_BigFontMat);
			break;
		case "zh-Hant":
			text.SetFont(TChi_BigFont, TChi_BigFontMat);
			break;
		case "en":
			break;
		default:
			Debug.LogError("Unsupported Language detected.");
			break;
		}
	}

	public void SetSmallFontMat(SpriteText text)
	{
		switch (SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance.getLangCode())
		{
		case "ko":
			text.SetFont(KO_SmallFont, KO_SmallFontMat);
			break;
		case "ja":
			text.SetFont(JA_SmallFont, JA_SmallFontMat);
			break;
		case "fr":
			text.SetFont(FR_SmallFont, FR_SmallFontMat);
			break;
		case "de":
			text.SetFont(DE_SmallFont, DE_SmallFontMat);
			break;
		case "zh-Hans":
			text.SetFont(SChi_SmallFont, SChi_SmallFontMat);
			break;
		case "zh-Hant":
			text.SetFont(TChi_SmallFont, TChi_SmallFontMat);
			break;
		case "en":
			break;
		default:
			Debug.LogError("Unsupported Language detected.");
			break;
		}
	}

	public void SetFontColor(SpriteText text, Color nonEnglishColor, Color englishColor)
	{
		switch (SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance.getLangCode())
		{
		case "ko":
		case "ja":
		case "fr":
		case "de":
		case "zh-Hans":
		case "zh-Hant":
			text.SetColor(nonEnglishColor);
			break;
		case "en":
			text.SetColor(englishColor);
			break;
		default:
			Debug.LogError("Unsupported Language detected.");
			break;
		}
	}
}
