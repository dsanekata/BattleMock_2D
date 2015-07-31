using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Army : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int id;
		public int maxHp;
		public int attack;
		public int defence;
		public float speed;
		public float attackRange;
		public float attackInterval;
	}
}
