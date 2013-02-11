public class Weapon : Item 
{
	int minDmg;
	int maxDmg;
	int range; 
	int reloadTime;
	float atkSpeed;
	
	public Weapon()
	{
		minDmg = 0;
		maxDmg = 0;
		range = 0;
		reloadTime = 0;
		atkSpeed = 0;
	}
	
	public Weapon(int mDmg, int maDmg, int rng, int rTime, float aSpeed)
	{
		minDmg = mDmg;
		maxDmg = maDmg;
		range = rng;
		reloadTime = rTime;
		atkSpeed = aSpeed;
	}
	
	public int MaxDamage
	{
		get {return maxDmg;}
		set {maxDmg = value;}
	}
	
	public int MinDamage
	{
		get {return minDmg;}
		set {minDmg = value;}
	}
	
	public int MaxRange
	{
		get {return range;}
		set {range = value;}
	}
	
	public int ReloadTime
	{
		get {return reloadTime;}
		set {reloadTime = value;}
	}
	
	public float AttackSpeed
	{
		get {return atkSpeed;}
		set {atkSpeed = value;}
	}
	
	public override string Tooltip()
	{
		return Name + "\n" + 
			Rarity + "\n" +
			"Durability: " + CurDurability + " / " + MaxDurability + "\n" +
			"Damage: " + minDmg + " - " + maxDmg + "\n" +
			"Attack Speed: " + atkSpeed + "\n" +
			"Reload Time: " + reloadTime + "\n" +
			"Range: " + range + "\n\n\n" +
			"Value: " + Value;
					
	}
}
