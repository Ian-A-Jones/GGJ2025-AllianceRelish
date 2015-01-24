using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	bool VillagerInfo = true;
	bool VillagerInfoMoving = false;
	public GUISkin gui;

	public float activeVilStatsHeight = Screen.height - 250;

	public float inActiveVilStatsHeight = Screen.height + 100;

	public float VilStatsTargetHeight;

	public float currentVilStatsHeight;
	
	Rect QuestionRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 3);
	Rect LabelRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 1);
	Rect AnswerRectangle = new Rect(Screen.width - (Screen.width/2 + 100 ), Screen.height - (Screen.height / 10 * 2), Screen.width / 6*3, Screen.height / 20 * 1);
	Rect AnswerRectangle2 = new Rect(Screen.width - (Screen.width/2 + 100), Screen.height - (Screen.height / 10 * 1), Screen.width / 6* 3, Screen.height / 20 * 1);

	// Use this for initialization
	void Start () {

		currentVilStatsHeight = activeVilStatsHeight;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!VillagerInfoMoving)
			{
				VillagerInfoMoving = true;
				if(VillagerInfo)
				{
					VilStatsTargetHeight = inActiveVilStatsHeight;
					VillagerInfo = false;
				}
				else
				{
					VilStatsTargetHeight = activeVilStatsHeight;
					VillagerInfo = true;
				}
			}
		}

		if(VillagerInfoMoving)
		{
			if(Mathf.Abs(currentVilStatsHeight - VilStatsTargetHeight) > 0.1)
			{
				currentVilStatsHeight = Mathf.Lerp(currentVilStatsHeight, VilStatsTargetHeight, 0.4f);
			}
			else
			{
				VillagerInfoMoving = false;
			}
		}
	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect (50, currentVilStatsHeight, 400, 300));
			
		GUI.Box(new Rect(0,0,250,400), "Villager Info");
		
		GUI.Label(new Rect(25,80,200,100), "Name: " );
		GUI.Label(new Rect(25,140,200,100), "Gender: " );
		GUI.Label(new Rect(25,200,200,100), "Age: " );
		
		GUI.EndGroup();

		Rect QuestionRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 3);
		Rect LabelRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 1);
		Rect AnswerRectangle = new Rect(Screen.width - (Screen.width/2 + 100 ), Screen.height - (Screen.height / 10 * 2), Screen.width / 6*3, Screen.height / 20 * 1);
		Rect AnswerRectangle2 = new Rect(Screen.width - (Screen.width/2 + 100), Screen.height - (Screen.height / 10 * 1), Screen.width / 6* 3, Screen.height / 20 * 1);

		GUI.BeginGroup(QuestionRectangle);
		GUI.Box(new Rect(0,0, Screen.width / 3 * 2, Screen.height / 10 * 3), "");
		GUI.Label(new Rect(0,0,0,0), "Question");
		if (GUI.Button(new Rect(AnswerRectangle), "Answer"))
		{
		}
		if (GUI.Button(new Rect(AnswerRectangle2), "Answer"))
		{
		}

		GUI.EndGroup();
	}
}
