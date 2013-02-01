using UnityEngine;
using System.Collections;

public class InventoryWindow : MonoBehaviour
{
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
		r = GUI.Window (0, r, window, "Inventory");	
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		
		if (Input.GetKeyDown (KeyCode.I)) {
			Debug.Log ("Inventory Close");
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Destroy (this.gameObject.GetComponent<InventoryWindow> ());
		}
	}
	
	void window (int windowID)
	{
		// Inventory
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		for (int i = 0; i < player.inventory.Count; i++) {
			GUI.Box (new Rect (0, r.height * 0.1f * i, r.width, r.height * 0.1f),
							((string)player.inventory [i]));	
		}
			
		GUI.EndScrollView ();
			
		// Money
		menuStyle.fontSize = Mathf.RoundToInt (r.height * 0.05f);
		menuStyle.alignment = TextAnchor.MiddleRight;
		GUI.Label (new Rect (r.width * 0.5f, r.height * 0.88f, r.width * 0.5f - 6, r.height * 0.1f), 
						"$ " + player.money, 
						menuStyle);
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
	}
}
