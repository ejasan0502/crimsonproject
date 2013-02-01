using UnityEngine;
using System.Collections;

public struct Skill  {
	public string name;
	public int level;
	public float damage;
	public float effect;
	public string description;
	
	public Skill(string n, int l, float d, float e, string desc){
		name = n;
		level = l;
		damage = d;
		effect = e;
		description = desc;
	}
}
