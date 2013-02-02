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
	
	// Game
	public ArrayList QuestList;
	public ArrayList MarksmanSkills;
	public ArrayList EngineerSkills;
	
	void Awake ()
	{
		DontDestroyOnLoad (this);	
		
		musicVolume = 1.0f;
		effectsVolume = 1.0f;
		sensitivity = 10.0f;
		
		CharacterList = new ArrayList ();
		for (int i = 0; i < 3; i++) {
			CharacterList.Add (null);	
		}
		
		QuestList = new ArrayList ();
		MarksmanSkills = new ArrayList ();
		EngineerSkills = new ArrayList ();

		GetCharData ();
		GenerateQuestList ();
		GenerateSkillsList ();
	}
	
	private void GetCharData ()
	{		
		StreamReader reader = new StreamReader (Application.dataPath + "/Scripts/1.txt");
		string str = "";
		bool setChar = false;
		int s = 0;
		string n = "";
		string c = "";
		int lvl = 0;
		int xp = 0;
		float hp = 100;
		float sta = 50;
		float dmg = 10;
		
		ArrayList inv = new ArrayList();
		int m = 0;
		ArrayList qa = new ArrayList ();
		ArrayList qc = new ArrayList ();
		ArrayList sk = new ArrayList ();
		
		try {
			while ((str = reader.ReadLine()) != null) {
				if (str == "character start") {
					setChar = true;		
				} else if (str == "character end") {
					CharacterList.Insert (s, new Character (s, n, c, inv, m, qa, qc, sk));
					((Character)CharacterList[s]).SetPlayer(lvl,hp,sta,dmg);
					((Character)CharacterList[s]).SetCurrent (xp,((Character)CharacterList[s]).healthMax,((Character)CharacterList[s]).staminaMax);
					setChar = false;
				}
				
				if (setChar) {
					string[] args = str.Split (':');
					switch (args [0]) {
					case "slot":
						s = int.Parse (args [1]);
						break;
					case "name":
						n = args [1];
						break;
					case "class":
						c = args [1];
						break;
					case "inventory":
						string[] invs = ((string)args [1]).Split (',');
						for (int i = 0; i < invs.Length; i++) {
							inv.Add (invs [i]);	
						}
						break;
						case "money":
							m = int.Parse(args[1]);
						break;
						case "level":
							lvl = int.Parse(args[1]);
						break;
						case "xp":
							xp = int.Parse (args[1]);
						break;
					case "health":
						hp = float.Parse(args[1]);
						break;
					case "stamina":
						sta = float.Parse(args[1]);
						break;
					case "damage":
						dmg = float.Parse(args[1]);
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
			Debug.Log("Saving Character Data");
			for (int i = 0; i < 3; i++) {
				if (CharacterList [i] != null) {
					writer.WriteLine ("character start");
					writer.WriteLine ("slot:" + ((Character)CharacterList[i]).slot);	
					writer.WriteLine ("name:" + ((Character)CharacterList[i]).name);	
					writer.WriteLine ("class:" + ((Character)CharacterList[i]).charClass);	
					writer.WriteLine ("level:" + ((Character)CharacterList[i]).level);
					writer.WriteLine ("xp:" + ((Character)CharacterList[i]).curExp);
					writer.WriteLine ("health:" + ((Character)CharacterList[i]).health);
					writer.WriteLine ("stamina:" + ((Character)CharacterList[i]).stamina);
					writer.WriteLine ("damage:" + ((Character)CharacterList[i]).damage);
					
					string s = "";
					int l = ((Character)CharacterList [i]).inventory.Count;
					for (int j = 0; j < l; j++) {
						s += ((string)((Character)CharacterList [i]).inventory [j]);
						if (s != ""){
							s += ",";	
						}
					}
					
					writer.WriteLine ("inventory:" + s);
					writer.WriteLine ("money:" + ((Character)CharacterList [i]).money);
					
					writer.WriteLine ("character end");
				}
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			writer.Close ();	
		}	
	}
	
	public void SetPlayerChar (int index)
	{
		player = (Character)CharacterList [index];	
	}
	
	public Character GetPlayerChar ()
	{
		return player;	
	}
	
	public void GenerateQuestList ()
	{
		StreamReader reader = new StreamReader (Application.dataPath + "/Scripts/QuestList.txt");
		
		string str = "";
		try {
			while ((str = reader.ReadLine()) != null) {
				string[] args = str.Split (',');
				QuestList.Add (new Quest (args [0], int.Parse(args [1]), args [2], args[3], int.Parse (args [4])));
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			reader.Close ();	
		}
	}
	
	public void GenerateSkillsList ()
	{
		StreamReader reader = new StreamReader (Application.dataPath + "/Scripts/MarksmanSkills.txt");
		
		string str = "";
		try {
			while ((str = reader.ReadLine()) != null) {
				string[] args = str.Split (',');
				MarksmanSkills.Add (new Skill (args [0], int.Parse (args [1]), float.Parse (args [2]), float.Parse (args [3]), args [4]));
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			reader.Close ();	
		}	
		
		reader = new StreamReader (Application.dataPath + "/Scripts/EngineerSkills.txt");
		str = "";
		try {
			while ((str = reader.ReadLine()) != null) {
				string[] args = str.Split (',');
				EngineerSkills.Add (new Skill (args [0], int.Parse (args [1]), float.Parse (args [2]), float.Parse (args [3]), args [4]));
			}
		} catch (Exception e) {
			Debug.Log ("ERROR: " + e.Message);	
		} finally {
			reader.Close ();	
		}
	}
}
