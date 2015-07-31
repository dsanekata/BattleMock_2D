using UnityEngine;
using System.Collections;
using System.Linq;

public class ArmyBaseController : BaseController 
{

	protected override void FindTarget ()
	{
		float distance = 0f;

		for (int i=0; i< BattleManager.GetInstance().enemiesList.Count; i++)
		{
			if (BattleManager.GetInstance ().enemiesList [i] == null ||
			    BattleManager.GetInstance ().enemiesList [i].gameObject == null ||
			    BattleManager.GetInstance ().enemiesList [i].isDead) {
				continue;
			}

			float tempDistance = Vector3.Distance(BattleManager.GetInstance().enemiesList[i].transform.position, transform.position);
			if (distance == 0 || tempDistance < distance)
			{
				target = BattleManager.GetInstance().enemiesList[i];
				distance = tempDistance;
			}
		}
	}

}
