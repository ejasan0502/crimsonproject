using UnityEngine;
using System.Collections;

public struct Quest {
	public string name;
	public int level;
	public string description;
	public string objective;
	public int amount;
	
	public Quest(string n, int l, string d, string o, int a){
		name = n;
		level = l;
		description = d;
		objective = o;
		amount = a;
	}
}
