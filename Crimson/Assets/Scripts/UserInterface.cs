using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour
{
	
	public ArrayList list;
	public GUIStyle menuStyle;
	private Game myGame;
	private SoundManager sm;
	private Character player;
	private GameObject playerObj;
	private bool display;
	private bool inventory;
	public Rect inventoryRect;
	private bool charWindow;
	private Rect characterRect;
	private bool skills;
	private Rect skillsRect;
	private bool quests;
	private Rect questsRect;
	public Texture2D background;
	public Texture2D foreground;
	public Texture2D foreground2;
	private float num;
	
	public Vector2 inventoryScrollPos;
	public Vector2 questsScrollPos;
	public Vector2 skillsScrollPos;
	
	// Use this for initialization
	void Start ()
	{
		list = new ArrayList ();	
		
		myGame = (Game)GameObject.Find ("Game").GetComponent<Game> ();
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find ("Player");
		
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		background = new Texture2D (1, 1, TextureFormat.RGB24, false);
		foreground = (Texture2D)Resources.Load ("Textures/CrimsonSwirl");
		foreground2 = (Texture2D)Resources.Load ("Textures/EmeraldSwirl");
		
		background.SetPixel (0, 0, Color.black);
		
		background.Apply ();
		
		display = true;
		inventory = false;
		charWindow = false;
		skills = false;
		quests = false;
		inventoryRect = new Rect (Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.8f);
		characterRect = new Rect (0, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.8f);
		skillsRect = new Rect (Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.8f);
		questsRect = new Rect (0, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.8f);
		
		num = 100;
		inventoryScrollPos = Vector2.zero;
		questsScrollPos = Vector2.zero;
		skillsScrollPos = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
		num -= 0.05f;
	}
	
	void OnGUI ()
	{
		if (display) {
			menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
			GUI.Label (new Rect (0, 0, Screen.width * 0.3f, Screen.height * 0.05f), player.name, menuStyle);	
			
			Rect pos = new Rect (Screen.width * 0.0125f, Screen.height * 0.05f, Screen.width * 0.3f, Screen.height * 0.075f);
			GUI.DrawTexture (pos, background, ScaleMode.StretchToFill, true);
			GUI.DrawTexture (new Rect (pos.x, pos.y, pos.width * (num / 100), pos.height), foreground, ScaleMode.StretchToFill, true);
			
			pos = new Rect (Screen.width * 0.0125f, Screen.height * 0.13f, Screen.width * 0.3f, Screen.height * 0.035f);
			GUI.DrawTexture (pos, background, ScaleMode.StretchToFill, true);
			GUI.DrawTexture (new Rect (pos.x, pos.y, pos.width * (num / 100), pos.height), foreground2, ScaleMode.StretchToFill, true);
		}
		
		// Inventory
		if (Input.GetKeyDown (KeyCode.I)) {
			Debug.Log ("Inventory open");
			inventory = true;
		} else if (Input.GetKeyUp (KeyCode.I)) {
			Debug.Log ("Inventory close");	
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			inventory = false;
		}
		
		if (inventory) {
			inventoryRect = GUI.Window (0, inventoryRect, window, "Inventory");	
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		}
		
		// Character
		if (Input.GetKeyDown (KeyCode.C)) {
			Debug.Log ("Character open");
			charWindow = true;
		} else if (Input.GetKeyUp (KeyCode.C)) {
			Debug.Log ("Character close");	
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			charWindow = false;
		}
		
		if (charWindow) {
			characterRect = GUI.Window (1, characterRect, window, "Character");	
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;	
		}
		
		// Skills
		if (Input.GetKeyDown (KeyCode.K)) {
			Debug.Log ("Skills open");
			skills = true;
		} else if (Input.GetKeyUp (KeyCode.K)) {
			Debug.Log ("Skills close");	
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			skills = false;
		}
		
		if (skills) {
			skillsRect = GUI.Window (2, skillsRect, window, "Skills");	
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;	
		}
		
		// Quests
		if (Input.GetKeyDown (KeyCode.T)) {
			Debug.Log ("Quests open");
			quests = true;
		} else if (Input.GetKeyUp (KeyCode.T)) {
			Debug.Log ("Quests close");	
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			quests = false;
		}
		
		if (quests) {
			questsRect = GUI.Window (3, questsRect, window, "Quests");	
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;	
		}
	}
	
	void window (int windowID)
	{
		if (inventory){	
			// Bags
			for (int i = 0; i < 5; i++){
				GUI.Button (new Rect(	3 + ((inventoryRect.width - 6)/5) * i, 
										inventoryRect.height * 0.1f, 
										(inventoryRect.width - 6)/5, 
										inventoryRect.height * 0.1f), 
										(i + 1).ToString());
			}
			
			// Slots
			int j = 0;
			for (int i = 0; i < 25; i++){
				if (i % 5 == 0 && i > 0){
					j++;
				}
				
				GUI.Box (new Rect(	3 + ((inventoryRect.width-6)/5) * (i % 5), 
									(inventoryRect.height * 0.1f + Screen.height * 0.1f) + (Screen.height * 0.1f) * j, 
									(inventoryRect.width - 6)/5, 
									Screen.height * 0.1f), 
									"");
			}
			
			// Money
			menuStyle.fontSize = Mathf.RoundToInt (inventoryRect.height * 0.075f);
			menuStyle.alignment = TextAnchor.MiddleRight;
			GUI.Label (new Rect(inventoryRect.width * 0.5f, inventoryRect.height * 0.88f, inventoryRect.width * 0.5f - 6, inventoryRect.height * 0.1f), "$ ", menuStyle);
		}
		
		GUI.DragWindow (new Rect(0,0,Screen.width,Screen.height));
	}
}
