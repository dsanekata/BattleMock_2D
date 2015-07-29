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
		InitArmies();
		InitEnemies();
	}

	void InitArmies()
	{
		foreach(var army in armiesList)
		{
			army.Init();
		}
	}

	void InitEnemies()
	{
		foreach(var enemy in enemiesList)
		{
			enemy.Init();
		}
	}
}
