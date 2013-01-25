using UnityEngine;
using System.Collections;

public struct Interface {
	public bool display;
	public string name;
	public Rect position;
		
	public Interface(Rect p, string n, bool d){
		position = p;
		name = n;
		display = d;
	}
}
