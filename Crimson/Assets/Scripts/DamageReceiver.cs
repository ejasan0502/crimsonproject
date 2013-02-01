using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour 
{
	public int hitPoints = 100;
	float detonateDelay = 0.0f;
	public Transform explosion;
	public Rigidbody replaceDead;
	
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
	
	void Detonate ()
	{
		// destroy the object
		Destroy(gameObject);
		
		// show explosion
		if (explosion)
		{
			Instantiate(explosion, transform.position, transform.rotation);
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
