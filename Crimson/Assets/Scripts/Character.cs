using UnityEngine;
using System.Collections;

public struct Character
{
	public int slot;
	public string name;
	public string charClass;
	public ArrayList inventory;
	public int money;
	public ArrayList questsAvailable;
	public ArrayList questsCompleted;
	public ArrayList skills;
		
	public Character (int s, string n, string c, ArrayList i, int m, ArrayList qa, ArrayList qc, ArrayList sk)
	{
		slot = s;
		name = n;
		charClass = c;
		inventory = i;
		money = m;
		questsAvailable = qa;
		questsCompleted = qc;
		skills = sk;
	}
}
