using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;
	public int maxMana;
	public int currentMana;
	
	private List<Spell> availableSpells = new List<Spell>();

	private void Awake()
	{
		availableSpells.Add(new Spell("Fireball", 10, 5));
		availableSpells.Add(new Spell("Water Splash", 8, 3));
		availableSpells.Add(new Spell("Lightning Bolt", 8, 3));
		availableSpells.Add(new Spell("Earthquake", 8, 3));
	}
	
	public List<Spell> GetAvailableSpells()
	{
		if (availableSpells == null)
		{
			Debug.LogError("availableSpells is null in Unit");
			availableSpells = new List<Spell>();
		}
		return availableSpells;
	}


	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}
