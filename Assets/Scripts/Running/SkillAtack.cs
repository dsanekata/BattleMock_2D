using UnityEngine;
using System.Collections;

public class SkillAtack : MonoBehaviour {

	GameObject bullet = null;
	

	bool isAtack = false;
	
	string runningPath = RunningConst.RUN_PATH;
	
	public void Initalize ()
	{
		bullet = Resources.Load(runningPath+"Bullet") as GameObject;
	}
	
	void Awake()
	{
		Initalize();
	}
	
	/// <summary>
	/// 射撃をする
	/// </summary>
	/// <param name="interval">発射間隔.</param>
	/// <param name="atackTime">有効時間.</param>
	public void doAtack(float interval,float atackTime)
	{
		if(!isAtack)
			StartCoroutine(Atack(interval,atackTime));
	}
	
	void InstantiateBullet ()
	{
		
		GameObject bullets = Instantiate(bullet,this.transform.position,this.transform.rotation) as GameObject;
		bullets.transform.parent = this.transform;
	}
	
	IEnumerator Atack (float interval, float atackTime)
	{
		isAtack = true;
		
		float time = 0.0f;
		
		float instantiateTime = 0.0f;
		
		InstantiateBullet();
		
		while (time < atackTime) {
			if (instantiateTime > interval) {
				InstantiateBullet();
				instantiateTime = 0.0f;
			}
			time += Time.deltaTime;
			instantiateTime += Time.deltaTime;
			yield return 0;
		}
		

		
		isAtack = false;
	}
}
