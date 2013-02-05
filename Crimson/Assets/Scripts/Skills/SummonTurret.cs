using UnityEngine;
using System.Collections;

// IN GAME: Use key 1
// summons a basic turret to fight for the player
public class SummonTurret : MonoBehaviour 
{
	public GameObject tur;
	public bool hasSkill;
	public float cooldown = 15;
	float useTime;
	
	// Update is called once per frame
	void Update () 
	{
		tur.tag = "FriendlyRobot";
		if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > (useTime + cooldown))
		{
			useTime = Time.time;
			Instantiate(tur, transform.position, Quaternion.identity);
		}
	}
		
	// set useTime so skill can be used right away
	void Awake ()
	{
		useTime = Time.time - cooldown;
	}
}
