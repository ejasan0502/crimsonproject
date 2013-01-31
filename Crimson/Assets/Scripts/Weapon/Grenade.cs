using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour 
{
	public Rigidbody grenade;
	int speed = 15;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire3"))
		{
			if (/*Inventory.ammo_grenade*/ 1 > 0)
			{
				Rigidbody instGrenade = (Rigidbody) Instantiate(grenade, transform.position, transform.rotation);
			
				instGrenade.velocity = transform.TransformDirection(0,(speed/2),speed);	
			
				Physics.IgnoreCollision(instGrenade.collider, transform.root.collider);
				
				//Inventory.ammo_grenade--;
			}
		}
	}
}
