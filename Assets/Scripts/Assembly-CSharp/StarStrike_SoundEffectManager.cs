using System.Collections;
using UnityEngine;

public class StarStrike_SoundEffectManager : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	public AudioClip baseExplosion;

	public AudioClip boxingGloveHit;

	public AudioClip shockwave;

	public AudioClip hitByHeavy;

	public AudioClip hitByMelee;

	public AudioClip meteor;

	public AudioClip laserFire;

	public AudioClip buttonClick;

	private Hashtable soundEffectMap;

	private bool addedAsListener;

	private void Start()
	{
		soundEffectMap = new Hashtable();
		soundEffectMap.Add(StarStrike_EventType.JERRY_BASE_DESTROYED, baseExplosion);
		soundEffectMap.Add(StarStrike_EventType.TOM_BASE_DESTROYED, baseExplosion);
		soundEffectMap.Add(StarStrike_EventType.ROCKET_GLOVE_HIT, boxingGloveHit);
		soundEffectMap.Add(StarStrike_EventType.EMP_EXPLODED, shockwave);
		soundEffectMap.Add(StarStrike_EventType.HIT_BY_HEAVY, hitByHeavy);
		soundEffectMap.Add(StarStrike_EventType.HIT_BY_MELEE, hitByMelee);
		soundEffectMap.Add(StarStrike_EventType.METEOR_SHOWER_BATCH_DEPLOYED, meteor);
		soundEffectMap.Add(StarStrike_EventType.LASER_BALL_FIRED, laserFire);
		soundEffectMap.Add(StarStrike_EventType.BUTTON_CLICKED, buttonClick);
		addedAsListener = false;
	}

	private void OnLevelWasLoaded(int level)
	{
		addedAsListener = false;
	}

	private void Update()
	{
		if (!addedAsListener)
		{
			if (!StarStrike_EventManagerInstance.GetInstance().ContainsListener(this))
			{
				StarStrike_EventManagerInstance.GetInstance().AddListener(this);
			}
			addedAsListener = true;
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		if (soundEffectMap.ContainsKey(gameEvent.GetEventType()))
		{
			AudioClip ac = (AudioClip)soundEffectMap[gameEvent.GetEventType()];
			if (gameEvent.ContainsAttachment(StarStrike_AttachmentKey.SOUND_SOURCE))
			{
				GameObject attachment = gameEvent.GetAttachment<GameObject>(StarStrike_AttachmentKey.SOUND_SOURCE);
				MinigameAudioManager.PlayClipAtPoint(ac, attachment.transform.position);
			}
			else
			{
				MinigameAudioManager.PlayClipAtPoint(ac, base.transform.position);
			}
		}
	}
}
