using UnityEngine;
using System.Collections;

[System.Serializable]
public class Villager : MonoBehaviour
{
	public bool hungry = false, thirsty = false, dead = false;

	public int daysHungry = 0, daysThirsty = 0;

	public static int DeathThreshold = 6;
	
	// Use this for initialization
	void Start () 
	{
	
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

    public Vector2 getNewDestination()
    {
        Vector2 newPos = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));

        return newPos;
    }

    public Vector2 curPosition()
    {
        Vector2 cPos = transform.position;
        return cPos;
    }

    public void moveVillager()
    {
        Vector2 curPos = curPosition();
        Vector2 dest = getNewDestination();

        Vector2 diff = dest - curPos;


        diff = diff.normalized;
        rigidbody2D.AddForce(diff);     
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "River")
        {
            this.rigidbody2D.AddForce(new Vector2(1, 0));

        }
        if (col.gameObject.tag == "Hut")
        {
            Vector2 curPos = curPosition();
            Vector2 dest = getNewDestination();

            Vector2 diff = dest - curPos;

            diff = diff.normalized;
            rigidbody2D.AddForce(diff);
        }
    }
	
}
