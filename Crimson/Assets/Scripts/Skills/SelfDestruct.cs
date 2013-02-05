using UnityEngine;
using System.Collections;

// IN GAME: Use key E
// self destucts the player owned robot furthest from the player
public class SelfDestruct : MonoBehaviour 
{
	public GameObject explosion;
	GameObject[] targ;
	GameObject player;
	public bool hasSkill;
	GameObject target;
	
	int closest = 0;
	float closestEnemy = 0f;
	float explRadius = 5.0f;
	float explPower = 10.0f;
	float explDamage = 100;
	
	
	// Update is called once per frame
	void Update () 
	{
		// if no target find one
		if (target == null && GameObject.FindGameObjectWithTag("FriendlyRobot"))
		{
			targ = GameObject.FindGameObjectsWithTag("FriendlyRobot");
			player = GameObject.FindGameObjectWithTag("Player");
			for (int i=0; i<targ.Length; i++)
			{
				Debug.Log("Checking length");
				float dist = Vector3.Distance(player.transform.position, targ[i].transform.position);
				if (dist > closestEnemy)
				{
					closest = i;
					closestEnemy = dist;
					Debug.Log("closest enemy: " + dist);
				}
			}
			
			target = targ[closest];
			Debug.Log("Target found" + target);
		}
		
		// if player hits e use skill
		if (Input.GetKeyDown(KeyCode.E))
		{
			Destroy(target);
			
			Instantiate(explosion, target.transform.position, target.transform.rotation);
			
			Explode ();
		}
	}
	
	void Explode()
	{
		Vector3 explPos = target.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explPos, explRadius);
		
		// check if any objects are hit
		foreach (Collider hit in colliders)
		{
			if (!hit) continue;
			
			if (hit.rigidbody) 
			{
				hit.rigidbody.AddExplosionForce(explPower, explPos, explRadius, 3.0f);
			
				// calculates damage based on distance from the center of the explosion
				Vector3 closePoint = hit.rigidbody.ClosestPointOnBounds(explPos);
				float dis = Vector3.Distance(closePoint, explPos);
			
				double hitPoints = 1.0 - Mathf.Clamp01(dis/explRadius);
				hitPoints *= explDamage;
			
				// apply damage to hit object
				hit.SendMessageUpwards("ApplyDamage", hitPoints, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
