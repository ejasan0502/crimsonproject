using UnityEngine;

public static class ItemGenerator
{
	public static Item Create()
	{
		Item item = CreateWeapon();
		
		
		return item;
	}
	
	private static Weapon CreateWeapon()
	{
		int ran = 0;
		Weapon weapon = new Weapon();
		
		// weapon info
		weapon.Name = "W:" + Random.Range(0,100);
		weapon.CurDurability = 100;
		weapon.MaxDurability = 100;
		
		// calculate rarity
		ran = Random.Range(0, 100);
		if (ran < 15)
			weapon.Rarity = RarityTypes.Rare;
		else if (ran >= 15 && ran < 50)
			weapon.Rarity = RarityTypes.Uncommon;
		else 
			weapon.Rarity = RarityTypes.Common;
		
		// set common stats
		if (weapon.Rarity == RarityTypes.Common)
		{
			weapon.Value = Random.Range(1, 20);
			weapon.MinDamage = Random.Range(1,3);
			weapon.MaxDamage = Random.Range(3,6);
			weapon.MaxRange = Random.Range(50,70);
			weapon.ReloadTime = Random.Range(3,4);
			weapon.AttackSpeed = Random.Range(0.6f, 0.9f);
		}
		// set uncommon stats
		else if (weapon.Rarity == RarityTypes.Uncommon)
		{
			weapon.Value = Random.Range(15, 30);
			weapon.MinDamage = Random.Range(2,4);
			weapon.MaxDamage = Random.Range(4,8);
			weapon.MaxRange = Random.Range(60,75);
			weapon.ReloadTime = Random.Range(2,3);
			weapon.AttackSpeed = Random.Range(0.4f, 0.7f);
		}
		// set rare stats
		else if (weapon.Rarity == RarityTypes.Rare)
		{
			weapon.Value = Random.Range(40, 60);
			weapon.MinDamage = Random.Range(3,6);
			weapon.MaxDamage = Random.Range(7,10);
			weapon.MaxRange = Random.Range(70,80);
			weapon.ReloadTime = Random.Range(1,3);
			weapon.AttackSpeed = Random.Range(0.3f, 0.5f);
		}
		
		return weapon;
	}
}

public enum ItemType
{
	Weapon
}