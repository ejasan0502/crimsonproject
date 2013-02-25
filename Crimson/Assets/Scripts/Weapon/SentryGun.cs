using UnityEngine;
using System.Collections;

public class SentryGun : MonoBehaviour 
{
	public float range;
	public float atkAngle;
	public GameObject target;
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
			{
				target = GetNearest("FriendlyRobot");
			}
		}
		else if (targetIsPlayer == false)
		{
			// check if an enemy is in range and in sight
			if (GameObject.FindWithTag ("Enemy"))
			{
				target = GetNearest("Enemy");
				Debug.Log("Target:: " + target);
				// if nearest enemy is not in sight find enemyrobot
				if (!CanSeeTarget(target))
				{
					target = GetNearest("EnemyRobot");
					Debug.Log("Target Unseen, change to robots:: " + target);
				}
			}		
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
	
	private GameObject GetNearest(string tag)
	{
		float nearDistSqr = Mathf.Infinity;
		GameObject[] nearObjs = GameObject.FindGameObjectsWithTag(tag);
		GameObject target = null;
		
		// find the nearest object with tag
		foreach (GameObject obj in nearObjs)
		{
			Vector3 pos = obj.transform.position;
			float dist = (pos - transform.position).sqrMagnitude;
			
			if (dist < nearDistSqr)
			{
				target = obj;
				nearDistSqr = dist;
			}
		}
		
		return target;
	}
}
