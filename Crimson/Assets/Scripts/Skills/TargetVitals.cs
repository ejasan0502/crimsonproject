using UnityEngine;
using System.Collections;

// IN GAME: Use Key 3
// shoots a bullet that causes the target to bleed doing high dps

public class TargetVitals : MonoBehaviour 
{
	public float cooldown = 20;
	public float range = 50;
	public int minDmg = 7;
	public int maxDmg = 16;
	public int ticksOfDmg = 5;
	
	public ParticleEmitter hitParticle;
	
	int ticks;
	int dmg;
	float useTime;
	RaycastHit hit;
	
	void Start () 
	{
		useTime = Time.time - cooldown;
		
		//emit particles when object is hit
		if (hitParticle)
		{
			hitParticle.emit = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > (useTime + cooldown))
		{
			useTime = Time.time;
			shoot();
		}
	}
	
	// checks to see if skill hit anything
	void shoot ()
	{
		if (audio)
		{
			if (!audio.isPlaying) audio.Play ();
		}
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		ticks = 0;
		
		//check if skill hits
		if (Physics.Raycast (transform.position, direction, out hit, range))
		{
			// spawn particles at position hit
			if (hitParticle)
			{
				hitParticle.transform.position = hit.point;
				hitParticle.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				hitParticle.Emit();
			}

			// damage the object over time
			InvokeRepeating("doDmg", 1, 1);
		}
	}
	
	// does damage over time
	void doDmg()
	{
		if (ticks >= ticksOfDmg)
		{
			Debug.Log("Bleed Dmg Done!");
			CancelInvoke("doDmg");
			return;
		}
		// calculate amount of damage to do
		dmg = Random.Range(minDmg, maxDmg);
		Debug.Log("Bleed dmg: " + dmg);
		
		// makes sure object still exists
		if (hit.collider)
		{
			hit.collider.SendMessageUpwards("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
		}
		ticks++;
	}
}
