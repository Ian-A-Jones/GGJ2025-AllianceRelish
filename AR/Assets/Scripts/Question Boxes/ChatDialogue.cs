using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatDialogue : MonoBehaviour {

	#region Variables
    public GUISkin GSKIN; 
	string Question;
	//string[] Answers; 
	bool activeQ;
	Rect QuestionRectangle = new Rect(Screen.width-1000,Screen.height -150, 1000,150);
	Rect AnswerRectangle = new Rect(Screen.width-950, Screen.height -90, 900,30);
	Rect AnswerRectangle2 = new Rect(Screen.width-950, Screen.height -60, 900,30);
	public Questions questions;

	int i = 0;
	string Answer1, Answer2;
	#endregion 

    void Start()
    {
        questions = new Questions();
        int id = randQ();
        Question = newQ(id);
        string[] Answers = newA(id);
        Answer1 = Answers[0];
        Answer2 = Answers[1];
        activeQ = true;
    }

	void OnGUI()
	{
        GUI.skin = GSKIN;
        if (activeQ)
        {
            GUI.Box(new Rect(QuestionRectangle), Question);

            if (GUI.Button(new Rect(AnswerRectangle), Answer1))
            {
                activeQ = false;
            }
            if (GUI.Button(new Rect(AnswerRectangle2), Answer2))
            {
                activeQ = false;
            }
        }

	}

	#region new question + answer
	private int randQ()
	{
		int qNum = Random.Range (0,3);
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

	#endregion
}
	

