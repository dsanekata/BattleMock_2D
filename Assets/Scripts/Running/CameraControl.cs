using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject player = null;
	
	Vector3 offset;
	
	public SkillDash skillDash = null;
	
	public PlayerController controller = null;
	float waitTime = 0;
	
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		Initialize();
	}
	
	public void Initialize()
	{
//		transform.position = new Vector3((player.transform.position.x + offset.x) - controller.GetRunSpeed(),transform.position.y
//		,player.transform.position.z+offset.z);
	}
	
	// Update is called once per frame
	void Update () {
		waitTime += Time.deltaTime;
		
		if(waitTime < RunningConst.START_WAIT_TIME) return;
		
		
		

		
	}
	
	public void CameraMove ()
	{
		if (!skillDash.GetDashing ()) {
			this.transform.position = new Vector3 (transform.position.x + (controller.GetRunSpeed () * Time.smoothDeltaTime), 
				transform.position.y, transform.position.z);
		} else {
			transform.position = new Vector3((player.transform.position.x + offset.x),transform.position.y
					,player.transform.position.z+offset.z);
		}
	}
	

	

}
