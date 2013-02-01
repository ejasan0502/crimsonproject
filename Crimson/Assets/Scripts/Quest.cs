using UnityEngine;
using System.Collections;

public struct Quest {
	public string name;
	public string description;
	public string objective;
	public int amount;
	
	public Quest(string n, string d, string o, int a){
		name = n;
		description = d;
		objective = o;
		amount = a;
	}
}
