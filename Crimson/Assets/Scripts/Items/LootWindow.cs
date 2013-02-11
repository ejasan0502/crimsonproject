using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attatch to UserInterface object
public class LootWindow : MonoBehaviour 
{
	public static Chest chest;
	
	// tooltip variables
	string tooltip = "";
	
	// loot window variables
	bool displayWindow;
	public float height = 120;
	public string windowName = "Loot Window";
	float offset = 100;
	int LOOT_WINDOW_ID = 0;
	Rect lootWindowRect = new Rect(0,0,0,0);
	
	// close button variables
	float closeHeight = 20;
	float closeWidth = 20;
		
	// Button Variables
	public float btnHeight = 40;
	public float btnWidth = 40;
	
	// slider variables
	Vector2 slider = Vector2.zero;
	
	// player variables
	GameObject playerObj;
	
	
	// Use this for initialization
	void Start () 
	{
		playerObj = GameObject.Find("Player");
	}
	
	void OnGUI()
	{
		if (displayWindow == true)
		{
			lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(offset, Screen.height - height, Screen.width-(offset*2), height), LootWin, windowName);
		}
		
		DisplayTooltip();
	}
	
	private void LootWin(int id)
	{
		if (chest == null) return;
		
		if (chest.loot.Count == 0)
		{
			ClearWindow();
			return;
		}
		
		// while window is open activate mouse
		Screen.lockCursor = false;
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		
		// create slider
		slider = GUI.BeginScrollView(new Rect(5, 40, lootWindowRect.width-30, 70), slider, new Rect(0,0,(chest.loot.Count*btnWidth)+10, btnHeight));
		
		// add items into loot window
		for (int i=0; i< chest.loot.Count; i++)
		{
			// if item is clicked add to inventory
			if (GUI.Button(new Rect(5 +(btnWidth*i), 7, btnWidth, btnHeight), new GUIContent(chest.loot[i].Name, chest.loot[i].Tooltip())))
			{
				Character.Inventory.Add(chest.loot[i]);
				chest.loot.RemoveAt(i);
				
				// make sure tooltip closes
				CloseTooltip();
			}
		}
		
		GUI.EndScrollView();
		
		SetTooltip();
		
		
		// create close window button
		if(GUI.Button(new Rect(lootWindowRect.width -30, 3, closeWidth, closeHeight), "X"))
		{
			ClearWindow();
		}	
	}
	
	private void ShowLoot()
	{
		displayWindow = true;
	}
	
	private void ClearWindow()
	{
		chest.OnMouseUp();
		
		Screen.lockCursor = true;
		playerObj.GetComponent<MouseLook> ().enabled = true;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
		
		chest = null;
		displayWindow = false;
	}
	
	public void CloseChest()
	{
		ClearWindow();
	}
	
	private void SetTooltip()
	{
		if (Event.current.type == EventType.Repaint && GUI.tooltip != tooltip)
		{
			if (tooltip != "")
				tooltip = "";
			if (GUI.tooltip != "")
				tooltip = GUI.tooltip;	
		}
			
	}
	
	private void DisplayTooltip()
	{
		if (tooltip != "")
			GUI.Box (new Rect(Screen.width/2 - 100, 10, 200, 175), tooltip);

	}
	
	private void CloseTooltip()
	{	
		tooltip = "";
	}
}
