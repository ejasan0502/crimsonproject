using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	// This class detects any object that collides with the player.
	// This class generates a crosshair
	
	
	private Game myGame;
	private Character player;
	private SoundManager sm;
	
	// Use this for initialization
	void Start () {
		myGame = GameObject.Find ("Game").GetComponent<Game>();
		player = myGame.GetPlayerChar ();
		sm = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		lockMouse = 0;
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect(Screen.width/2 - 25,Screen.height/2 - 25,50,50),(Texture2D)Resources.Load ("Textures/crosshair"));	
	}
	
	void OnControllerColliderHit(ControllerColliderHit other){
		switch(other.gameObject.tag){
		case "Health 30":
			Debug.Log ("Obtained Health Pack 30");
			if (player.health + 30 > player.healthMax){
				player.health = player.healthMax;	
			} else {
				player.health += 30;	
			}
			Destroy(other.gameObject);
			break;
		case "Health 50":
			Debug.Log ("Obtained Health Pack 50");
			if (player.health + 50 > player.healthMax){
				player.health = player.healthMax;	
			} else {
				player.health += 50;	
			}
			Destroy(other.gameObject);
			break;
		case "Health 75":
			Debug.Log ("Obtained Health Pack 75");
			if (player.health + 75 > player.healthMax){
				player.health = player.healthMax;	
			} else {
				player.health += 75;	
			}
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
			if (player.stamina + 30 > player.staminaMax){
				player.stamina = player.staminaMax;	
			} else {
				player.stamina += 30;	
			}
			Destroy(other.gameObject);
			break;
		case "Stamina 50":
			Debug.Log ("Obtained Stamina Pack 50");
			if (player.stamina + 50 > player.staminaMax){
				player.stamina = player.staminaMax;	
			} else {
				player.stamina += 50;	
			}
			Destroy(other.gameObject);
			break;
		case "Stamina 75":
			Debug.Log ("Obtained Stamina Pack 75");
			if (player.stamina + 75 > player.staminaMax){
				player.stamina = player.staminaMax;	
			} else {
				player.stamina += 75;	
			}
			Destroy(other.gameObject);
			break;
		case "Equip":
			Debug.Log ("Obtained " + other.gameObject.name);
			player.inventory.Add (other.gameObject.name);
			Destroy(other.gameObject);
			break;
		case "Marksman":
			Debug.Log ("Marksman");
			GameObject.Find ("Rifle1").GetComponent<Weapon_Equip>().EquipWeapon("Rifle1");
			Destroy(other.gameObject);
			break;
		case "Engineer":
			Debug.Log ("Engineer");
			GameObject.Find ("Pistol1").GetComponent<Weapon_Equip>().EquipWeapon("Pistol1");
			Destroy(other.gameObject);
			break;
		case "Wall":
			break;
		case "TutorialPortal":
			GameObject.Find ("TutorialGuide").GetComponent<NPC_TutorialGuide>().entrance = true;
			break;
		}
	}
}
