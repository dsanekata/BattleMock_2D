using UnityEngine;
using System.Collections;

/// <summary>
/// ダッシュ
/// </summary>
public class SkillDash : MonoBehaviour {

	public PlayerController controller = null; 
	
	public Vector2 dashVel;
	
	public float dashTime = 3.0f;
	
	Vector2 runningVel;
	
	public Transform playerTrans;
	
	public ScreenCollider screenCollider = null;
	
	Vector3 startPos;
	
	float yPos;
	
	float time = 0.0f;
	
	bool isDashing = false;

	public void Initialize ()
	{
		
	}
	
	public void doDash (float dashTime)
	{
		if(!isDashing)
			StartCoroutine(Dash(dashTime));
	}
	
	public bool GetDashing ()
	{
		return isDashing;
	}
	
	IEnumerator Dash (float dashTime)
	{
		StartCoroutine(PrepareDash(0.3f));
		
		//yield return 0;
		
		controller.SetVelocity (dashVel);
	
		yield return new WaitForSeconds(dashTime);
		
		controller.SetGravity(3.5f);
		controller.SetVelocity(runningVel);
		screenCollider.SetEnableCollider(0.01666f);
		isDashing = false;
		
	}
	
	IEnumerator PrepareDash (float interval)
	{
		runningVel = controller.GetRigidbody2D ().velocity;
		
		isDashing = true;
		
		controller.SetGravity (0.0f);
		//	controller.SetVelocity(new Vector2(2.0f,1000.0f));
		controller.SetInvincible (dashTime + interval);
		
		startPos = playerTrans.position;
		float time = 0.0f;
		
		
		while (time <= interval) {
			yPos = Mathf.Lerp (startPos.y,3.5f, time / interval);
			playerTrans.position = new Vector3 (playerTrans.position.x, yPos, playerTrans.position.z);
			time += Time.deltaTime;
			yield return 0;
		}			
		
	}
	
}
