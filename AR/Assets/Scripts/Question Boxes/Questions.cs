using UnityEngine;
using System.Collections;

public class Questions {
	
	protected string[] QuestionArr; 
	protected string[,] AnswerArr; 
	public int numQuestions; 


	public Questions()
	{
		numQuestions = 0;
		QuestionArr = new string[3];
		QuestionArr [0] = "Our food supply has been poisoned, what do we do now?";
		QuestionArr [1] = "Our water supply has been poisoned, what do we do now?";
		QuestionArr [2] = "You're a little bitch, you want a fight?"; 
		AnswerArr = new string[3, 2];
		AnswerArr [0, 0] = "Destroy the food,it will only cause harm!";
		AnswerArr [0, 1] = "Keep the food, I'm sure we'll be fine!";
		AnswerArr [1, 0] = "Don't drink the water, it could be deadly!";
		AnswerArr [1, 1] = "Continue to drink the water, we'll be fine!";
		AnswerArr [2, 0] = "Yeah, bring it m8";
		AnswerArr [2, 1] = "No, you win";

		//numQuestions = QuestionArr.Length;
	}

	public string returnQuestion(int qID)
	{
		string rQuestion;
		rQuestion = QuestionArr [qID];

		return rQuestion;
	}
	public string[] returnAnswer(int aID)
	{
		string[] rAnswer = new string[2];
		rAnswer[0] = AnswerArr[aID,0];
		rAnswer [1] = AnswerArr [aID, 1];

		return rAnswer;
	}

}
