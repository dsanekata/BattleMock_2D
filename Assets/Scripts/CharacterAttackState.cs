using UnityEngine;
using System;
using System.Collections;

public class CharacterAttackState : StateMachineBehaviour
{
	Action stateEnter = null;
	Action stateExit = null;

	public void RegisterCallbacks(Action stateEnter,Action stateExit)
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
