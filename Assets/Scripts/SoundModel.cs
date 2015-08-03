using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundModel 
{
	public Dictionary<string,AudioClip> bgmDict { get; set;}

	public Dictionary<string,AudioClip> clipDict { get; set;}

	public float bgmVolume{ get; private set;}

	public float seVolume{ get; private set;}

	public SoundModel ()
	{	
		float bgmVol = 0.5f;
		float seVol = 0.5f;

		bgmDict = new Dictionary<string, AudioClip>();
		clipDict = new Dictionary<string, AudioClip>();

		SetBGMVolume (bgmVol);
		SetSEVolume (seVol);
	}

	//BGM
	public void SetBGMVolume (float vol)
	{
		bgmVolume = vol;
	}

	//SE
	public void SetSEVolume (float vol)
	{
		seVolume = vol;
	}
}
