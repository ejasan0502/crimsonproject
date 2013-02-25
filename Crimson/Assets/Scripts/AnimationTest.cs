using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {
	
	bool attack;
	
	// Use this for initialization
	void Start () {
		animation["Attack"].wrapMode = WrapMode.Once;
		attack = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && !attack){
			attack = true;
		} else if (!attack){
			if (!animation.IsPlaying("Attack"))
				animation.Play ("Idle");	
		} else {
			animation.Play("Attack");
			attack = false;
		}
	}
}
