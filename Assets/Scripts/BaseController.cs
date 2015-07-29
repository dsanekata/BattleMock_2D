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
	protected bool isDead = false;

	// Update is called once per frame
	void Update () 
	{
		UpdateAction();
	}

	protected virtual void Initialize()
	{
		this.characterMove = GetComponent<CharacterMove>();
		this.animationController = GetComponent<AnimationController>();
		animationController.SetAnimator(transform.FindChild("Model").GetComponent<Animator>());
		isInitialized = true;
		ChangeState(ActionState.Idle);
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

		if(CheckDistance(this.target.transform.position))
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
		Debug.Log("animstart");
	}


	protected virtual void AttackEnd()
	{
		canAttack = true;
		Debug.Log("animend");
	}

	#endregion

	#region about Dead

	protected virtual void Dead()
	{
		if(this.actionState != this.prevActionState)
		{
			this.DoPreviewActionState(this.prevActionState);
		}
	}

	protected virtual void DeadOnEnter()
	{
		this.prevActionState = this.actionState;
	}

	protected virtual void DeadOnUpdate()
	{
		
	}

	protected virtual void DeadOnExit()
	{
		
	}

	#endregion

	#region About find target

	protected virtual void FindTarget()
	{
		
	}

	protected bool CheckDistance(Vector2 targetPos)
	{
		return (this.characterParam.attackRange >= Vector2.Distance(this.transform.position,targetPos));
	}

	#endregion

}

public class CharacterParameter
{
	public int hp = 0;
	public int maxHp = 500;
	public int attack = 10;
	public int deffence = 5;
	public float speed = 1f;
	public float attackRange = 5f;
}
