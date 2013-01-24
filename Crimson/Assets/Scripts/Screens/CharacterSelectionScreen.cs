using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CharacterSelectionScreen : MonoBehaviour {

	private GUIStyle menuStyle;
	
	private SoundManager sm;
	private Game myGame;
	
	// Use this for initialization
	void Start () {
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		myGame = (Game)GameObject.Find ("Game").GetComponent<Game>();
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.08f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){	
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.08f);
		GUI.Label (new Rect(Screen.width * 0.1f,0,Screen.width, Screen.height * 0.1f), "Select a Character", menuStyle);
		
		for (int i = 0; i < 3; i++){
			if (myGame.CharacterList[i] == null){
				if (GUI.Button (new Rect(Screen.width * 0.35f * i, Screen.height * 0.45f, Screen.width * 0.3f, Screen.height * 0.1f), "Select", menuStyle)){
					myGame.CharacterSlotSelected = i;
					Instantiate (Resources.Load ("Prefabs/Character Creation Menu"));
					DestroyImmediate (this.gameObject);
				}
			} else {
				string str = ((Character)myGame.CharacterList[i]).name + "\n" + ((Character)myGame.CharacterList[i]).charClass;
				menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
				if (GUI.Button (new Rect(Screen.width * 0.35f * i, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * 0.2f), str, menuStyle)){
					//Application.LoadLevel ("Game");
				}
			}
		}
	}
}
