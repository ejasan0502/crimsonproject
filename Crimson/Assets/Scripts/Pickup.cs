using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	private Game myGame;
	private Character player;
	private SoundManager sm;
	
	// Use this for initialization
	void Start () {
		myGame = GameObject.Find ("Game").GetComponent<Game>();
		player = myGame.GetPlayerChar ();
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnControllerColliderHit(ControllerColliderHit other){
		switch(other.gameObject.tag){
		case "Health 30":
			Debug.Log ("Obtained Health Pack 30");
			Destroy(other.gameObject);
			break;
		case "Health 50":
			Debug.Log ("Obtained Health Pack 50");
			Destroy(other.gameObject);
			break;
		case "Health 75":
			Debug.Log ("Obtained Health Pack 75");
			Destroy(other.gameObject);
			break;
		case "Damage":
			Debug.Log ("Obtained Damage Booster 10");
			Destroy(other.gameObject);
			break;
		case "Speed":
			Debug.Log ("Obtained Speed Booster 10");
			Destroy(other.gameObject);
			break;
		case "Stamina 30":
			Debug.Log ("Obtained Stamina Pack 30");
			Destroy(other.gameObject);
			break;
		case "Stamina 50":
			Debug.Log ("Obtained Stamina Pack 50");
			Destroy(other.gameObject);
			break;
		case "Stamina 75":
			Debug.Log ("Obtained Stamina Pack 75");
			Destroy(other.gameObject);
			break;
		case "Equip":
			Debug.Log ("Obtained " + this.gameObject.name);
			Destroy(other.gameObject);
			break;
		}
	}
}
