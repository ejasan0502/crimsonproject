public class Item 
{
	RarityTypes itemRarity;
	string itemName;
	int itemValue;
	int curDur;
	int maxDur;
	
	public Item()
	{
		itemName = "Enter Name";
		itemRarity = RarityTypes.Common;
		itemValue = 0;
		maxDur = 100;
		curDur = maxDur;
	}
	
	public Item(string name, RarityTypes rare, int val, int mDur, int cDur)
	{
		itemName = name;
		itemRarity = rare;
		itemValue = val;
		maxDur = mDur;
		curDur = cDur;
	}
	
	public string Name
	{
		get{ return itemName;}
		set{ itemName = value;}
	}
	
	public RarityTypes Rarity
	{
		get{ return itemRarity;}
		set{ itemRarity = value;}
	}
	
	public int Value
	{
		get{ return itemValue;}
		set{ itemValue = value;}
	}
	
	public int MaxDurability
	{
		get{ return maxDur;}
		set{ maxDur = value;}
	}
	
	public int CurDurability
	{
		get{ return curDur;}
		set{ curDur = value;}
	}
	
	// allows function to be overwriten by inheriting classes
	public virtual string Tooltip()
	{
		return itemName + "\n" + 
			itemRarity + "\n" +
			"Durability: " + curDur + " / " + maxDur + "\n";
			
	}
}

public enum RarityTypes
{
	Common, Uncommon, Rare
}