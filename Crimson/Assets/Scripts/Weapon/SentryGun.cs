using UnityEngine;
using System.Collections;

public class SentryGun : MonoBehaviour 
{
	public float range;
	public float atkAngle;
	Transform target;
	
	// set target to player
	void Start () 
	{
		if (target == null && GameObject.FindWithTag("Player"))
		target = GameObject.FindWithTag("Player").transform;
	}
	
	// check if player is in range/sight and if so fire
	void Update () 
	{
		if (target == null)
			return;
	
		if (!CanSeeTarget ())
			return;
	
		// Rotate towards target	
		Vector3 targetPoint = target.position;
		Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		// start firing when close to the correct angle
		var forward = transform.TransformDirection(Vector3.forward);
		var targetDir = target.position - transform.position;
		if (Vector3.Angle(forward, targetDir) < atkAngle)
			SendMessage("Fire");
	}
	
	// check if the player is in sight
	bool CanSeeTarget ()
	{
		if (Vector3.Distance(transform.position, target.position) > range)
			return false;
		
		RaycastHit hit;
		if (Physics.Linecast (transform.position, target.position, out hit))
			return hit.transform == target;

		return false;
	}
}
