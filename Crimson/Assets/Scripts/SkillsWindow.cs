using UnityEngine;
using System.Collections;

// creates an interactive skills menu that can be used to pick new skills when leveling
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
	string charClass;
	bool windowOpen = false;
	
	// skill bools
	bool PistolMaster, RifleMaster, BurningShot, QuickShot, PoisonShot,
			Frenzy, TargetVitals, EmpShot;
	bool ToolMaster, RobotMaster, SummonTurret, RobotDefence, SummonAssault, 
			SelfDestruct, RepairRobots, SummonMAADD;
	
	
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
		
		// FIX TO BE BASED ON PLAYER
		numSkills = 5;
		charClass = "Engineer";
		Debug.Log(charClass);
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
			playerObj.GetComponent<MouseLook> ().enabled = true;
			Camera.mainCamera.GetComponent<MouseLook> ().enabled = true;
			Screen.lockCursor = true;
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
		PistolMaster = GUI.Toggle (new Rect(r.width/8, 40, r.width/4, 40), PistolMaster, new GUIContent("Pistol Mastery", 
				"Increases the damage done by Pistol weapons."));
		if (PistolMaster)
		{
		}
		else if (!PistolMaster)
		{
		}
		//Rifle Mastery
		RifleMaster = GUI.Toggle (new Rect((r.width/8)*3, 40, r.width/4, 40), RifleMaster, new GUIContent("Rifle Mastery", 
				"Increases the damage done by Rifle weapons."));
		if (RifleMaster)
		{
		}
		else if (!RifleMaster)
		{
		}
		//Burning Shot
		BurningShot = GUI.Toggle (new Rect((r.width/8)*5, 40, r.width/4, 40), BurningShot, new GUIContent("Burning Shot", 
				"Shoots a bullet that burns the target over time."));
		if (BurningShot)
		{
			BurningShot script;
			script = skillsScript.GetComponent<BurningShot>();
			script.enabled = true;
		}
		else if (!BurningShot)
		{
			BurningShot script;
			script = skillsScript.GetComponent<BurningShot>();
			script.enabled = true;
		}
		//Quick Shot
		QuickShot = GUI.Toggle (new Rect(r.width/4, 80, r.width/4, 40), QuickShot, new GUIContent("Quick Shot", 
				"Increases how fast the player shoots for a short time."));
		if (QuickShot)
		{
		}
		else if (!QuickShot)
		{
		}
		//Poison Shot
		PoisonShot = GUI.Toggle (new Rect((r.width/8)*5, 80, r.width/4, 40), PoisonShot, new GUIContent("Poison Shot", 
				"Shoots a bullet that does medium poison damage for a short time."));
		if (PoisonShot)
		{
			PoisonShot script;
			script = skillsScript.GetComponent<PoisonShot>();
			script.enabled = true;
		}
		else if (!PoisonShot)
		{
			PoisonShot script;
			script = skillsScript.GetComponent<PoisonShot>();
			script.enabled = true;
		}
		//Frenzy
		Frenzy = GUI.Toggle (new Rect(r.width/4, 130, r.width/4, 40), Frenzy, new GUIContent("Frenzy", 
				"Increases the damage done player weapons for a short time."));
		if (Frenzy)
		{
		}
		else if (!Frenzy)
		{
		}
		//Target Vitals
		TargetVitals = GUI.Toggle (new Rect((r.width/8)*5, 130, r.width/4, 40), TargetVitals, new GUIContent("Target Vitals", 
				"Shoots a bullet that causes high bleed damage for a short time."));
		if (TargetVitals)
		{
			TargetVitals script;
			script = skillsScript.GetComponent<TargetVitals>();
			script.enabled = true;
		}
		else if (!TargetVitals)
		{
			TargetVitals script;
			script = skillsScript.GetComponent<TargetVitals>();
			script.enabled = true;
		}
		//EMP Shot
		EmpShot = GUI.Toggle (new Rect((r.width/8)*3, 180, r.width/4, 40), EmpShot, new GUIContent("EMP Shot", 
				"Shoots an EMP that does extra damage to robots."));
		if (EmpShot)
		{
			EMPShot script;
			script = skillsScript.GetComponent<EMPShot>();
			script.enabled = true;
		}
		else if (!EmpShot)
		{
			EMPShot script;
			script = skillsScript.GetComponent<EMPShot>();
			script.enabled = true;
		}
		
		Debug.Log("Marksman window");
		// Skills
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		int amount = 0;
		ArrayList skills = new ArrayList();
		if (player.charClass == "Marksman")
		{
			amount = myGame.MarksmanSkills.Count;
			skills = myGame.MarksmanSkills;
		} 
		else if (player.charClass == "Engineer")
		{
			amount = myGame.EngineerSkills.Count;
			skills = myGame.EngineerSkills;
		}
		
		for (int i = 0; i < amount; i++) {
			GUI.Box (	new Rect (0, r.height * 0.1f * i, r.width, r.height * 0.1f),
						((Skill)skills[i]).name);	
		}
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
		
		// show tooltip
		GUI.Label(new Rect((r.width/2)-50, 600, 80, 50), "Description");
		GUI.Label(new Rect(75, 625,r.width, 50), GUI.tooltip);
	}
	
	void engineerWindow (int windowID)
	{
		// create label with char class and number of skill points
		GUI.Label(new Rect(r.width/4, 15, r.width - 200, 20), "Class: " + charClass +
						"					" + 
						"Skill Points: " + numSkills);
		
		// create a clickable buttons
		//Tool Mastery
		ToolMaster = GUI.Toggle (new Rect(r.width/8, 40, r.width/4, 40), ToolMaster, new GUIContent("Tool Mastery", 
				"Increases the damage done by Tool weapons."));
		if (ToolMaster)
		{
		}
		else if (!ToolMaster)
		{
		}
		//Robot Mastery
		RobotMaster = GUI.Toggle (new Rect((r.width/8)*3, 40, r.width/4, 40), RobotMaster, new GUIContent("Robot Mastery", 
				"Increases the damage done by Robots."));
		if (RobotMaster)
		{
		}
		else if (!RobotMaster)
		{
		}
		//Summon Turret
		SummonTurret = GUI.Toggle (new Rect((r.width/8)*5, 40, r.width/4, 40), SummonTurret, new GUIContent("Summon Turret", 
				"Summons a turret to help the player for a short time."));
		if (SummonTurret)
		{
			SummonTurret script;
			script = skillsScript.GetComponent<SummonTurret>();
			script.enabled = true;
		}
		else if (!SummonTurret) 
		{
			SummonTurret script;
			script = skillsScript.GetComponent<SummonTurret>();
			script.enabled = false;
		}
		//Robot Defence
		RobotDefence = GUI.Toggle (new Rect(r.width/4, 80, r.width/4, 40), RobotDefence, new GUIContent("Robot Defence", 
				"Increases the defence of the player when they have a robot summoned."));
		if (RobotDefence)
		{
		}
		else if (!RobotDefence)
		{
		}
		//Summon Assault
		SummonAssault = GUI.Toggle (new Rect((r.width/8)*5, 80, r.width/4, 40), SummonAssault, new GUIContent("Summon Assault Bot", 
				"Summons a assault robot to help the player for a short time."));
		if (SummonAssault)
		{
			SummonAssault script;
			script = skillsScript.GetComponent<SummonAssault>();
			script.enabled = true;
		}
		else if (!SummonAssault)
		{
			SummonAssault script;
			script = skillsScript.GetComponent<SummonAssault>();
			script.enabled = false;
		}
		//Self Destruct
		SelfDestruct = GUI.Toggle (new Rect(r.width/4, 130, r.width/4, 40), SelfDestruct, new GUIContent("Self Destruct", 
				"Causes the furthest player owned robot to self destruct, doing damage to nearby enemies."));
		if (SelfDestruct)
		{
			SelfDestruct script;
			script = skillsScript.GetComponent<SelfDestruct>();
			script.enabled = true;
		}
		else if (!SelfDestruct)
		{
			SelfDestruct script;
			script = skillsScript.GetComponent<SelfDestruct>();
			script.enabled = false;
		}
		//Repair Robots
		RepairRobots = GUI.Toggle (new Rect((r.width/8)*5, 130, r.width/4, 40), RepairRobots, new GUIContent("Repair Robots", 
				"Repairs all robots in range."));
		if (RepairRobots)
		{
			Repair script;
			script = skillsScript.GetComponent<Repair>();
			script.enabled = true;
		}
		else if (!RepairRobots)
		{
			Repair script;
			script = skillsScript.GetComponent<Repair>();
			script.enabled = false;
		}
		//Summon MAADD
		SummonMAADD = GUI.Toggle (new Rect((r.width/8)*3, 180, r.width/4, 40), SummonMAADD, new GUIContent("Summon M.A.A.D.D.", 
				"Summon Mobile Anti-Alien Destruction Device for a short time."));
		if (SummonMAADD)
		{
		}
		else if (!SummonMAADD)
		{
		}
		
		Debug.Log("Engineer window");
		// Skills
		scrollPosition = GUI.BeginScrollView (new Rect (0, r.height * 0.1f, r.width, r.height),
														scrollPosition, new Rect (0, 0, r.width, r.height));
			
		int amount = 0;
		ArrayList skills = new ArrayList();
		if (player.charClass == "Marksman")
		{
			amount = myGame.MarksmanSkills.Count;
			skills = myGame.MarksmanSkills;
		} 
		else if (player.charClass == "Engineer")
		{
			amount = myGame.EngineerSkills.Count;
			skills = myGame.EngineerSkills;
		}
		
		for (int i = 0; i < amount; i++) {
			GUI.Box (	new Rect (0, r.height * 0.1f * i, r.width, r.height * 0.1f),
						((Skill)skills[i]).name);	
		}
			
		GUI.EndScrollView ();
		
		Rect rec = new Rect (0, 0, r.width, r.height * 0.1f);

		GUI.DragWindow (rec);
		
		// show tooltip
		GUI.Label(new Rect((r.width/2)-50, 600, 80, 50), "Description");
		GUI.Label(new Rect(75, 625,r.width, 50), GUI.tooltip);
	}
}
