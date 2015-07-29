using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Missile : MonoBehaviour {

	Rigidbody2D mRigidBody = null;
	
	Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		mRigidBody = GetComponent<Rigidbody2D>();
		gameObject.name = "Missile";
	}
	
	// Update is called once per frame
	void Update () {
		Shot();
	}
	
	void Shot ()
	{
		mRigidBody.velocity = new Vector2(-5.0f,0.0f);
		float distance =Mathf.Abs(startPos.x - transform.position.x);
		if(distance > 20.0f)
			Destroy(this.gameObject);
			
	}
	
	
}
