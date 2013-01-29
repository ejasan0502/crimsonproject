using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnControllerColliderHit(ControllerColliderHit other){
		if (other.gameObject.tag == "Health"){
			Debug.Log ("Obtained Health Pack");
			Destroy(other.gameObject);
		}
	}
}
