using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterHUD : MonoBehaviour
{
	Text[] damageTextList = null;
	int textIndex = 0;

	public void InitHUD()
	{
		InitDamageTexts();
	}

	void InitDamageTexts()
	{
		damageTextList = this.transform.FindChild("Damage").GetComponentsInChildren<Text>(true);

		for(int i=0; i <damageTextList.Length; i++)
		{
			damageTextList[i].text = string.Empty;
			damageTextList[i].gameObject.SetActive(false);
		}
	}

	public void PopDamageText(int damage)
	{
		damageTextList[textIndex].gameObject.SetActive(true);
		damageTextList[textIndex].text = damage.ToString();

		string animName = (textIndex % 2 == 0) ? CommonAnimationState.DAMAGE_TEXT_LEFT : CommonAnimationState.DAMAGE_TEXT_RIGHT;
		damageTextList[textIndex].GetComponent<Animator>().Play(animName);
		textIndex ++;

		if(textIndex > damageTextList.Length - 1)
		{
			textIndex = 0;
		}
	}
		
}
