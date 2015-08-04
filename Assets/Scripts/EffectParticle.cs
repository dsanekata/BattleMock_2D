using UnityEngine;
using System;
using System.Collections;

public class EffectParticle : MonoBehaviour 
{

	Transform targetTransform = null;
	Transform effectTrans = null;
	GameObject body = null;
	GameObject hit = null;
	Animator effectAnimator = null;
	Action hitCallBack = null;

	bool isShot = false;

	public void Init ()
	{
		effectTrans = transform.FindChild ("effect");
		body = effectTrans.FindChild("Body").gameObject;
		hit = effectTrans.FindChild("Hit").gameObject;

		body.SetActive(false);
		hit.SetActive(false);
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
		effectTrans.position = transform.position;
		body.SetActive(true);
		hit.SetActive(false);
	}

	public void UpdateEffect ()
	{
		MoveToTarget();
	}

	public void Reset ()
	{
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
			body.SetActive(false);
			hit.SetActive(true);

			if(hitCallBack != null)
			{
				hitCallBack ();
			}

			isShot = false;
		}
	}
}
