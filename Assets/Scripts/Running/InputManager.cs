using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public SkillManager skillManager;
	
	float waitTime = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		waitTime += Time.deltaTime;
		if(waitTime < RunningConst.START_WAIT_TIME) return;
		
		if(Input.GetKeyDown(KeyCode.D))
				skillManager.UseDash();
		else if(Input.GetKeyDown(KeyCode.W))
				skillManager.UseInvincible();
		else if(Input.GetKeyDown(KeyCode.A))
				skillManager.UseAtack();
		
		else if(Input.GetKeyDown(KeyCode.B))
				skillManager.UseBoost();
		else if(Input.GetKeyDown(KeyCode.S))
				skillManager.UseMagnet();
	}
}
