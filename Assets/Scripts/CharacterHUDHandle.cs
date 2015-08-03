using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterHUDHandle : MonoBehaviour 
{
	GameObject offset;

	Slider hpSlider = null;
	Slider spSlider = null;

	Text hpText = null;
	Text spText = null;

	Image disableMask = null;
	Button iconButton = null;

	CanvasGroup iconMask = null;
	bool canSkill = false;
	float elapsedTime = 0f;

	public void Init(float maxHp, float maxSp)
	{
		offset = transform.FindChild("Offset").gameObject;

		hpSlider = transform.FindChild("Offset/Gauges/HpGauge").GetComponent<Slider>();
		spSlider = transform.FindChild("Offset/Gauges/SkillGauge").GetComponent<Slider>();

		hpText = transform.FindChild("Offset/Texts/HPText").GetComponent<Text>();
		spText = transform.FindChild("Offset/Texts/SPText").GetComponent<Text>();
		disableMask = transform.FindChild("Offset/Disable").GetComponent<Image>();
		iconButton = transform.FindChild("Offset/Icon").GetComponent<Button>();

		iconMask = iconButton.transform.FindChild("Mask").GetComponent<CanvasGroup>();
		SetMask(false);

		hpSlider.maxValue = maxHp;
		hpSlider.value = maxHp;

		spSlider.maxValue = maxSp;
		spSlider.value = 0;

		hpText.text = FormatSlashValue(maxHp,maxHp);
		spText.text = FormatSlashValue(0,maxSp);

		offset.SetActive(true);
	}

	public void ChangeHpValue(float current)
	{
		hpSlider.value = current;
		hpText.text = FormatSlashValue(current,hpSlider.maxValue);
	}

	public void ChangeSpValue(float current)
	{
		spSlider.value = current;
		spText.text = FormatSlashValue(current,spSlider.maxValue);
	}

	public void SetMask(bool isEnable)
	{
		disableMask.enabled = isEnable;
		if(isEnable)
		{
			canSkill = false;
		}
	}

	string FormatSlashValue(float current,float max)
	{
		return string.Format("{0}/{1}",current,max);
	}

	public void RegisterIconClickEvent(System.Action callback)
	{
		iconButton.onClick.RemoveAllListeners();
		iconButton.onClick.AddListener(()=>{callback();});
	}

	public void SetCanSkill(bool canSkill)
	{
		this.canSkill = canSkill;
	}

	public void UpdateUI()
	{
		if(!canSkill)
		{
			iconMask.alpha = 0;
			return;
		}

		iconMask.alpha = Mathf.PingPong(Time.time * 0.3f,0.3f);
	}
}
