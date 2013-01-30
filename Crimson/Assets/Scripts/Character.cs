using UnityEngine;
using System.Collections;

public struct Character
{
	public int slot;
	public string name;
	public string charClass;
	public ArrayList inventory;
		
	public Character (int s, string n, string c, ArrayList i)
	{
		slot = s;
		name = n;
		charClass = c;
		inventory = i;
	}
}
