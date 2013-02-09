using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this.GetComponent<MeshRenderer>());
		Destroy (this.GetComponent<MeshFilter>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
