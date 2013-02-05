using UnityEngine;
using System.Collections;

public class SummonTimer : MonoBehaviour 
{
	public bool isSummoned = true;
	public float summonLength = 30;
	float summonTime;
	
	// Use this for initialization
	void Start () 
	{
		summonTime = Time.time;
	}
	
	void Update ()
	{
		if (isSummoned)
		{
			if (Time.time > summonTime + summonLength)
				Destroy(gameObject);
		}
	}
	
}
