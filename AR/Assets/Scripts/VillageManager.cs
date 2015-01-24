using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{		
	#region Village stats
	//Total number of Villagers
	public int population;

	//Total amount of food and water available
	public int foodSupply;
	public int waterSupply;

	//Drain of Food and Water based on Population
	public float foodDrain;
	public float waterDrain;
	
	//How happy village is as a whole
	public float happiness;

	//Number of people that are sick
	public int sickness;
	
	#endregion

	List<Villager> Villagers;

	#region timers

	//Time since last day
	float dayTimer = 0;

	//Time since last decision
	float decisionTimer = 0;

	//Time reqiured for next decision
	float nextDecisionTimer = 60;

	#endregion

	#region Constants

	//Time for each day to pass
	float TIMEPERDAY = 10;

	float HAPPYFOODTHRESH = 1.5f;
	float HAPPYWATERTHRESH = 1.5f;

	float SADFOODTHRESH = 0.8f;
	float SADWATERTHRESH = 0.8f;

	#endregion
	
	// Use this for initialization
	void Start () 
	{
		population = 10;

		foodSupply = 280;
		waterSupply = 200;

		happiness = 50;

		sickness = 0;

		Villagers = new List<Villager>(population);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If game isn't over
		if(!gameOver())
		{
			//Decision timer stuff
			if(decisionTimer > nextDecisionTimer)
			{
				//Start decision
				Debug.Log ("Next decision");
				//ChooseNextDecision();

				//Pick random amount of time for next decision
				nextDecisionTimer = Random.Range(0,70);

				decisionTimer = 0;
			}
			else
			{
				decisionTimer += Time.deltaTime;
			}

			//VillageStat updates
			//IF a day has passed
			if(dayTimer > TIMEPERDAY)
			{
				dayTimer = 0;

				foodSupply 	-= population;
				if(foodSupply < 0)
				{
					makeHungry(Mathf.Abs(foodSupply));
					foodSupply = 0;
				}

				waterSupply -= population;
				if(waterSupply < 0)
				{
					makeThirsty(Mathf.Abs(waterSupply));
					waterSupply = 0;
				}
				
				happyCalc(foodSupply, HAPPYFOODTHRESH, SADFOODTHRESH);
				
				happyCalc(waterSupply, HAPPYWATERTHRESH, SADWATERTHRESH);

				foreach(Villager villager in Villagers)
				{
					villager.tick();
				}
				
				debugStats();
			}
			else
			{
				dayTimer += Time.deltaTime;
			}
		}
	}

	void makeHungry(int toConvert)
	{
		int madeHungry = 0;

		foreach(Villager villager in Villagers)
		{
			if(!villager.hungry)
			{
				villager.hungry = true;
				madeHungry ++;

				if(madeHungry == toConvert)
				{
					return;
				}
			}
		}
	}

	void makeThirsty(int toConvert)
	{
		int madeThirsty = 0;
		
		foreach(Villager villager in Villagers)
		{
			if(!villager.thirsty)
			{
				villager.thirsty = true;
				madeThirsty ++;
				
				if(madeThirsty == toConvert)
				{
					return;
				}
			}
		}
	}

	void happyCalc(float supply, float happyThresh, float sadThresh)
	{
		if(supply / population > happyThresh)
		{
			happiness ++;
		}
		else if(supply / population < sadThresh)
		{
			happiness --;
		}
	}

	bool gameOver()
	{
		if(population <=0)
		{
			//Dead Village end state
			return true;
		}

		if (happiness <= 0) 
		{
			//Unhappy Village end state
			return true;
		}

		return false;
	}

	float percentPop(float val)
	{
		return Mathf.Clamp(((val/population) * 100), 0, 100);
	}

	void debugStats()
	{
		Debug.Log ("population: " + population);

		Debug.Log ("Total Food: " + foodSupply);
		Debug.Log ("Food %: " + percentPop(foodSupply) + "%");
		Debug.Log ("Total Water: " + waterSupply);
		Debug.Log ("Water %: " + percentPop(waterSupply) + "%");

		Debug.Log (happiness);
	}

}