using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tree : MonoBehaviour {

	public List<GameObject> treesRef;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialise(Vector2 pos){
		gameObject.transform.localPosition = pos;
	}

	void OnTriggerStay2D(Collider2D col){

		if (col.gameObject.tag == "River") {
			Destroy(gameObject);
			treesRef.Remove(gameObject);
		}
	}

}
