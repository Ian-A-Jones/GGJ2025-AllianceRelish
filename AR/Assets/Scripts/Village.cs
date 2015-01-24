using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Village : MonoBehaviour {

	List<GameObject> huts = new List<GameObject>();
	List<GameObject> trees = new List<GameObject>();

	GameObject river;

	private const float HUT_COUNT = 100;
	private const float FOREST_DENSITY = 0.8f;
	private const float THICKETS = 50;
	private const float VILLAGE_RADIUS = 5;

	private float riverXPos = 0;
	// Use this for initialization
	void Start () {



		GenerateRiver ();
		GenerateForest ();
		GenerateHuts ();
		SetRenderingOrder ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void GenerateHuts(){


		for (int i = 0; i<HUT_COUNT; i++) {


			//Vector2 hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;

			Vector2 hutPos; 
			do{
				hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;
			}while(Vector2.Distance(Vector2.zero, hutPos)<1);
			
			
			huts.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
			huts[huts.Count-1].GetComponent<Hut>().Initialise(hutPos);
			huts[huts.Count-1].GetComponent<Hut>().hutsRef = huts;
			huts[huts.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");
		}

		//Move huts away from each other and river
		for (int i = 0; i<huts.Count; i++) {
			for(int j = 0; j<huts.Count; j++){
				if(huts[i]!=huts[j]){
					if(huts[i].GetComponent<BoxCollider2D>().bounds.Intersects(huts[j].GetComponent<BoxCollider2D>().bounds)){
						
						Vector2 offset = Random.insideUnitCircle;

						huts[i].transform.localPosition += new Vector3 (offset.x, offset.y, 0);
					}

				}
				
			}
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

		//Generate Trees
		for(int i = 0; i<THICKETS; i++){

			Vector2 randomRange = new Vector2(Random.Range(-9,9),Random.Range(-5,5));
			Vector2 thicketPos = randomRange;//OffsetTrees(randomRange);


			for (int j = 0; j<FOREST_DENSITY*10; j++) {
				trees.Add(Instantiate(Resources.Load("Prefabs/Tree")) as GameObject);
				trees[trees.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Trees");
				trees[trees.Count-1].GetComponent<Tree>().Initialise(new Vector2(thicketPos.x,thicketPos.y));
				trees[trees.Count-1].GetComponent<Tree>().treesRef = trees;


			}
		}



		//Move trees away from each other and river
		for (int i = 0; i<trees.Count; i++) {
			for(int j = 0; j<trees.Count; j++){
				if(trees[i]!=trees[j]){
					if(trees[i].GetComponent<BoxCollider2D>().bounds.Intersects(trees[j].GetComponent<BoxCollider2D>().bounds)){

						Vector2 offset = Random.insideUnitCircle;
						trees[i].transform.localPosition += new Vector3 (offset.x, offset.y, 0);
					}
					//Destroy trees on village
					if(Vector2.Distance(Vector2.zero, trees[i].transform.localPosition)<VILLAGE_RADIUS-1){
						trees[i].SetActive(false);
					}
				}
				
			}
		}
	
		int index = trees.Count;
		for(int i = 0; i<index; i++){
			if(!trees[i].activeSelf){
				trees.Remove(trees[i]);
				index--;
			}

		}



		
	}
	
	Vector2 OffsetTrees(Vector2 pos){
		Vector2 offset = new Vector2 ();

		if (pos.x > 0) {
			offset.x = 1f;
		}
		else{
			offset.x = -1f;
		}

		if (pos.y > 0) {
			offset.y = 1f;
		}
		else{
			offset.y = -1f;
		}

		offset *= VILLAGE_RADIUS*0.6f;

		return pos+offset;
	}

	void SetRenderingOrder(){

		List<GameObject> treesAndHuts = new List<GameObject> ();
		treesAndHuts.AddRange (trees);
		treesAndHuts.AddRange (huts);

		//Change Tree render order - higher number is top
		for (int i = 0; i<treesAndHuts.Count; i++) {
			
			int renderOrder = 0;
			
			for(int j = 0; j<treesAndHuts.Count; j++){
				//Tree j  is above i
				if(treesAndHuts[i].transform.localPosition.y<treesAndHuts[j].transform.localPosition.y){
					renderOrder++;
				}
				
			}
			
			treesAndHuts[i].GetComponent<SpriteRenderer>().sortingOrder = renderOrder;
		}

	}
}
