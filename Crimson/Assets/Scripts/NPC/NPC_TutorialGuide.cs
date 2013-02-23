using UnityEngine;
using System.Collections;

public class NPC_TutorialGuide : MonoBehaviour
{
	
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	private GUIStyle menuStyle;
	private string message;
	public bool start;
	public bool WeaponAcquired;
	public bool tutorialSwitch;
	public bool entrance;
	
	// Use this for initialization
	void Start ()
	{
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find("Player");
		
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.025f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		message = "";
		start = true;
		WeaponAcquired = false;
		tutorialSwitch = false;
		entrance = false;
	}
	
	void Update ()
	{
		if (myGame.tutorial) {
			StartCoroutine (TutorialMode ());		
		}
	}
	
	void OnGUI ()
	{
		if (myGame.tutorial) {
			GUI.TextField (new Rect (Screen.width * 0.2f, Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * 0.1f), message, menuStyle);
		}
	}
	
	IEnumerator wait(float val){
		yield return new WaitForSeconds(val);	
	}
	
	IEnumerator TutorialMode ()
	{
		if (start) {
			start = false;
			message = "Rookie! We are under attack.";
			yield return new WaitForSeconds(3.0f);
			message = "Grab a weapon! \n(Rifle for Marksman or Pistol for Engineer)";
		}
		
		if (WeaponAcquired) {
			WeaponAcquired = false;
			message = "Alright lets move out!";
			yield return new WaitForSeconds(3.0f);
			message = "Rookie, find the control room and press the switch /nto seal off the facility. We'll meet you at the entrance!";
			yield return new WaitForSeconds(3.0f);
			message = "";
		}
		
		if (tutorialSwitch){
			message = "Better head to the entrance.";	
			yield return new WaitForSeconds(3.0f);
			message = "";
		}
		
		if (entrance){
			message = "You have fainted.";
			yield return new WaitForSeconds(3.0f);
			message = "Loading next scene . . .";
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;	
			myGame.SaveCharData ();
			yield return new WaitForSeconds(3.0f);
			Destroy(this.gameObject);
			myGame.tutorial = false;
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Application.LoadLevel ("testing");
		}
	}
}
