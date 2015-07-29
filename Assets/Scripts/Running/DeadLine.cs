using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class DeadLine : MonoBehaviour {

	public GameObject player = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			player.SendMessage("SetDie");
		}
	}
}
