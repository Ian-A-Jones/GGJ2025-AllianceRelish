using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hut : MonoBehaviour {

	SpriteRenderer rend;
	public List<GameObject> hutsRef;
	bool canMove = true;
	float timeCreated = 0;
	// Use this for initialization
	void Start () {
		timeCreated = Time.time;
		int rand = Random.Range (1, 6);
		rend = gameObject.GetComponent<SpriteRenderer> ();
		rend.sprite = Resources.Load<Sprite> ("Sprites/Huts/Hut_" + rand);
		rend.color = new Color (255, 255, 255, 0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (!canMove) {
			return;
		}
		if ((Time.time - timeCreated) > 0.1f) {
			Debug.Log("nomove");
			canMove = false;
			rend.color = new Color (255, 255, 255, 1f);
		}
	}

	public void Initialise(Vector2 pos){
		gameObject.transform.localPosition = pos;


	}

	void OnTriggerStay2D(Collider2D col){


		if (!canMove) {
			return;
		}

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
