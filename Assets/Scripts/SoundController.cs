using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController 
{
	private const int SE_CHANNELS = 16;

	private SoundModel soundModel = null; 

	private float BgmVolume { get { return soundModel.bgmVolume; } }
	private float SeVolume { get { return soundModel.seVolume; } }
	private IEnumerator bgmFadeoutCoroutine;
	private IEnumerator bgmFadeinCoroutine;

	public Transform bgmManager = null;
	public Transform seManager = null;
	private AudioSource bgmSource = null;
	SoundManager view;

	private List<AudioSource> seSourceList = new List<AudioSource>();
	private List<AudioSource> currentFrameSeList = new List<AudioSource>();
	private List<AudioSource> loopSeList = new List<AudioSource>();


	public SoundController(SoundManager view,SoundModel model)
	{
		this.bgmManager      = view.transform.FindChild("BGMManager");
		this.seManager       = view.transform.FindChild("SEManager");
		this.bgmSource       = this.bgmManager.GetComponent<AudioSource>();

		this.view = view;
		this.soundModel = model;
		CreateSEChannels(this.soundModel.seVolume);
	}

	/// <summary>
	/// SE再生用のGameObjectを生成する
	/// </summary>
	private void CreateSEChannels(float volume)
	{
		for(int i = 0; i < SE_CHANNELS; i++)
		{
			if(this.seManager.childCount < SE_CHANNELS)
			{
				GameObject go = AddChild(this.seManager,new GameObject("SE_"+(i+1).ToString()));
				AudioSource source = go.AddComponent<AudioSource>();

				source.volume = volume;
				this.seSourceList.Add(source);
			}
		}
	}

	GameObject AddChild(Transform parent, GameObject go)
	{
		go.transform.SetParent(parent);

		return go;
	}

	/// <summary>
	/// SEの音量を設定する
	/// </summary>
	/// <param name="volume">Volume.</param>
	public void SetSeVolume(float volume)
	{
		for(int i = 0; i < SE_CHANNELS; i++)
		{
			this.seSourceList[i].volume = volume;
		}
		soundModel.SetSEVolume(volume);
	}

	/// <summary>
	/// BGMの音量を設定する
	/// </summary>
	/// <param name="volume">Volume.</param>
	public void SetBgmVolume(float volume)
	{
		this.bgmSource.volume = volume;
		soundModel.SetBGMVolume(volume);
	}

	/// <summary>
	/// BGMを再生する
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlayBGM(string name)
	{
		StopFadeCoroutine();

		if(this.bgmSource.clip != null && this.soundModel.bgmDict.ContainsKey(name))
		{
			if(name != this.bgmSource.clip.name)
			{
				this.bgmSource.clip = soundModel.bgmDict[name];
				this.bgmSource.Play();
			}
		}
		else
		{
			this.bgmSource.clip = soundModel.bgmDict[name];
			this.bgmSource.Play();
		}
	}

	/// <summary>
	/// SEを再生する
	/// </summary>
	/// <param name="clip">Clip.</param>
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	public void PlaySE(string clipName,bool isLoop=false)
	{

		//同じフレームで同じ音を再生しようとしているかチェック
		if(CheckOverlapSe(this.soundModel.clipDict[clipName]))
			return;

		for(int i = 0; i < SE_CHANNELS; i++)
		{
			//
			if(this.seSourceList[i].isPlaying)
				continue;

			this.seSourceList[i].clip = this.soundModel.clipDict[clipName];
			this.seSourceList[i].loop = isLoop;

			this.currentFrameSeList.Add(this.seSourceList[i]);

			if(isLoop)
			{
				this.loopSeList.Add(this.seSourceList[i]);
			}

			this.seSourceList[i].Play();
			break;
		}
	}

	/// <summary>
	/// 同一フレームで同じSEを再生しようとしているかチェックする
	/// </summary>
	/// <returns><c>true</c>, if overlap se was checked, <c>false</c> otherwise.</returns>
	private bool CheckOverlapSe(AudioClip clip)
	{
		for(int i=0; i < this.currentFrameSeList.Count; i++)
		{
			if(this.currentFrameSeList[i].clip == clip)
			{
				return true;
			}
		}

		return false;
	}


	public void StopBGM()
	{
		this.bgmSource.Stop();
	}

	/// <summary>
	/// SEを停止する
	/// </summary>
	/// <param name="name">Name.</param>
	public void StopSE(string name)
	{
		var newList = this.loopSeList;

		for(int i = 0; i < loopSeList.Count; i++)
		{
			if(name == newList[i].clip.name)
			{
				newList[i].Stop();
				loopSeList.Remove(newList[i]);
				return;
			}
		}
	}

	/// <summary>
	/// すべてのSEを停止する
	/// </summary>
	public void StopAllSE()
	{
		loopSeList.Clear();

		for(int i = 0; i < SE_CHANNELS; i++)
		{
			this.seSourceList[i].Stop();
			this.seSourceList[i].clip = null;
		}
	}

	public void FadeOutBgm(float duration)
	{
		bgmFadeoutCoroutine = CoFadeOut(duration);

		view.StartCoroutine(bgmFadeoutCoroutine);
	}

	public void FadeInBgm(float duration)
	{
		bgmFadeinCoroutine = CoFadeIn(duration);

		view.StartCoroutine(bgmFadeinCoroutine);
	}

	public void MuteBGM(bool isMute)
	{
		this.bgmSource.mute = isMute;
	}

	public void MuteSE(bool isMute)
	{
		for(int i = 0; i < SE_CHANNELS; i++)
		{
			this.seSourceList[i].mute = isMute;
		}
	}

	private void StopFadeCoroutine()
	{               
		if(bgmFadeoutCoroutine != null)
			view.StopCoroutine(bgmFadeoutCoroutine);

		if(bgmFadeinCoroutine != null)
			view.StopCoroutine(bgmFadeinCoroutine);

		this.bgmSource.volume = BgmVolume;
	}

	public AudioSource GetBGMSource()
	{
		return this.bgmSource;
	}

	public AudioClip GetBGMClip()
	{
		return this.bgmSource.clip;
	}


	public void OnLateUpdate()
	{
		currentFrameSeList.Clear();
	}

	public void Dispose()
	{
		this.soundModel = null;
		this.view = null;
	}

	#region Coroutine
	IEnumerator CoFadeOut(float time)
	{
		float elapsedTime = 0f;
		float volume = this.bgmSource.volume;

		while(elapsedTime <= time)
		{
			this.bgmSource.volume =  Mathf.Lerp(volume,0f,elapsedTime/time);
			yield return null;

			elapsedTime += Time.deltaTime;
		}

		this.bgmSource.volume = 0;

		bgmFadeoutCoroutine = null;
	}

	IEnumerator CoFadeIn(float time)
	{
		float elapsedTime = 0f;
		float volume = BgmVolume;

		this.bgmSource.volume = 0f;

		while(elapsedTime <= time)
		{
			this.bgmSource.volume =  Mathf.Lerp(0,volume,elapsedTime/time);
			yield return null;

			elapsedTime += Time.deltaTime;
		}

		this.bgmSource.volume = volume;

		bgmFadeinCoroutine = null;
	}

	#endregion
}
