using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatDialogue : MonoBehaviour {
	
	public VillageManager villageManagerRef;
	public string qOutcome;

	float temp = 0.0f;

	#region Variables
	public GUISkin GSKIN; 
	string Question;
	string[] Answers; 
	public static bool activeQ = false;
	Rect QuestionRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 3);
	Rect LabelRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 1);
	Rect AnswerRectangle = new Rect(Screen.width - (Screen.width/2 + 100 ), Screen.height - (Screen.height / 10 * 2), Screen.width / 6*3, Screen.height / 20 * 1);
	Rect AnswerRectangle2 = new Rect(Screen.width - (Screen.width/2 + 100), Screen.height - (Screen.height / 10 * 1), Screen.width / 6* 3, Screen.height / 20 * 1);
	
	public Questions questions;
	
	//keep track of the questions
	public List<int> ListaskedQ= new List<int>();
	
	public bool Q3Active = false, Q4Active = false, Q30Next = false;
	
	//Rect LabelRectangle = new Rect(Screen.width-900, Screen.height-140, 500, 30);
	
	string Answer1, Answer2, Outcome1, Outcome2;

    public ParticleSystem Rain;
    public ParticleSystem BloodRain;

    #region AudioSources
    //Other
    public AudioSource Thunder;

    //Voices
    public AudioSource Laugh;
    public AudioSource Groan;  

    #endregion
    float RAINTIMER = 20f;
    float timeRained = 0;
    int days = 0;
    bool isRaining = false;

	#endregion 

	#region temp Variables

	int popLoss;

	#endregion
	
	void Start()
	{
        gameObject.GetComponent<RenderParticlesOnLayer>().changeOrder(Rain);
        gameObject.GetComponent<RenderParticlesOnLayer>().changeOrder(BloodRain);
		questions = new Questions();
		nextQ();
	}
	
	void OnGUI()
	{
		GUI.skin = GSKIN;
		GSKIN.button.fontSize = Screen.width / 55;
		int scaleFontSize = Screen.width / 55;

		GUIStyle scaleFont;

		scaleFont = new GUIStyle (GSKIN.label);
		scaleFont.fontSize = scaleFontSize;

		if (activeQ)
		{
			Debug.Log (ListaskedQ.Count);
			// GUI.Window(0, WindowRectangle, DoMyWindow,"");
			if(ListaskedQ.Count < 39)
			{

				GUI.Box(new Rect(QuestionRectangle), "");
				GUI.Label(new Rect(LabelRectangle), Question, scaleFont);
				if (GUI.Button(new Rect(AnswerRectangle), Answer1))
				{
					purformOutcome(Outcome1);
					activeQ = false;
					villageManagerRef.setVillagersKinematic(false);
					villageManagerRef.VillagerAlert.SetActive(false);
					nextQ();
					
					
				}
				if (GUI.Button(new Rect(AnswerRectangle2), Answer2))
				{
					purformOutcome(Outcome2);
					villageManagerRef.setVillagersKinematic(false);
					villageManagerRef.VillagerAlert.SetActive(false);
					activeQ = false;			
					nextQ();
				}
			}
			else
			{
				//Completed Decisions end state
				villageManagerRef.questionVictory = true;
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
        update();
	}

	#region new question + answer
	private int randQ()
	{
		
		int qNum = -1;
		bool newQFound = false;
		
		while(!newQFound)
		{
			qNum = Random.Range (0, 41);
			
			if(!ListaskedQ.Contains(qNum))
			{
				if(Q30Next)
				{
					Q30Next = false;
					newQFound = true;
					ListaskedQ.Add (30);
					return 30;
				}
				if(((qNum != 4 && qNum != 3) || qNum == 3 && Q3Active || qNum == 4 && Q4Active) && qNum != 30)
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

		string[] outcomes = outcome.Split ("," [0]);
        
		for (int i = 0; i < outcomes.Length; i ++) {
			switch(outcomes[i])
			{
			case "loseFood":
				villageManagerRef.foodSupply*= Random.Range(0.50f, 0.75f);
				villageManagerRef.foodSupply-= 10;
				qOutcome = "Lose Food";
				break;
			case "loseHappiness":
				Debug.Log ("Happiness loss");
				villageManagerRef.happiness*= Random.Range(0.50f, 0.75f);
				villageManagerRef.happiness-= 10;
				qOutcome = "Lose Happiness";
				break;
			case "loseWater":
				villageManagerRef.waterSupply*= Random.Range(0.50f, 0.75f);
				villageManagerRef.waterSupply-= 10;
				qOutcome = "Lose Water";
				break;
			case "losePopulation":
				Debug.Log ("Pop loss");
				popLoss = Random.Range(1,4);
				villageManagerRef.cull (popLoss);
				qOutcome = "Lose Population";
				break;
			case "loseSupplies":
				villageManagerRef.foodSupply*= Random.Range(0.55f, 0.80f);
				villageManagerRef.foodSupply-= 10;
				villageManagerRef.waterSupply*= Random.Range(0.55f, 0.80f);
				villageManagerRef.waterSupply-= 10;
				qOutcome = "Lose Supplies";
				break;
			case "loseFoodIncrease":
				villageManagerRef.foodGain*= Random.Range (0.55f, 0.80f);
				qOutcome = "Lose Food Increase";
				break;
			case "loseWaterIncrease":
				villageManagerRef.waterGain*= Random.Range (0.55f, 0.80f);
				qOutcome = "Lose Water Increase";
				break;
			case "loseRandom":
				int rand = Random.Range(1,3);
				if (rand == 1)
				{
					villageManagerRef.foodSupply*= Random.Range (0.30f, 0.65f);
					villageManagerRef.foodSupply-= 10;
					qOutcome = "Lose Food";
				}else if (rand == 2)
				{
					villageManagerRef.waterSupply*= Random.Range (0.30f, 0.65f);
					villageManagerRef.waterSupply-= 10;
					qOutcome = "Lose Water";
				}else if (rand == 3)
				{
					popLoss = Random.Range(1,3);
					
					villageManagerRef.cull (popLoss);
					qOutcome = "Lose Population";
				}
				villageManagerRef.happiness*= Random.Range (0.30f, 0.65f);
				break;
			case "lose1Food":
				villageManagerRef.foodSupply--;
				qOutcome = "Lose Food";
				break;
			case "lose1Population":
				villageManagerRef.cull(1);
				qOutcome = "Lose Population";
				break;
			case "lose1Happiness":
				villageManagerRef.happiness--;
				qOutcome = "Lose Happiness";
				break;
			case "loseGame":
				//TODO:call end state here
				break;
			case "gainFood":
				villageManagerRef.foodSupply*= Random.Range(1.15f,1.40f);
				qOutcome = "Gain Food";
				break;
			case "gainHappiness":
				villageManagerRef.happiness*= Random.Range (1.15f,1.40f);
				qOutcome = "Gain Happiness";
				break;
			case "gainWater":
				villageManagerRef.waterSupply*= Random.Range (1.15f,1.40f);
				qOutcome = "Gain Water";
				break;
			case "gainPopulation":

				villageManagerRef.population+= Random.Range (2,4);

				for(int j = 0; j < (int)(villageManagerRef.population - villageManagerRef.Villagers.Count-1); j++)
				{
					villageManagerRef.AddVillager();
				}
				qOutcome = "Gain Population";
				break;
			case "gainSupplies":
				villageManagerRef.foodSupply*= Random.Range (1.10f,1.35f);
				villageManagerRef.waterSupply*= Random.Range (1.10f,1.35f);
				qOutcome = "Gain Supplies";
				break;
			case "gainFoodIncrease":
				villageManagerRef.foodGain*= Random.Range (1.10f,1.35f);
				qOutcome = "Gain Food Increase";
				break;
			case "gainWaterIncrease":
				villageManagerRef.waterGain*= Random.Range (1.10f,1.35f);
				qOutcome = "Gain Water Increase";
				break;
			case "gain1Population":
				villageManagerRef.population++;
				villageManagerRef.AddVillager();
				qOutcome = "Gained 1 Population";
				break;
			case "gain1Happiness":
				villageManagerRef.happiness++;
				qOutcome = "Gain Happiness";
				break;
			case "gain1Food":
				villageManagerRef.foodSupply++;
				qOutcome = "Gain Food";
				break;
			case"nothing":
				break;
			case "randomFood":
				//TODO:delay the suply;
				int rand3 = Random.Range(1,2);
				if(rand3 == 1)
				{
					villageManagerRef.foodSupply*= Random.Range(1.10f,1.35f);
					qOutcome = "Gain Food";
				}else if (rand3 == 2)
				{
					villageManagerRef.foodSupply*= Random.Range(1.20f,1.65f);
					qOutcome = "Gain Food";
				}
				break;
			case "randomHappiness":
				int rand2 = Random.Range(1,2);
				if (rand2 == 1){
					villageManagerRef.happiness*= Random.Range(1.10f,1.35f);
					qOutcome = "Gain Happiness";
				}else if (rand2 == 2) {
					villageManagerRef.happiness*= Random.Range(0.50f,0.85f);
					qOutcome = "Lose Happiness";
				}
				break;

				//TODO: all visual changes
			case "cutTree":
				villageManagerRef.removeTree();
				break;                
			case "goldHut":
                Debug.Log("YOU CANT RUN THIS SHIT");
				villageManagerRef.GoldHut();
				break;
			case "gainGraffiti":
				villageManagerRef.Graffiti();
				break;
			case "gainFineArt":
				villageManagerRef.FineArt();
				break;
			case "burnHut":
				villageManagerRef.BurntHut();
				break;
			case "arrowKnee":
				villageManagerRef.arrowKnee();
				break;
			case "Q30":
				Q30Next = true;
				break;
			case "activate3":
				Q3Active = true;
				break;
			case "activate4":
				Q4Active = true;
				break;
            case"raining":
                Rain.Play(true);
                isRaining = true;
                Thunder.Play();
                update();
                break;
			case "RainingBlood":
                BloodRain.Play();
                isRaining = true;
                Laugh.Play();
                update();
                break;
			case "Tornado":
				villageManagerRef.AddTornado();
				break;
			}
           
		}
	}

    void update()
    {
        Debug.Log("IM UPDATING");
        if (isRaining)
        {
            timeRained += Time.deltaTime;
            Debug.Log("Time Rained: " + timeRained);
            if (timeRained > RAINTIMER)
            {
                Debug.Log("Stop rain");
                timeRained = 0;
                isRaining = false;
                BloodRain.Stop();
                Rain.Stop();
            }
        }

    }
}
	