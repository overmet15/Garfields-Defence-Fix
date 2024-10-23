using System.Collections;
using UnityEngine;

internal class StarStrike_LevelDefinition
{
	private IList waveList;

	private IList enabledItemList;

	private float waveTimeInterval;

	private int waveCursor;

	public StarStrike_LevelDefinition(float waveTimeInterval)
	{
		this.waveTimeInterval = waveTimeInterval;
		waveList = new ArrayList();
		enabledItemList = new ArrayList();
		ResetIterator();
	}

	public void AddWave(StarStrike_WaveDefinition wave)
	{
		waveList.Add(wave);
	}

	public bool HasNextWave()
	{
		return waveCursor < waveList.Count;
	}

	public StarStrike_WaveDefinition MoveToNextWave()
	{
		StarStrike_WaveDefinition result = (StarStrike_WaveDefinition)waveList[waveCursor];
		waveCursor++;
		return result;
	}

	public void ResetIterator()
	{
		waveCursor = 0;
	}

	public StarStrike_WaveDefinition GetRandomWave()
	{
		StarStrike_Utils.SeedRandomizer();
		int index = (int)(Random.value * (float)waveList.Count);
		return (StarStrike_WaveDefinition)waveList[index];
	}

	public float GetWaveTimeInterval()
	{
		return waveTimeInterval;
	}

	public void AddEnabledItem(string itemName)
	{
		enabledItemList.Add(itemName);
	}

	public bool HasEnabledItem(string itemName)
	{
		return enabledItemList.Contains(itemName);
	}
}
