using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryWindow : MonoBehaviour
{
	private GUIStyle menuStyle;
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	public Vector2 scrollPosition;
	public Rect r;
	
	// tooltip variables
	string tooltip = "";
	
	// inventory variables
	bool displayInv;
	string[] items;
	int invRows = 6;
	int invCols = 5;
	
	// Button Variables
	public float btnHeight = 40;
	public float btnWidth = 40;
	
	void Awake ()
	{
		r = new Rect (Screen.width * 0.3f, Screen.height * 0.1f, Screen.width * 0.3f, Screen.height * 0.75f);
	}
	
	// Use this for initialization
	void Start ()
	{
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find ("Player");
		
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I)) 
		{
			// show/hide inventory
			displayInv = !displayInv;
			
			// unlock/lock mouse
			Screen.lockCursor = !displayInv;
			playerObj.GetComponent<MouseLook> ().enabled = !displayInv;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = !displayInv;
		}
	}
	
	void OnGUI ()
	{
		if (displayInv == true)
			r = GUI.Window (2, r, window, "Inventory");	
		
		DisplayTooltip();
	}
	
	void window (int windowID)
	{
		int cnt = 0;
		
		for (int y=0; y<invRows; y++)
		{
			for (int x=0; x<invCols; x++)
			{
				if (cnt < Character.Inventory.Count)
				{
					GUI.Button(new Rect(20+ (x*btnWidth), 20+ (y*btnHeight), btnWidth, btnHeight), new GUIContent(Character.Inventory[cnt].Name, Character.Inventory[cnt].Tooltip()));
				}
				else
				{
					GUI.Label(new Rect(20+ (x*btnWidth), 20+ (y*btnHeight), btnWidth, btnHeight), (x + y * invCols).ToString(), "box");
				}
				
				cnt++;
			}
		}
		
		// Money
		menuStyle.fontSize = Mathf.RoundToInt (r.height * 0.05f);
		menuStyle.alignment = TextAnchor.MiddleRight;
		GUI.Label (new Rect (r.width * 0.5f, r.height * 0.88f, r.width * 0.5f - 6, r.height * 0.1f), 
						"$ " + player.money, 
						menuStyle);
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);
		
		SetTooltip();
		GUI.DragWindow (rec);
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
