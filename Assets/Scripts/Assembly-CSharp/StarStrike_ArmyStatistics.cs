internal class StarStrike_ArmyStatistics : StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private int jerryArmyCount;

	private int tomArmyCount;

	private int[] destroyedUnits;

	public StarStrike_ArmyStatistics()
	{
		jerryArmyCount = 0;
		tomArmyCount = 0;
		destroyedUnits = new int[6];
		for (int i = 0; i < destroyedUnits.Length; i++)
		{
			destroyedUnits[i] = 0;
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_ArmyUnit starStrike_ArmyUnit = null;
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.UNIT_CREATED:
			starStrike_ArmyUnit = gameEvent.GetAttachment<StarStrike_ArmyUnit>(StarStrike_AttachmentKey.ARMY_UNIT);
			Increment(starStrike_ArmyUnit.GetOwner());
			break;
		case StarStrike_EventType.UNIT_DESTROYED:
			starStrike_ArmyUnit = gameEvent.GetAttachment<StarStrike_ArmyUnit>(StarStrike_AttachmentKey.ARMY_UNIT);
			Decrement(starStrike_ArmyUnit.GetOwner());
			UpdateDestroyedUnits(starStrike_ArmyUnit);
			break;
		case StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED:
		{
			jerryArmyCount = 0;
			tomArmyCount = 0;
			for (int i = 0; i < destroyedUnits.Length; i++)
			{
				destroyedUnits[i] = 0;
			}
			break;
		}
		}
	}

	private void Increment(Owner owner)
	{
		switch (owner)
		{
		case Owner.JERRY:
			jerryArmyCount++;
			break;
		case Owner.TOM:
			tomArmyCount++;
			break;
		}
	}

	private void Decrement(Owner owner)
	{
		switch (owner)
		{
		case Owner.JERRY:
			jerryArmyCount--;
			break;
		case Owner.TOM:
			tomArmyCount--;
			break;
		}
	}

	private void UpdateDestroyedUnits(StarStrike_ArmyUnit unit)
	{
		if (unit.GetOwner() != 0)
		{
			destroyedUnits[ResolveUnitTypeIndex(unit.GetUnitType())]++;
		}
	}

	public int GetDestroyedUnitsCount(UnitType unitType)
	{
		return destroyedUnits[ResolveUnitTypeIndex(unitType)];
	}

	private static int ResolveUnitTypeIndex(UnitType unitType)
	{
		switch (unitType)
		{
		case UnitType.MELEE:
			return 0;
		case UnitType.RANGED:
			return 1;
		case UnitType.HEAVY:
			return 2;
		case UnitType.HEAL:
			return 3;
		case UnitType.HERO:
			return 4;
		case UnitType.DEFENSE:
			return 5;
		default:
			StarStrike_Assertion.Assert(false, "The specified unit type was not resolved.");
			return -1;
		}
	}

	public int GetArmyCount(Owner owner)
	{
		switch (owner)
		{
		case Owner.JERRY:
			return jerryArmyCount;
		case Owner.TOM:
			return tomArmyCount;
		default:
			return 0;
		}
	}
}
