using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour 
{
	float prevPosx = 0;
	float characterScale = 0;

	void Start()
	{
		characterScale = this.transform.localScale.x;
	}

	/// <summary>
	/// ターゲットに向かってMoveTowardsで移動
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="moveSpeed">Move speed.</param>
	public void MoveToTarget(Transform target,float moveSpeed)
	{

		float step = moveSpeed * Time.deltaTime;
		Vector3 moveVec = Vector3.MoveTowards(this.transform.position,target.position,step);

		this.transform.position = new Vector3(moveVec.x,ClampPosition(moveVec.y),ClampPosition(moveVec.y));

		CheckDirection();
	}
		
	float ClampPosition(float posY)
	{
		return Mathf.Clamp(posY,BattleConst.POSY_MIN,BattleConst.POSY_MAX);
	}

	void CheckDirection()
	{
		if(this.transform.position.x - prevPosx > 0)
		{
			this.transform.localScale = new Vector3(characterScale,characterScale,characterScale);
		}
		else
		{
			this.transform.localScale = new Vector3(-characterScale,characterScale,characterScale);
		}

		prevPosx = this.transform.position.x;
	}
}
