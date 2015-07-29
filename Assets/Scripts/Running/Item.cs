using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SkillItem))]

public class Item : MonoBehaviour {

	public SkillMagnet skillMagnet = null;
	
	int itemType = 0;
	
	SkillItem skillItem = null;
	
	bool activeMagnet = false;
	
	string objName = "";

	void Start () {
		Initialize();
	}
	
	void Initialize()
	{
		objName = gameObject.name;
		if(objName == "SkillItem")
			skillItem = GetComponent<SkillItem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!activeMagnet) return;
		
		skillMagnet.MoveToPlayer(this.gameObject);
		
		
	}
		
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			Debug.Log("hit");
			if(objName == "SkillItem")
				GetItem(col.gameObject);
			else if(objName == "Coin")
				GetCoin(col.gameObject);
		}
		if(col.tag == "Magnet")
			activeMagnet = true;
	}
	
	void GetCoin (GameObject obj)
	{
		EffectManager.Instance.InstantiateEffectInChild("GetEffect",0.3f,obj);
		obj.SendMessage("PlusCoinCount");
		Destroy(this.gameObject);
	}
	
	void GetItem(GameObject obj)
	{
		skillItem.SendSkillRequest();
		EffectManager.Instance.InstantiateEffectInChild("GetEffect",0.3f,obj);
		Destroy(this.gameObject);
	}
}
