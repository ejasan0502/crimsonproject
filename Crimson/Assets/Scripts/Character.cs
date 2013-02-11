using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Character
{
	public int slot;
	public string name;
	public string charClass;
	public float money;
	public ArrayList inventory;
	public ArrayList questsAvailable;
	public ArrayList questsCompleted;
	public ArrayList skills;
	public int level;
	public int curExp;
	public float health;
	public float healthMax;
	public float stamina;
	public float staminaMax;
	public float damage;
	
	// inventory items
	private static List<Item> inventoryList = new List<Item>();
		
	public Character (int s, string n, string c, ArrayList i, float m, ArrayList qa, ArrayList qc, ArrayList sk)
	{
		slot = s;
		name = n;
		charClass = c;
		inventory = i;
		
		money = m;
		questsAvailable = qa;
		questsCompleted = qc;
		skills = sk;
		
		level = 1;
		curExp = 0;
		health = 100;
		healthMax = 100;
		stamina = 50;
		staminaMax = 50;
		damage = 10;
	}
	
	public void SetPlayer(int l, float h, float s, float d){
		level = l;
		health = h;
		stamina = s;
		damage = d;
	}
	
	public void SetCurrent(int e, float h, float s){
		curExp = e;
		healthMax = h;
		staminaMax = s;
	}
	
	public static List<Item> Inventory
	{
		get{return inventoryList;}
	}
}
