using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTap(TapGesture tap)
	{
		Debug.Log (tap.StartSelection);
	}
}
