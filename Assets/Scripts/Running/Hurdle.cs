using UnityEngine;
using System.Collections;

public class Hurdle : MonoBehaviour {

	GameObject player = null;
	
	PlayerController controller;
	
	Rigidbody2D rigidbody;
	
	float upwardForce = RunningConst.UPWARD_FORCE;
	
	float downForce;
	
	void Start () {
		player = GameObject.Find("BMB");
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.gravityScale = 0.0f;
		controller = player.GetComponent<PlayerController>();
	}
	
	
	void OnTriggerEnter2D (Collider2D col)
	{
		switch (col.tag) {
		case "DeadCollision": player.SendMessage("SetDie");
			break;
		case "Player": JumpUp();
			break;
		case "UpperCollider": Down();
			break;
		case "Invincible": DestroyObject();
			break;
		}

	}
	
	
	
	void EmitDestroyEffect()
	{
		EffectManager.Instance.InstantiateEffect("Fireworks",0.5f,this.gameObject);	
	}
	
	void JumpUp ()
	{
		player.SendMessage ("Jump", upwardForce);
		player.SendMessage ("SetJumpCount", 1);
		EmitDestroyEffect();
		
		Destroy (this.gameObject);
	}
		
	void DestroyObject ()
	{
		EmitDestroyEffect();
		Destroy(this.gameObject);
	}
	
	void Down()
	{
		downForce = -controller.GetRigidbody2D().velocity.y;
		player.SendMessage ("Jump", downForce);
		EmitDestroyEffect();
		Destroy(this.gameObject);
	}
}
