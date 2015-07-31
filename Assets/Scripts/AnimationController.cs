using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class AnimationController 
{
	protected Animator animator { get; private set; }

	protected CharacterAnimationStateManager stateManager = new CharacterAnimationStateManager();

	Action onAnimStart = null;
	Action onAnimEnd = null;

	public string AnimatorName
	{
		get
		{
			if (animator == null)
				return null;
			return animator.name;
		}
	}

	public virtual void SetAnimator(Animator animator)
	{
		if (this.animator != null)
		{
			if (object.ReferenceEquals(this.animator, animator) == false)
			{
				Debug.LogError(string.Format("Animation controller locked. It already has an Animator [{0}].", animator.name));
			}
		}

		if (animator == null)
		{
			Debug.LogError("Not found animator!");
		}

		this.animator = animator;
		stateManager.ResetStateManager(this.animator);
	}

	protected void DoAction(string animationName)
	{
		if (animationName == null) return;

		animator.Play(animationName);

		//		StartCoroutine(PollAnimatorAsync(0, CharacterAnimationState.Attack1.ToString(), onAnimBegin, onAnimEnd));
	}

	/// <summary>
	/// Determines whether this instance is playing the specified animation.
	/// </summary>
	/// <returns><c>true</c> if this instance is playing the specified animation; otherwise, <c>false</c>.</returns>
	/// <param name="animation">Animation.</param>
	protected bool IsPlaying(string animation)
	{
		return (animator.GetCurrentAnimatorStateInfo(0).IsName(animation));
	}
		
	#region Idle
	public virtual void Idle()
	{
		if (!IsPlaying(CharacterAnimationState.IDLE))
		{
			DoAction(CharacterAnimationState.IDLE);
		}
	}
	#endregion

	#region Run
	public virtual void Run()
	{
		if (this.IsRunning()) return;

		DoAction(CharacterAnimationState.RUN);
	}

	public bool IsRunning()
	{
		return (IsPlaying (CharacterAnimationState.RUN));

	}
	#endregion

	#region Death
	public virtual void Death(Action onBegin, Action onEnd)
	{
		if(IsDeathing()) return;

		this.stateManager.RegisterDeadCallbacks (onBegin, onEnd);
		DoAction(CharacterAnimationState.DEAD);
	}

	public bool IsDeathing()
	{
		return (IsPlaying (CharacterAnimationState.DEAD));
	}
	#endregion

	#region Attack
	public virtual void Attack(Action onBegin,Action onEnd)
	{
		if (IsAttacking()) return;

		this.stateManager.RegisterAttackCallbacks (onBegin, onEnd);
		DoAction(CharacterAnimationState.ATTACK);

	}

	public bool IsAttacking()
	{
		return (IsPlaying (CharacterAnimationState.ATTACK));
	}
	#endregion
}
