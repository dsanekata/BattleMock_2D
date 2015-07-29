using UnityEngine;
using System.Collections;

public class EnemyBaseController : BaseController
{
	public void Init()
	{
		base.Initialize();
		InitCharacterParams();
	}

	protected virtual void InitCharacterParams()
	{
		this.characterParam.attackRange = 2f;
	}

	protected override void FindTarget ()
	{
		float distance = 0f;

		for (int i=0; i< BattleManager.GetInstance().armiesList.Count; i++)
		{
			if (BattleManager.GetInstance().armiesList[i] == null || BattleManager.GetInstance().armiesList[i].gameObject == null) continue;

			float tempDistance = Vector3.Distance(BattleManager.GetInstance().armiesList[i].transform.position, transform.position);
			if (distance == 0 || tempDistance < distance)
			{
				target = BattleManager.GetInstance().armiesList[i];
				distance = tempDistance;
			}
		}
	}
}
