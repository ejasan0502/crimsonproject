using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	
	private Game myGame;
	
	// Use this for initialization
	void Start () {
		myGame = GameObject.Find ("Game").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
