using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	
	public GameObject playerObj;
	
	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		if (Vector3.Distance (this.gameObject.transform.position,playerObj.transform.position) < 5){
			if (this.gameObject.tag == "TutorialSwitch"){
				GameObject.Find ("TutorialGuide").GetComponent<NPC_TutorialGuide>().tutorialSwitch = true;	
			}
		}
	}
}
