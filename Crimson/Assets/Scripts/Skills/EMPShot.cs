using UnityEngine;
using System.Collections;

// IN GAME: Use Key 4
// shoots an EMP, if target hit is robot they get extra damage
public class EMPShot : MonoBehaviour 
{
	public float cooldown = 20;
	public float range = 50;
	public float dmg = 25;
	public float bonusScale = 1.5f;
	
	public ParticleEmitter hitParticle;
	
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
		if (Input.GetKeyDown(KeyCode.Alpha4) && Time.time > (useTime + cooldown))
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

			doDmg();
		}
	}
	
	// checks to see of object hit is a robot, does extra dmg if so
	void doDmg()
	{
		if (hit.collider.tag == "EnemyRobot")
		{
			hit.collider.SendMessageUpwards("ApplyDamage", (dmg*bonusScale), SendMessageOptions.DontRequireReceiver);
			Debug.Log("Damage: " + (dmg*bonusScale));
		}
		else
			hit.collider.SendMessageUpwards("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);

	}
}
