using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public GameObject player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D (Collision2D other)
	{
//		if (other.collider.tag == "Player") {
//			player.SendMessage("SetOnGround");
//		}
	}
	
}
