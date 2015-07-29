using UnityEngine;
using System.Collections;

public class RunningManager : MonoBehaviour {

	public PlayerController controller = null;
	
	public CameraControl camera = null;
	
	public CameraControl pet = null;
	
	public SkillManager skillManager = null;
	
	float waitTime = 0.0f;
	
	void Awake()
	{
		controller.Initialize();
		camera.Initialize();
		pet.Initialize();
		skillManager.Initialize();
	}
	
	void Update () {
		
		waitTime += Time.deltaTime;
		
		if(waitTime < RunningConst.START_WAIT_TIME) return;
		
		controller.Run();
		camera.CameraMove();
		pet.CameraMove();
		
		if(Input.GetKeyDown(KeyCode.P))
			PauseManager.Instance.Pause();
		if(Input.GetKeyDown(KeyCode.L))
			PauseManager.Instance.Resume();
	}
	
}
