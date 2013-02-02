using UnityEngine;
using System.Collections;

public class SkillsWindow : MonoBehaviour {

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
		r = GUI.Window (3, r, window, "Skills");	
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		
		if (Input.GetKeyDown (KeyCode.K)) {
			Debug.Log ("Skills Close");
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Destroy (this.gameObject.GetComponent<SkillsWindow> ());
		}
	}
	
	void window (int windowID)
	{
		// Skills
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		int amount = 0;
		ArrayList skills = new ArrayList();
		if (player.charClass == "Marksman"){
			amount = myGame.MarksmanSkills.Count;
			skills = myGame.MarksmanSkills;
		} else if (player.charClass == "Engineer"){
			amount = myGame.EngineerSkills.Count;
			skills = myGame.EngineerSkills;
		}
		
		for (int i = 0; i < amount; i++) {
			GUI.Box (	new Rect (0, r.height * 0.1f * i, r.width, r.height * 0.1f),
						((Skill)skills[i]).name);	
		}
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
	}
}
