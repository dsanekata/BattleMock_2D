using UnityEngine;
using System.Collections;

public class StrongestArmyController : ArmyBaseController
{
	EffectParticle effect;

	public override void Initialize (CharacterParameterModel model)
	{
		base.Initialize (model);
		effect = transform.FindChild("Model/EffectAttack").GetComponent<EffectParticle>();
		effect.Init();
	}

	protected override void AttackOnUpdate ()
	{
		base.AttackOnUpdate ();

		effect.UpdateEffect();
	}

	protected override void AttackStart()
	{
		canAttack = false;
		canMove = false;

		if (target != null && !target.isDead) 
		{
			effect.Shot (this.target.transform, AddDamageToTarget);
			SoundManager.GetInstance().PlaySE(SoundConst.SE_LASER);
		}
	}

	protected override void AttackEnd ()
	{
		Invoke ("NextAttack", this.characterParam.attackInterval);
	}

	protected override void AttackOnExit ()
	{
		effect.Reset();
	}
}
