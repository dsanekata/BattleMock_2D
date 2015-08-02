using UnityEngine;

public class AttackEffectHit : StateMachineBehaviour
{
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.localPosition = Vector3.zero;
		animator.gameObject.SetActive (false);
	}
}
