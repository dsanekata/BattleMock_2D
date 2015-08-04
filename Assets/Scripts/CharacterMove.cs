using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour 
{
	float prevPosx = 0;
	float characterScale = 0;

	Transform modelTrans = null;
	Transform hud = null;

	void Start()
	{
		modelTrans = this.transform.FindChild("Model");
		hud = this.transform.FindChild("HUD");
		characterScale = modelTrans.localScale.x;
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
		
	public void LookAtTarget(Transform target)
	{
		float dist = target.transform.position.x - this.transform.position.x;

		if(dist < 0)
		{
			//modelTrans.localScale = new Vector3(-characterScale,characterScale,characterScale);
			transform.eulerAngles = new Vector3(0,180,0);
		}
		else
		{
			//modelTrans.localScale = new Vector3(characterScale,characterScale,characterScale);
			transform.eulerAngles = new Vector3(0,0,0);
		}

		hud.eulerAngles = Vector3.zero;
	}

	float ClampPosition(float posY)
	{
		return Mathf.Clamp(posY,BattleConst.POSY_MIN,BattleConst.POSY_MAX);
	}

	void CheckDirection()
	{
		if(this.transform.position.x - prevPosx > 0)
		{
			//modelTrans.localScale = new Vector3(characterScale,characterScale,characterScale);
			transform.eulerAngles = new Vector3(0,0,0);
		}
		else
		{
			//modelTrans.localScale = new Vector3(-characterScale,characterScale,characterScale);
			transform.eulerAngles = new Vector3(0,180,0);
		}

		hud.eulerAngles = Vector3.zero;
		prevPosx = this.transform.position.x;
	}
}
