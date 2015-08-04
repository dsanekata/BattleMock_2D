using UnityEngine;
using System;
using System.Collections;

public class CharacterAnimationStateManager 
{
	protected CharacterAttackState attackState = null;
	protected CharacterDeadState deadState = null;
	protected CharacterSkillState skillState = null;

	public void ResetStateManager(Animator _animator)
	{
		attackState = _animator.GetBehaviour<CharacterAttackState> ();
		deadState = _animator.GetBehaviour<CharacterDeadState> ();
		skillState = _animator.GetBehaviour<CharacterSkillState>();
	}

	public void RegisterAttackCallbacks(Action onBegin, Action onEnd)
	{
		attackState.RegisterCallbacks (onBegin, onEnd);
	}

	public void RegisterDeadCallbacks(Action onBegin, Action onEnd)
	{
		deadState.RegisterCallbacks (onBegin, onEnd);
	}

	public void RegisterSkillCallbacks(Action onBegin, Action onEnd)
	{
		skillState.RegisterCallbacks (onBegin, onEnd);
	}
}
