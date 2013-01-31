using UnityEngine;
using System.Collections;

public struct Character
{
	public int slot;
	public string name;
	public string charClass;
	public ArrayList inventory;
	public int money;
	public int level;
	public int curExp;
		
	public Character (int s, string n, string c, ArrayList i, int m)
	{
		slot = s;
		name = n;
		charClass = c;
		inventory = i;
		money = m;
		level = 1;
		curExp = 0;
	}
}
