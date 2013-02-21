using UnityEngine;
using System.Collections;

public class PauseMenuScreen : MonoBehaviour {
	
	private GUIStyle menuStyle;
	private Game myGame;
	private UserInterface ui;
	private GameObject playerObj;
	
	// Use this for initialization
	void Start () {
		myGame = (Game)GameObject.Find ("Game").GetComponent<Game>();
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		ui = (UserInterface)GameObject.Find ("UserInterface").GetComponent<UserInterface>();
		playerObj = GameObject.Find ("Player");
		
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){

			menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
			menuStyle.alignment = TextAnchor.MiddleCenter;
			if (GUI.Button (new Rect(0,Screen.height * 0.25f, Screen.width, Screen.height * 0.1f),"Resume",menuStyle)){
				ui.pause = false;
				playerObj.GetComponent<MouseLook> ().enabled = true;
				Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
				Destroy (this.gameObject.GetComponent<PauseMenuScreen>());
			}
			if (GUI.Button (new Rect(0,Screen.height * 0.35f, Screen.width, Screen.height * 0.1f),"Main Menu",menuStyle)){
				myGame.SaveCharData ();
				Application.LoadLevel ("Start");
			}
			if (GUI.Button (new Rect(0,Screen.height * 0.45f, Screen.width, Screen.height * 0.1f),"Exit",menuStyle)){
				myGame.SaveCharData ();
				Application.Quit();	
			}

		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Pause Close");
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Destroy (this.gameObject.GetComponent<PauseMenuScreen> ());
		}	
	}
}
