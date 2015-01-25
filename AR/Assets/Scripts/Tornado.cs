using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tornado : MonoBehaviour {

	public Rigidbody2D tornBody;
	public List<GameObject> trees = new List<GameObject>();
    public AudioSource tornado;
	// Use this for initialization
	void Start () {
		trees = GameObject.Find ("Village").GetComponent<VillageGenerator> ().trees;
		tornBody.gravityScale = 0;
        
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -30) {
			Destroy(gameObject.transform.parent.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "Tree") {
			GameObject treeToRemove = col.gameObject;

			trees.Remove(col.gameObject);

			Destroy(treeToRemove);
		}
	}

	public void StartTornado(){
		tornBody.gravityScale = 0.3f;
        tornado.Play();
	}
}
