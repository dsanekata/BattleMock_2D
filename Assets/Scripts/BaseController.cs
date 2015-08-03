using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour 
{
	[SerializeField]
	protected BaseController target = null;

	protected CharacterMove characterMove = null;
	protected AnimationController animationController;
	protected CharacterHUD hud = null;
	protected SkillController  skillController = null;

	protected CharacterParameter characterParam = new CharacterParameter();

	protected bool isInitialized = false;

	[SerializeField]
	protected ActionState actionState = ActionState.None;
	[SerializeField]
	protected ActionState prevActionState = ActionState.None;
	[SerializeField]
	protected bool canAttack = false;
	[SerializeField]
	protected bool canMove = false;
	[SerializeField]
	protected float attackDamage = 0;
	[SerializeField]
	public bool isDead = false;

	public bool canDrag = false;

	protected int skillDamage;

	public virtual void Initialize(CharacterParameterModel model)
	{
		this.characterMove = GetComponent<CharacterMove>();
		this.animationController = new AnimationController();
		animationController.SetAnimator(transform.FindChild("Model").GetComponent<Animator>());
		hud = transform.FindChild("HUD").GetComponent<CharacterHUD>();
		hud.InitHUD();
		InitCharacterParam (model);
		canMove = true;
		isInitialized = true;
		canDrag = true;
		ChangeState(ActionState.Idle);
	}

	protected virtual void InitCharacterParam(CharacterParameterModel model)
	{
		this.characterParam.maxHp = model.maxHp;
		this.characterParam.attack = model.attack;
		this.characterParam.defence = model.defence;
		this.characterParam.speed = model.speed;
		this.characterParam.critical = model.critical;
		this.characterParam.maxSp = model.maxSp;
		this.characterParam.attackRange = model.attackRange;
		this.characterParam.attackInterval = model.attackInterval;
		this.characterParam.hp = this.characterParam.maxHp;
		skillDamage = characterParam.attack * 5;
	}

	public void ChangeState(ActionState state)
	{
		this.actionState = state;
	}

	public virtual void UpdateAction()
	{
		if(!isInitialized || isDead)
		{
			return;
		}
			
		switch(this.actionState)
		{
			case ActionState.Idle:
				Idle();
				break;
			case ActionState.Move:
				Move();
				break;
			case ActionState.Attack:
				Attack();
				break;
			case ActionState.Skill:
				Skill ();
				break;
			case ActionState.Drag:
				Drag();
				break;
			case ActionState.Dead:
				Dead();
				break;
		}
	}

	protected void DoPreviewActionState(ActionState _actionState)
	{
		switch (this.prevActionState)
		{
		case ActionState.Idle:
			this.IdleOnExit ();
			break;
		case ActionState.Move:
			this.MoveOnExit ();
			break;
		case ActionState.Attack:
			this.AttackOnExit ();
			break;
		case ActionState.Skill:
			SkillOnExit ();
			break;
		case ActionState.Drag:
			DragOnExit ();
			break;
		case ActionState.Dead:
			break;
		}
	}

	#region about Idle

	protected virtual void Idle()
	{
		if(this.actionState != this.prevActionState)
		{
			this.DoPreviewActionState(this.prevActionState);
			this.IdleOnEnter();
		}

		this.IdleOnUpdate();
	}

	protected virtual void IdleOnEnter()
	{
		this.prevActionState = this.actionState;
		this.animationController.Idle();
	}

	protected virtual void IdleOnUpdate()
	{
		FindTarget();

		if(target != null && canMove)
		{
			ChangeState(ActionState.Move);
		}
	}

	protected virtual void IdleOnExit()
	{
		
	}

	#endregion

	#region about Move

	protected virtual void Move()
	{
		if(this.actionState != this.prevActionState)
		{
			this.DoPreviewActionState(this.prevActionState);
			this.MoveOnEnter();
		}

		this.MoveOnUpdate();
	}

	protected virtual void MoveOnEnter()
	{
		this.prevActionState = this.actionState;
		canAttack = true;
		canDrag = true;
	}

	protected virtual void MoveOnUpdate()
	{
		if(target == null)
		{
			ChangeState(ActionState.Idle);
			return;
		}

		this.animationController.Run();
		characterMove.MoveToTarget(this.target.transform,this.characterParam.speed);

		if(CheckAttackDistance(this.target.transform.position))
		{
			canAttack = true;
			ChangeState(ActionState.Attack);
		}
	}

	protected virtual void MoveOnExit()
	{
		
	}

	#endregion

	#region about Attack

	protected virtual void Attack()
	{
		if(this.actionState != this.prevActionState)
		{
			this.DoPreviewActionState(this.prevActionState);
			this.AttackOnEnter();
		}

		AttackOnUpdate();
	}

	protected virtual void AttackOnEnter()
	{
		this.prevActionState = this.actionState;
	}

	protected virtual void AttackOnUpdate()
	{
		if(target.isDead)
		{
			target = null;
			ChangeState(ActionState.Idle);
			return;
		}
			
		if(!canAttack)
		{
			return;
		}
			
		if(!CheckAttackDistance (target.transform.position))
		{
			ChangeState (ActionState.Idle);
			return;
		}

		characterMove.LookAtTarget(this.target.transform);
		animationController.Attack(AttackStart,AttackEnd);
	}

	protected virtual void AttackOnExit()
	{
		
	}

	protected virtual void AttackStart()
	{
		canAttack = false;
		canMove = false;
	}


	protected virtual void AttackEnd()
	{
		Invoke ("NextAttack", this.characterParam.attackInterval);

		if(this.target != null && !this.target.isDead)
		{
			AddDamageToTarget ();
		}
	}

	private void NextAttack()
	{
		canAttack = true;
		canMove = true;
	}

	#endregion

	#region about Dead

	protected virtual void Dead()
	{
		if(this.actionState != this.prevActionState)
		{
			this.DoPreviewActionState(this.prevActionState);
			this.DeadOnEnter ();
		}
		DeadOnUpdate();
	}

	protected virtual void DeadOnEnter()
	{
		this.prevActionState = this.actionState;
		this.animationController.Death (DeadStart,DeadEnd);
	}

	protected virtual void DeadOnUpdate()
	{
	}

	protected virtual void DeadOnExit()
	{		
	}

	protected virtual void DeadStart()
	{
		isDead = true;
	}

	protected virtual void DeadEnd ()
	{
		Destroy (this.gameObject);
	}

	#endregion

	#region about skill
	protected virtual void Skill()
	{
		if(actionState != prevActionState)
		{
			DoPreviewActionState (prevActionState);
			SkillOnEnter ();
		}

		SkillOnUpdate ();
	}

	protected virtual void SkillOnEnter()
	{
		prevActionState = actionState;
	}

	protected virtual void SkillOnExit()
	{
		skillController.ForcedFinishSkill();
	}

	protected virtual void SkillOnUpdate()
	{
		if(skillController == null || isDead)
		{
			return;
		}

		skillController.SkillUpdate();
	}

	protected virtual void SkillStart()
	{
		
	}
		
	protected virtual void SkillFinish()
	{
		
	}

	protected virtual bool CanSkillInvoke()
	{
		return (characterParam.sp >= skillController.needSkillPoint);
	}
	#endregion

	#region about drag
	protected virtual void Drag()
	{
		if(actionState != prevActionState)
		{
			DoPreviewActionState (prevActionState);
			DragOnEnter ();
		}

		DragOnUpdate ();
	}

	protected virtual void DragOnEnter()
	{
		prevActionState = actionState;
		canAttack = false;
		canMove = false;
		BattleManager.GetInstance().SetDraggingArmy(true);
	}

	protected virtual void DragOnUpdate()
	{
		animationController.Drag();
	}

	protected virtual void DragOnExit()
	{
		canAttack = true;
		canMove = true;
		BattleManager.GetInstance().SetDraggingArmy(false);
	}

	public void DragArmy(Vector3 pos)
	{
		this.transform.position = new Vector3(pos.x,Mathf.Clamp(pos.y,BattleConst.POSY_MIN,BattleConst.POSY_MAX), Mathf.Clamp(pos.y,BattleConst.POSY_MIN,BattleConst.POSY_MAX));
		ChangeState (ActionState.Drag);
	}

	public void DragEnd()
	{
		ChangeState (ActionState.Idle);
	}
		
	#endregion

	#region About find target

	protected virtual void FindTarget()
	{
		
	}

	protected bool CheckAttackDistance(Vector2 targetPos)
	{
		return (this.characterParam.attackRange >= Vector2.Distance(this.transform.position,targetPos));
	}

	#endregion

	#region About damage

	protected virtual void AddDamageToTarget()
	{
		if(this.target == null ||
			this.target.isDead || 
			isDead || 
			!CheckAttackDistance(target.transform.position)){
			return;
		}

		float random = Random.Range(0,BattleConst.CRITICAL_MAX);
		bool isCritical = ((float)random <= characterParam.critical / BattleConst.CRITICAL_PARAM_DIVISION);

		float damage = this.characterParam.attack;

		if(isCritical)
		{
			damage *= BattleConst.CRITICAL_DAMAGEUP_FACTOR;
		}

		ModifySp(this.target.Damaged (Mathf.RoundToInt(damage),isCritical));
	}

	public virtual int Damaged(int damage,bool isCritical=false)
	{
		if(IsInvincible())
		{
			return 0;
		}

		int receivedDamage = CalcDamage(damage);

		hud.PopDamageText(receivedDamage,isCritical);

		ModifyHp(-receivedDamage);

		if(this.characterParam.hp == 0)
		{
			ChangeState (ActionState.Dead);
		}

		return receivedDamage;
	}

	protected virtual int CalcDamage(float damage)
	{
		int calcDamage = Mathf.RoundToInt(damage - this.characterParam.defence);

		return Mathf.Clamp(calcDamage,BattleConst.MIN_DAMAGE,BattleConst.MAX_DAMAGE);
	}

	public virtual bool IsInvincible()
	{
		return (actionState == ActionState.Skill || actionState == ActionState.Drag);
	}

	protected virtual void ModifyHp(int amount)
	{
		this.characterParam.hp += amount;
		this.characterParam.hp = Mathf.Clamp(this.characterParam.hp,0,characterParam.maxHp);
	}

	protected virtual void ModifySp(int amount)
	{
		this.characterParam.sp += amount;
		this.characterParam.sp = Mathf.Clamp(this.characterParam.sp,0,characterParam.maxSp);
	}

	#endregion

	public ActionState GetCurrentState()
	{
		return actionState;
	}

	public virtual void PlayController()
	{
		this.enabled = true;
		animationController.EnaleAnimator();
	}

	public virtual void PauseController()
	{
		this.enabled = false;
		animationController.DisableAnimator();
	}

	public bool IsInsideCamera()
	{
		bool inSide = false;

		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

		if(viewportPos.x <= 1 || viewportPos.x >= 0)
		{
			inSide = true;
		}

		return inSide;
	}

	void OnEnable()
	{
		if(isInitialized)
		{
			animationController.SetAnimator(transform.FindChild("Model").GetComponent<Animator>());
		}
	}
}

public class CharacterParameter
{
	public int hp ;
	public int maxHp;
	public int attack;
	public int defence;
	public float speed;
	public float critical;
	public int maxSp;
	public float attackRange;
	public float attackInterval;
	public int sp;
}
