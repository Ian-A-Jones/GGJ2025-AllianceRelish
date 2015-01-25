using UnityEngine;
using System.Collections;

public class Cow : MonoBehaviour {

	Vector2 targetPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTarget()
	{
		targetPos = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
	}
	
	
	
	public void Move()
	{
		
		if (Vector2.Distance (new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y), targetPos) < 0.2f) {
			SetTarget();
		}
		
		//(int)((gameObject.transform.localPosition.y-10) * 100) * -1;
		
		Vector2 direction = targetPos - new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
		
		
		
		direction = direction.normalized;
		
		

		if (direction.x > 0) {
			gameObject.transform.localScale = new Vector3(1,1,1);
		}
		else{
			gameObject.transform.localScale = new Vector3(-1,1,1);
		}

		
		
		
		
		rigidbody2D.velocity=direction;     
	}
}
