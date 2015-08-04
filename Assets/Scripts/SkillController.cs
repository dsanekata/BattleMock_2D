using UnityEngine;
using System;
using System.Collections;

public class SkillController : MonoBehaviour 
{
	public float skillFinishTime;
	public float effectDuration;
	public int needSkillPoint;

	public Vector2 effectPositionOffset;

	public SkillType skillType;

	Action finishedCallback = null;

	GameObject cachedEffect;

	public float ElapsedTime 
	{
		get 
		{
			return elapsedTime;
		}
	}

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
		case SkillType.Laser:
			name = EffectNameConst.SKILL_LASER;
			break;
		case SkillType.Bomb:
			name = EffectNameConst.SKILL_BOMB;
			break;
		}

		cachedEffect = Resources.Load<GameObject>("Effect/Skill/"+name);
	}

	public void InvokeSkill(Action callback = null,bool addChild = false)
	{
		elapsedTime = 0f;
		finishedCallback = callback;
		invokedSkill = true;

		InstaniateEffect(cachedEffect,this.transform,addChild);
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
		case SkillType.Laser :
			seName = SoundConst.SE_LASER_LONG;
			break;
		case SkillType.Bomb:
			seName = SoundConst.SE_SKILL_BOMB;
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

	public bool IsAliveEffect()
	{
		return (elapsedTime <= effectDuration);
	}

	public void ForcedFinishSkill()
	{
		Reset();
	}


	GameObject InstaniateEffect(GameObject obj,Transform _target,bool addChild)
	{
		GameObject go = Instantiate(obj,
			new Vector3(_target.position.x,_target.position.y,-5f),_target.rotation) as GameObject;


		if(addChild)
		{
			go.transform.SetParent(_target);
			go.transform.localPosition = new Vector3(effectPositionOffset.x,effectPositionOffset.y,-5f);
			go.GetComponent<DestroyGameObject>().destroyDelay = effectDuration;
		}

		return go;
	}
}
