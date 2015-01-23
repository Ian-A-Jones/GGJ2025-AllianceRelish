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
	private const float VILLAGE_RADIUS = 5;

	private float riverXPos = 0;
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


			Vector2 hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;


			huts.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
			huts[huts.Count-1].GetComponent<Hut>().Initialise(hutPos);
			huts[huts.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");
		}

	}

	void GenerateRiver(){
		river = (Instantiate(Resources.Load("Prefabs/River")) as GameObject);
		riverXPos = Random.Range (-7, -4);

		if (Random.Range (1.0f, 3.0f)==2) {
			riverXPos = Random.Range (4.0f, 7.0f);
		}
		river.transform.position = new Vector3 (riverXPos, 0, 0);
	}

	void GenerateForest(){

		for(int i = 0; i<THICKETS; i++){

			Vector2  randomRange = Random.insideUnitCircle*VILLAGE_RADIUS;
			Vector2 thicketPos = OffsetTrees(randomRange);


			for (int j = 0; j<FOREST_DENSITY*10; j++) {



				trees.Add(Instantiate(Resources.Load("Prefabs/Tree")) as GameObject);
				trees[trees.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Trees");
				trees[trees.Count-1].GetComponent<Tree>().Initialise(new Vector2(thicketPos.x,thicketPos.y));

			}
		}

	}

	Vector2 OffsetTrees(Vector2 pos){
		Vector2 offset = new Vector2 ();

		if (pos.x > 0) {
			offset.x = 1;
		}
		else{
			offset.x = -1;
		}

		if (pos.y > 0) {
			offset.y = 1;
		}
		else{
			offset.y = -1;
		}

		offset *= VILLAGE_RADIUS;

		return offset;
	}
}
