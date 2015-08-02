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
	Skill,
	Drag,
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

	public const int MAX_DAMAGE = 9999;

	public const int MIN_DAMAGE = 1;

	public const float ATTACK_EFFECT_SPEED = 15f;

	public const int SKILL_POINT = 200;

	public const float SKILL_CUTIN_DURATION = 1.5f;

	public const float DRAG_TIME_LIMIT = 5f;
}

public class CharacterAnimationState
{
	public const string IDLE = "Idle";

	public const string RUN = "Run";

	public const string ATTACK = "Attack";

	public const string DEAD = "Dead";
}

public class CommonAnimationState
{
	public const string DAMAGE_TEXT_LEFT = "PopLeft";
	public const string DAMAGE_TEXT_RIGHT = "PopRight";

	public const string EFFECT_ATTACK_MOVE = "Moving";

	public const string EFFECT_ATTACK_HIT = "Hit";
}

public enum AttackType
{
	SHORT_DISTANCE,
	MIDDLE_DISTANCE,
	LONG_DISTANCE,
}


