using UnityEngine;
using System.Collections;

public class MainMenuScreen : MonoBehaviour {
	
	private GUIStyle menuStyle;
	private SoundManager sm;
	
	// Use this for initialization
	void Start () {
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.1f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){		
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.15f);	
		GUI.Label(new Rect(Screen.width * 0.20f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.2f),"Crimson",menuStyle);
		
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.7f, Screen.width * 0.2f, Screen.height * 0.1f),"Play",menuStyle)){
			//Application.LoadLevel ("Game");	
			sm.playSound (0);
		}
		
		if (GUI.Button(new Rect(Screen.width * 0.23f, Screen.height * 0.7f, Screen.width * 0.2f, Screen.height * 0.1f),"Settings",menuStyle)){
			sm.playSound (0);
			Instantiate (Resources.Load ("Prefabs/Settings Menu"));
			DestroyImmediate (this.gameObject);
		}
		
		if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.7f, Screen.width * 0.20f, Screen.height * 0.1f), "Credits",menuStyle)){
			sm.playSound (0);
		}
		
		if (GUI.Button(new Rect(Screen.width * 0.83f, Screen.height * 0.7f, Screen.width * 0.20f, Screen.height * 0.1f), "Exit", menuStyle)){
			sm.playSound (0);
			Application.Quit();	
		}
	}
}
