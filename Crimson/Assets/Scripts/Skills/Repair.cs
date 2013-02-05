using UnityEngine;
using System.Collections;

// IN GAME: Use key R
// heals all nearby friendly robots
public class Repair : MonoBehaviour 
{
	float healRadius = 10;
	float healAmount = 50;
	float cooldown = 30;
	float useTime;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.R) && Time.time > (useTime + cooldown))
		{
			useTime = Time.time;
			Vector3 healPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(healPos, healRadius);
		
			// check if any objects are hit
			foreach (Collider hit in colliders)
			{
				if (!hit) continue;
			
				if (hit.rigidbody && hit.tag == "FriendlyRobot") 
				{
					// apply damage to hit object
					hit.SendMessageUpwards("GiveHealth", healAmount, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.R))
			Debug.Log("Spell on cooldown");
	}
	
	void Awake()
	{
		// set useTime back 30 sec so the skill can be used right away
		useTime = Time.time - cooldown;
	}
}
