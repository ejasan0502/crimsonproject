using UnityEngine;
using System.Collections;

public class Vendor : NPC {
	
	// Use this for initialization
	void Start () {
		display = false;
		init (this.gameObject.name,NPC.occupation.vendor);
	}
	
	void OnGUI(){
		if (display){
			DisplayWindow();
		}
	}
	
	void OnMouseDown(){
		if (!display && Vector3.Distance (this.gameObject.transform.position,playerObj.transform.position) < 5){
			display = true;	
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;	
			ui.display = false;
		}
	}
}
