using UnityEngine;
using System.Collections;

public class SentryGun : MonoBehaviour 
{
	public float range;
	public float atkAngle;
	GameObject target;
	public bool targetIsPlayer;
	GameObject player;
	
	// set target to player
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
	}
	
	// check if target is in range/sight and if so fire
	void Update () 
	{
		// update target
		target = null;
	
		// if the object targets the player and can see them target player
		if (targetIsPlayer == true && CanSeeTarget(player))
		{
			target = GameObject.FindWithTag("Player");
			Debug.Log(target.tag + " Targeted!");
		}
		// if the object targets the player, but cant see the player, target his robots
		else if (targetIsPlayer == true && !CanSeeTarget(player))
		{
			if (GameObject.FindWithTag("FriendlyRobot"))
				target = GameObject.FindWithTag("FriendlyRobot");
		}
		else if (targetIsPlayer == false)
		{
			if (GameObject.FindWithTag ("Enemy"))
			{
				target = GameObject.FindWithTag("Enemy");
				Debug.Log(target.tag + " Targeted!");
			}
			else if (GameObject.FindWithTag ("EnemyRobot"))
				target = GameObject.FindWithTag("EnemyRobot");
		}
		
		// check if we have a target
		if (target == null) return;
		// check if target is in sight to fire
		if (!CanSeeTarget(target)) return;
	
		// Rotate towards target	
		Vector3 targetPoint = target.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		// start firing when close to the correct angle
		var forward = transform.TransformDirection(Vector3.forward);
		var targetDir = target.transform.position - transform.position;
		if (Vector3.Angle(forward, targetDir) < atkAngle)
			SendMessage("Fire");
		
		
	}
	
	// check if the target is in sight
	bool CanSeeTarget (GameObject tar)
	{
		if (Vector3.Distance(transform.position, tar.transform.position) > range)
			return false;
		
		RaycastHit hit;
		if (Physics.Linecast (transform.position, tar.transform.position, out hit))
			return hit.transform == tar.transform;

		return false;
	}
}
