using UnityEngine;
using System;
using System.Collections;

public class CharacterAttackState : CharacterStateBase
{
	Animator attackEffectAnim = null;

	public override void InitState (Transform _transform,AttackType type)
	{
		attackEffectAnim = _transform.FindChild ("AttackEffect/effect").GetComponent<Animator> ();
	}

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter (animator, stateInfo, layerIndex);
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit (animator, stateInfo, layerIndex);
	}

}
