using UnityEngine;
using System;
using System.Collections;

public class EffectSlash : EffectBase 
{
	Transform targetTransform = null;
	Transform effectTrans = null;
	Animator effectAnimator = null;
	Action hitCallBack = null;

	bool isShot = false;

	public override void Init ()
	{
		base.Init ();
		effectTrans = transform.FindChild ("effect");
	}

	public void Shot(Transform target,Action callback)
	{
		if(isShot)
		{
			return;
		}
		targetTransform = target;
		hitCallBack = callback;
		isShot = true;

		PlayMoving ();
	}
		
	public override void UpdateEffect ()
	{
		MoveToTarget();
	}

	public override void Reset ()
	{
		base.Reset();
		isShot = false;
	}
		

	void MoveToTarget()
	{
		if(!isShot || targetTransform == null)
		{
			return;
		}

		float step = BattleConst.ATTACK_EFFECT_SPEED * Time.deltaTime;
			
		effectTrans.position = Vector3.MoveTowards (effectTrans.position, targetTransform.position, step);

		if(Vector3.Distance(effectTrans.position,targetTransform.position) < 1f)
		{
			PlayHit ();
			if(hitCallBack != null)
			{
				hitCallBack ();
			}

			isShot = false;
		}
	}
}
