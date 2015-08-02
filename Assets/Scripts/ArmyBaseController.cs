using UnityEngine;
using System.Collections;
using System.Linq;

public class ArmyBaseController : BaseController 
{
	public float skillFinishTime;

	public override void Initialize (CharacterParameterModel model)
	{
		base.Initialize (model);
		
		skillController = GetComponent<SkillController> ();
		if (skillController != null) 
		{
			skillController.Init ();
		}

	}

	protected override void AttackOnUpdate ()
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

		if(skillController != null && CanSkillInvoke())
		{
			BattleManager.GetInstance ().SetTimeScale (0);
			BattleUIManager.GetInstance ().StartSkillCutIn (1, SkillStart);
		}
		else
		{
			animationController.Attack(AttackStart,AttackEnd);
		}

	}
		

	protected override void SkillStart ()
	{
		BattleManager.GetInstance ().SetDefaultTimeScale();
		skillController.InvokeSkill (skillFinishTime,SkillFinish);
		ChangeState (ActionState.Skill);
	}

	protected override void SkillFinish ()
	{
		AddDamageToAllEnemies ();
		canAttack = true;
		canMove = true;
		ChangeState (ActionState.Idle);
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

	protected void AddDamageToAllEnemies()
	{
		EnemyBaseController[] targetList = BattleManager.GetInstance ().enemiesList.Where (x => !x.isDead 
			&& x.gameObject.activeInHierarchy).ToArray();

		Debug.Log (targetList);
		if(targetList == null)
		{
			return;
		}

		for(int i = 0; i < targetList.Length; i++)
		{
			targetList [i].Damaged (skillDamage);
		}

		characterParam.skillPoint -= BattleConst.SKILL_POINT;

		Mathf.Clamp (characterParam.skillPoint, 0, 9999);
	}



}
