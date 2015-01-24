using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageGenerator : MonoBehaviour {
	
	List<GameObject> huts = new List<GameObject>();
	List<GameObject> trees = new List<GameObject>();

	public GameObject elderHut;

    List<GameObject> villagers = new List<GameObject>();

	GameObject river;

	GameObject fineArt;

	private const float FOREST_DENSITY = 0.8f;
	private const float THICKETS = 100;
	private const float VILLAGE_RADIUS = 6;
	
	private float riverXPos = 0;
	// Use this for initialization
	void Start () {


		
		//		GenerateRiver ();
		//		GenerateForest ();
		//		GenerateHuts ();
		//		SetRenderingOrder ();
	}
	
	public void GenerateVillage(int population)
	{

		GenerateRiver ();
		GenerateHuts (population);
		GenerateForest ();

		SetRenderingOrder ();

		fineArt.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateHuts(int population){		
		
		for (int i = 0; i < population; i++) {
			
			

			//Vector2 hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;
			
			Vector2 hutPos; 
			do{
				hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;
			}while(Vector2.Distance(Vector2.zero, hutPos)<2);
			
			
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
						
						Vector2 offset = Random.insideUnitCircle*6;
						
						huts[i].transform.localPosition += new Vector3 (offset.x*2, offset.y*2, 0);
					}
					
				}
				
			}
		}
		
	}
	
	void GenerateRiver(){
		river = (Instantiate(Resources.Load("Prefabs/River")) as GameObject);
		riverXPos = Random.Range (-7, -4);
		
		if (Random.Range (1, 3)==2) {
			riverXPos = Random.Range (4.0f, 7.0f);
		}
		river.transform.position = new Vector3 (riverXPos, 0, 0);
	}
	
	void GenerateForest(){
		
		//Generate Trees
		for(int i = 0; i<THICKETS; i++){
			
			Vector2 randomRange = new Vector2(Random.Range(-15,15),Random.Range(-10,10));
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
					if(Vector2.Distance(Vector2.zero, trees[i].transform.localPosition)<VILLAGE_RADIUS+3){
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

    Vector2 OffsetTrees(Vector2 pos)
    {
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

	public void removeATree()
	{
		float closestTree = 100;
		float closestTreeIndex = 0;
		int randomHut = Random.Range(0,huts.Count);

		for(int i = 0; i < trees.Count; i++)
		{
			float tempDist = Vector2.Distance(huts[randomHut].transform.position, trees[i].transform.position);

			if(tempDist < closestTree)
			{
				closestTree = tempDist;
				closestTreeIndex = i;
			}
		}

		//Change to Stump sprite
		//
	}

	public void addAGoldHut()
	{
		int randHut = Random.Range(0,huts.Count);

		//Change randomHut to Gold
	}

	public void  addGraffiti()
	{
		int randHut = Random.Range(0,huts.Count);
		
		//Change randomHut to Graffiti
	}

	public void addFineArt()
	{
		Vector2 ArtPos; 
		do{
			ArtPos = Random.insideUnitCircle*VILLAGE_RADIUS;
		}while(Vector2.Distance(Vector2.zero, ArtPos)<0.75f);

		fineArt = Instantiate(Resources.Load("Prefabs/Villagerlol")) as GameObject;
		fineArt.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");
		fineArt.transform.localPosition = new Vector3(ArtPos.x, ArtPos.y, 0);
		fineArt.SetActive(true);
	}

	public void addBurntHut()
	{
		int randHut = Random.Range(0,huts.Count);
		
		//Change randomHut to Burn
	}

	public void addArrowKnee()
	{
		int randHut = Random.Range(0,villagers.Count);
		
		//Change randomVillager to ArrowInKnee
	}
}
