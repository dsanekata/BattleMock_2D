using UnityEngine;
using System.Collections;

/// <summary>
/// スキル管理クラス
/// </summary>
public class SkillManager : MonoBehaviour {
	
	float dashTime = RunningConst.DASH_TIME;
	
	float magnetTime = RunningConst.MAGNET_TIME;
	
	float invincibleTime = RunningConst.INVINCIBLE_TIME;
	
	float atackTime = RunningConst.ATACK_TIME;
	
	float atackInterval = RunningConst.ATACK_INTERVAL;
	
	float boostTime = RunningConst.BOOST_TIME;
	
	public SkillDash skillDash = null;
	
	public SkillMagnet skillMagnet = null;
	
	public SkillInvincible skillInv = null; 
	
	public SkillAtack skillAtack = null;
	
	public SkillBoost skillBoost = null;
	
	public void Initialize ()
	{
		skillDash.Initialize();
		skillMagnet.Initialize();
		skillInv.Initialize();
		skillAtack.Initalize();
	}
	
	/// <summary>
	/// ダッシュを使用する
	/// </summary>
	public void UseDash()
	{
		skillDash.doDash(dashTime);
		skillMagnet.SetEnableMagnet(dashTime);
	}
	
	/// <summary>
	/// 磁石を使用する
	/// </summary>
	public void UseMagnet()
	{
		skillMagnet.SetEnableMagnet(magnetTime); 
	}
	
	public void UseInvincible()
	{
		skillInv.doInvincible(invincibleTime);
		skillMagnet.SetEnableMagnet(invincibleTime);
	}
	
	public void UseAtack()
	{
		skillAtack.doAtack(atackInterval,atackTime);
	}
	
	public void UseBoost ()
	{
		skillBoost.doBoost(boostTime);	
	}
}
