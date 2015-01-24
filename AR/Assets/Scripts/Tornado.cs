using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tornado : MonoBehaviour {

	public Rigidbody2D tornBody;
	public List<GameObject> trees = new List<GameObject>();
	// Use this for initialization
	void Start () {
		trees = GameObject.Find ("Village").GetComponent<VillageGenerator> ().trees;
		tornBody.gravityScale = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "Tree") {
			Destroy(col.gameObject);
			trees.Remove(col.gameObject);
		}
	}

	public void StartTornado(){
		tornBody.gravityScale = 0.3f;
	}
}
