using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour 
{
	[SerializeField]
	protected BaseController target = null;

	protected CharacterMove characterMove = null;
	protected AnimationController animationController;

	protected CharacterParameter characterParam = new CharacterParameter();

	private bool isInitialized = false;

	[SerializeField]
	protected ActionState actionState = ActionState.None;
	[SerializeField]
	protected ActionState prevActionState = ActionState.None;
	[SerializeField]
	protected bool canAttack = false;
	[SerializeField]
	protected float attackDamage = 0;
	[SerializeField]
	public bool isDead = false;

	void Update () 
	{
		UpdateAction();
	}

	public virtual void Initialize(CharacterParameterModel model)
	{
		this.characterMove = GetComponent<CharacterMove>();
		this.animationController = GetComponent<AnimationController>();
		animationController.SetAnimator(transform.FindChild("Model").GetComponent<Animator>());
		InitCharacterParam (model);
		isInitialized = true;
		ChangeState(ActionState.Idle);
	}

	protected virtual void InitCharacterParam(CharacterParameterModel model)
	{
		this.characterParam.maxHp = model.maxHp;
		this.characterParam.attack = model.attack;
		this.characterParam.defence = model.defence;
		this.characterParam.speed = model.speed;
		this.characterParam.attackRange = model.attackRange;
		this.characterParam.attackInterval = model.attackInterval;
		this.characterParam.hp = this.characterParam.maxHp;
	}

	public void ChangeState(ActionState state)
	{
		this.actionState = state;
	}

	protected virtual void UpdateAction()
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

		if(target != null)
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

		animationController.Attack(AttackStart,AttackEnd);
	}

	protected virtual void AttackOnExit()
	{
		
	}

	protected virtual void AttackStart()
	{
		canAttack = false;
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
		if(this.target == null || this.target.isDead || isDead)
		{
			return;
		}

		this.target.Damaged (this.characterParam.attack);
	}

	protected virtual void Damaged(int damage)
	{
		this.characterParam.hp = Mathf.Clamp (this.characterParam.hp - damage, 0, this.characterParam.maxHp);

		if(this.characterParam.hp == 0)
		{
			ChangeState (ActionState.Dead);
		}
		Debug.Log ("Character" + GetHashCode () + ":" + this.characterParam.hp);
	}

	#endregion
}

public class CharacterParameter
{
	public int hp ;
	public int maxHp;
	public int attack;
	public int defence;
	public float speed;
	public float attackRange;
	public float attackInterval;
}
