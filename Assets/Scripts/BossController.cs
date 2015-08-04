using UnityEngine;
using System.Collections;

public class BossController : EnemyBaseController
{
	EffectSlash slash = null;
	SkillRangeSector rangeSector = null;

	public override void Initialize (CharacterParameterModel model)
	{
		base.Initialize (model);
		isBoss = true;
		skillController = GetComponent<SkillController>();
		slash = transform.FindChild ("Model/EffectAttack").GetComponent<EffectSlash> ();
		rangeSector = transform.FindChild("Model/SkillRange").GetComponent<SkillRangeSector>();
		skillController.Init();
		slash.Init ();
		rangeSector.InitSector();
	}

	protected override void AttackStart ()
	{
		canAttack = false;
		canMove = false;

		if (target != null && !target.isDead) 
		{
			slash.Shot (this.target.transform, AddDamageToTarget);
			SoundManager.GetInstance().PlaySE(SoundConst.SE_LASER);
		}
	}

	protected override void AttackEnd ()
	{
		Invoke ("NextAttack", this.characterParam.attackInterval);
	}

	protected override void AttackOnUpdate ()
	{
		if(target.isDead)
		{
			target = null;
			ChangeState(ActionState.Idle);
			return;
		}

		slash.UpdateEffect();

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

		if(skillController != null && CanSkillInvoke())
		{
			InvokeSkill();
		}
		else
		{
			animationController.Attack(AttackStart,AttackEnd);
		}
	}

	protected override void SkillOnEnter ()
	{
		base.SkillOnEnter ();
		rangeSector.gameObject.SetActive(true);
		animationController.Skill(null,SkillStart);
	}

	protected override void SkillOnUpdate ()
	{
		base.SkillOnUpdate ();
		rangeSector.UpdateSector();

		if(!skillController.IsAliveEffect())
		{
			canAttack = true;
			canMove = true;
			ChangeState(ActionState.Idle);
		}
	}

	protected override void SkillStart ()
	{
		ModifySp(-skillController.needSkillPoint);
		rangeSector.gameObject.SetActive(false);
		skillController.InvokeSkill(SkillFinish,true);
	}

	protected override void SkillFinish ()
	{
		AddDamageToInsideRangeSector();

	}

	protected void InvokeSkill()
	{
		if(skillController.invokedSkill)
		{
			return;
		}

		ChangeState(ActionState.Skill);
	}

	protected void AddDamageToInsideRangeSector()
	{
		for(int i = 0; i < rangeSector.GetTargetList().Count; i++)
		{
			if(!rangeSector.GetTargetList()[i].isDead)
			{
				rangeSector.GetTargetList()[i].Damaged(skillDamage,true);
			}
		}

		rangeSector.ClearTargets();
	}
}
