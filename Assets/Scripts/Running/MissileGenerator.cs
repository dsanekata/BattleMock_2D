using UnityEngine;
using System.Collections;

public class MissileGenerator : MonoBehaviour {

	string missilePath = RunningConst.RUN_PATH + "Missile";
	
	GameObject missile = null;
	
	Vector3[] generatePos;
	
	public float[] yPos;
	
	int generateNum = 0;
	
	float genTime = 0.0f;
	
	public void Initialize ()
	{
		generatePos = new Vector3[3];
		missile = Resources.Load(missilePath) as GameObject;
	}
	
	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	// Update is called once per frame
	void Update ()
	{
		genTime += Time.deltaTime;
		
		if (genTime > 2.0f) {
			Generate();
			genTime = 0.0f;
		}
		
	
	}
	
	
	
	void Generate()
	{
		generateNum = Random.Range(0,2);
		Reposition();
		Instantiate(missile,generatePos[generateNum],transform.rotation);
	}
	
	void Reposition ()
	{
		for (int i = 0; i < generatePos.Length; i++) {
			generatePos[i] = new Vector3(transform.position.x,yPos[i],transform.position.z);
		}
	}
}
