using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Village : MonoBehaviour {

	List<GameObject> huts = new List<GameObject>();
	List<GameObject> trees = new List<GameObject>();

	GameObject river;

	private const float HUT_DENSITY = 0.8f;
	private const float FOREST_DENSITY = 0.8f;
	private const float THICKETS = 5;
	// Use this for initialization
	void Start () {



		GenerateRiver ();
		GenerateForest ();
		GenerateHuts ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void GenerateHuts(){


		for (int i = 0; i<HUT_DENSITY*10; i++) {
			huts.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
			huts[huts.Count-1].GetComponent<Hut>().Initialise(new Vector2(0,0));
		}

	}

	void GenerateRiver(){
		river = (Instantiate(Resources.Load("Prefabs/River")) as GameObject);
		int riverXPos = Random.Range (2, 16);
		river.transform.position = new Vector3 (riverXPos, 0, 0);
	}

	void GenerateForest(){

		for(int i = 0; i<THICKETS; i++){
			Vector2 thicketPos = new Vector2(Random.Range(0,18), Random.Range(0,10));
			for (int j = 0; j<HUT_DENSITY*10; j++) {
				trees.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
				trees[trees.Count-1].GetComponent<Tree>().Initialise(new Vector2(thicketPos.x,thicketPos.y));
			}
		}

	}
}
