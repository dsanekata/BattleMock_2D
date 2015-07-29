using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour {

	GameObject mainScene = null;
	
	string runningPath = RunningConst.RUN_PATH;
	
	bool init = false;
	void Initialize()
	{
		mainScene = Resources.Load(runningPath+"MainScene") as GameObject;
		Instantiate(mainScene,Vector3.zero,new Quaternion());
	}
	
	void Awake()
	{
		//Initialize();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	
	}
	

}
