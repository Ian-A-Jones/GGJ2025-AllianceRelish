using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{		
	//Reference to Village Generator
	public Village VillageRef;

	#region Village stats
	//Total number of Villagers
	public int population;

	//Total amount of food and water available
	public int foodSupply;
	public int waterSupply;

	public int foodGain;
	public int waterGain;

	//How happy village is as a whole
	public float happiness;

	//Number of people that are sick
	public int sickness;

	//How many days have passed
	public int days = 0;
	
	#endregion

	public List<GameObject> Villagers;

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
	float TIMEPERDAY = 5;

	float HAPPYFOODTHRESH = 1.5f;
	float HAPPYWATERTHRESH = 1.5f;

	float SADFOODTHRESH = 0.8f;
	float SADWATERTHRESH = 0.8f;

	#endregion

	public GUIContent food;
	public GUIContent water;
	public GUIContent happinessIcon;
	public GUIContent pop;
	public GUIContent box;
	public GUISkin skin;
	public GUISkin skin2;

	// Use this for initialization
	void Start () 
	{
		population = 10;

		VillageRef.GenerateVillage(population);

		foodSupply = 200;
		waterSupply = 200;

		foodGain = 5;
		waterGain = 5;

		happiness = 50;

		sickness = 0;

		Villagers = new List<GameObject>();

		for(int i = 0; i < population; i++)
		{
			Villagers.Add(new GameObject("Villager " + (i+1)));
			Villagers[i].AddComponent("Villager");
		}
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
				days ++;
				dayTimer = 0;

				foodSupply += foodGain;
				waterSupply += waterGain;

				foreach(GameObject villager in Villagers.ToArray())
				{
					if(villager.GetComponent<Villager>().alive())
					{
						if(foodSupply > 0)
						{
							villager.GetComponent<Villager>().unHunger();
							foodSupply --;
						}
						else
						{
							villager.GetComponent<Villager>().hungerTick();
						}

						if(waterSupply > 0)
						{
							villager.GetComponent<Villager>().unThirst();
							waterSupply --;
						}
						else
						{
							villager.GetComponent<Villager>().thirstTick();
						}		
					}
					else
					{
						Villagers.Remove(villager);
						Destroy(villager);
						population--;
					}
				}

				happyCalc(foodSupply, HAPPYFOODTHRESH, SADFOODTHRESH);
				
				happyCalc(waterSupply, HAPPYWATERTHRESH, SADWATERTHRESH);

				if(happiness > 50 && foodSupply > 0 && waterSupply > 0)
				{
					population++;
				}

				debugStats();
			}
			else
			{
				dayTimer += Time.deltaTime;
			}
		}
		else
		{
			Debug.Log ("Game over");
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
		Debug.Log ("Day " + days);
		Debug.Log ("population: " + population);

		Debug.Log ("Total Food: " + foodSupply);
		Debug.Log ("Food %: " + percentPop(foodSupply) + "%");
		Debug.Log ("Total Water: " + waterSupply);
		Debug.Log ("Water %: " + percentPop(waterSupply) + "%");

		Debug.Log (happiness);
	}

	void OnGUI()
	{
		GUI.skin = skin;

		GUI.Box (new Rect (-200, 0, 2500, 50), "");
		GUI.Label (new Rect (Screen.width / 2 - 400, 10, 150, 100), "Total Food: " + foodSupply);
		GUI.Label (new Rect (Screen.width / 2 - 200, 10, 150, 100), "Total Water: " + waterSupply);
		GUI.Label (new Rect (Screen.width / 2 , 10, 150, 100), "Happiness: " + happiness + "%");
		GUI.Label (new Rect (Screen.width / 2 + 200, 10, 150, 100), "Population: " + population);

		GUI.skin = skin2;
		GUI.Label (new Rect (Screen.width / 2 - 425, 10, 50, 50), food);
		GUI.Label (new Rect (Screen.width / 2 - 225, 10, 50, 50), water);
		GUI.Label (new Rect (Screen.width / 2 - 25, 10, 50, 50), happinessIcon);
		GUI.Label (new Rect (Screen.width / 2 + 175, 10, 50, 50), pop);

	}

}