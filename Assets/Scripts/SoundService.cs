using UnityEngine;
using System.Collections;
using System.Linq;

public class SoundService 
{
	public SoundModel LoadAudio()
	{
		SoundModel model = new SoundModel();

		model.bgmDict = Resources.LoadAll<AudioClip>("Audio/BGM").ToDictionary(bgm => bgm.name);
		model.clipDict = Resources.LoadAll<AudioClip>("Audio/SE").ToDictionary(se => se.name);

		return model;
	}

}
