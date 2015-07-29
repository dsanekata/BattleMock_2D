using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

	int itemType = 0;
	
	GameObject skillManeger = null;
	
	void Initialize ()
	{
		skillManeger = GameObject.Find("SkillManager");
	}
	
	// Use this for initialization
	void Start () {
		Initialize();
		itemType = Random.Range(0,3);
	}
	
	public void SendSkillRequest ()
	{
		switch (itemType) {
		case 0:
			skillManeger.SendMessage("UseMagnet");
			break;
		case 1:
			skillManeger.SendMessage("UseAtack");
			break;
		case 2:
			skillManeger.SendMessage("UseDash");
			break;
		case 3:
			skillManeger.SendMessage("UseInvincible");
			break;
		}
	}
}
