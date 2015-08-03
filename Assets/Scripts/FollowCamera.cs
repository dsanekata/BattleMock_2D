using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FollowCamera : MonoBehaviour 
{
	Vector3 offset;
	Transform targetTransform;
	float smoothing;
	public List<GameObject> bgList = new List<GameObject>();

	public bool isDragging = false;

	void SetPositionInitialized()
	{
		offset = transform.position - targetTransform.position;
	}
		
	public void SetTarget(Transform target)
	{
		targetTransform = target;
		SetPositionInitialized();
	}

	public void SetTarget(BaseController target)
	{
		if(target == null)
		{
			targetTransform = null;
			return;
		}

		SetTarget(target.transform);
	}
		
	public void UpdateCameraPosition()
	{
		if(isDragging)
		{
			smoothing = BattleConst.CAMERA_FOLLWO_SMOOTHING_LOW;
		}
		else
		{
			smoothing = BattleConst.CAMERA_FOLLOW_SMOOTHING_DEFAULT;
		}	

		if(targetTransform == null || targetTransform.GetComponent<BaseController>().isDead)
		{
			SetTarget(BattleManager.GetInstance().armiesList.Find(x => x.gameObject != null && !x.isDead));
			return;
		}

		Follow();
	}

	/// <summary>
	/// カメラを味方戦術機に追従させる
	/// </summary>
	private void Follow()
	{
		float cameraX = Mathf.Lerp(transform.position.x,targetTransform.position.x+offset.x,smoothing * Time.deltaTime);

		transform.position = new Vector3(cameraX,transform.position.y, transform.position.z);
	}

	public bool IsEnemyInsideCamera(EnemyBaseController[] enemies)
	{
		bool inSide = false;

		for(int i = 0; i < enemies.Length; i++)
		{
			if(enemies[i] != null)
			{
				Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemies[i].transform.position);

				if(viewportPos.x <= 1 && viewportPos.x >= 0)
				{
					inSide = true;
					break;
				}
			}
		}

		return inSide;
	}


}
