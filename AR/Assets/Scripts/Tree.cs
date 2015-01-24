using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tree : MonoBehaviour {



	public List<GameObject> treesRef;
	// Use this for initialization
	void Start () {
		int rand = Random.Range (1, 3);
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Trees/Tree_" + rand);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialise(Vector2 pos){
		gameObject.transform.localPosition = pos;

		float scale = Random.Range (0.7f, 1.2f);
		gameObject.transform.localScale = new Vector3 (scale, scale, 1);
	}

	void OnTriggerStay2D(Collider2D col){

		if (col.gameObject.tag == "River") {
			Destroy(gameObject);
			treesRef.Remove(gameObject);
		}
	}

}
