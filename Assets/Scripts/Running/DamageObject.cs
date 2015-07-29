using UnityEngine;
using System.Collections;

public class DamageObject : MonoBehaviour {

void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			col.gameObject.SendMessage("SetDie");
			if(gameObject.name == "Missile")
				DestroyObject();
		}
		else if(col.tag == "ScreenCollider" || col.tag == "Invincible")
			DestroyObject();
	}
	
	void DestroyObject()
	{
		Destroy(gameObject);
	}
}
