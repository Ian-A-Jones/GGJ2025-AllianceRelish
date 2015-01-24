using UnityEngine;
using System.Collections;
using System.IO;

public class VillagerImporter : MonoBehaviour {

	JSONObject villagerData;
	// Use this for initialization
	void Awake () {
		JSONObject json = JSON_Reader.GetJsonFromFile("Assets/VillagerData/Villagers.json");

		if (json.HasField ("Villagers")) {
			villagerData = json.GetField("Villagers");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public JSONObject GetVillagerInfo(){

		int rand = Random.Range (0, villagerData.Count);

		JSONObject villagerInfo = villagerData [rand];

		return villagerInfo;
	}
}


public static class JSON_Reader
{
	public static JSONObject GetJsonFromFile(string filepath){
		StreamReader sr = new StreamReader(filepath);
		string encodedString = sr.ReadToEnd();
		sr.Close();
		return new JSONObject(encodedString);
		
	}

	
}