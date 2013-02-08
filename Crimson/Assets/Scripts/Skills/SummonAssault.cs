using UnityEngine;
using System.Collections;

// IN GAME: Use Key 2
// summons an assault robot to help the player
public class SummonAssault : MonoBehaviour 
{
	public GameObject bot;
	public float cooldown = 15;
	float useTime;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > (useTime + cooldown))
		{
			useTime = Time.time;
			Instantiate(bot, transform.position, Quaternion.identity);
		}
	}
		
	// set useTime so skill can be used right away
	void Awake ()
	{
		bot.tag = "FriendlyRobot";
		useTime = Time.time - cooldown;
	}
}
