using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

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
		//gameObject.transform.localPosition += new Vector3 (Random.Range(-1f,1f), Random.Range(-1f,1f), 0);

	}
}
