using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class BattleManager : MonoBehaviour 
{
	public static BattleManager GetInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<BattleManager>();
		}

		return instance;
	}
	static BattleManager instance;
	FollowCamera followCamera = null;
	BattleService service = new BattleService();
	CharacterParameterModel[] armyModels = null;

	[SerializeField]
	GameObject gestureGo;

	[SerializeField]
	Transform uiCanvas;

	public List<ArmyBaseController> armiesList = new List<ArmyBaseController>();
	public List<EnemyBaseController> enemiesList = new List<EnemyBaseController>();
	public BattleState battleState;

	GameObject enemySrc = null;

	public int currentWaveId = 0;
	int dragCount = BattleConst.DRAG_LIMIT;
	bool canUpdate = true;

	void Awake()
	{
		InitBattleManager();
	}
		
	// Update is called once per frame
	void Update ()
	{
		if(!canUpdate)
		{
			return;
		}

		UpdateCharacters();
	}

	void LateUpdate()
	{
		if(!canUpdate)
		{
			return;
		}

		CheckRemainArmy();
		CheckRemainEnemy();
		followCamera.UpdateCameraPosition();
		CheckBattleState();
	}

	void InitBattleManager()
	{
		armiesList.Clear ();
		enemiesList.Clear ();
		followCamera = Camera.main.GetComponent<FollowCamera>();
		BattleUIManager.GetInstance().InitTransforms();
		service.LoadData();

		Transform enemiesParent = this.transform.root.FindChild ("Enemies");

		foreach(Transform child in enemiesParent)
		{
			enemiesList.Add (child.GetComponent<EnemyBaseController> ());
		}


		InitArmies();
		InitEnemies();

		Invoke("BattleStart",0.5f);
	}

	void UpdateCharacters()
	{
		for(int i = 0; i < this.armiesList.Count; i++)
		{
			armiesList[i].UpdateAction();
		}

		for(int i = 0; i < this.enemiesList.Count; i++)
		{
			enemiesList[i].UpdateAction();
		}
	}

	void CheckBattleState()
	{

		if(followCamera.IsEnemyInsideCamera(enemiesList.Where(x => x.waveId == currentWaveId).ToArray()))
		{
			battleState = BattleState.InBattle;
		}
		else
		{
			battleState = BattleState.Moving;
		}
	}

	void InitArmies()
	{
		ConfigureArmies(service.GetArmyModelList());
	}

	void InitEnemies()
	{
		int index = 0;
		CharacterParameterModel[] modelList = service.GetEnemyModelList();

		foreach(var enemy in enemiesList)
		{
			enemy.Initialize(modelList[index]);
			enemy.gameObject.SetActive(false);
			index++;

			if(index > modelList.Length - 1)
			{
				index = 0;
			}
		}
	}
		
		
	void CheckRemainArmy()
	{
		List<ArmyBaseController> newList = armiesList;

		for(int i = 0; i < newList.Count; i++)
		{
			if(armiesList[i].isDead)
			{
				newList.Remove(armiesList[i]);
			}
		}
			
		armiesList = newList;
	}

	void CheckRemainEnemy()
	{
		List<EnemyBaseController> newList = enemiesList;

		for(int i = 0; i < newList.Count; i++)
		{
			if(enemiesList[i].isDead)
			{
				newList.Remove(enemiesList[i]);
			}
		}
						
		if(newList.Where(x => x.waveId == currentWaveId).ToArray().Length == 0)
		{
			ActivateEnemies();
		}

		enemiesList = newList;
	}

	void ActivateEnemies()
	{
		currentWaveId ++;

		for(int i = 0; i < enemiesList.Count; i++)
		{
			if(enemiesList[i].waveId == currentWaveId)
			{
				enemiesList[i].gameObject.SetActive(true);
			}
		}
	}

	void ConfigureArmies(CharacterParameterModel[] modelList)
	{
		Transform armiesParent = this.transform.root.FindChild ("Armies");

		GameObject cachedObj;
		for(int i = 0; i < modelList.Length; i++)
		{
			if(modelList[i].id == 0) continue;

			cachedObj = Resources.Load<GameObject>((new StringBuilder("Army/Army_").Append(modelList[i].id.ToString())).ToString());
			ArmyBaseController army = Instantiate(cachedObj).GetComponent<ArmyBaseController>();
			army.transform.SetParent(armiesParent);
			Vector3 startPos = this.transform.root.FindChild ("StartPoints").FindChild("StartPoint_"+(i+1).ToString()).position;
			army.transform.position = startPos;
			army.Initialize(modelList[i]);
			army.SetHUDHandle(uiCanvas.FindChild("Offset/Bottom/Armies").GetChild(i).GetComponent<CharacterHUDHandle>());
			this.armiesList.Add(army);
		}

		cachedObj = null;
	}

	void BattleStart()
	{
		if(armiesList.Count == 0)
		{
			return;
		}

		followCamera.SetTarget(armiesList[0].transform);
		BattleUIManager.GetInstance().SetDragCount(dragCount);
		SoundManager.GetInstance().PlayBGM(SoundConst.BGM_BATTLE);
		InitFingerGesture ();
	}

	public void SetDraggingArmy(bool dragging)
	{
		followCamera.isDragging = dragging;
	}

	public void PlayBattle()
	{
		canUpdate = true;
		for(int i = 0; i < armiesList.Count; i++)
		{
			if(armiesList[i] != null)
			{
				armiesList[i].PlayController();
			}
		}

		for(int i = 0; i < enemiesList.Count; i++)
		{
			if(enemiesList[i] != null)
			{
				enemiesList[i].PlayController();
			}
		}
	}

	public void StopBattle()
	{
		canUpdate = false;

		for(int i = 0; i < armiesList.Count; i++)
		{
			if(armiesList[i] != null)
			{
				armiesList[i].PauseController();
			}
		}

		for(int i = 0; i < enemiesList.Count; i++)
		{
			if(enemiesList[i] != null)
			{
				enemiesList[i].PauseController();
			}
		}
	}

	public void SetTimeScale(float timeScale)
	{
		Time.timeScale = timeScale;
	}

	public void SetDefaultTimeScale()
	{
		SetTimeScale (1);
	}

	void InitFingerGesture()
	{
		if(gestureGo != null)
		{
			gestureGo.SetActive (true);
		}
	}

	void OnDrag(DragGesture drag)
	{
		
		if(drag.StartSelection == null || dragCount == 0)
		{
			return;
		}

		ArmyBaseController selected = drag.StartSelection.GetComponent<ArmyBaseController> ();

		if(!selected.canDrag)
		{
			drag.ForcedFinishDrag();
			return;
		}

		if((selected.GetCurrentState() == ActionState.Drag && drag.ElapsedTime >= BattleConst.DRAG_TIME_LIMIT) ||
			drag.Phase == ContinuousGesturePhase.Ended)
		{
			dragCount --;
			BattleUIManager.GetInstance().SetDragCount(dragCount);
			BattleUIManager.GetInstance().SetDragLimit(0);
			selected.DragEnd();
			drag.ForcedFinishDrag();
			return;
		}

		BattleUIManager.GetInstance().SetDragLimit(BattleConst.DRAG_TIME_LIMIT - drag.ElapsedTime);

		Vector3 viewPoint = Camera.main.ScreenToViewportPoint(drag.Position);

		if(viewPoint.x <= 0 || viewPoint.x >= 1)
		{
			return;
		}

		selected.DragArmy (Camera.main.ScreenToWorldPoint (drag.Position));
	}

	IEnumerator CallWaitForEndOfFrame(System.Action callback)
	{
		yield return new WaitForEndOfFrame();

		callback();
	}
}

