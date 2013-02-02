using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public GameObject explosion;
	
	float creationTime = Time.time;
	
	float explRadius = 5.0f;
	float explPower = 10.0f;
	float explDamage = 100;
	float explTime = 1.0f;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > creationTime+5)
		{
			Destroy(gameObject);
			
			Instantiate(explosion, transform.position, transform.rotation);
			
			Explode ();
		}
	}
	
	void Awake ()
	{
		creationTime = Time.time;
	}
	
	void Explode () 
	{
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
			
				double hitPoints = 1.0 - Mathf.Clamp01(dis/explRadius);
				hitPoints *= explDamage;
			
				// apply damage to hit object
				hit.SendMessageUpwards("ApplyDamage", hitPoints, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
