using UnityEngine;
using System.Collections;

public class Weapon_Equip : MonoBehaviour
{
	
	private Game myGame;
	public GameObject equippedWeapon;
	private ArrayList weaponsList;
	
	// Use this for initialization
	void Start ()
	{
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		equippedWeapon = null;
		weaponsList = myGame.WeaponsList;
		
		this.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	public void EquipWeapon (string name)
	{
		if (equippedWeapon == null) {
			Debug.Log ("Equipping Weapon");	
			if (myGame.tutorial) {
				if (name == "Rifle1") {
					GameObject.Find ("Rifle1").GetComponent<MeshRenderer> ().enabled = true;
					Destroy (GameObject.Find ("Pistol1_Pickup"));
					equippedWeapon = GameObject.Find ("Rifle1");
					GameObject.Find ("TutorialGuide").GetComponent<NPC_TutorialGuide>().WeaponAcquired = true;
				} else if (name == "Pistol1") {
					GameObject.Find ("Pistol1").GetComponent<MeshRenderer> ().enabled = true;
					Destroy (GameObject.Find ("Rifle1_Pickup"));
					equippedWeapon = GameObject.Find ("Pistol1");
					GameObject.Find ("TutorialGuide").GetComponent<NPC_TutorialGuide>().WeaponAcquired = true;
				}
			}
		}
	}
}
