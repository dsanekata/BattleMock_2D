using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour {

	Rigidbody2D mRigidBody = null;
	
	Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		mRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Shot();
	}
	
	void Shot ()
	{
		mRigidBody.velocity = new Vector2(30.0f,0.0f);
		float distance =Mathf.Abs(startPos.x - transform.position.x);
		if(distance > 10.0f)
			Destroy(this.gameObject);
			
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.tag == "Hurdle")
			Destroy(this.gameObject);	
	}
}
