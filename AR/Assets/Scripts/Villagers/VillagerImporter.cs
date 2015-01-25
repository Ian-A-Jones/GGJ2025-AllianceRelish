using UnityEngine;
using System.Collections;
using System.IO;

public class VillagerImporter {

	JSONObject villagerData;

	public string[,] villagers;

	int currentVil = 0;

	// Use this for initialization
	public VillagerImporter () {
		villagers = new string[15, 2];
		villagers [0, 0] = "Alan";
		villagers [0, 1] = "Male";
		villagers [1, 0] = "Ellis";
		villagers [1, 1] = "Male";
		villagers [2, 0] = "Ian";
		villagers [2, 1] = "Male";
		villagers [3, 0] = "Katie";
		villagers [3, 1] = "Female";
		villagers [4, 0] = "Dan";
		villagers [4, 1] = "Male";
		villagers [5, 0] = "Ross";
		villagers [5, 1] = "Male";
		villagers [6, 0] = "Gareth";
		villagers [6, 1] = "Male";
		villagers [7, 0] = "Sean";
		villagers [7, 1] = "Male";
		villagers [8, 0] = "Simon";
		villagers [8, 1] = "Male";
		villagers [9, 0] = "Carina";
		villagers [10, 1] = "Female";
		villagers [11, 0] = "Andy";
		villagers [11, 1] = "Male";
		villagers [12, 0] = "Lucy";
		villagers [12, 1] = "Female";
		villagers [13, 0] = "Roxanne";
		villagers [13, 1] = "Female";
		villagers [14, 0] = "Melinda";
		villagers [14, 1] = "Female";

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetNewVillager(){
		int rand = Random.Range (0, 14);
		currentVil = rand;
	}

	public string getName(){

		return villagers[currentVil,0];
	}

	public string getGender(){
		return villagers[currentVil,1];
	}
}


