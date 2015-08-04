using UnityEngine;
using System.Collections.Generic;

public class SkillRangeSector : MonoBehaviour 
{
 	List<ArmyBaseController> targetList = new List<ArmyBaseController>();
	SpriteRenderer rangeSp;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Enter:"+other);
		if(other.tag == "Player" && other.GetComponent<ArmyBaseController>() != null)
		{
			if(!targetList.Contains(other.GetComponent<ArmyBaseController>()))
			{
				targetList.Add(other.gameObject.GetComponent<ArmyBaseController>());
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Exit:"+other);
		if(other.tag == "Player" && other.GetComponent<ArmyBaseController>() != null)
		{
			targetList.Remove(other.gameObject.GetComponent<ArmyBaseController>());
		}
	}

	public void InitSector()
	{
		rangeSp = GetComponent<SpriteRenderer>();
	}

	public List<ArmyBaseController> GetTargetList()
	{
		return targetList;
	}

	public void UpdateSector()
	{
		if(!gameObject.activeSelf)
		{
			return;
		}

		rangeSp.color = new Color(1,1,1,(Mathf.PingPong(Time.time * 0.3f,0.3f)));
	}

	public void ClearTargets()
	{
		targetList.Clear();
	}
}
