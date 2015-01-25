using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageGenerator : MonoBehaviour {
	
	public List<GameObject> huts = new List<GameObject>();
	public List<GameObject> trees = new List<GameObject>();

	public GameObject elderHut;

    List<GameObject> villagers = new List<GameObject>();

	public GameObject river;


	private const float FOREST_DENSITY = 0.4f;
	private const float THICKETS = 100;
	private const float VILLAGE_RADIUS = 6;
	
	private float riverXPos = 0;
	// Use this for initialization
	void Start () {


	}
	
	public void GenerateVillage(int population)
	{

		GenerateRiver ();
		GenerateHuts (population);
		GenerateForest ();
		GenerateBushes ();
		TurnRiverBlood ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateHuts(int population){		

		huts.Add (elderHut);
		
		for (int i = 0; i < population; i++) {
			
			

			//Vector2 hutPos = Random.insideUnitCircle*VILLAGE_RADIUS;
			
			Vector3 hutPos; 
			do{
				hutPos = Random.insideUnitCircle * VILLAGE_RADIUS * 1.35f;
			}while(Vector2.Distance(Vector2.zero, hutPos)>3.5f);//Vector2.Distance(Vector2.zero, hutPos)<4);
			
			
			huts.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
			huts[huts.Count-1].GetComponent<Hut>().Initialise(new Vector2(hutPos.x, hutPos.y));
			huts[huts.Count-1].GetComponent<Hut>().hutsRef = huts;
			huts[huts.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");
		}
		
		//Move huts away from each other and river

		for (int i = 0; i<huts.Count; i++) {
			for(int j = 0; j<huts.Count; j++){
				if(huts[i]!=huts[j] && i !=0){
					if(huts[i].GetComponent<BoxCollider2D>().bounds.Intersects(huts[j].GetComponent<BoxCollider2D>().bounds)){
						do{
							Vector2 offset = Random.insideUnitCircle;
						
							huts[i].transform.localPosition += new Vector3 (offset.x*5, offset.y*5, 0);

						}while(huts[i].GetComponent<BoxCollider2D>().bounds.Intersects(huts[j].GetComponent<BoxCollider2D>().bounds));
					}
					
				}
				
			}

			huts[i].GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((huts[i].transform.position.y-10) * 100f) * -1;
		}
		
	}
	
	void GenerateRiver(){
		Debug.Log (1);
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
			
			Vector2 randomRange = new Vector2(Random.Range(-15f,15f),Random.Range(-10f,10f));
			Vector2 thicketPos = randomRange;//OffsetTrees(randomRange);
			
			
			for (int j = 0; j<FOREST_DENSITY*10; j++) {
				trees.Add(Instantiate(Resources.Load("Prefabs/Tree")) as GameObject);
				trees[trees.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Trees");
				trees[trees.Count-1].GetComponent<Tree>().Initialise(new Vector2(thicketPos.x,thicketPos.y));
				trees[trees.Count-1].GetComponent<Tree>().treesRef = trees;
				
				
			}
		}
		
		int total = trees.Count;
		
		//Move trees away from each other and river
		for (int i = 0; i<total; i++) {
			for(int j = 0; j<total; j++){
				if(i>trees.Count-1 || j>trees.Count-1){
					continue;
				}
				if(trees[i]!=trees[j]){
					if(trees[i].GetComponent<BoxCollider2D>().bounds.Intersects(trees[j].GetComponent<BoxCollider2D>().bounds)){
						
						Vector2 offset = Random.insideUnitCircle;
						trees[i].transform.localPosition += new Vector3 (offset.x, offset.y, 0);
					}

					if(trees[i].gameObject.transform.localPosition.x>15 || trees[i].gameObject.transform.localPosition.x<-15 ||
					   trees[i].gameObject.transform.localPosition.y>8 || trees[i].gameObject.transform.localPosition.y<-10){
						trees[i].SetActive(false);
						Destroy(trees[i]);
						trees.Remove(trees[i]);
						total--;
						continue;
					}
					//Destroy trees on village
					if(Vector2.Distance(Vector2.zero, trees[i].transform.localPosition)<VILLAGE_RADIUS+3){
						trees[i].SetActive(false);
						GameObject tr = trees[i];
						trees.Remove(trees[i]);
						Destroy(tr);
						total--;
					}


				}
				
			}
			if(i>trees.Count-1){
				continue;
			}
			trees[i].GetComponent<SpriteRenderer>().sortingOrder = (int)((trees[i].transform.position.y-10) * 100f) * -1; 
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
	


	public void removeATree()
	{
        //float closestTree = 100;
        //float closestTreeIndex = 0;
        //int randomHut = Random.Range(0,huts.Count);
		float closestTree = 100;
		int index = 0;

        for(int i = 0; i < trees.Count; i++)
        {
			float dist = Vector2.Distance(Vector2.zero, new Vector2(trees[i].transform.localPosition.x, trees[i].transform.localPosition.y));
			if(dist<closestTree && trees[i].name !="Stump"){
				closestTree = dist;
				index = i;
			}
//			if(Mathf.Abs(trees[i].transform.localPosition.x)<Mathf.Abs(trees[closestTree].transform.localPosition.x)){
//				closestTree = i;
//
//			}
		}

		//Change to Stump sprite
		//
		trees[index].GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite> ("Sprites/Trees/Tree_Stump");
		trees[index].name = "Stump";
	}

	public void addAGoldHut()
	{

		//Change randomHut to Gold

		huts [0].GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite> ("Sprites/Huts/Hut_Gold");
	}

	public void addGraffiti()
	{
		int randHut = Random.Range(1,huts.Count);

		GameObject graffiti = new GameObject ();
		graffiti.transform.parent = huts [randHut].transform;
		SpriteRenderer rend =graffiti.AddComponent<SpriteRenderer> ();
		rend.sprite = Resources.Load <Sprite> ("Sprites/Huts/Graffiti");
		rend.sortingLayerName = "Objects";

		graffiti.transform.localPosition = Vector3.zero;
		rend.sortingOrder = ((int)((graffiti.transform.parent.position.y-10) * 100f) * -1)+1;
		//Change randomHut to Graffiti
	}

	public void addFineArt()
	{


		GameObject fineArt = new GameObject ();
		fineArt.transform.localPosition = new Vector3 (Random.Range(-3,3), -2, 0);
		fineArt.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");

		SpriteRenderer rend = fineArt.AddComponent<SpriteRenderer> ();
		rend.sprite = Resources.Load<Sprite> ("Sprites/Art");
		rend.sortingOrder = (int)((fineArt.transform.position.y-10) * 100f) * -1;
		rend.sortingLayerName = "Objects";
		fineArt.name = "ART";
	}

	public void addBurntHut()
	{
		if (huts.Count == 1) {
			return;
		}
		int randHut = Random.Range(1,huts.Count);
		huts [randHut].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Huts/Hut_Burnt");
		//Change randomHut to Burn
	}

	public void addArrowKnee()
	{
		int randHut = Random.Range(0,villagers.Count);
		
		//Change randomVillager to ArrowInKnee
	}

	public void updateHuts(int pop)
	{
		if(pop > huts.Count-1)
		{

			Vector3 hutPos; 
			do{
				hutPos = Random.insideUnitCircle * VILLAGE_RADIUS * 1.35f;
			}while(Vector2.Distance(huts[0].transform.localPosition, hutPos)> 3.5f);//Vector2.Distance(Vector2.zero, hutPos)<4);

			huts.Add(Instantiate(Resources.Load("Prefabs/Hut")) as GameObject);
			huts[huts.Count-1].GetComponent<Hut>().Initialise(new Vector2(hutPos.x, hutPos.y));
			huts[huts.Count-1].GetComponent<Hut>().hutsRef = huts;
			huts[huts.Count-1].gameObject.transform.parent = gameObject.transform.FindChild("Terrain").FindChild("Huts");
			huts[huts.Count-1].GetComponent<SpriteRenderer>().sortingOrder = (int)((huts[huts.Count-1].transform.position.y-10) * 100f) * -1;

			for(int i = 0; i < 3; i++)
			{
				removeATree();
			}
		}
	}

	void GenerateBushes(){
		for (int i = 0; i<40; i++) {
			GameObject bush = new GameObject();
			bush.transform.parent = gameObject.transform.FindChild("Terrain");
			SpriteRenderer rend = bush.AddComponent<SpriteRenderer>();

			int rand = Random.Range(1,3);
			bush.name = "Bush_"+rand;
		
			rend.sprite = Resources.Load<Sprite>("Sprites/Trees/Bush_"+rand);

			Vector2 randomRange = new Vector2(Random.Range(-15f,15f),Random.Range(-10f,10f));

			bush.transform.localPosition = randomRange;
			bush.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
			bush.GetComponent<SpriteRenderer>().sortingOrder = (int)((bush.transform.position.y-10) * 100f) * -1;

			int randomScale = Random.Range(0,2);

			if(randomScale==1){
				bush.transform.localScale = new Vector3(-1,1,1);
			}
		}
	}

	void TurnRiverBlood(){
		//river.transform.FindChild ("RiverParticles").gameObject.GetComponent<ParticleSystem> ().
	}
}
