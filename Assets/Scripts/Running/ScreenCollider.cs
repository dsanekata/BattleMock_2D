using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class ScreenCollider : MonoBehaviour {

	BoxCollider2D mBoxCollider = null;
	
	public void Initalize ()
	{
		mBoxCollider = GetComponent<BoxCollider2D>();
		mBoxCollider.isTrigger = true;
		mBoxCollider.enabled = false;
	}
	
	
	void Start () {
		Initalize();
	}
	
	public void SetEnableCollider (float time)
	{
		StartCoroutine(EnableCollider(time));
	}
	
	IEnumerator EnableCollider (float time)
	{
		mBoxCollider.enabled = true;
		
		yield return new WaitForSeconds(time);
		
		mBoxCollider.enabled = false;
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.tag != "Item")
			col.gameObject.SendMessage("DestroyObject",SendMessageOptions.DontRequireReceiver);
	}
}
