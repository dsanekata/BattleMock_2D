using UnityEngine;
using System;
using System.Collections;

public class DamageTextState : StateMachineBehaviour
{

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.gameObject.SetActive(false);
	}
		
}
