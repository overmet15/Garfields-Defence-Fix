using UnityEngine;

public class UnitIDManager : MonoBehaviour
{
	private int UnitID;

	public int GetUnitID()
	{
		UnitID++;
		return UnitID;
	}
}
