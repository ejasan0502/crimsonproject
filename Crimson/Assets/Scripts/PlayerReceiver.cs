using UnityEngine;
using System.Collections;

public class PlayerReceiver : MonoBehaviour 
{
	public float BASE_HP;
	public float HP_SCALE;// amount of hp to gain per level
	public int BASE_XP;// base amount of xp required to level
	public int XP_SCALE;// how much more xp will be required next level. min >1
	public AudioClip smallDmg;
	public AudioClip bigDmg;
	public AudioClip Death;
	public float hitPoints;
	int curLevel = 2;
	int curXP;
	int MAX_LEVEL = 10; // set max level
	Character player;
	GameObject plr;
	int[] xpToLevel;
	float gotHit;
	float maxHP;
	Rect deadMsg;
	
	
	
	// instantiate variables
	void Awake()
	{
		player = GameObject.Find("Game").GetComponent<Game>().GetPlayerChar();
		plr = GameObject.Find ("Game");
		
		//curLevel = player.level;
		curXP = player.curExp;
		
		maxHP = BASE_HP + (curLevel * HP_SCALE);
		hitPoints = maxHP;
		
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
		Debug.Log(curXP + " / " + xpToLevel[curLevel]);
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
		
		maxHP = BASE_HP + (curLevel * HP_SCALE);
		hitPoints = maxHP; 
		
		plr.SendMessage("SaveCharData");
	}
	
	// do damage to the player
	void ApplyDamage (int dmg)
	{
		// player is already dead
		if (hitPoints <= 0) return;
		
		hitPoints -= dmg;
		
		// play a sound when player is hit, limit how often sound can play
		if (Time.time > gotHit && bigDmg && smallDmg) 
		{
			// Play a big pain sound
			if (dmg > 20) 
			{
				audio.PlayOneShot(bigDmg, audio.volume);
				gotHit = Time.time + Random.Range(bigDmg.length * 2, bigDmg.length * 3);
			} 
			else 
			{
				// Play a small pain sound
				audio.PlayOneShot(smallDmg, audio.volume);
				gotHit = Time.time + Random.Range(smallDmg.length * 2, smallDmg.length * 3);
			}
		}
		
		// check if player dies
		if (hitPoints <= 0 )
		{
			killPlayer ();
		}
	}
	
	// player has died
	void killPlayer ()
	{
		audio.PlayOneShot (Death, audio.volume);
		// Disable all script behaviours (Essentially deactivating player control)
		Component[] coms = GetComponentsInChildren<MonoBehaviour>();
		foreach (MonoBehaviour b in coms) 
		{
			MonoBehaviour p = b as MonoBehaviour;
			if (p)
				p.enabled = false;
		}
		StartCoroutine(reloadLevel());
	}
	
	
	// reload the level
	IEnumerator reloadLevel ()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel(Application.loadedLevel);
	}
}

