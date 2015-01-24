using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hut : MonoBehaviour {

	public List<GameObject> hutsRef;
	// Use this for initialization
	void Start () {
		int rand = Random.Range (1, 6);
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Huts/Hut_" + rand);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialise(Vector2 pos){
		gameObject.transform.localPosition = pos;


	}

	void OnTriggerStay2D(Collider2D col){
		
		if (col.gameObject.tag == "River") {
			this.transform.position += new Vector3(col.gameObject.transform.position.x * -2,0,0);
		}

	}
}
