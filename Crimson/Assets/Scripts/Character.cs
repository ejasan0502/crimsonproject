using UnityEngine;
using System.Collections;

public struct Character
{
	public int slot;
	public string name;
	public string charClass;
		
	public Character (int s, string n, string c)
	{
		slot = s;
		name = n;
		charClass = c;
	}
}
