using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Slider manaSlider;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		
		if(manaSlider != null)
		{
			manaSlider.maxValue = unit.maxMana;
			manaSlider.value = unit.currentMana;
		}
	}
	
	public void SetMana(int mana)
	{
		if(manaSlider != null)
			manaSlider.value = mana;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
