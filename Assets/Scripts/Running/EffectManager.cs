using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {

	#region like Singleton
	static EffectManager _Instance;
	
	static public EffectManager Instance {
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
			//DontDestroyOnLoad(this);
        }

    private void _RemoveInstance()
        {
            _Instance = null;
        }
	
	void OnDestory() {
		_RemoveInstance();
    }
	
	#endregion
	
	string effectpath = RunningConst.EFFECT_PATH;
	
	void Awake () {
		_SetInstance();
	}
	
	/// <summary>
	/// エフェクトを発生させる
	/// </summary>
	/// <param name="effectName">エフェクトの名前.</param>
	/// <param name="time">発生させる時間.</param>
	/// <param name="obj">対象のゲームオブジェクト.</param>
	public void InstantiateEffect (string effectName,float time,GameObject obj)
	{
		GameObject effect = Instantiate(Resources.Load(effectpath+effectName),
										obj.transform.position,obj.transform.rotation) as GameObject;
		Destroy(effect,time);
	}
	
	/// <summary>
	/// エフェクトを発生させる（子として追加）
	/// </summary>
	/// <param name="effectName">エフェクトの名前.</param>
	/// <param name="time">発生時間.</param>
	/// <param name="obj">対象のゲームオブジェクト.</param>
	public void InstantiateEffectInChild(string effectName,float time,GameObject obj)
	{
		GameObject effect = Instantiate(Resources.Load(effectpath+effectName),
										obj.transform.position,obj.transform.rotation) as GameObject;
		effect.transform.parent = obj.transform;
		Destroy(effect,time);
	}
	

}
