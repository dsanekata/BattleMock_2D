using UnityEngine;
using System;
using System.Collections;

public class CharacterStateBase : StateMachineBehaviour
{
	protected Action stateEnter;
	protected Action stateExit;

	public virtual void RegisterCallbacks(Action stateEnter,Action stateExit)
	{
		this.stateEnter = stateEnter;
		this.stateExit = stateExit;
	}

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(this.stateEnter != null)
		{
			this.stateEnter();
			this.stateEnter = null;
		}
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(this.stateExit != null)
		{
			this.stateExit();
			this.stateExit = null;
		}
	}
}
