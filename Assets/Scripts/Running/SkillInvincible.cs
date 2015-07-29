using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class SkillInvincible : MonoBehaviour {

	public PlayerController controller = null;
	
	public ScreenCollider screenCollider = null;
		
	bool isInvincible = false;
	
	BoxCollider2D invincibleCollider = null;
	
	public  void Initialize ()
	{
		invincibleCollider = GetComponent<BoxCollider2D>();
		invincibleCollider.isTrigger = true;
		invincibleCollider.enabled = false;
	}
	
	public void doInvincible (float time)
	{
		if (!isInvincible) {
			StartCoroutine (Invincible (time));
			EffectManager.Instance.InstantiateEffectInChild("InvEffect",time,this.gameObject);
		}
	}
	
	IEnumerator Invincible(float time)
	{
		isInvincible = true;
		invincibleCollider.enabled = true;
		
		yield return new WaitForSeconds(time);
		
		isInvincible = false;
		invincibleCollider.enabled = false;
		
		screenCollider.SetEnableCollider(0.01666f);
	}
}
