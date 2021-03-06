﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class PauseManager : MonoBehaviour {
	List<PauseManager> targets = new List<PauseManager>();    // ポーズ対象のスクリプト
 
	static PauseManager _Instance;
	
    /// <summary>
    /// 
    /// </summary>
    Behaviour[] pauseBehavs = null;
 
	public GameObject[] pauseObj = null;
	
    Rigidbody[] rgBodies = null;
    Vector3[] rgBodyVels = null;
    Vector3[] rgBodyAVels = null;
 
	Rigidbody2D[] rg2dBodies = null;
    Vector2[] rg2dBodyVels = null;
    float[] rg2dBodyAVels = null;
 
	
	bool isPause = false;
	
	static public PauseManager Instance {
		get{ return _Instance; }
	}
	
	private void _SetInstance()
        {
            if (_Instance != null)
            {
                Debug.Log("cannot instantiate");
                Destroy(this.gameObject);
                return;
            }
            _Instance = this;
			DontDestroyOnLoad(this);
        }

        private void _RemoveInstance()
        {
            _Instance = null;
        }

    void Awake() {
        // ポーズ対象に追加する
		_SetInstance();
        targets.Add(this);
    }

    void OnDestory() {
        // ポーズ対象から除外する
        targets.Remove(this);
		_RemoveInstance();
    }

    void OnPause ()
	{
		if (pauseBehavs != null) {
			return;
		}
 
		// 有効なコンポーネントを取得
        pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled; });
        foreach ( var com in pauseBehavs ) {
            com.enabled = false;
        }
        
        rgBodies = Array.FindAll(GetComponentsInChildren<Rigidbody>(), (obj) => { return !obj.IsSleeping(); });
        rgBodyVels = new Vector3[rgBodies.Length];
        rgBodyAVels = new Vector3[rgBodies.Length];
        for ( var i = 0 ; i < rgBodies.Length ; ++i ) {
            rgBodyVels[i] = rgBodies[i].velocity;
            rgBodyAVels[i] = rgBodies[i].angularVelocity;
            rgBodies[i].Sleep();
        }
 
        rg2dBodies = Array.FindAll(GetComponentsInChildren<Rigidbody2D>(), (obj) => { return !obj.IsSleeping(); });

		
        	rg2dBodyVels = new Vector2[rg2dBodies.Length];
        	rg2dBodyAVels = new float[rg2dBodies.Length];
		
        for ( var i = 0 ; i < rg2dBodies.Length ; ++i ) {
            rg2dBodyVels[i] = rg2dBodies[i].velocity;
            rg2dBodyAVels[i] = rg2dBodies[i].angularVelocity;
            rg2dBodies[i].Sleep();
        }
    }
 
    void OnResume ()
	{
		Debug.Log(pauseBehavs);
		if (pauseBehavs == null) {
			return;
		}
		Debug.Log("resume");
		// ポーズ前の状態にコンポーネントの有効状態を復元
		foreach (var com in pauseBehavs) {
			com.enabled = true;
		}
        
		for (var i = 0; i < rgBodies.Length; ++i) {
			rgBodies [i].WakeUp ();
			rgBodies [i].velocity = rgBodyVels [i];
			rgBodies [i].angularVelocity = rgBodyAVels [i];
		}
        
		for (var i = 0; i < rg2dBodies.Length; ++i) {
			rg2dBodies [i].WakeUp ();
			rg2dBodies [i].velocity = rg2dBodyVels [i];
			rg2dBodies [i].angularVelocity = rg2dBodyAVels [i];
		}
 
		pauseBehavs = null;
 
		rgBodies = null;
		rgBodyVels = null;
		rgBodyAVels = null;
        
		rg2dBodies = null;
		rg2dBodyVels = null;
		rg2dBodyAVels = null;
	}
 
    /// <summary>
    /// ポーズする
    /// </summary>
	public void Pause ()
	{
		
		if (!isPause) {
			foreach (var obj in targets) {
				obj.OnPause ();
			}
			Time.timeScale = 0;
			isPause = true;
		} else {
			Resume();
		}
    }
 
    /// <summary>
    /// ポーズ解除
    /// </summary>
	public void Resume() {
		
        foreach ( var obj in targets ) {
            obj.OnResume();
        }
		Time.timeScale = 1;
		isPause = false;
    }
}
