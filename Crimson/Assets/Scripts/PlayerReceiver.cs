using UnityEngine;
using System.Collections;

public class PlayerReceiver : MonoBehaviour 
{
	public int BASE_HP;
	public int HP_SCALE;// amount of hp to gain per level
	public int BASE_XP;// base amount of xp required to level
	public int XP_SCALE;// how much more xp will be required next level. min >1
	int hitPoints;
	int curLevel = 2;
	int curXP;
	int MAX_LEVEL = 10; // set max level
	Character player;
	GameObject plr;
	int[] xpToLevel;
	
	
	// instantiate variables
	void Awake()
	{
		player = GameObject.Find("Game").GetComponent<Game>().GetPlayerChar();
		plr = GameObject.Find ("Game");
		
		//curLevel = player.level;
		curXP = player.curExp;
		
		hitPoints = BASE_HP + (curLevel * HP_SCALE);
		
		xpToLevel = new int[MAX_LEVEL];
		
		// calculate the required XP to reach each level
		xpToLevel[0] = BASE_XP;
		for (int i=1; i < MAX_LEVEL; i++)
		{
			xpToLevel[i] = xpToLevel[i-1] * XP_SCALE;
			Debug.Log("Level " + i + " = " + xpToLevel[i]);
		}
	}
	
	// gives the player xp
	public void giveXP (int amount)
	{
		Debug.Log("Player recieved xp");
		curXP += amount;
		
		// check if player has leveled
		if (curXP >= xpToLevel[curLevel])
		{
			Debug.Log("Player Leveled");
			giveLvl();
		}
	}
	
	// give the player a level
	void giveLvl ()
	{
		curLevel++;
		curXP = 0;
		
		player.level = curLevel;
		player.curExp = curXP;
		
		plr.SendMessage("SaveCharData");
	}
	
	// do damage to the player
	void damagePlayer (int dmg)
	{
		// player is already dead
		if (hitPoints <= 0) return;
		
		hitPoints -= dmg;
		
		// check if player
		if (hitPoints <= 0 )
		{
			killPlayer ();
		}
	}
	
	// player has died
	void killPlayer ()
	{
		Destroy(gameObject);
	}
}
