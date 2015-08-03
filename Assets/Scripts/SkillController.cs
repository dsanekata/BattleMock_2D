using UnityEngine;
using System;
using System.Collections;

public class SkillController : MonoBehaviour 
{
	public float effectDuration;
	public int needSkillPoint;

	public SkillType skillType;

	Action finishedCallback = null;

	float skillFinishTime;
	GameObject cachedEffect;
	float elapsedTime = 0;
	public bool invokedSkill = false;

	public void Init()
	{
		LoadEffect();
	}

	public void LoadEffect()
	{
		string name = "";

		switch(skillType)
		{
		case SkillType.Explosion:
			name = EffectNameConst.SKILL_EXPLOSION;
			break;
		case SkillType.Lightning:
			name = EffectNameConst.SKILL_LIGHTNING;
			break;
		case SkillType.Wind:
			name = EffectNameConst.SKILL_WIND;
			break;
		}

		cachedEffect = Resources.Load<GameObject>("Effect/Skill/"+name);
	}

	public void InvokeSkill(float finishTime,Action callback = null)
	{
		elapsedTime = 0f;
		skillFinishTime = finishTime;
		finishedCallback = callback;
		invokedSkill = true;

		InstaniateEffect(cachedEffect,this.transform);
	}
		
	public string GetSkillSeName()
	{
		string seName = "";

		switch(skillType)
		{
		case SkillType.Explosion :
			seName = SoundConst.SE_SKILL_EXPLOSION;
			break;
		case SkillType.Lightning : 
			seName = SoundConst.SE_SKILL_LIGHTNING;
			break;
		case SkillType.Wind :
			seName = SoundConst.SE_SKILL_WIND;
			break;
		}

		return seName;
	}

	void Reset()
	{
		elapsedTime = 0f;
		finishedCallback = null;
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

	public void ForcedFinishSkill()
	{

		if(finishedCallback != null)
		{
			finishedCallback ();
			Reset();
		}
	}

	GameObject InstaniateEffect(GameObject obj,Transform _target)
	{
		return Instantiate(obj,
			new Vector3(_target.position.x,_target.position.y,-2f),_target.rotation) as GameObject;
	}
}
