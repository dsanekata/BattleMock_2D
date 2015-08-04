using System;

public enum BattleStatus 
{

}

public enum BattleState
{
	None,
	Moving,
	InBattle,
	Finish,
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
	public const float POSY_MAX = 0.5f;
	/// <summary>
	/// キャラクターのY座標下限
	/// </summary>
	public const float POSY_MIN = -2f;

	public const float CHARACTER_SCALE = 1.5f;

	public const float BACKGRAOUND_WIDTH = 40.9f;

	public const float CAMERA_FOLLOW_SMOOTHING_DEFAULT = 1f;

	public const float CAMERA_FOLLWO_SMOOTHING_LOW = 0.02f;

	public const int MAX_DAMAGE = 9999;

	public const int MIN_DAMAGE = 1;

	public const float ATTACK_EFFECT_SPEED = 15f;

	public const int SKILL_POINT = 200;

	public const float SKILL_CUTIN_DURATION = 1.5f;

	public const float DRAG_TIME_LIMIT = 3f;

	public const float CRITICAL_DAMAGEUP_FACTOR = 1.3f;

	public const int CRITICAL_MAX = 100;

	public const float CRITICAL_PARAM_DIVISION = 10f;

	public const int DRAG_LIMIT = 10;
}

public class CharacterAnimationState
{
	public const string IDLE = "Idle";

	public const string RUN = "Run";

	public const string ATTACK = "Attack";

	public const string DEAD = "Dead";

	public const string DRAGGING = "Dragging";

	public const string SKILL = "Skill";
}

public class CommonAnimationState
{
	public const string DAMAGE_TEXT_LEFT = "PopLeft";
	public const string DAMAGE_TEXT_RIGHT = "PopRight";
	public const string DAMAGE_TEXT_LEFT_CRITICAL = "PopLeftCritical";
	public const string DAMAGE_TEXT_RIGHT_CRITICAL = "PopRightCritical";
	public const string EFFECT_ATTACK_MOVE = "Moving";
	public const string EFFECT_ATTACK_HIT = "Hit";
}

public class EffectNameConst
{
	public const string SKILL_LIGHTNING = "ef_Lightning";
	public const string SKILL_EXPLOSION = "ef_Explosion";
	public const string SKILL_WIND = "ef_Wind";
	public const string SKILL_LASER = "ef_Laser";
	public const string SKILL_BOMB = "ef_Bomb";
}

public enum AttackType
{
	SHORT_DISTANCE,
	MIDDLE_DISTANCE,
	LONG_DISTANCE,
}

public enum SkillType
{
	Lightning,
	Explosion,
	Wind,
	Laser,
	Bomb,
}


