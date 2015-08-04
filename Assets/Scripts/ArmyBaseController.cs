using UnityEngine;
using System.Collections;
using System.Linq;

public class ArmyBaseController : BaseController 
{
	CharacterHUDHandle hudHandle = null;

	public override void Initialize (CharacterParameterModel model)
	{
		base.Initialize (model);
		
		skillController = GetComponent<SkillController> ();
		if (skillController != null) 
		{
			skillController.Init ();
		}

	}

	public override void UpdateAction ()
	{

		if(!isInitialized || isDead || !gameObject.activeSelf)
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

		hudHandle.UpdateUI();
		hudHandle.SetCanSkill(CanSkillInvoke());
	}

	public void SetHUDHandle(CharacterHUDHandle handle)
	{
		this.hudHandle = handle;
		hudHandle.Init(this.characterParam.maxHp,this.characterParam.maxSp);
		hudHandle.RegisterIconClickEvent(()=>{
			if(CanSkillInvoke())
			{
				InvokeSkill();
			}	
		});
	}
		
	protected override void AttackOnUpdate ()
	{
		if(!canAttack && BattleManager.GetInstance().BattleState == BattleState.InBattle)
		{
			return;
		}

		if(target.isDead || BattleManager.GetInstance().BattleState != BattleState.InBattle)
		{
			target = null;
			ChangeState(ActionState.Idle);
			return;
		}
			
		if(!CheckAttackDistance (target.transform.position))
		{
			ChangeState (ActionState.Move);
			return;
		}

		characterMove.LookAtTarget(this.target.transform);

		if(skillController != null && BattleUIManager.GetInstance().autoInvokeSkill && CanSkillInvoke())
		{
			InvokeSkill();
		}
		else
		{
			animationController.Attack(AttackStart,AttackEnd);
		}

	}

	protected override void AttackStart ()
	{
		base.AttackStart ();
		SoundManager.GetInstance().PlaySE(SoundConst.SE_MACHINEGUN);
	}


	protected override void SkillStart ()
	{
		BattleManager.GetInstance ().PlayBattle();
		SoundManager.GetInstance().PlaySE(skillController.GetSkillSeName());
		skillController.InvokeSkill (SkillFinish);
		ModifySp(-skillController.needSkillPoint);
		canDrag = false;
	}

	protected override void SkillFinish ()
	{
		AddDamageToAllEnemies ();
		canAttack = true;
		canMove = true;
		canDrag = true;
		ChangeState (ActionState.Idle);
	}

	protected override void ModifyHp (int amount)
	{
		base.ModifyHp (amount);
		hudHandle.ChangeHpValue(this.characterParam.hp);
	}

	protected override void ModifySp (int amount)
	{
		base.ModifySp (amount);
		hudHandle.ChangeSpValue(this.characterParam.sp);
	}

	protected override void AddDamageToTarget ()
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
		
	protected override void FindTarget ()
	{
		float distance = 0f;

		for (int i=0; i< BattleManager.GetInstance().enemiesList.Count; i++)
		{
			if (BattleManager.GetInstance ().enemiesList [i] == null ||
			    BattleManager.GetInstance ().enemiesList [i].gameObject == null ||
			    BattleManager.GetInstance ().enemiesList [i].isDead) {
				continue;
			}

			float tempDistance = Vector3.Distance(BattleManager.GetInstance().enemiesList[i].transform.position, transform.position);
			if (distance == 0 || tempDistance < distance)
			{
				target = BattleManager.GetInstance().enemiesList[i];
				distance = tempDistance;
			}
		}
	}

	protected override void DeadOnEnter ()
	{
		base.DeadOnEnter ();
		hudHandle.SetMask(true);
	}

	protected void AddDamageToAllEnemies()
	{
		EnemyBaseController[] targetList = BattleManager.GetInstance ().enemiesList.Where (x => !x.isDead 
			&& x.gameObject.activeInHierarchy && x.IsInsideCamera()).ToArray();

		if(targetList == null)
		{
			return;
		}

		for(int i = 0; i < targetList.Length; i++)
		{
			targetList [i].Damaged (skillDamage,true);
		}
			
	}

	protected void InvokeSkill()
	{
		if(BattleManager.GetInstance().BattleState != BattleState.InBattle || 
			skillController.invokedSkill)
		{
			return;
		}

		SoundManager.GetInstance().PlaySE(SoundConst.SE_SKILL_INVOKE);
		BattleManager.GetInstance ().StopBattle();
		BattleUIManager.GetInstance ().StartSkillCutIn (1, SkillStart);
		ChangeState(ActionState.Skill);
	}

	public void Show()
	{
		this.gameObject.SetActive(true);
		this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.4f,0.4f,0f));
		animationController.SetAnimator(transform.FindChild("Model").GetComponent<Animator>());
	}



}
