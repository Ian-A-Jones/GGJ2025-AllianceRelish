using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatDialogue : MonoBehaviour {

	#region Variables
    public GUISkin GSKIN; 
	string Question;
	//string[] Answers; 
	bool activeQ;
    Rect QuestionRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 3);
    Rect LabelRectangle = new Rect(Screen.width - (Screen.width / 3 * 2), Screen.height - (Screen.height / 10 * 3), Screen.width / 3 * 2, Screen.height / 10 * 1);
    Rect AnswerRectangle = new Rect(Screen.width - (Screen.width/2 + 100 ), Screen.height - (Screen.height / 10 * 2), Screen.width / 6*3, Screen.height / 20 * 1);
    Rect AnswerRectangle2 = new Rect(Screen.width - (Screen.width/2 + 100), Screen.height - (Screen.height / 10 * 1), Screen.width / 6* 3, Screen.height / 20 * 1);





    ////Rect LabelRectangle = new Rect(Screen.width-900, Screen.height-140, 500, 30);
    //Rect AnswerRectangle = new Rect(Screen.width-950, Screen.height -90, 900,30);
    //Rect AnswerRectangle2 = new Rect(Screen.width-950, Screen.height -60, 900,30);
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
           // GUI.Window(0, WindowRectangle, DoMyWindow,"");

            GUI.Box(new Rect(QuestionRectangle), "");
            GUI.Label(new Rect(LabelRectangle),Question);
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
   //
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
	

