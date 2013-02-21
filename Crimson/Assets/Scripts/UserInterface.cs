using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour
{
	
	public ArrayList list;
	public GUIStyle menuStyle;
	private Game myGame;
	private SoundManager sm;
	private PlayerReceiver player;
	private GameObject playerObj;
	public bool display;
	public bool inventory;
	public bool charWindow;
	public bool skills;
	public bool quests;
	public Texture2D background;
	public Texture2D foreground;
	public Texture2D foreground2;
	
	// Use this for initialization
	void Start ()
	{
		list = new ArrayList ();	
		
		myGame = (Game)GameObject.Find ("Game").GetComponent<Game> ();
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		player = (PlayerReceiver)GameObject.Find ("Player").GetComponent<PlayerReceiver>();
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
	}
	
	void OnGUI ()
	{
		if (display) {
			menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
			menuStyle.alignment = TextAnchor.MiddleLeft;
			GUI.Label (new Rect (0, 0, Screen.width * 0.3f, Screen.height * 0.05f), player.name, menuStyle);	
			
			Rect pos = new Rect (Screen.width * 0.0125f, Screen.height * 0.05f, Screen.width * 0.3f, Screen.height * 0.075f);
			GUI.DrawTexture (pos, background, ScaleMode.StretchToFill, true);
			GUI.DrawTexture (new Rect (pos.x, pos.y, pos.width * (player.hitPoints / player.maxHP), pos.height), foreground, ScaleMode.StretchToFill, true);
			
			//pos = new Rect (Screen.width * 0.0125f, Screen.height * 0.13f, Screen.width * 0.3f, Screen.height * 0.035f);
			//GUI.DrawTexture (pos, background, ScaleMode.StretchToFill, true);
			//GUI.DrawTexture (new Rect (pos.x, pos.y, pos.width * (player.stamina / player.staminaMax), pos.height), foreground2, ScaleMode.StretchToFill, true);
		}
		
		if (!inventory){
			if (Input.GetKeyDown (KeyCode.I)) {
				Debug.Log ("Inventory open");
				this.gameObject.AddComponent<InventoryWindow>();
				inventory = true;
			}
		} else if (GetComponent<InventoryWindow>() == null && Input.GetKeyDown (KeyCode.I)){
			inventory = false;	
		}
		
		if (!skills){
			if (Input.GetKeyDown (KeyCode.K)) {
				Debug.Log ("Skills open");
				this.gameObject.AddComponent<SkillsWindow>();
				skills = true;
			}
		} else if (GetComponent<SkillsWindow>() == null && Input.GetKeyDown (KeyCode.K)){
			skills = false;	
		}
		
		if (!quests){
			if (Input.GetKeyDown (KeyCode.T)) {
				Debug.Log ("Quests open");
				this.gameObject.AddComponent<QuestsWindow>();
				quests = true;
			}
		} else if (GetComponent<QuestsWindow>() == null && Input.GetKeyDown (KeyCode.T)){
			quests = false;	
		}
		
		if (!charWindow){
			if (Input.GetKeyDown (KeyCode.C)) {
				Debug.Log ("Character open");
				this.gameObject.AddComponent<CharacterWindow>();
				charWindow = true;
			}
		} else if (GetComponent<CharacterWindow>() == null && Input.GetKeyDown (KeyCode.C)){
			charWindow = false;	
		}
	}
}
