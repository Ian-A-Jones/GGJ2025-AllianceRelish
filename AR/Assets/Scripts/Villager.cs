using UnityEngine;
using System.Collections;

[System.Serializable]
public class Villager : MonoBehaviour
{
	public bool hungry = false, thirsty = false;

	public int daysHungry = 0, daysThirsty = 0;

	public static int DeathThreshold = 6;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	public bool alive()
	{
		if(daysHungry + daysThirsty >= DeathThreshold)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public void unHunger()
	{
		hungry = false;
		daysHungry = 0;
	}
	
	public void hungerTick()
	{
		if(hungry)
		{
			daysHungry ++;
		}
		else
		{
			hungry = true;
		}
	}

	public void unThirst()
	{
		thirsty = false;
		daysThirsty = 0;
	}

	public void thirstTick()
	{
		if(thirsty)
		{
			daysThirsty ++;
		}
		else
		{
			thirsty = true;
		}
	}
	
	
}
