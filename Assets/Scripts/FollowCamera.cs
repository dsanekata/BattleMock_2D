using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FollowCamera : MonoBehaviour 
{
	const float FOLLOW_SMOOTHING = 0.5f;

	Vector3 offset;
	Transform targetTransform;
	public List<GameObject> bgList = new List<GameObject>();

	void SetPositionInitialized()
	{
		offset = transform.position - targetTransform.position;
	}
		
	void Start()
	{
		targetTransform = BattleManager.GetInstance().armiesList[0].transform;
		SetPositionInitialized();
	}

	void LateUpdate()
	{
		UpdateCameraPosition();
	}

	public void UpdateCameraPosition()
	{
		if(targetTransform == null)
		{
			return;
		}

		Follow();
	}

	/// <summary>
	/// カメラを味方戦術機に追従させる
	/// </summary>
	private void Follow()
	{
		float cameraX = Mathf.Lerp(transform.position.x,targetTransform.position.x+offset.x,FOLLOW_SMOOTHING * Time.deltaTime);

		transform.position = new Vector3(cameraX,transform.position.y, transform.position.z);
	}
}
