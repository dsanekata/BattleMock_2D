using UnityEngine;
using System.Collections;

public class Starter : MonoBehaviour {

	void Start () 
	{
		SceneManager.GetInstance().LoadScene("BattlePlay");
	}
}
