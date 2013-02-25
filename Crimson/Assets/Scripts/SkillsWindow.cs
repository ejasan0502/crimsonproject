using UnityEngine;
using System.Collections;

// creates an interactive skills menu that can be used to pick new skills when leveling
// Works by placing script in Skills Component of Player(FPS Controller)
public class SkillsWindow : MonoBehaviour 
{
	private GUIStyle menuStyle;
	private Game myGame;
	private Character player;
	private GameObject playerObj;
	private GameObject skillsScript;
	public Vector2 scrollPosition;
	public Rect r;
	
	int numSkills;
	int playerLvl;
	string charClass;
	bool windowOpen = false;
	bool used;
	
	// skill bools
	bool PistolMaster, RifleMaster, BurningShot, QuickShot, PoisonShot,
			Frenzy, TargetVitals, EmpShot;
	bool ToolMaster, RobotMaster, SummonTurret, RobotDefence, SummonAssault, 
			SelfDestruct, RepairRobots, SummonMAADD;
	bool[] skillsInUse;
	
	int numSkillsInUse;
	
	
	void Awake ()
	{
		r = new Rect (Screen.width * 0.4f, Screen.height * 0.1f, Screen.width * 0.4f, Screen.height * 0.75f);
	}
	
	// Use this for initialization
	void Start ()
	{
		skillsScript = GameObject.Find("Skills");
		myGame = GameObject.Find ("Game").GetComponent<Game> ();
		player = myGame.GetPlayerChar ();
		playerObj = GameObject.Find ("Player");
		
		menuStyle = new GUIStyle ();
		menuStyle.fontSize = Mathf.RoundToInt (Screen.height * 0.05f);
		menuStyle.normal.textColor = Color.white;
		menuStyle.font = (Font)Resources.Load ("Fonts/After_Shok");
		
		skillsInUse = new bool[8];
		
		// FIX TO BE BASED ON PLAYER LEVEL playerLvl = player.level;
		playerLvl = 1;
		numSkills = playerLvl;
		charClass = "";
		Debug.Log(player.level);
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.K))
		{
			if (windowOpen == false) windowOpen = true;
			else windowOpen = false;
		}
	}
	
	void OnGUI ()
	{
		if (windowOpen) 
		{
			Screen.lockCursor = false;
			playerObj.GetComponent<MouseLook> ().enabled = false;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = false;
			used = false;
			
			if (charClass == "Marksman")
			{
				r = GUI.Window (3, r, marksmanWindow, "Skills");
			}
			
			if (charClass == "Engineer")
			{
				r = GUI.Window (3, r, engineerWindow, "Skills");
			}
		}
		else
		{
			if (!used)
			{
				playerObj.GetComponent<MouseLook> ().enabled = true;
				Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
				Screen.lockCursor = true;
				used = true;
			}
		}
	}
	
	void marksmanWindow (int windowID)
	{
		// create label with char class and number of skill points
		GUI.Label(new Rect(r.width/4, 15, r.width - 200, 20), "Class: " + charClass +
						"					" + 
						"Skill Points: " + numSkills);
		
		// create a clickable buttons
		//Pistol Mastery
		// Skill Number: 0
		// Requirement: None
		PistolMaster = GUI.Toggle (new Rect(r.width/8, 40, r.width/4, 40), PistolMaster, new GUIContent("Not Implemented", 
				"Increases the damage done by Pistol weapons."));
		if (PistolMaster)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				skillsInUse[0] = true;
			}
			else
				PistolMaster = !PistolMaster;
		}
		else if (!PistolMaster)
		{
			skillsInUse[0] = false;	
		}
		//Rifle Mastery
		// Skill Number: 1
		// Requirement: None
		RifleMaster = GUI.Toggle (new Rect((r.width/8)*3, 40, r.width/4, 40), RifleMaster, new GUIContent("Not Implemented", 
				"Increases the damage done by Rifle weapons."));
		if (RifleMaster)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				skillsInUse[1] = true;
			}
			else 
				RifleMaster = !RifleMaster;
		}
		else if (!RifleMaster)
		{
			skillsInUse[1] = false;
		}
		//Burning Shot
		// Skill Number: 2
		// Requirement: None
		BurningShot = GUI.Toggle (new Rect((r.width/8)*5, 40, r.width/4, 40), BurningShot, new GUIContent("Burning Shot", 
				"Shoots a bullet that burns the target over time."));
		if (BurningShot)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				BurningShot script;
				script = skillsScript.GetComponent<BurningShot>();
				script.enabled = true;
				skillsInUse[2] = true;
			}
			else 
				BurningShot = !BurningShot;
		}
		else if (!BurningShot)
		{
			BurningShot script;
			script = skillsScript.GetComponent<BurningShot>();
			script.enabled = false;
			skillsInUse[2] = true;
		}
		//Quick Shot
		// Skill Number: 3
		// Requirement: Level 3
		QuickShot = GUI.Toggle (new Rect(r.width/4, 80, r.width/4, 40), QuickShot, new GUIContent("Not Implemented", 
				"Increases how fast the player shoots for a short time."));
		if (QuickShot)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 3)
			{
				skillsInUse[3] = true;
			}
			else
				QuickShot = !QuickShot;
		}
		else if (!QuickShot)
		{
			skillsInUse[3] = false;
		}
		//Poison Shot
		// Skill Number: 4
		// Requirement: Level 3
		PoisonShot = GUI.Toggle (new Rect((r.width/8)*5, 80, r.width/4, 40), PoisonShot, new GUIContent("Poison Shot", 
				"Shoots a bullet that does medium poison damage for a short time."));
		if (PoisonShot)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 3)
			{
				PoisonShot script;
				script = skillsScript.GetComponent<PoisonShot>();
				script.enabled = true;
				skillsInUse[4] = true;	
			}
			else
				PoisonShot = !PoisonShot;
		}
		else if (!PoisonShot)
		{
			PoisonShot script;
			script = skillsScript.GetComponent<PoisonShot>();
			script.enabled = false;
			skillsInUse[4] = false;
		}
		//Frenzy
		// Skill Number: 5
		// Requirement: Level 7
		Frenzy = GUI.Toggle (new Rect(r.width/4, 130, r.width/4, 40), Frenzy, new GUIContent("Not Implemented", 
				"Increases the damage done player weapons for a short time."));
		if (Frenzy)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 7)
			{
				skillsInUse[5] = true;
			}
			else 
				Frenzy = !Frenzy;
		}
		else if (!Frenzy)
		{
			skillsInUse[5] = false;
		}
		//Target Vitals
		// Skill Number: 6
		// Requirement: Level 7
		TargetVitals = GUI.Toggle (new Rect((r.width/8)*5, 130, r.width/4, 40), TargetVitals, new GUIContent("Target Vitals", 
				"Shoots a bullet that causes high bleed damage for a short time."));
		if (TargetVitals)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 7)
			{
				TargetVitals script;
				script = skillsScript.GetComponent<TargetVitals>();
				script.enabled = true;
				skillsInUse[6] = true;
			}
			else
				TargetVitals = !TargetVitals;
		}
		else if (!TargetVitals)
		{
			TargetVitals script;
			script = skillsScript.GetComponent<TargetVitals>();
			script.enabled = false;
			skillsInUse[6] = false;
		}
		//EMP Shot
		// Skill Number: 7
		// Requirement: Level 10
		EmpShot = GUI.Toggle (new Rect((r.width/8)*3, 180, r.width/4, 40), EmpShot, new GUIContent("EMP Shot", 
				"Shoots an EMP that does extra damage to robots."));
		if (EmpShot)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 10)
			{
				EMPShot script;
				script = skillsScript.GetComponent<EMPShot>();
				script.enabled = true;
				skillsInUse[7] = true;
			}
			else
				EmpShot = !EmpShot;
		}
		else if (!EmpShot)
		{
			EMPShot script;
			script = skillsScript.GetComponent<EMPShot>();
			script.enabled = false;
			skillsInUse[7] = false;
		}
		
		Debug.Log("Marksman window");
		// Skills
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
		
		// show tooltip
		GUI.Label(new Rect((r.width/2)-50, (r.height/10)*9, 80, 50), "Description");
		GUI.Label(new Rect((r.width/15), (r.height/15)*14,r.width, 50), GUI.tooltip);
	}
	
	void engineerWindow (int windowID)
	{
		// create label with char class and number of skill points
		GUI.Label(new Rect(r.width/4, 15, r.width - 200, 20), "Class: " + charClass +
						"					" + 
						"Skill Points: " + numSkills);
		
		// create a clickable buttons
		//Tool Mastery
		// Skill Number: 0
		// Requirement: None
		ToolMaster = GUI.Toggle (new Rect(r.width/8, 40, r.width/4, 40), ToolMaster, new GUIContent("Not Implemented", 
				"Increases the damage done by Tool weapons."));
		if (ToolMaster)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				skillsInUse[0] = true;
			}
			else 
				ToolMaster = !ToolMaster;
		
		}
		else if (!ToolMaster)
		{
			skillsInUse[0] = false;
			
		}
		//Robot Mastery
		// Skill Number: 1
		// Requirement: None
		RobotMaster = GUI.Toggle (new Rect((r.width/8)*3, 40, r.width/4, 40), RobotMaster, new GUIContent("Not Implemented", 
				"Increases the damage done by Robots."));
		if (RobotMaster)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				skillsInUse[1] = true;
			}
			else
				RobotMaster = !RobotMaster;
		}
		else if (!RobotMaster)
		{
			skillsInUse[1] = false;
		}
		//Summon Turret
		// Skill Number: 2
		// Requirement: None
		SummonTurret = GUI.Toggle (new Rect((r.width/8)*5, 40, r.width/4, 40), SummonTurret, new GUIContent("Summon Turret", 
				"Summons a turret to help the player for a short time."));
		if (SummonTurret)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills)
			{
				SummonTurret script;
				script = skillsScript.GetComponent<SummonTurret>();
				script.enabled = true;
				skillsInUse[2] = true;
			}
			else 
				SummonTurret = !SummonTurret;
		}
		else if (!SummonTurret) 
		{
			SummonTurret script;
			script = skillsScript.GetComponent<SummonTurret>();
			script.enabled = false;
			skillsInUse[2] = false;
		}
		//Robot Defence
		// Skill Number: 3
		// Requirement: Level 3
		RobotDefence = GUI.Toggle (new Rect(r.width/4, 80, r.width/4, 40), RobotDefence, new GUIContent("Not Implemented", 
				"Increases the defence of the player when they have a robot summoned."));
		if (RobotDefence)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 3)
			{
				skillsInUse[3] = true;
			}
			else
				RobotDefence = !RobotDefence;
		}
		else if (!RobotDefence)
		{
			skillsInUse[3] = false;
		}
		//Summon Assault
		// Skill Number: 4
		// Requirement: Level 3
		SummonAssault = GUI.Toggle (new Rect((r.width/8)*5, 80, r.width/4, 40), SummonAssault, new GUIContent("Summon Assault Bot", 
				"Summons a assault robot to help the player for a short time."));
		if (SummonAssault)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 3)
			{
				SummonAssault script;
				script = skillsScript.GetComponent<SummonAssault>();
				script.enabled = true;
				skillsInUse[4] = true;
			}
			else 
				SummonAssault = !SummonAssault;
		}
		else if (!SummonAssault)
		{
			SummonAssault script;
			script = skillsScript.GetComponent<SummonAssault>();
			script.enabled = false;
			skillsInUse[4] = false;
		}
		//Self Destruct
		// Skill Number: 5
		// Requirement: Level 7
		SelfDestruct = GUI.Toggle (new Rect(r.width/4, 130, r.width/4, 40), SelfDestruct, new GUIContent("Self Destruct", 
				"The furthest player robot self destructs, damaging nearby enemies."));
		if (SelfDestruct)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 7)
			{
				SelfDestruct script;
				script = skillsScript.GetComponent<SelfDestruct>();
				script.enabled = true;
				skillsInUse[5] = true;
			}
			else 
				SelfDestruct = !SelfDestruct;
		}
		else if (!SelfDestruct)
		{
			SelfDestruct script;
			script = skillsScript.GetComponent<SelfDestruct>();
			script.enabled = false;
			skillsInUse[5] = false;
		}
		//Repair Robots
		// Skill Number: 6
		// Requirement: Level 7
		RepairRobots = GUI.Toggle (new Rect((r.width/8)*5, 130, r.width/4, 40), RepairRobots, new GUIContent("Repair Robots", 
				"Repairs all robots in range."));
		if (RepairRobots)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 7)
			{
				Repair script;
				script = skillsScript.GetComponent<Repair>();
				script.enabled = true;
				skillsInUse[6] = true;
			}
			else
				RepairRobots = !RepairRobots;
		}
		else if (!RepairRobots)
		{
			Repair script;
			script = skillsScript.GetComponent<Repair>();
			script.enabled = false;
			skillsInUse[6] = false;
		}
		//Summon MAADD
		// Skill Number: 7
		// Requirement: Level 10
		SummonMAADD = GUI.Toggle (new Rect((r.width/8)*3, 180, r.width/4, 40), SummonMAADD, new GUIContent("Not Implemented", 
				"Summon Mobile Anti-Alien Destruction Device for a short time."));
		if (SummonMAADD)
		{
			//  check if player has avail skill points
			numSkillsInUse = 0;
			for (int i=0; i<skillsInUse.Length; i++)
			{
				if (skillsInUse[i] == true)
				{
					numSkillsInUse++;
				}
			}
			if (numSkillsInUse <= numSkills && playerLvl >= 10)
			{
				skillsInUse[7] = true;
			}
			else
				SummonMAADD = !SummonMAADD;
		}
		else if (!SummonMAADD)
		{
			skillsInUse[7] = false;
		}
		
		Debug.Log("Engineer window");
		// Skills
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
		
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);
		 
		GUI.DragWindow (rec);
		
		// show tooltip
		GUI.Label(new Rect((r.width/2)-50, (r.height/10)*9, 80, 50), "Description");
		GUI.Label(new Rect((r.width/15), (r.height/15)*14,r.width, 50), GUI.tooltip);
	}
	
	void AddLevel()
	{
		playerLvl++;
		numSkills = playerLvl;
	}
	
	void SetClass (string Class)
	{
		charClass = Class;
	}
}
