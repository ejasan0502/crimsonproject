using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class NPC : MonoBehaviour
{
	
	public struct Item
	{
		public string name;
		public int amount;
		
		public Item (string n, int a)
		{
			name = n;
			amount = a;
		}
	}
	
	public struct Weapon
	{
		public string name;
		public int damage;
		
		public Weapon (string n, int d)
		{
			name = n;
			damage = d;
		}
	}
	
	public enum occupation
	{
		none,
		vendor,
		merchant,
		medic,
		marine
	}
	
	public string name;
	public string dialogue;
	public ArrayList selection;
	public occupation occ = occupation.none;
	private GUIStyle menuStyle;
	public Game myGame;
	private Character player;
	public GameObject playerObj;
	public Rect r;
	public bool display;
	public bool displayTradeMenu;
	public UserInterface ui;
	public Vector2 scrollPosition;
	
	public void init (string n, occupation o)
	{
		name = n;
		occ = o;
		selection = new ArrayList ();
		
		display = false;
		displayTradeMenu = false;
		
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find ("Player");
		
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		r = new Rect (0, 0, Screen.width, Screen.height * 0.5f);
		ui = GameObject.Find ("UserInterface").GetComponent<UserInterface> ();
		scrollPosition = Vector2.zero;
		
		SetNPC ();
	}
	
	private void SetNPC ()
	{
		if (occ != occupation.none) {
			switch (occ) {
			case occupation.vendor:
				dialogue = name + "\n\nHow can I help you?";
				selection.Add ("Quest");
				selection.Add ("Trade");
				selection.Add ("Exit");
				break;
			case occupation.merchant:
				dialogue = name + "\n\nWhat do you need?";
				selection.Add ("Quest");
				selection.Add ("Trade");
				selection.Add ("Exit");
				break;
			case occupation.medic:
				dialogue = name + "\n\nDo you need something?";
				selection.Add ("Quest");
				selection.Add ("Heal");
				selection.Add ("Exit");
				break;
			case occupation.marine:
				dialogue = name + "\n\nWhat do you want?";
				selection.Add ("Quest");
				selection.Add ("Exit");
				break;
			}
		}
	}
	
	public void DisplayWindow ()
	{
		r = GUI.Window (5, r, window, "");
	}
	
	public void window (int windowID)
	{
		if (occ != occupation.none && !displayTradeMenu) {
			// Icon
			switch (occ) {
			case occupation.vendor:
				GUI.Box (new Rect (0, 0, Screen.width * 0.35f, Screen.height * 0.4f), "Vendor");
				break;
			case occupation.merchant:
				GUI.Box (new Rect (0, 0, Screen.width * 0.35f, Screen.height * 0.4f), "Merchant");
				break;
			case occupation.medic:
				GUI.Box (new Rect (0, 0, Screen.width * 0.35f, Screen.height * 0.4f), "Medic");
				break;
			case occupation.marine:
				GUI.Box (new Rect (0, 0, Screen.width * 0.35f, Screen.height * 0.4f), "Marine");
				break;
			}	
		
			// Dialogue
			GUI.Label (new Rect (Screen.width * 0.4f, 0, Screen.width * 0.6f, Screen.height * 0.25f), dialogue);
		
			// Selection	
			for (int i = 0; i < selection.Count; i++) {
				if (GUI.Button (new Rect (Screen.width * 0.4f + Screen.width * 0.2f * i, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.1f), (string)selection [i])) {
					if ((string)selection [i] == "Exit") {
						playerObj.GetComponent<MouseLook> ().enabled = true;
						Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;	
						ui.display = true;
						display = false;	
					}
					
					if ((string)selection [i] == "Trade") {
						displayTradeMenu = true;	
					}
					
					if ((string)selection [i] == "Heal") {
						player.health = player.healthMax;	
						playerObj.GetComponent<MouseLook> ().enabled = true;
						Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;	
						ui.display = true;
						display = false;	
					}
				}
			}
		} else if (displayTradeMenu) {
			if (occ == occupation.vendor) {
				GUI.Box (new Rect (0, 0, r.width, r.height * 0.15f), "Trade");
				scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.15f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
				for (int i = 0; i < myGame.VendorList.Count; i++) {
					GUI.Button (new Rect (0, r.height * 0.3f * i, r.width, r.height * 0.3f), ((Item)myGame.VendorList [i]).name + "\n" + ((Item)myGame.VendorList [i]).amount);	
				}
			
				GUI.EndScrollView ();
			} else if (occ == occupation.merchant) {
				GUI.Box (new Rect (0, 0, r.width, r.height * 0.15f), "Trade");
				scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.15f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
				for (int i = 0; i < myGame.WeaponsList.Count; i++) {
					GUI.Button (new Rect (0, r.height * 0.3f * i, r.width, r.height * 0.3f), ((Weapon)myGame.WeaponsList [i]).name + "\n" + ((Weapon)myGame.WeaponsList [i]).damage);	
				}
			
				GUI.EndScrollView ();
			} 
		}
	}
}
