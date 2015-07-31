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

	public List<ArmyBaseController> armiesList = new List<ArmyBaseController>();
	public List<EnemyBaseController> enemiesList = new List<EnemyBaseController>();

	GameObject enemySrc = null;

	public int currentWaveId = 0;

	void Awake()
	{
		InitBattleManager();
	}
		
	// Update is called once per frame
	void Update ()
	{
		UpdateCharacters();
	}

	void LateUpdate()
	{
		CheckRemainEnemy();
	}

	void InitBattleManager()
	{
		armiesList.Clear ();
		enemiesList.Clear ();
		followCamera = Camera.main.GetComponent<FollowCamera>();

		service.LoadData();

		Transform enemiesParent = this.transform.root.FindChild ("Enemies");

		foreach(Transform child in enemiesParent)
		{
			enemiesList.Add (child.GetComponent<EnemyBaseController> ());
		}


		InitArmies();
		InitEnemies();

		BattleStart();
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
	}

}
