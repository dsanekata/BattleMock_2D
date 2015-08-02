using UnityEngine;
using System.Collections;

public class LongRangeArmyController : ArmyBaseController
{
	EffectSlash slash = null;

	public override void Initialize (CharacterParameterModel model)
	{
		base.Initialize (model);
		slash = transform.FindChild ("Model/EffectAttack").GetComponent<EffectSlash> ();
		slash.Init ();
	}

	protected override void AttackStart ()
	{
		base.AttackStart ();

		if (target != null && !target.isDead) 
		{
			slash.Shot (this.target.transform, AddDamageToTarget);
		}
	}

	protected override void AttackEnd ()
	{
		Invoke ("NextAttack", this.characterParam.attackInterval);
	}
}
