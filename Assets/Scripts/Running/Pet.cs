using UnityEngine;
using System.Collections;

public class Pet : MonoBehaviour {

	public PlayerController controller = null;
	
	Rigidbody2D mRigidBoby = null;
	
	Vector3 startPos;
	
	Vector3 endPos;
	
	float yPos;
	
	bool isMoving = false;
	
	float waitTime = 0.0f;
	
	void Initialize ()
	{
		startPos = this.transform.position;
		
		endPos = new Vector3(startPos.x,startPos.y+0.01f,startPos.z);
		
		mRigidBoby = GetComponent<Rigidbody2D>();
	}
	
	void Start () {
		
		Initialize();
		
		//TweenStart();
		
		TweenPet();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		waitTime += Time.deltaTime;
		
		if(waitTime < RunningConst.START_WAIT_TIME) return;
		

		
	}
	
	void TweenStart()
	{
		StartCoroutine(TweenUp(1.0f));
		
	}
	

	
	IEnumerator TweenUp (float interval)
	{
	
		isMoving = true;
		float time = 0.0f;
		while (time <= interval) {
			yPos = Mathf.Lerp (startPos.y, startPos.y + 2.0f, time / interval);
			this.transform.position = new Vector3 (transform.position.x, yPos, 0);
			time += Time.deltaTime;
			yield return 0;
			
		}
		startPos = this.transform.position;
			
		
		StartCoroutine(TweenDown(1.0f));
	}
	
	IEnumerator TweenDown (float interval)
	{
		
		float time = 0.0f;
		while (time <= interval) {
			yPos = Mathf.Lerp (startPos.y, startPos.y - 2.0f, time / interval);
			this.transform.position = new Vector3 (transform.position.x, yPos, 0);
			time += Time.deltaTime;
			yield return 0;
		}	
		startPos = this.transform.position;
			
		
		StartCoroutine(TweenUp(1.0f));
	}
	
	void TweenPet()
	{
		iTween.MoveBy(this.gameObject,
			iTween.Hash("y",1.5f,
				"time",1.0f,
				"looptype",iTween.LoopType.pingPong,
				"easetype",iTween.EaseType.easeInOutQuad,
				"oncomplete","TweenPet")); 
		
	}
	


	
	
}
