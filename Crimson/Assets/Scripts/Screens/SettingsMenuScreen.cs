using UnityEngine;
using System.Collections;

public class SettingsMenuScreen : MonoBehaviour {
	
	enum Settings { volume, camera, controls };
	
	private SoundManager sm;
	
	private GUIStyle menuStyle;
	private Settings settings;
	
	public float musicVolumeBar;
	public float effectsVolumeBar;
	public float mouseSensitivity;
	
	private Game myGame;
	
	// Use this for initialization
	void Start () {	
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		myGame = GameObject.Find ("Game").GetComponent<Game>();
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.1f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		settings = Settings.volume;
		
		musicVolumeBar = myGame.musicVolume;
		effectsVolumeBar = myGame.effectsVolume;
		mouseSensitivity = myGame.sensitivity;
	}
	
	// Update is called once per frame
	void Update () {
		switch(settings){
			case Settings.volume:
				sm.setBGMVolume (musicVolumeBar);
				sm.setSEVolume (effectsVolumeBar);
			break;
		}
	}
	
	void OnGUI(){
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.1f);	
		GUI.Label (new Rect(Screen.width * 0.25f, Screen.height * 0.0f, Screen.width * 0.5f, Screen.height * 0.15f),"Settings",menuStyle);
		
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		if (GUI.Button (new Rect(0, Screen.height * 0.15f, Screen.width * 0.2f, Screen.height * 0.1f), "Volume",menuStyle)){
			sm.playSound (0);
			settings = Settings.volume;	
		}
		
		if (GUI.Button (new Rect(Screen.width * 0.25f, Screen.height * 0.15f, Screen.width * 0.2f, Screen.height * 0.1f), "Camera", menuStyle)){
			sm.playSound (0);
			settings = Settings.camera;	
		}
		
		if (GUI.Button (new Rect(Screen.width * 0.4f, Screen.height * 0.8f, Screen.width * 0.4f, Screen.height * 0.1f), "Return",menuStyle)){
			sm.playSound (0);
			Instantiate (Resources.Load ("Prefabs/Main Menu"));
			DestroyImmediate (this.gameObject);
		}
		
		switch(settings){
			case Settings.volume:
				GUI.Label (new Rect(0, Screen.height * 0.25f, Screen.width * 0.2f, Screen.height * 0.1f), "Music",menuStyle);
				musicVolumeBar = GUI.HorizontalScrollbar (new Rect(Screen.width * 0.2f, Screen.height * 0.25f, Screen.width * 0.6f, Screen.height * 0.1f),musicVolumeBar,0.1f,0.0f,1.1f);
				GUI.Label (new Rect(Screen.width * 0.85f, Screen.height * 0.25f, Screen.width * 0.15f, Screen.height * 0.1f), musicVolumeBar.ToString (), menuStyle);
			
				GUI.Label (new Rect(0, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.1f), "Sound",menuStyle);
				effectsVolumeBar = GUI.HorizontalScrollbar (new Rect(Screen.width * 0.2f, Screen.height * 0.4f, Screen.width * 0.6f, Screen.height * 0.1f),effectsVolumeBar,0.1f,0.0f,1.1f);
				GUI.Label (new Rect(Screen.width * 0.85f, Screen.height * 0.4f, Screen.width * 0.15f, Screen.height * 0.1f), effectsVolumeBar.ToString (), menuStyle);
			break;
			
			case Settings.camera:
				GUI.Label (new Rect(0, Screen.height * 0.25f, Screen.width * 0.4f, Screen.height * 0.1f), "Mouse Sensitivity", menuStyle);
				GUI.Label (new Rect(Screen.width * 0.75f, Screen.height * 0.25f, Screen.width * 0.2f, Screen.height * 0.1f), mouseSensitivity.ToString (), menuStyle);
			
				if (GUI.Button (new Rect(Screen.width * 0.7f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.1f), "<", menuStyle)){
					mouseSensitivity--;
				}
			
				if (GUI.Button (new Rect(Screen.width * 0.85f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.1f), ">", menuStyle)){
					mouseSensitivity++;
				}
			break;
		}
	}
}
