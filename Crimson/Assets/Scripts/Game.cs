using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public float musicVolume;
	public float effectsVolume;
	public float sensitivity;
	
	void Awake(){
		DontDestroyOnLoad(this);	
		
		musicVolume = 1.0f;
		effectsVolume = 1.0f;
		sensitivity = 10.0f;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
