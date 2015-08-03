using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterHUD : MonoBehaviour
{
	Text[] damageTextList = null;
	Image[] damageBgList = null;
	int textIndex = 0;

	public void InitHUD()
	{
		InitDamageTexts();
	}

	void InitDamageTexts()
	{
		damageTextList = this.transform.FindChild("Damage").GetComponentsInChildren<Text>(true);
		damageBgList = transform.FindChild("Damage").GetComponentsInChildren<Image>(true);

		for(int i=0; i <damageTextList.Length; i++)
		{
			damageTextList[i].text = string.Empty;
		}
	}

	public void PopDamageText(int damage,bool isCritical = false)
	{
		damageBgList[textIndex].gameObject.SetActive(true);
		damageTextList[textIndex].text = damage.ToString();

		damageBgList[textIndex].enabled = isCritical;

		string animName = (textIndex % 2 == 0) ? 
			((isCritical) ? CommonAnimationState.DAMAGE_TEXT_LEFT_CRITICAL : CommonAnimationState.DAMAGE_TEXT_LEFT) :
			((isCritical) ? CommonAnimationState.DAMAGE_TEXT_RIGHT_CRITICAL : CommonAnimationState.DAMAGE_TEXT_RIGHT);
		damageBgList[textIndex].GetComponent<Animator>().Play(animName);
		textIndex ++;

		if(textIndex > damageTextList.Length - 1)
		{
			textIndex = 0;
		}
	}
		
}
