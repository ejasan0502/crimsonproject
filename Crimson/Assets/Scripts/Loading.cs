using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public Texture2D texture;
	private GUIStyle menuStyle;
	
	GameObject playerObj;
	
	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find ("Player");
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.08f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		playerObj.GetComponent<MouseLook> ().enabled = false;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
		StartCoroutine(wait ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		GUI.Label (new Rect(0,0,Screen.width * 2,Screen.height * 2),texture);
		menuStyle.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(0,Screen.height * 0.4f,Screen.width, Screen.height * 0.1f),"Loading",menuStyle);
	}
	
	void destroy(){
		playerObj.GetComponent<MouseLook> ().enabled = true;
		Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
		Destroy (this.gameObject);	
	}
	
	IEnumerator wait(){
		yield return new WaitForSeconds(5.0f);	
		destroy ();
	}
}
