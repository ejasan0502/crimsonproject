using UnityEngine;
using System.Collections;

public class CharacterCreationScreen : MonoBehaviour
{
	private GUIStyle menuStyle;
	
	private SoundManager sm;
	private Game myGame;
	
	private string charName;
	private string charClass;
	public int charNum;
	
	// Use this for initialization
	void Start () {
		sm = (SoundManager)GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		myGame = (Game)GameObject.Find ("Game").GetComponent<Game>();
		
		menuStyle = new GUIStyle();
		menuStyle.fontSize = Mathf.RoundToInt(Screen.height * 0.08f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		charName = "Enter Here";
		charClass = "Marksman";
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		GUI.Label (new Rect (Screen.width * 0.1f, 0, Screen.width, Screen.height * 0.1f), "Create a Character", menuStyle);	
			
		GUI.Label (new Rect (0, Screen.height * 0.1f, Screen.width * 0.3f, Screen.height * 0.1f), "Name", menuStyle);
		charName = GUI.TextField (new Rect (Screen.width * 0.35f, Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.1f), charName, 25, menuStyle);
			
		GUI.Label (new Rect (0, Screen.height * 0.25f, Screen.width * 0.3f, Screen.height * 0.1f), "Class", menuStyle);
		if (GUI.Button (new Rect (Screen.width * 0.35f, Screen.height * 0.25f, Screen.width * 0.3f, Screen.height * 0.1f), charClass, menuStyle)) {
			if (charClass == "Marksman")
				charClass = "Engineer";
			else
				charClass = "Marksman";
		}
			
		if (GUI.Button (new Rect (Screen.width * 0.35f, Screen.height * 0.8f, Screen.width * 0.4f, Screen.height * 0.1f), "Create", menuStyle)) {	
			myGame.CharacterList.Insert (myGame.CharacterSlotSelected, new Character(myGame.CharacterSlotSelected, charName, charClass));
			myGame.SaveCharData();
			
			Instantiate (Resources.Load ("Prefabs/Character Selection Menu"));
			DestroyImmediate(this.gameObject);
		}	
	}
}
