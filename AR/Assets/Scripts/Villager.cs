using UnityEngine;
using System.Collections;

[System.Serializable]
public class Villager : MonoBehaviour
{
	public bool alive = true, hungry = false, thirsty = false;

	public int daysHungry = 0, daysThirsty = 0;

	public static int DeathThreshold = 3;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void tick()
	{
		if(hungry)
		{
			daysHungry++; 
		}

		if(thirsty)
		{
			daysThirsty++;
		}

		if(daysHungry > DeathThreshold && daysThirsty > DeathThreshold)
		{
			alive = false;
		}
	}


}
