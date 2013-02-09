using UnityEngine;
using System.Collections;

// IN GAME: Use key 1
// summons a basic turret to fight for the player
public class SummonTurret : MonoBehaviour 
{
	public GameObject tur;
	public float cooldown = 15;
	float useTime;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > (useTime + cooldown))
		{
			useTime = Time.time;
			Instantiate(tur, transform.position, Quaternion.identity);
		}
	}
		
	// set useTime so skill can be used right away
	void Awake ()
	{
		tur.tag = "FriendlyRobot";
		useTime = Time.time - cooldown;
	}
}
