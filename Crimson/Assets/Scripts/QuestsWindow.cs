using UnityEngine;
using System.Collections;

public class QuestsWindow : MonoBehaviour {

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
		r = GUI.Window (1, r, window, "Quests");	
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		
		if (Input.GetKeyDown (KeyCode.T)) {
			Debug.Log ("Quests Close");
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Destroy (this.gameObject.GetComponent<QuestsWindow> ());
		}
	}
	
	void window (int windowID)
	{
		// Inventory
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		for (int i = 0; i < myGame.QuestList.Count; i++) {
			GUI.Box ( new Rect (0, r.height * 0.2f * i, r.width, r.height * 0.2f),
					  ((Quest)myGame.QuestList[i]).name + "\nLevel Req: " + ((Quest)myGame.QuestList[i]).level + "\n" + ((Quest)myGame.QuestList[i]).objective + ": " +
					  ((Quest)myGame.QuestList[i]).amount);	
		}
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
	}
}
