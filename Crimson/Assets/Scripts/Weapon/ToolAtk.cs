using UnityEngine;
using System.Collections;

public class ToolAtk : MonoBehaviour 
{
	public ParticleEmitter hitParticle;
	
	public bool isOnPlayer;
	
	public int range = 3;
	public float atkRate = 5.0f ;
	public int force = 20;
	public int dmg = 15;

	float nextAtk = 0;
	
	// Use this for initialization
	void Start () 
	{
		hitParticle = GetComponentInChildren<ParticleEmitter>();
		
		//emit particles when object is hit
		if (hitParticle)
		{
			hitParticle.emit = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			Fire();
		}
	}
	
	// check if player should be able to atk based on atkRate
	void Fire()
	{
		if (Time.time - atkRate > nextAtk) nextAtk = Time.time - Time.deltaTime;
		
		while (nextAtk < Time.time)
		{
			Attack();
			nextAtk += atkRate;
		}
	}
	
	
	void Attack ()
	{
		// swing weapon
		animation.Play();
		
		//check if weapon hits
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		
		if (Physics.Raycast (transform.position, direction, out hit, range))
		{
			Debug.Log(hit);
			// apply force to object hit
			if (hit.rigidbody) hit.rigidbody.AddForceAtPosition(force * direction, hit.point);
			
			// spawn particles at position hit
			if (hitParticle)
			{
				hitParticle.transform.position = hit.point;
				hitParticle.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				hitParticle.Emit();
			}
				
			// damage the object
			hit.collider.SendMessageUpwards("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
		}
	}
}
