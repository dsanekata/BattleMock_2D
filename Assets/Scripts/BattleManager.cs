using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	BattleService service = null;
	CharacterParameterModel[] armyModels = null;

	public List<ArmyBaseController> armiesList = new List<ArmyBaseController>();
	public List<EnemyBaseController> enemiesList = new List<EnemyBaseController>();

	void Awake()
	{
		InitBattleManager();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	void InitBattleManager()
	{
		armiesList.Clear ();
		enemiesList.Clear ();

		Transform armiesParent = this.transform.root.FindChild ("Armies");
		Transform enemiesParent = this.transform.root.FindChild ("Enemies");

		foreach(Transform child in armiesParent)
		{
			armiesList.Add (child.GetComponent<ArmyBaseController> ());
		}

		foreach(Transform child in enemiesParent)
		{
			enemiesList.Add (child.GetComponent<EnemyBaseController> ());
		}

		service = new BattleService ();

		InitArmies();
		InitEnemies();
	}

	void InitArmies()
	{
		int index = 0;

		CharacterParameterModel[] modelList = service.GetArmyModelList();
		foreach(var army in armiesList)
		{
			army.Initialize(modelList[index]);
			index++;
			if(index > modelList.Length - 1)
			{
				index = 0;
			}
		}
	}

	void InitEnemies()
	{
		int index = 0;
		CharacterParameterModel[] modelList = service.GetEnemyModelList();

		foreach(var enemy in enemiesList)
		{
			enemy.Initialize(modelList[index]);
			index++;

			if(index > modelList.Length - 1)
			{
				index = 0;
			}
		}
	}

	public void RemoveEnemy(EnemyBaseController enemy)
	{
		if(!enemiesList.Contains(enemy))
		{
			return;
		}

		List<EnemyBaseController> newList = enemiesList;
		newList.Remove (enemy);
		enemiesList = newList;
	}
		
}
