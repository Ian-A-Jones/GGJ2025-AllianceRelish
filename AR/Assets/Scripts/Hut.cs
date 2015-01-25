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
			this.transform.position += new Vector3(col.gameObject.transform.position.x * -1.5f,0,0);
		}
		else if(col.gameObject.tag == "Hut")
		{
			Debug.Log (GetInstanceID() + "is colliding with another hut");
			Vector2 diff = this.transform.localPosition - col.transform.localPosition;

			diff.Normalize();

			this.transform.position += new Vector3(diff.x, diff.y, 0) *1f;
			this.GetComponent<SpriteRenderer>().sortingOrder = (int)((this.transform.position.y-10) * 100f) * -1;
		}

	}
}
