using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{

	public static SoundManager GetInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<SoundManager>();
		}

		return instance;
	}

	static SoundManager instance;

	SoundService service = new SoundService();
	SoundController controller;

	void Awake()
	{
		InitSoundManager();
	}

	public void InitSoundManager()
	{
		controller = new SoundController(this,service.LoadAudio());
	}

	public void PlayBGM(string name)
	{
		controller.PlayBGM(name);
	}

	public void PlaySE(string name,bool isLoop=false)
	{
		controller.PlaySE(name,isLoop);
	}

	public void StopBGM()
	{
		controller.StopBGM();
	}

	void LateUpdate()
	{
		if(controller != null)
		{
			controller.OnLateUpdate();
		}
	}

	void OnDestroy()
	{
		instance = null;
	}

}
