using UnityEngine;
using System.Collections;

public class CharacterWindow : MonoBehaviour {

	private GUIStyle menuStyle;
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	public Vector2 scrollPosition;
	public Rect r;
	
	void Awake ()
	{
		r = new Rect (Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.75f);
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
	
	void OnGUI ()
	{
		r = GUI.Window (0, r, window, "Character");	
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		
		if (Input.GetKeyDown (KeyCode.C)) {
			Debug.Log ("Character Close");
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Destroy (this.gameObject.GetComponent<CharacterWindow> ());
		}
	}
	
	void window (int windowID)
	{
		menuStyle.fontSize = Mathf.RoundToInt (r.height * 0.05f);
		GUI.Label (new Rect(3,r.height * 0.1f, r.width - 6, r.height * 0.1f),player.name,menuStyle);
		GUI.Label (new Rect(3,r.height * 0.2f, r.width - 6, r.height * 0.1f),"Class: " + player.charClass,menuStyle);
		GUI.Label (new Rect(3,r.height * 0.3f, r.width - 6, r.height * 0.1f),"Level: " + player.level.ToString (),menuStyle);
		GUI.Label (new Rect(3,r.height * 0.4f, r.width - 6, r.height * 0.1f),"Exp: " + player.curExp.ToString (),menuStyle);
		GUI.Label (new Rect(3,r.height * 0.5f, r.width - 6, r.height * 0.1f),"Health: " + player.health.ToString () + "/" + player.healthMax.ToString (),menuStyle);
		GUI.Label (new Rect(3,r.height * 0.6f, r.width - 6, r.height * 0.1f),"Stamina: " + player.stamina.ToString () + "/" + player.staminaMax.ToString (),menuStyle);
		GUI.Label (new Rect(3,r.height * 0.7f, r.width - 6, r.height * 0.1f),"Damage: " + player.damage.ToString (),menuStyle);
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
	}
}
