using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour 
{
	public int hitPoints = 100;
	public int MAXHP = 100;
	public int xp;
	public Transform explosion;
	public Rigidbody replaceDead;
	GameObject player;
	
	
	
	
	void ApplyDamage (int damage)
	{
		// Object is already dead
		if (hitPoints <= 0) return;
		
		hitPoints -= damage;
		
		// check if object dies
		if (hitPoints <= 0 )
		{
			Detonate();
		}
		
	}
	
	void GiveHealth (int hp)
	{
		hitPoints += hp;
		Debug.Log("Health given");
		
		// set object to maxhp if over
		if (hitPoints >= MAXHP) hitPoints = MAXHP;
	}
	
	void Detonate ()
	{
		// check if player owned
		if (gameObject.tag != "FriendlyRobot")
		{
			// find and send player xp
			player = GameObject.Find("Player");
			player.SendMessage("giveXP", xp);
		}
		// destroy the object
		Destroy(gameObject);
		
		// if object includes explosion explode and do dmg to nearby objects
		if (explosion)
		{
			float explRadius = 5.0f;
			float explPower = 100.0f;
			float explDamage = 100f;
			
			Instantiate(explosion, transform.position, transform.rotation);
			
			Vector3 explPos = transform.position;
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
			
					double dmg = 1.0 - Mathf.Clamp01(dis/explRadius);
					dmg *= explDamage;
			
					// apply damage to hit object
					hit.SendMessageUpwards("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
		// replace with "dead" object
		if (replaceDead)
		{
			Rigidbody dead = (Rigidbody)Instantiate(replaceDead, transform.position, transform.rotation);
			
			// keep old objects velocity
			dead.rigidbody.velocity = rigidbody.velocity;
			dead.angularVelocity = rigidbody.angularVelocity;
		}
		
		
	}
	
	
}
