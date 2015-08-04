using UnityEngine;
using System.Collections;

public class DestroyGameObject : MonoBehaviour 
{
	public float destroyDelay;

	void Start()
	{
		Destroy(this.gameObject,destroyDelay);
	}

}
