using System;

public enum BattleStatus 
{

}

public enum ActionState
{
	None,
	Idle,
	Move,
	Attack,
	Dead,
}

public class BattleConst
{
	/// <summary>
	/// キャラクターのY座標制限
	/// </summary>
	public const float POSY_MAX = 0f;
	/// <summary>
	/// キャラクターのY座標下限
	/// </summary>
	public const float POSY_MIN = -2.5f;

	public const float CHARACTER_SCALE = 1.5f;

	public const float BACKGRAOUND_WIDTH = 40.9f;

	public const float CAMERA_FOLLOW_SMOOTHING = 1f;
}

public class CharacterAnimationState
{
	public const string IDLE = "Idle";

	public const string RUN = "Run";

	public const string ATTACK = "Attack";

	public const string DEAD = "Dead";
}

