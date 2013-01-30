using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Game : MonoBehaviour
{	
	// Settings
	public float musicVolume;
	public float effectsVolume;
	public float sensitivity;
	
	// Character
	public ArrayList CharacterList;
	public int CharacterSlotSelected;
	public Character player;
	
	void Awake ()
	{
		DontDestroyOnLoad (this);	
		
		musicVolume = 1.0f;
		effectsVolume = 1.0f;
		sensitivity = 10.0f;
		
		CharacterList = new ArrayList();
		for (int i = 0; i < 3; i++){
			CharacterList.Add (null);	
		}

		GetCharData ();
	}
	
	private void GetCharData ()
	{		
		StreamReader reader = new StreamReader (Application.dataPath + "/Scripts/1.txt");
		string str = "";
		bool setChar = false;
		int s = 0;
		string n = "";
		string c = "";
		ArrayList inv = new ArrayList();
		
		try {
			while ((str = reader.ReadLine()) != null) {
				if (str == "character start"){
					setChar = true;		
				} else if (str == "character end"){
					CharacterList.Insert (s, new Character(s,n,c,inv));
					setChar = false;
				}
				
				if (setChar){
					string[] args = str.Split(':');
					switch(args[0]){
						case "slot":
							s = int.Parse(args[1]);
						break;
						case "name":
							n = args[1];
						break;
						case "class":
							c = args[1];
						break;
						case "inventory":
							string[] invs = ((string)args[1]).Split (',');
							for (int i = 0; i < invs.Length; i++){
								inv.Add (invs[i]);	
							}
						break;
					}
				}
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			reader.Close ();	
		}
	}
	
	public void SaveCharData ()
	{
		StreamWriter writer = new StreamWriter (Application.dataPath + "/Scripts/1.txt");

		try {
			for (int i = 0; i < 3; i++){
				if (CharacterList[i] != null){
					writer.WriteLine ("character start");
					writer.WriteLine ("slot:" + ((Character)CharacterList[i]).slot);	
					writer.WriteLine ("name:" + ((Character)CharacterList[i]).name);	
					writer.WriteLine ("class:" + ((Character)CharacterList[i]).charClass);	
					
					string s = "";
					int l = ((Character)CharacterList[i]).inventory.Count;
					for (int j = 0; j < l; j++){
						s += ((string)((Character)CharacterList[i]).inventory[j] + ",");
					}
					
					writer.WriteLine ("inventory:" + s);
					
					writer.WriteLine ("character end");
				}
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			writer.Close ();	
		}	
	}
	
	public void SetPlayerChar(int index){
		player = (Character)CharacterList[index];	
	}
	
	public Character GetPlayerChar(){
		return player;	
	}
}
