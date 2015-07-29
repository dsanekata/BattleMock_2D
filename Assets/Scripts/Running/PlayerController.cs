using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator),typeof(Rigidbody2D),typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// プレイヤーのアニメーター
	/// </summary>
	Animator mPlayerAnimator;
	
	/// <summary>
	/// プレイヤーのリジッドボディ
	/// </summary>
	Rigidbody2D mRigidBody2D;
	
	/// <summary>
	/// ボックスコライダー2D
	/// </summary>
	BoxCollider2D mBoxCollider;
	
	/// <summary>
	/// 攻撃中かどうか
	/// </summary>
	bool isAtacking = false;
	
	/// <summary>
	/// 死んでいるかどうか
	/// </summary>
	bool isDying = false;
	
	/// <summary>
	/// 地面にいるかどうか
	/// </summary>
	bool mOnGround = false;
	
	bool isInvincible = false;
	
	float firstSpeed = 0.0f;
	
	float time = 0.0f;
	
	
	/// <summary>
	/// ぶつかった時に後退する力
	/// </summary>
	Vector2 backwardForce = new Vector2(-300,250);
	
	float runSpeed = RunningConst.FIRST_SPEED;
	
	float jumpPower = RunningConst.JUMP_POWER;
	
	int jumpLimit = RunningConst.JUMP_LIMIT;
	
	float speedRate = RunningConst.SPEED_RATE;
	
	float updateTime = RunningConst.UPDATE_TIME;
	
	float plusSpeed = 1.0f;
	
	/// <summary>
	/// スキルマネージャー
	/// </summary>
	public SkillManager skillManager;
	
	public BoxCollider2D[] collider;
	
	public BoxCollider2D invincibleCollider = null;
	
	public GameObject mainCamera;
	
	int jumpCount = 0;
	
	float waitTime = 0;
	
	public void Initialize ()
	{
		mPlayerAnimator = GetComponent<Animator>();
		mRigidBody2D = GetComponent<Rigidbody2D>();
		mBoxCollider = GetComponent<BoxCollider2D>();
		invincibleCollider.enabled = false;
	}
		
	/// <summary>
	/// プレイヤーを走らせる
	/// </summary>
	public void Run ()
	{
		if (!isDying && !isInvincible) {
			Accelarete ();
			Reposition ();
			mRigidBody2D.velocity = new Vector2 (runSpeed + plusSpeed, mRigidBody2D.velocity.y);
		
			if (Input.GetMouseButtonDown (0)) {
				if (jumpCount < jumpLimit || mOnGround)
				if (!mOnGround)
					Jump (jumpPower * 0.7f);
				else
					Jump (jumpPower);
			}
		}
	}
	
	void Jump (float jumpPow)
	{
		if(mRigidBody2D.velocity.y != 0)
			mRigidBody2D.velocity = new Vector2(runSpeed,0);
		
		mRigidBody2D.AddForce (Vector2.up * jumpPow);
		mOnGround = false;
		
		if(jumpPow > 0)
			jumpCount++;
	}
	
	void SetDie ()
	{
		runSpeed = 0.0f;
		mRigidBody2D.velocity = Vector2.zero;
		mPlayerAnimator.SetBool ("die", true); 
		mRigidBody2D.AddForce(backwardForce);
		mBoxCollider.enabled = false;
		isDying = true;
		Invoke("Retry",1.0f);
	}
	
	void Retry ()
	{
		Application.LoadLevel("TestScene");	
	}
	
	void Accelarete ()
	{
			time += Time.deltaTime;
			if (time > updateTime) {
				runSpeed += speedRate;
				time = 0.0f;
			}
		}
	
	/// <summary>
	/// Gets the run speed.
	/// </summary>
	/// <returns>The run speed.</returns>
	public float GetRunSpeed ()
	{
		return runSpeed;
	}
	
	public bool GetDying ()
	{
		return isDying;
	}
	
	void SetOnGround ()
	{
		if (!mOnGround) {
			mOnGround = true;
			jumpCount = 0;
		}
	}
	

	void Reposition ()
	{
		Vector3 screenPoint = mainCamera.GetComponent<Camera>().WorldToScreenPoint (this.gameObject.transform.position);
		if (screenPoint.x < 100) {
			plusSpeed = 0.5f;
		} else if (screenPoint.x > 110) {
			plusSpeed = -5.0f;
		} else {
			plusSpeed = 0.0f;
		}
		
	}
	
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.tag == "Ground") {
			SetOnGround ();
		} 
		
	}
	/// <summary>
	/// プレイヤーのリジッドボディ取得
	/// </summary>
	/// <returns>rigidbody2d.</returns>
	public Rigidbody2D GetRigidbody2D ()
	{
		return mRigidBody2D;
	}
	
	public void SetGravity (float gravity)
	{
		mRigidBody2D.gravityScale = gravity;	
	}
	/// <summary>
	/// プレイヤーの速度を設定する
	/// </summary>
	/// <param name="vel">速度</param>
	public void SetVelocity (Vector2 vel)
	{
		runSpeed = vel.x;
		mRigidBody2D.velocity = Vector2.zero;
		mRigidBody2D.velocity = vel;
	}
	/// <summary>
	/// 現在のジャンプ回数を設定する
	/// </summary>
	/// <param name="count">ジャンプ回数</param>
	public void SetJumpCount (int count)
	{
		jumpCount = count;
	}
	/// <summary>
	/// 無敵状態にする
	/// </summary>
	/// <param name="time">無敵時間</param>
	public void SetInvincible (float time)
	{
		StartCoroutine(Invincible(time));
	}
	
	/// <summary>
	/// 無敵設定のコルーチン
	/// </summary>
	/// <param name="time">Time.</param>
	IEnumerator Invincible (float time)
	{
		isInvincible = true;
		for (int i = 0; i < collider.Length; i++) {
			collider [i].enabled = false;
		}
			invincibleCollider.enabled = true;
			
			//mBoxCollider.isTrigger = true;
			yield return new WaitForSeconds(time);
		
			isInvincible = false;
			for (int i = 0; i < collider.Length; i++) {
				collider [i].enabled = true;
		}
				invincibleCollider.enabled = false;
		//mBoxCollider.isTrigger = false;
	}
	

}
