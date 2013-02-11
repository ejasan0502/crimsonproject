using UnityEngine;
using System.Collections;

// open window with P
public class EquipWindow : MonoBehaviour 
{
	private GUIStyle menuStyle;
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	public Vector2 scrollPosition;
	public Rect r;
	
	
	
	// tooltip variables
	string tooltip = "";
	
	// Equipment varaibles
	bool displayEquip;
	
	
	// Button Variables
	public float btnHeight = 40;
	public float btnWidth = 80;
	
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
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			// show/hide inventory
			displayEquip = !displayEquip;
			
			// unlock/lock mouse
			Screen.lockCursor = !displayEquip;
			playerObj.GetComponent<MouseLook> ().enabled = !displayEquip;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = !displayEquip;
		}
	}
	
	void OnGUI ()
	{
		if (displayEquip == true)
			r = GUI.Window (3, r, window, "Equipment");	
		
		DisplayTooltip();
	}
	
	void window (int windowID)
	{
		// create weapon slot
		if (Character.EquipWeapon == null)
		{
			GUI.Label(new Rect(10, 20, btnWidth, btnHeight), "No Weapon");

		}
		else
			GUI.Button(new Rect(10, 20, btnWidth, btnHeight), new GUIContent(Character.EquipWeapon.Name, Character.EquipWeapon.Tooltip()));
		
		// set item tooltip
		SetTooltip();
		
		// make top dragable
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);
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
