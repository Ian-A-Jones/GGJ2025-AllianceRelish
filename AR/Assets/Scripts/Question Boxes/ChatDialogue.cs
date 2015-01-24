using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatDialogue : MonoBehaviour {

	public VillageManager villageManagerRef;

	#region Variables
    public GUISkin GSKIN; 
	string Question;
	//string[] Answers; 
	public static bool activeQ;
    Rect QuestionRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 3);
    Rect LabelRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 1);
    Rect AnswerRectangle = new Rect(Screen.width - (Screen.width/2 + 100 ), Screen.height - (Screen.height / 10 * 2), Screen.width / 6*3, Screen.height / 20 * 1);
    Rect AnswerRectangle2 = new Rect(Screen.width - (Screen.width/2 + 100), Screen.height - (Screen.height / 10 * 1), Screen.width / 6* 3, Screen.height / 20 * 1);

	public Questions questions;

	//keep track of the questions
	public List<int> ListaskedQ= new List<int>();

	public bool Q3Active = false, Q4Active = false;
	
	string Answer1, Answer2, Outcome1, Outcome2;


    public ParticleSystem Rain;
    public ParticleSystem BloodRain;
	#endregion 

    void Start()
    {
        questions = new Questions();
		nextQ();
        activeQ = false;        
    }

	void OnGUI()
	{
        GUI.skin = GSKIN;
        if (activeQ)
        {
			if(ListaskedQ.Count < villageManagerRef.Villagers.Count)
			{
	            GUI.Box(new Rect(QuestionRectangle), "");
	            GUI.Label(new Rect(LabelRectangle),Question);
	            if (GUI.Button(new Rect(AnswerRectangle), Answer1))
	            {
					purformOutcome(Outcome1);
	                activeQ = false;

					//Time.timeScale = 1;
					nextQ();


	            }
	            if (GUI.Button(new Rect(AnswerRectangle2), Answer2))
	            {
					purformOutcome(Outcome2);
	                activeQ = false;

					//Time.timeScale = 1;
					nextQ();
	            }
			}
			{
				//Completed Decisions end state
			}
          
        }

	}

	void nextQ()
	{
		int id = randQ();
		Question = newQ(id);
		string[] Answers = newA(id);
		Answer1 = Answers[0];
		Answer2 = Answers[1];
		string[] Outcomes = newOutcome(id);
		Outcome1 = Outcomes[0];
		Outcome2 = Outcomes[1];
	}
   //
	#region new question + answer
	private int randQ()
	{	//TODO: cheack for a repeated question, if so pick another number.

		int qNum = -1;
		bool newQFound = false;

		while(!newQFound)
		{
			qNum = Random.Range (0, 41);

			if(!ListaskedQ.Contains(qNum))
			{
				if(qNum != 4 || qNum != 3 || Q3Active && qNum == 3 || Q4Active && qNum == 4)
				{
					ListaskedQ.Add (qNum);
					newQFound = true;
				}
			}
		}

		return qNum;

	}
	private string newQ(int id)
	{
		string question = questions.returnQuestion(id);
		return question;
	}
	private string[] newA(int id)
	{
		string[] answers = questions.returnAnswer (id);

		return answers;
	}
	private string[] newOutcome(int id)
	{
		string[] outcomes = questions.returnOutcome (id);
		
		return outcomes;
	}


	#endregion

	void purformOutcome(string outcome)
	{
        //death.enabled = false;
		string[] outcomes = outcome.Split ("," [0]);

		for (int i = 0; i < outcomes.Length; i ++) {
			switch(outcomes[i])
			{
			case "loseFood":
				//TODO:pick range for loss
				Debug.Log ("Food loss");
				villageManagerRef.foodSupply-= Random.Range(1, 5);
				break;
			case "loseHappiness":
				Debug.Log ("Happiness loss");
				villageManagerRef.happiness-= Random.Range(1,5);
				break;
			case "loseWater":
				villageManagerRef.waterSupply-= Random.Range(1,5);
				break;
			case "losePopulation":
				Debug.Log ("Pop loss");
				int randPopLoss = Random.Range(1,5);

				villageManagerRef.cull(randPopLoss);
				break;
			case "loseSupplies":
				villageManagerRef.foodSupply-= Random.Range(1,3);
				villageManagerRef.waterSupply-= Random.Range(1,3);
				break;
			case "loseFoodIncrease":
				villageManagerRef.foodGain-= Random.Range (1,3);
				break;
			case "loseWaterIncrease":
				villageManagerRef.waterGain-= Random.Range (1,3);
				break;
			case "loseRandom":
				int rand = Random.Range(1,3);
				if (rand == 1)
				{
					villageManagerRef.foodSupply-= Random.Range (5,10);
				}else if (rand == 2)
				{
					villageManagerRef.waterSupply-= Random.Range (5,10);
				}else if (rand == 3)
				{
					villageManagerRef.population-= Random.Range (5,10);
                    
				}
				villageManagerRef.happiness-= Random.Range (3,6);
				break;
			case "lose1Food":
				villageManagerRef.foodSupply--;
				break;
			case "lose1Population":
				villageManagerRef.cull(1);
                
				break;
			case "lose1Happiness":
				villageManagerRef.happiness--;
				break;
			case "loseGame":
				//TODO:call end state here
				break;
			case "gainFood":
				villageManagerRef.foodSupply+= Random.Range(1,5);
				break;
			case "gainHappiness":
				villageManagerRef.happiness+= Random.Range (1,5);
				break;
			case "gainWater":
				villageManagerRef.waterSupply+= Random.Range (1,5);
				break;
			case "gainPopulation":
				rand = Random.Range (1,5);
				villageManagerRef.population += rand;

				for(int j = 0 ; j < rand; j++)
				{
					villageManagerRef.Villagers.Add(Instantiate(Resources.Load("Prefabs/Villagerlol")) as GameObject);
					villageManagerRef.Villagers[villageManagerRef.Villagers.Count - 1].AddComponent<Villager>();
				}
				break;
			case "gainSupplies":
				villageManagerRef.foodSupply+= Random.Range (1,3);
				villageManagerRef.waterSupply+= Random.Range (1,3);
				break;
			case "gainFoodIncrease":
				villageManagerRef.foodGain+= Random.Range (1,3);
				break;
			case "gainWaterIncrease":
				villageManagerRef.waterGain+= Random.Range (1,3);
				break;
			case "gain1Population":

				villageManagerRef.Villagers.Add(Instantiate(Resources.Load("Prefabs/Villagerlol")) as GameObject);
				villageManagerRef.Villagers[villageManagerRef.Villagers.Count - 1].AddComponent<Villager>();
				villageManagerRef.population++;
				break;
			case "gain1Happiness":
				villageManagerRef.happiness++;
				break;
			case "gain1Food":
				villageManagerRef.foodSupply++;
				break;
			case"nothing":
				break;
			case "randomFood":
				//TODO:delay the suply;
				int rand3 = Random.Range(1,2);
				if(rand3 == 1)
				{
					villageManagerRef.foodSupply++;
				}else if (rand3 == 2)
				{
					villageManagerRef.foodSupply+= 6;
				}
				break;
			case "randomHappiness":
				int rand2 = Random.Range(1,2);
				if (rand2 == 1){
					villageManagerRef.happiness+= Random.Range(1,5);
				}else if (rand2 == 2) {
					villageManagerRef.happiness-= Random.Range(1,5);
				}
				break;

				//TODO: all visual changes
			case "cutTree":
				break;
			case "goldHut":
				break;
			case "gainGraffiti":
				break;
			case "gainFineArt":
				break;
			case "burnHut":
				break;
			case "arrowKnee":
				break;
			case "Q30":
				break;
			case "activate3":
				Q3Active = true;
				break;
			case "activate4":
				Q4Active = true;
				break;
            case "raining":
                Debug.Log("RAIN NOW");
                //Rain.playOnAwake = true;
                BloodRain.Play(false);
                Rain.Play(true);
                break;
            case "Rainingblood":
                Debug.Log("RAIN NOW");
                Rain.Play(false);
                BloodRain.Play(true);
                break;


			}
		}
	}
}
	

