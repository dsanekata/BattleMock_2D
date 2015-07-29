using UnityEngine;
using System.Collections;

public class SkillBoost : MonoBehaviour {

	int coinCount = 0;
	
	bool boost = false;
	
	int coinLimit = RunningConst.BOOST_COUNT;
	
	public SkillAtack skillAtack = null;
	
	public SkillInvincible skillInv = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(coinCount);
	}
	
	void PlusCoinCount()
	{
		if(coinCount<coinLimit)
			coinCount ++;
		
		if(coinCount == coinLimit)
			boost = true;
		else
			boost = false;
	}
	
	public void doBoost (float boostTime)
	{
		if (boost) {
			skillInv.doInvincible(boostTime);
			skillAtack.doAtack(0.25f,boostTime);
			boost = false;
			DecreaseCoin();
		}
		
	}
	
	public void DecreaseCoin ()
	{
		StartCoroutine(Decrease());
	}
	
	IEnumerator Decrease()
	{
		while (coinCount > 0) {
			
			coinCount --;
			
			yield return 0;
		}
	}
	
	public int GetCoinCount ()
	{
		return coinCount;
	}
}
