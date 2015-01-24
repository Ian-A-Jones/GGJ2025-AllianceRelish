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

    //Rect LabelRectangle = new Rect(Screen.width-900, Screen.height-140, 500, 30);
	
	string Answer1, Answer2, Outcome1, Outcome2;
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
           // GUI.Window(0, WindowRectangle, DoMyWindow,"");

            GUI.Box(new Rect(QuestionRectangle), "");
            GUI.Label(new Rect(LabelRectangle),Question);
            if (GUI.Button(new Rect(AnswerRectangle), Answer1))
            {
				purformOutcome(Outcome1);
                activeQ = false;

				Time.timeScale = 1;
				nextQ();


            }
            if (GUI.Button(new Rect(AnswerRectangle2), Answer2))
            {
				purformOutcome(Outcome2);
                activeQ = false;

				Time.timeScale = 1;
				nextQ();
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
				ListaskedQ.Add (qNum);
				newQFound = true;
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

		string[] outcomes = outcome.Split ("," [0]);

		for (int i = 0; i < outcomes.Length; i ++) {
			switch(outcomes[i])
			{
			case "loseFood":
				//TODO:pick range for loss
				Debug.Log ("Food loss");
				villageManagerRef.foodSupply*= Random.Range(0.80f, 0.85f);
				break;
			case "loseHappiness":
				Debug.Log ("Happiness loss");
				villageManagerRef.happiness*= Random.Range(0.80f, 0.85f);
				break;
			case "loseWater":
				villageManagerRef.waterSupply*= Random.Range(0.80f, 0.85f);
				break;
			case "losePopulation":
				Debug.Log ("Pop loss");
				villageManagerRef.population*= Random.Range(0.80f, 0.85f);
				break;
			case "loseSupplies":
				villageManagerRef.foodSupply*= Random.Range(0.85f, 0.90f);
				villageManagerRef.waterSupply*= Random.Range(0.85f, 0.90f);
				break;
			case "loseFoodIncrease":
				villageManagerRef.foodGain*= Random.Range (0.85f, 0.90f);
				break;
			case "loseWaterIncrease":
				villageManagerRef.waterGain*= Random.Range (0.85f, 0.90f);
				break;
			case "loseRandom":
				int rand = Random.Range(1,3);
				if (rand == 1)
				{
					villageManagerRef.foodSupply*= Random.Range (0.50f, 0.75f);
				}else if (rand == 2)
				{
					villageManagerRef.waterSupply*= Random.Range (0.50f, 0.75f);
				}else if (rand == 3)
				{
					villageManagerRef.population*= Random.Range (0.50f, 0.75f);
				}
				villageManagerRef.happiness-= Random.Range (0.50f, 0.75f);
				break;
			case "lose1Food":
				villageManagerRef.foodSupply--;
				break;
			case "lose1Population":
				villageManagerRef.population--;
				break;
			case "lose1Happiness":
				villageManagerRef.happiness--;
				break;
			case "loseGame":
				//TODO:call end state here
				break;
			case "gainFood":
				villageManagerRef.foodSupply*= Random.Range(1.15f,1.20f);
				break;
			case "gainHappiness":
				villageManagerRef.happiness*= Random.Range (1.15f,1.20f);
				break;
			case "gainWater":
				villageManagerRef.waterSupply*= Random.Range (1.15f,1.20f);
				break;
			case "gainPopulation":
				villageManagerRef.population*= Random.Range (1.15f,1.20f);
				break;
			case "gainSupplies":
				villageManagerRef.foodSupply*= Random.Range (1.10f,1.15f);
				villageManagerRef.waterSupply*= Random.Range (1.10f,1.15f);
				break;
			case "gainFoodIncrease":
				villageManagerRef.foodGain*= Random.Range (1.10f,1.15f);
				break;
			case "gainWaterIncrease":
				villageManagerRef.waterGain*= Random.Range (1.10f,1.15f);
				break;
			case "gain1Population":
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
					villageManagerRef.foodSupply*= Random.Range(1.10f,1.15f);
				}else if (rand3 == 2)
				{
					villageManagerRef.foodSupply*= Random.Range(1.20f,1.25f);
				}
				break;
			case "randomHappiness":
				int rand2 = Random.Range(1,2);
				if (rand2 == 1){
					villageManagerRef.happiness*= Random.Range(1.10f,1.15f);
				}else if (rand2 == 2) {
					villageManagerRef.happiness*= Random.Range(0.80f,0.85f);
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


			}
		}
	}
}
	

