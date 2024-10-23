using System;

[Serializable]
public class tk2dSpriteAnimationClip
{
	public enum WrapMode
	{
		Loop = 0,
		LoopSection = 1,
		Once = 2,
		PingPong = 3,
		Single = 4
	}

	public string name = "Default";

	public tk2dSpriteAnimationFrame[] frames;

	public float fps = 30f;

	public int loopStart;

	public WrapMode wrapMode;
}
