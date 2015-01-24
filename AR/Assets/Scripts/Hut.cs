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

		float rand = Random.Range (0.7f, 0.95f);
		gameObject.transform.localScale = new Vector3 (rand, rand, 1);
	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "River") {
			Destroy(gameObject);
			hutsRef.Remove(gameObject);
		}
	}
}
