using UnityEngine;

internal class StarStrike_InstantiatedObjectsParent : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private Transform thisTransform;

	private void Start()
	{
		thisTransform = base.transform;
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.UNIT_CREATED:
		{
			StarStrike_ArmyUnit attachment6 = gameEvent.GetAttachment<StarStrike_ArmyUnit>(StarStrike_AttachmentKey.ARMY_UNIT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment6.transform);
			break;
		}
		case StarStrike_EventType.EMP_USED:
		{
			GameObject attachment5 = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.EMP_OBJECT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment5.transform);
			break;
		}
		case StarStrike_EventType.ROCKET_CANNON_USED:
		{
			GameObject attachment4 = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.ROCKET_CANNON_OBJECT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment4.transform);
			break;
		}
		case StarStrike_EventType.ROCKET_GLOVE_FIRED:
		{
			GameObject attachment3 = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.ROCKET_GLOVE_OBJECT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment3.transform);
			break;
		}
		case StarStrike_EventType.METEOR_SHOWER_USED:
		{
			GameObject attachment2 = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.METEOR_SHOWER_OBJECT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment2.transform);
			break;
		}
		case StarStrike_EventType.METEOR_CREATED:
		{
			GameObject attachment = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.METEOR_OBJECT);
			StarStrike_Utils.SetAsParent(thisTransform, attachment.transform);
			break;
		}
		case StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED:
		{
			foreach (Transform item in thisTransform)
			{
				Object.Destroy(item.gameObject);
			}
			break;
		}
		case StarStrike_EventType.TOM_BASE_DESTROYED:
		case StarStrike_EventType.JERRY_BASE_DESTROYED:
			SetUnitsToIdle();
			break;
		case StarStrike_EventType.UNIT_DESTROYED:
		case StarStrike_EventType.METEOR_HIT_GROUND:
		case StarStrike_EventType.METEOR_SHOWER_BATCH_DEPLOYED:
		case StarStrike_EventType.HIT_BY_HEAVY:
		case StarStrike_EventType.HIT_BY_MELEE:
		case StarStrike_EventType.LASER_BALL_FIRED:
		case StarStrike_EventType.ROCKET_GLOVE_HIT:
		case StarStrike_EventType.EMP_EXPLODED:
			break;
		}
	}

	private void SetUnitsToIdle()
	{
		StarStrike_ArmyUnit[] componentsInChildren = GetComponentsInChildren<StarStrike_ArmyUnit>();
		StarStrike_ArmyUnit[] array = componentsInChildren;
		foreach (StarStrike_ArmyUnit starStrike_ArmyUnit in array)
		{
			starStrike_ArmyUnit.PerformAction(new StarStrike_IdleAction(starStrike_ArmyUnit, "Idle"));
		}
	}
}
