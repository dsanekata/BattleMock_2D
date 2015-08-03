using UnityEngine;
using UnityEngine.UI;
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

	Text dragCountText = null;
	Text dragLimitText = null;

	Toggle autoSkillToggle = null;

	public bool autoInvokeSkill = true;

	void Awake()
	{
	}
		
	public void InitTransforms()
	{
		cutIn = transform.FindChild ("CutInImage").gameObject;
		dragCountText = transform.FindChild("Offset/Top/Drag/CountText").GetComponent<Text>();
		dragLimitText = transform.FindChild("Offset/Top/Drag/LimitText/Value").GetComponent<Text>();
		autoSkillToggle = transform.FindChild("Offset/Top/AutoSkill/Toggle").GetComponent<Toggle>();
		autoSkillToggle.onValueChanged.AddListener(SetAutoSkill);
	}

	public void SetAutoSkill(bool isAuto)
	{
		autoInvokeSkill = isAuto;
	}

	public void SetDragCount(int count)
	{
		dragCountText.text = string.Format("x{0}",count);
	}

	public void SetDragLimit(float time)
	{
		if(time <= 0)
		{
			dragLimitText.text = "-";
		}
		else
		{
			dragLimitText.text = string.Format("{0:f2}",time);
		}
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
