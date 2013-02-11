using UnityEngine;
using System.Collections;

public class Rifle : MonoBehaviour 
{
	// if this is on player change to be based on equipped weapon
	public int range = 30;
	public float fireRate = .5f;
	public int minDmg = 4;
	public int maxDmg = 7;
	public int reloadTime = 2;
	
	// weapon variables
	public int force = 10;
	public int clipSize = 20;
	public int clips = 5;
	ParticleEmitter hitParticle;
	public Renderer muzzleFlash;
	
	int bulletsLeft = 0;
	float nextFireTime = 0;
	float m_LastFrameShot = -1;
	
	public bool IsOnPlayer;
	public bool IsAutomatic;
	
	private Weapon curWeapon;
	
	// Use this for initialization
	void Start () 
	{
		hitParticle = GetComponentInChildren<ParticleEmitter>();
		
		//emit particles when object is hit
		if (hitParticle)
		{
			hitParticle.emit = false;
		}
		bulletsLeft = clipSize;
		
		// if this is the players rifle find their equipped weapon stats
		if (IsOnPlayer)
		{
			// if player has weapon equipped get its stats, if not set to default
			if (Character.EquipWeapon != null)
			{
				range = Character.EquipWeapon.MaxRange;
				fireRate = Character.EquipWeapon.AttackSpeed;
				minDmg = Character.EquipWeapon.MinDamage;
				maxDmg = Character.EquipWeapon.MaxDamage;
				reloadTime = Character.EquipWeapon.ReloadTime;
			}
			else
			{
				range = 40;
				fireRate = .6f;
				minDmg = 1;
				maxDmg = 3;
				reloadTime = 4;
			}
			
			curWeapon = Character.EquipWeapon;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check if weapon stats are current, if not set
		if (curWeapon != Character.EquipWeapon)
		{
			range = Character.EquipWeapon.MaxRange;
			fireRate = Character.EquipWeapon.AttackSpeed;
			minDmg = Character.EquipWeapon.MinDamage;
			maxDmg = Character.EquipWeapon.MaxDamage;
			reloadTime = Character.EquipWeapon.ReloadTime;
			
			curWeapon = Character.EquipWeapon;
		}
		if (IsAutomatic == true)
		{
			if (Input.GetButton("Fire1"))
			{
				if (bulletsLeft > 0)
				{
					BroadcastMessage("Fire");
				}
			}
		}
		else
		{
			if (Input.GetButtonDown("Fire1"))
			{
				if (bulletsLeft > 0)
				{
					BroadcastMessage("Fire");
				}
			}
		}
	}
	
	void LateUpdate()
	{
		if (muzzleFlash)
		{
			//bullet shot, show muzzle flash
			if (m_LastFrameShot == Time.frameCount)
			{
				muzzleFlash.transform.localRotation = 
					Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward);
				muzzleFlash.enabled = true;
				
				if (audio)
				{
					if (!audio.isPlaying) audio.Play ();
					audio.loop = true;
				}
			}
			
			//didnt shoot disable muzzle flash
			else
			{
				muzzleFlash.enabled = false;
				
				if (audio)
				{
					audio.loop = false;
				}
			}
		}
	}
	
	void Fire()// calculates if weapon should fire based on the fire rate
	{
		if (bulletsLeft == 0) return;
		
		if (Time.time - fireRate > nextFireTime) nextFireTime = Time.time - Time.deltaTime;
		
		while (nextFireTime < Time.time && bulletsLeft != 0)
		{
			FireOneShot();
			nextFireTime += fireRate;
		}
	}
	
	void FireOneShot()//shoots a bullet
	{
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		
		//check if bullet hits
		if (Physics.Raycast (transform.position, direction, out hit, range))
		{
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
			hit.collider.SendMessageUpwards("ApplyDamage", Random.Range(minDmg, maxDmg), SendMessageOptions.DontRequireReceiver);
		}
		
		bulletsLeft--;
		
		// register that we shot so muzzleflash will enable for 1 frame
		m_LastFrameShot = Time.frameCount;
		enabled = true;
		
		// reload if out of bullets
		if (bulletsLeft == 0) StartCoroutine(Reload());
	}
	
	IEnumerator Reload()
	{
		// for for reload time then reload bullets
		yield return new WaitForSeconds(reloadTime);
		
		// check if there is another clip
		if (clips > 0)
		{
			clips--;
			bulletsLeft = clipSize;
		}
	}
	
	int GetBulletsLeft()
	{
		return bulletsLeft;
	}
}
