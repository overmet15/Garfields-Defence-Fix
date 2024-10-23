using UnityEngine;

internal class StarStrike_Constants
{
	public static Vector3 HUD_SCALE = new Vector3((float)Screen.width / 800f, (float)Screen.height / 600f, 1f);

	public static Matrix4x4 GUI_MATRIX = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, HUD_SCALE);
}
