using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleService
{

	Entity_Army armyEntity = null;
	Entity_Members memberEntity = null;

	public BattleService()
	{
	}

	public void LoadData()
	{
		armyEntity = Resources.Load<Entity_Army>("CharacterConst");
		memberEntity = Resources.Load<Entity_Members>("UserDeckConst");
	}

	public CharacterParameterModel[] GetArmyModelList()
	{
		List<CharacterParameterModel> modelList = new List<CharacterParameterModel>();

		int index = 0;
		foreach(var item in memberEntity.sheets[0].list)
		{
			if(item.id == 0)
			{
				continue;
			}

			CharacterParameterModel paramModel = new CharacterParameterModel ();

			var model = armyEntity.sheets[0].list.Find( x => x.id == item.id);

			paramModel.id = model.id;
			paramModel.maxHp = model.maxHp;
			paramModel.attack = model.attack;
			paramModel.defence = model.defence;
			paramModel.speed = model.speed;
			paramModel.attackRange = model.attackRange;
			paramModel.attackInterval = model.attackInterval;

			modelList.Add(paramModel);

			index++;
		}

//		foreach(var model in armyEntity.sheets[0].list)
//		{
//			CharacterParameterModel paramModel = new CharacterParameterModel ();
//			paramModel.maxHp = model.maxHp;
//			paramModel.attack = model.attack;
//			paramModel.defence = model.defence;
//			paramModel.speed = model.speed;
//			paramModel.attackRange = model.attackRange;
//			paramModel.attackInterval = model.attackInterval;
//
//			modelList [index] = paramModel;
//
//			index++;
//		}

		return modelList.ToArray();
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
