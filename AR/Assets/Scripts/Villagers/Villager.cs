using UnityEngine;
using System.Collections;

[System.Serializable]
public class Villager : MonoBehaviour
{
	public bool hungry = false, thirsty = false, dead = false;

	public int daysHungry = 0, daysThirsty = 0, age = 0;

	public static int DeathThreshold = 6;

	public JSONObject info;

	string gender = "Male";

	Vector2 targetPos = new Vector2();


	// Use this for initialization
	void Awake () 
	{
		gameObject.transform.localPosition = new Vector3 (0, 0, 0);
		SetTarget ();
	}
	
	public bool alive()
	{
		if(daysHungry + daysThirsty >= DeathThreshold || dead)
		{
			return false;            
		}
		else
		{
			return true;
		}
	}

	public void setInfo(JSONObject newInfo){
		info = newInfo;
		gender = info.GetField ("Gender").str;


		if (gender == "Female") {
			gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/People/Villager_Front_Female");
		}

	}

	public void unHunger()
	{
		hungry = false;
		daysHungry = 0;
	}
	
	public void hungerTick()
	{
		if(hungry)
		{
			daysHungry ++;
		}
		else
		{
			hungry = true;
		}
	}

	public void unThirst()
	{
		thirsty = false;
		daysThirsty = 0;
	}

	public void thirstTick()
	{
		if(thirsty)
		{
			daysThirsty ++;
		}
		else
		{
			thirsty = true;
		}
	}

    public void SetTarget()
    {
      	targetPos = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
    }



    public void moveVillager()
    {

		if (Vector2.Distance (new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y), targetPos) < 0.2f) {
			SetTarget();
		}

		//(int)((gameObject.transform.localPosition.y-10) * 100) * -1;
		
		Vector2 direction = targetPos - new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);


		direction = direction.normalized;
		rigidbody2D.velocity=direction;     
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "River")
        {
            this.rigidbody2D.AddForce(new Vector2(1, 0));

        }
        if (col.gameObject.tag == "Hut")
        {

			

        }
    }
	
}
