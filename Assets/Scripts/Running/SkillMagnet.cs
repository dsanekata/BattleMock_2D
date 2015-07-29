using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

/// <summary>
/// 磁石
/// </summary>
public class SkillMagnet : MonoBehaviour {

	BoxCollider2D boxCollider = null;
	
	public Transform playerTrans = null;
	
	Vector3 PlayerPos;
	
	GameObject item;
	
	bool isMagnet = false;
	
	float time = 0.0f;
	
	public void Initialize ()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		boxCollider.enabled = false;
	}
	
	/// <summary>
	/// プレイヤーの位置までオブジェクトを動かす.
	/// </summary>
	/// <param name="obj">対象のゲームオブジェクト</param>
	public void MoveToPlayer (GameObject obj)
	{
		iTween.MoveUpdate(obj,iTween.Hash("position",playerTrans.position,
										  "time",0.7f,
										  "easeType",iTween.EaseType.easeInBounce));
	}
		
	public void SetEnableMagnet (float time)
	{
		StartCoroutine(EnableCollider(time));	
	}
	
	IEnumerator EnableCollider (float time)
	{
		boxCollider.enabled = true;
		isMagnet = true;
		
		yield return new WaitForSeconds(time);
		
		boxCollider.enabled = false;
		isMagnet = false;
	}
	

	

}
