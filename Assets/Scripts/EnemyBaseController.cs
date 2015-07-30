using UnityEngine;
using System.Collections;

public class EnemyBaseController : BaseController
{
		
	protected override void FindTarget ()
	{
		float distance = 0f;

		for (int i=0; i< BattleManager.GetInstance().armiesList.Count; i++)
		{
			if (BattleManager.GetInstance ().armiesList [i] == null ||
			    BattleManager.GetInstance ().armiesList [i].gameObject == null ||
			    BattleManager.GetInstance ().armiesList [i].isDead) {
				continue;
			}

			float tempDistance = Vector3.Distance(BattleManager.GetInstance().armiesList[i].transform.position, transform.position);
			if (distance == 0 || tempDistance < distance)
			{
				target = BattleManager.GetInstance().armiesList[i];
				distance = tempDistance;
			}
		}
	}
		
	protected override void DeadStart ()
	{
		base.DeadStart ();
		BattleManager.GetInstance ().RemoveEnemy (this);
	}
}
