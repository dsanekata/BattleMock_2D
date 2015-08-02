using UnityEngine;
using System;
using System.Collections;

public class SkillController : MonoBehaviour 
{
	public float effectDuration;
	public int needSkillPoint;

	Action finishedCallback = null;

	float skillFinishTime;
	Transform effectTransform = null;
	float elapsedTime = 0;
	bool invokedSkill = false;

	public void Init()
	{
		effectTransform = transform.FindChild ("Model/SkillEffect");
		effectTransform.gameObject.SetActive (false);
	}

	public void InvokeSkill(float finishTime,Action callback = null)
	{
		elapsedTime = 0f;
		skillFinishTime = finishTime;
		finishedCallback = callback;
		effectTransform.gameObject.SetActive (true);
		invokedSkill = true;
		Invoke ("Reset", effectDuration);
	}

	void Reset()
	{
		elapsedTime = 0f;
		finishedCallback = null;
		effectTransform.gameObject.SetActive (false);
		invokedSkill = false;
	}


	public void SkillUpdate()
	{
		if(!invokedSkill)
		{
			return;
		}

		if(elapsedTime >= skillFinishTime)
		{
			if(finishedCallback != null)
			{
				finishedCallback ();
			}

		}

		elapsedTime += Time.deltaTime;
	}
}
