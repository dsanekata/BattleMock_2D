using UnityEngine;
using System;
using System.Collections.Generic;

public class EffectBase : MonoBehaviour 
{
	Animator attackEffectAnim = null;

	public virtual void Init()
	{
		attackEffectAnim = transform.FindChild ("effect").GetComponent<Animator> ();
		attackEffectAnim.gameObject.SetActive (false);
	}

	public virtual void PlayMoving()
	{
		attackEffectAnim.gameObject.SetActive (true);
		attackEffectAnim.Play (CommonAnimationState.EFFECT_ATTACK_MOVE);
	}

	public virtual void PlayHit()
	{
		attackEffectAnim.gameObject.SetActive (true);
		attackEffectAnim.Play (CommonAnimationState.EFFECT_ATTACK_HIT);
	}
		
}
