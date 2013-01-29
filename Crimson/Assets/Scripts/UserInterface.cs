using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {
	
	public ArrayList list;
	public GUIStyle menuStyle;
	//public Game myGame;
	//public SoundManager sm;
	
	private bool display;
	
	public Texture2D background;
	public Texture2D foreground;
	public Texture2D foreground2;
	
	private float num;
	
	// Use this for initialization
	void Start () {
		list = new ArrayList();	
		
		//myGame = (Game)GameObject.Find ("Game").GetComponent<Game>();
		//sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = (Texture2D)Resources.Load ("Textures/CrimsonSwirl");
		foreground2 = (Texture2D)Resources.Load ("Textures/EmeraldSwirl");
		
		background.SetPixel(0,0,Color.black);
		
		background.Apply ();
		
		display = true;
		num = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
		num -= 0.05f;
	}
	
	void OnGUI(){
		if (display){
			//GUI.Label (new Rect(0,0,Screen.width * 0.3f, Screen.height * 0.05f), ((Character)myGame.CharacterList[myGame.CharacterSlotSelected]).name, menuStyle);	
			GUI.Label (new Rect(Screen.width * 0.0125f,0,Screen.width * 0.3f, Screen.height * 0.05f), "Name", menuStyle);
			
			Rect pos = new Rect(Screen.width * 0.0125f,Screen.height * 0.05f, Screen.width * 0.3f, Screen.height * 0.075f);
			GUI.DrawTexture(pos, background, ScaleMode.StretchToFill,true);
			GUI.DrawTexture(new Rect(pos.x,pos.y,pos.width * (num/100), pos.height), foreground, ScaleMode.StretchToFill,true);
			
			pos = new Rect(Screen.width * 0.0125f,Screen.height * 0.13f, Screen.width * 0.3f, Screen.height * 0.035f);
			GUI.DrawTexture(pos, background, ScaleMode.StretchToFill,true);
			GUI.DrawTexture(new Rect(pos.x,pos.y,pos.width * (num/100), pos.height), foreground2, ScaleMode.StretchToFill,true);
		}
	}
	
	void LateUpdate(){
			
	}
}
