using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{		
	//Reference to Village Generator
	public VillageGenerator VillageGenRef;

	#region Village stats
	//Total number of Villagers
	public float population;

	//Total amount of food and water available
	public float foodSupply;
	public float waterSupply;

	public float foodGain;
	public float waterGain;

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
	float nextDecisionTimer = 1;

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

	#region stateConditions
	bool villageDeadEndState = false;
	bool unhappyVillageEndState = false;
	public bool questionVictory = false;
	int happinessVictory = 200;
	int populationVictory = 100;
	#endregion

	// Use this for initialization
	void Start () 
	{
		population = 10;

		VillageGenRef.GenerateVillage((int)population);

		foodSupply = 200;
		waterSupply = 200;

		foodGain = 5;
		waterGain = 5;

		happiness = 50;

		sickness = 0;

		Villagers = new List<GameObject>();
        //If the new Population is greater then create more
        for (int i = 0; i < population; i++)
        {
            Villagers.Add(Instantiate(Resources.Load("Prefabs/Villagerlol")) as GameObject);
            Villagers[Villagers.Count - 1].AddComponent<Villager>();
            // Debug.Log("DRAWING VILLAGER");
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If game isn't over
		if(!gameOver())
		{
			if(!ChatDialogue.activeQ)
			{
				foreach (GameObject villager in Villagers.ToArray())
	            {
					if(villager.GetComponent<Villager>().alive())
					{
	                	villager.GetComponent<Villager>().moveVillager();
					}
					else
					{
						Villagers.Remove(villager);
						Destroy(villager);
						population--;
					}
	            }
				//Decision timer stuff
				if(decisionTimer > nextDecisionTimer)
				{
					//Start decision
					Debug.Log ("Next decision");
					ChatDialogue.activeQ = true;

					//Pick random amount of time for next decision
					nextDecisionTimer = 2;// Random.Range(0,1);

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

					//If Village is happy enough and there's a surplus of food
					if(happiness > 50 && foodSupply > population * 1.5 && waterSupply > population * 1.5)
					{
						//25% chance of Pop increase
						if(Random.value > 0.75f)
						{
							Villagers.Add(Instantiate(Resources.Load("Prefabs/Villagerlol")) as GameObject);
							Villagers[Villagers.Count - 1].AddComponent<Villager>();
							population++;
						}
					}

					debugStats();
				}
				else
				{
					dayTimer += Time.deltaTime;
				}
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

	public void cull(int toCull)
	{
		int totalCulled = 0;
		while(totalCulled < toCull)
		{
			int randVil = Random.Range(0, Villagers.Count);
			if(Villagers[randVil].GetComponent<Villager>().alive())
			{
				Villagers[randVil].GetComponent<Villager>().dead = true;
				totalCulled++;
			}
		}
	}
	bool gameOver()
	{
		if(population <=0)
		{
			//Dead Village end state
			villageDeadEndState = true;
			return true;
		}

		if (happiness <= 0) 
		{
			//Unhappy Village end state
			unhappyVillageEndState = true;
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

	public void  removeTree()
	{
		VillageGenRef.removeATree();
	}

	public void  GoldHut()
	{
		VillageGenRef.addAGoldHut();
	}

	public void  Graffiti()
	{
		VillageGenRef.addGraffiti();
	}

	public void  FineArt()
	{
		VillageGenRef.addFineArt();
	}

	public void  BurntHut()
	{
		VillageGenRef.addBurntHut();
	}

	public void arrowKnee()
	{
		VillageGenRef.addArrowKnee();
	}

	void OnGUI()
	{
		GUI.skin = skin;

		GUI.Box (new Rect (-200, 0, 2500, 50), "");
		GUI.Label (new Rect (Screen.width / 2 - 400, 10, 150, 100), "Total Food: " + foodSupply.ToString("F0"));
		GUI.Label (new Rect (Screen.width / 2 - 200, 10, 150, 100), "Total Water: " + waterSupply.ToString("F0"));
		GUI.Label (new Rect (Screen.width / 2 , 10, 150, 100), "Happiness: " + happiness);
		GUI.Label (new Rect (Screen.width / 2 + 200, 10, 150, 100), "Population: " + population.ToString("F0"));

		GUI.skin = skin2;
		GUI.Label (new Rect (Screen.width / 2 - 425, 10, 50, 50), food);
		GUI.Label (new Rect (Screen.width / 2 - 225, 10, 50, 50), water);
		GUI.Label (new Rect (Screen.width / 2 - 25, 10, 50, 50), happinessIcon);
		GUI.Label (new Rect (Screen.width / 2 + 175, 10, 50, 50), pop);

		if (questionVictory) 
		{
			GUI.BeginGroup(new Rect (Screen.width / 2 - 250, Screen.height / 2, 400, 200), "");
			GUI.Box (new Rect (10,0, 400, 200), "");
			GUI.Label (new Rect (10,10, 400, 100), "Congratulations you have made all the decisions and survived");
			if (GUI.Button (new Rect ( 100,75, 100, 50), "Restart."))
			{
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect ( 200,75, 100, 50), "Quit."))
			{
				Application.Quit();
			}
			GUI.EndGroup();
		}

		if (happiness == happinessVictory) 
		{
			GUI.BeginGroup(new Rect (Screen.width / 2 - 250, Screen.height / 2, 400, 200), "");
			GUI.Box (new Rect (10,0, 400, 200), "");
			GUI.Label (new Rect (10,10, 400, 100), "congratulations, you have managed to please the entire village!");
			if (GUI.Button (new Rect ( 100,75, 100, 50), "Restart."))
			{
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect ( 200,75, 100, 50), "Quit."))
			{
				Application.Quit();
			}
			GUI.EndGroup();
		}
		
		if (population == populationVictory) 
		{
			GUI.BeginGroup(new Rect (Screen.width / 2 - 250, Screen.height / 2, 400, 200), "");
			GUI.Box (new Rect (10,0, 400, 200), "");
			GUI.Label (new Rect (10,10, 400, 100), "congratulations, you have managed to make your community thrive!");
			if (GUI.Button (new Rect ( 100,75, 100, 50), "Restart."))
			{
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect ( 200,75, 100, 50), "Quit."))
			{
				Application.Quit();
			}
			GUI.EndGroup();
		}

		if (unhappyVillageEndState == true) 
		{
			GUI.BeginGroup(new Rect (Screen.width / 2 - 250, Screen.height / 2, 400, 200), "");
			GUI.Box (new Rect (10,0, 400, 200), "");
			GUI.Label (new Rect (10,10, 400, 100), "You have lost! You have been overthrown!");
			if (GUI.Button (new Rect ( 100,75, 100, 50), "Restart."))
			{
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect ( 200,75, 100, 50), "Quit."))
			{
				Application.Quit();
			}
			GUI.EndGroup();
		}
		
		if (villageDeadEndState == true) 
		{
			GUI.BeginGroup(new Rect (Screen.width / 2 - 250, Screen.height / 2, 400, 200), "");
			GUI.Box (new Rect (10,0, 400, 200), "");
			GUI.Label (new Rect (10,10, 400, 100), "You have lost! you have let all of your villagers die!");
			if (GUI.Button (new Rect ( 100,75, 100, 50), "Restart."))
			{
				Application.LoadLevel (1);
			}
			if (GUI.Button (new Rect ( 200,75, 100, 50), "Quit."))
			{
				Application.Quit();
			}
			GUI.EndGroup();
		}
		
	}

}