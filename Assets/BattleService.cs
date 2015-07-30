using UnityEngine;
using System.Collections;

public class BattleService
{

	Entity_Army armyEntity = null;

	public BattleService()
	{
		armyEntity = Resources.Load<Entity_Army> ("CharacterConst");
	}

	public CharacterParameterModel[] GetArmyModelList()
	{
		CharacterParameterModel[] modelList = new CharacterParameterModel[armyEntity.sheets [0].list.Count];

		int index = 0;

		foreach(var model in armyEntity.sheets[0].list)
		{
			CharacterParameterModel paramModel = new CharacterParameterModel ();
			paramModel.maxHp = model.maxHp;
			paramModel.attack = model.attack;
			paramModel.defence = model.defence;
			paramModel.speed = model.speed;
			paramModel.attackRange = model.attackRange;
			paramModel.attackInterval = model.attackInterval;

			modelList [index] = paramModel;

			index++;
		}

		return modelList;
	}

	public CharacterParameterModel[] GetEnemyModelList()
	{
		CharacterParameterModel[] modelList = new CharacterParameterModel[armyEntity.sheets [1].list.Count];

		int index = 0;

		foreach(var model in armyEntity.sheets[1].list)
		{
			CharacterParameterModel paramModel = new CharacterParameterModel ();

			paramModel.maxHp = model.maxHp;
			paramModel.attack = model.attack;
			paramModel.defence = model.defence;
			paramModel.speed = model.speed;
			paramModel.attackRange = model.attackRange;
			paramModel.attackInterval = model.attackInterval;

			modelList [index] = paramModel;
			index++;
		}

		return modelList;
	}
}
