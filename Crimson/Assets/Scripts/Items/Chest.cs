using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour 
{
	public enum State
	{
		open, closed, inUse
	}
	
	GameObject ui;
	bool inUse;
	
	public AudioClip closeSound;
	public AudioClip openSound;
	
	public State chestState;
	
	// highlight variables
	public GameObject[] parts;
	private Color[] defaultColors;
	
	// player variables
	private GameObject playerObj;
	public float maxLootRange = 3;
	Transform myTransform;
	
	// item variables
	public List<Item> loot = new List<Item>();
	private bool used;
	
	// Use this for initialization
	void Start () 
	{
		ui = GameObject.FindGameObjectWithTag("UI");
		myTransform = transform;
		chestState = Chest.State.closed;
		
		defaultColors = new Color[parts.Length];
		
		if (parts.Length > 0)
			for (int i=0; i<defaultColors.Length; i++)
				defaultColors[i] = parts[i].renderer.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!inUse) return;
		if (playerObj == null) return;
		
		if (Vector3.Distance(myTransform.position, playerObj.transform.position) > maxLootRange)
		{
			LootWindow.chest.ForceClose();
		}
	}
	
	// highlight chest when scrolled over
	public void OnMouseEnter()
	{
		Highlight(true);
	}
	
	// remove highlight when mouse leaves chest
	public void OnMouseExit()
	{
		Highlight(false);
	}
	
	// open chest when clicked
	public void OnMouseUp()
	{
		// check if player is in range to loot
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj == null) return;
		if (Vector3.Distance(myTransform.position, playerObj.transform.position) > maxLootRange && !inUse) 
			return; 
		
		switch(chestState)
		{
			case State.open:
				chestState = Chest.State.inUse;
				ForceClose();
				break;
			
			case State.closed:
				if (LootWindow.chest != null)
				{
					LootWindow.chest.ForceClose();
				}
				chestState = Chest.State.inUse;
				StartCoroutine("OpenChest");
				break;
		}
	}
	
	// opens chest
	private IEnumerator OpenChest ()
	{
		LootWindow.chest = this;
		playerObj = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		animation.Play("OpenChest");
		audio.PlayOneShot(openSound);
		
		// checks if items have already been generated
		if (!used)
			PopulateChest(5);
		
		yield return new WaitForSeconds(animation["OpenChest"].length);
		
		chestState = Chest.State.open;
		
		ui.SendMessage("ShowLoot", SendMessageOptions.DontRequireReceiver);
	}
	
	// create items in chest
	private void PopulateChest(int x)
	{
		for (int i=0; i<x; i++)
		{
			loot.Add(ItemGenerator.Create());
		}
		
		used = true;
	}
	
	// close chest
	private IEnumerator CloseChest ()
	{
		inUse = false;
		playerObj = null;
		
		animation.Play("CloseChest");
		audio.PlayOneShot(closeSound);
		
		yield return new WaitForSeconds(animation["CloseChest"].length);
		
		chestState = Chest.State.closed;
		
		// if chest has no loot destroy it
		if (loot.Count == 0)
			Destroy(gameObject);
		
	}
	
	// force the chest closed
	public void ForceClose()
	{
		ui.SendMessage("CloseChest", SendMessageOptions.DontRequireReceiver);
		
		StopCoroutine("OpenChest");
		StartCoroutine("CloseChest");
	}
	
	// used to highlight chest
	private void Highlight(bool glow)
	{
		if (glow)
		{
			if (parts.Length > 0)
				for (int i=0; i<defaultColors.Length; i++)
					parts[i].renderer.material.SetColor("_Color", Color.yellow);
		}
		else
		{
			if (parts.Length > 0)
				for (int i=0; i<defaultColors.Length; i++)
					parts[i].renderer.material.SetColor("_Color", defaultColors[i]);
		}
	}
}
