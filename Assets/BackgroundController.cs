using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{
	public List<GameObject> bgList = new List<GameObject> ();
	public Transform cameraTransform;

	int bgIndex = 0;
	int prevIndex = 0;
	float currentBgOffset = 0f;

	// Use this for initialization
	void Start () 
	{
		currentBgOffset += BattleConst.BACKGRAOUND_WIDTH / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CheckPosition())
		{
			UpdateOffset ();
		}
	}

	void UpdateOffset()
	{
		currentBgOffset += BattleConst.BACKGRAOUND_WIDTH / 2 ;

		bgList [bgIndex].transform.localPosition = new Vector3 (currentBgOffset,bgList [bgIndex].transform.localPosition.y ,0);
		bgList [bgIndex].transform.localScale = new Vector3 (bgList [prevIndex].transform.localScale.x * -1f, 1, 1);
		prevIndex = bgIndex;
		bgIndex++;

		if(bgIndex > bgList.Count - 1)
		{
			bgIndex = 0;
		}
	}

	bool CheckPosition()
	{
		return (cameraTransform.position.x > currentBgOffset);
	}

}
