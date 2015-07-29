using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class AnimationController : MonoBehaviour
{
	protected Animator animator { get; private set; }

	protected CharacterAttackState attackState = null;
	protected CharacterAttackState runState = null;

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
		attackState = this.animator.GetBehaviour<CharacterAttackState>();
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

	public IEnumerator PollAnimatorAsync(int layer, string animName, Action onAnimBegin = null, Action onAnimEnd = null)
	{

		// wait for desired animation to start
		while (animator.GetCurrentAnimatorStateInfo(layer).IsName(animName) == false)
			yield return null;

		yield return null;

		float length = animator.GetCurrentAnimatorStateInfo(layer).length;

		if (onAnimBegin != null)
			onAnimBegin();

		while (animator.GetCurrentAnimatorStateInfo(layer).IsName(animName))
		{
			yield return null;
		}

		if (onAnimEnd != null)
			onAnimEnd();

	}

	public void AnimatorInvoke(int layer, string animName, Action onAnimBegin = null, Action onAnimEnd = null)
	{
		this.onAnimStart = onAnimBegin;
		this.onAnimEnd = onAnimEnd;
		Debug.Log("animatorInvoke:"+GetHashCode());

		float length = animator.GetCurrentAnimatorStateInfo(layer).length;
		if(length <= 0)
		{
			return;
		}

		CallAnimBegin();
		Invoke("CallAnimEnd",length);
	}

	void CallAnimBegin()
	{
		if(this.onAnimStart != null)
		{
			this.onAnimStart();
			this.onAnimStart = null;
		}
	}

	void CallAnimEnd()
	{
		if(this.onAnimEnd != null)
		{
			this.onAnimEnd();
			this.onAnimEnd = null;
		}
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
		if (IsPlaying(CharacterAnimationState.RUN))
			return true;
		else
			return false;
	}
	#endregion

	#region Death
	public virtual void Death()
	{
		DoAction(CharacterAnimationState.DEAD);
	}

	public bool IsDeathing()
	{
		if (IsPlaying(CharacterAnimationState.DEAD))
			return true;
		else
			return false;
	}
	#endregion

	#region Attack
	public virtual void Attack(Action onBegin,Action onEnd)
	{
		if (IsAttacking()) return;

		this.attackState.RegisterCallbacks(onBegin,onEnd);
		DoAction(CharacterAnimationState.ATTACK);

	}

	public bool IsAttacking()
	{
		if (IsPlaying(CharacterAnimationState.ATTACK))
			return true;
		else 
			return false;
	}
	#endregion
}
