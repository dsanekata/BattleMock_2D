using UnityEngine;
using System.Collections;

public class BattleUIManager : MonoBehaviour 
{

	public static BattleUIManager GetInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<BattleUIManager>();
		}

		return instance;
	}
	static BattleUIManager instance;

	GameObject cutIn = null;

	void Awake()
	{
		cutIn = transform.FindChild ("CutInImage").gameObject;
	}
		
	public void StartSkillCutIn(float waitTime,System.Action callback)
	{
		StartCoroutine (SkillCutIn (waitTime, callback));
	}

	IEnumerator SkillCutIn(float waitTime, System.Action callBack = null)
	{
		cutIn.SetActive (true);
		float time = 0.0f;


		//			while (cutInImage.GetComponent<CanvasGroup> ().alpha < 1)
		//			{
		//				cutInImage.GetComponent<CanvasGroup> ().alpha = Mathf.Lerp(0, 1, time / duration);
		//                time += Time.unscaledDeltaTime;
		//				yield return 0;
		//			}

		time = 0f;
		//yield return new WaitForSeconds(waitTime);
		while(time < waitTime)
		{
			time += Time.unscaledDeltaTime;
			yield return 0;
		}


		//			while (cutInImage.GetComponent<CanvasGroup> ().alpha > 0) 
		//			{
		//				cutInImage.GetComponent<CanvasGroup> ().alpha = Mathf.Lerp(1, 0, time / duration);
		//                time += Time.unscaledDeltaTime;
		//				yield return 0;
		//			}

		cutIn.SetActive (false);

		yield return null;

		if(callBack != null)
			callBack();
	}
}
