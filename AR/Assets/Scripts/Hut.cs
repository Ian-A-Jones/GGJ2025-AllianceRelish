using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hut : MonoBehaviour {

	public List<GameObject> hutsRef;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialise(Vector2 pos){
		gameObject.transform.localPosition = pos;

		float scale = 1.5f-((gameObject.transform.localPosition.y+10)*0.05f);
		gameObject.transform.localScale = new Vector3 (scale, scale, 1);
	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "River") {
			Destroy(gameObject);
			hutsRef.Remove(gameObject);
		}
	}
}
