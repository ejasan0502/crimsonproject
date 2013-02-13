using UnityEngine;
using System.Collections;

public class AssaultBot : MonoBehaviour 
{
	public float moveSpeed;
	public float rotationSpeed;
	public float range;
	public float atkAngle;
	public float seekRange;
	GameObject target;
	public bool targetIsPlayer;
	GameObject player;
	float targDist;
	public bool ranged = true;
	
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
			else 
				target = GameObject.FindWithTag("Player");
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
		
		// Rotate towards target	
		Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		
		// Move towards target to point seekRange
		targDist = Vector3.Distance(transform.position, target.transform.position);
		if (targDist > seekRange && targDist < 40)
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		
		// check if target is in sight to fire
		if (!CanSeeTarget(target)) return;
		
		// start firing when close to the correct angle
		var forward = transform.TransformDirection(Vector3.forward);
		var targetDir = target.transform.position - transform.position;
		if (Vector3.Angle(forward, targetDir) < atkAngle)
		{
			SendMessage("Fire"); 
		}
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
	
	IEnumerator wait(float val){
		yield return new WaitForSeconds(val);	
	}
}
