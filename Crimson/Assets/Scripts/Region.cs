using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {
	
	public enum area { none, town, dungeon }
	
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	
	// Use this for initialization
	void Start () {
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(){
		switch(this.gameObject.tag){
		case "Town":
			myGame.playerArea = area.town;
			break;
		case "Dungeon":
			myGame.playerArea = area.dungeon;
			break;
		}	
	}
	
	void OnTriggerExit(){
		myGame.playerArea = area.none;	
	}
}
