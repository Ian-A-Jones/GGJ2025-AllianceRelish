using UnityEngine;
using System.Collections;

public class Questions {
	
	protected string[] QuestionArr; 
	protected string[,] AnswerArr; 
	public int numQuestions; 


	public Questions()
	{
		numQuestions = 0;
		QuestionArr = new string[41];
		QuestionArr [0] = "The food has been poisoned, what do we do now???";
		QuestionArr [1] = "The river is contaminated, what do we do now?";
		QuestionArr [2] = "Greetings elder, me and a few other villagers are thinking about leaving and starting a new village, what do we do now?"; //leads to the next 2 depending on answer.
		QuestionArr [3] = "Greetings elder the people have returned, thank you for helping, we have returned with supplies in return.";//for helping them
		QuestionArr [4] = "Greetings elder the people that you refused to help have returned, they are not happy so they have taken some supplies.";//for not helping them
		QuestionArr [5] = "Oh exalted one we have procured a supply of metal, what do we do now?";
		QuestionArr [6] = "You hear a bellowing voice ‘We are not pleased with you, you must make a blood sacrife to please us!"; 
		QuestionArr [7] = "We have come across a new forest, we have a bountiful selection of wood, what do we do now?"; 
		QuestionArr [8] = "We have had a very successful hunt elder, we have a plentiful amount of food, what do we do now?";
		QuestionArr [9] = "Im sorry elder, we have had a very poor hunt, what do we do now?"; 
		QuestionArr [10] = "Elder, there is a drought, what do we do now?"; 
		QuestionArr [11] = "Elder some of our people are becoming ill, what do we do now?";
		QuestionArr [12] = "God is unhappy with us, a “natural disaster choice” has struck the village";
		QuestionArr [13] = "I challenge you for position of elder!"; 
		QuestionArr [14] = "Elder you must think about your lineage, what do we do now? "; 
		QuestionArr [15] = "Elder there are strangers at our door, what do we do now?";
		QuestionArr [16] = "Elder a villager has been caught stealing, what do we do now?";
		QuestionArr [17] = "A villager has claimed that you are his parent, what do we do now??"; 
		QuestionArr [18] = "This hunter is a cowardly dog, what do we do now?"; 
		QuestionArr [19] = "Elder, your mother has invited you over for dinner, what do we do now?";
		QuestionArr [20] = "we have encountered a pack of wolves elder, what do we do now?";
		QuestionArr [21] = "elder, the local farmer Gunter, his chicken has laid a couple of eggs, what do we do now?"; 
		QuestionArr [22] = "One of the local villager wants to become artist, what do we do now?"; 
		QuestionArr [23] = "The harvest looks plentiful already, what should we do now?";
		QuestionArr [24] = "Elder, this villager is annoyed at the tree near his hut, what do we do now?";
		QuestionArr [25] = "The crops haven’t grown elder, what do we do now?"; 
		QuestionArr [26] = "Elder, someone stole this mans sweet roll! What do we do now?"; 
		QuestionArr [27] = "Elder, the villagers are requesting more farms, what do we do now?";
		QuestionArr [28] = "A wandering trader, wants to trade? What do we do now?";
		QuestionArr [29] = "Elder, this mans hut is drafty, and cold. What do we do now?"; // the next question is instant after this one if the right one is selected.
		QuestionArr [30] = "Elder, the mans hut is now on fire. He wanted to warm it up, what do we do now?"; 
		QuestionArr [31] = "Elder, Ian has been caught... ‘mowing’ other peoples lawns, what do we do now?";
		QuestionArr [32] = "Elder, Alan took a long time get food from the stores, what do we do now?";
		QuestionArr [33] = "Elder, Katie was caught vandalising the huts, what do we do now?";
		QuestionArr [34] = "Dan is meddling with the cattle again, what do we do now?"; 
		QuestionArr [35] = "Elder, Ellis has overslept again and has missed the hunt, what do we do now?";
		QuestionArr [36] = "Elder, Ross has been plundering for booty again, what do we do now?";
		QuestionArr [37] = "Elder, a wild Simon has appeared at the gate, what do we do now?"; 
		QuestionArr [38] = "Elder, a wild Carina has appeared at the gate, what do we do now?"; 
		QuestionArr [39] = "Elder, a wild Andy has appeared at the gate, what do we do now?"; 
		QuestionArr [40] = "Elder, a wild Lloyd has appeared at the gate, what do we do now?"; 

		AnswerArr = new string[41, 2];
		AnswerArr [0, 0] = "Destroy the food,it will only cause harm!";
		AnswerArr [0, 1] = "Keep the food, I'm sure we'll be fine!";

		AnswerArr [1, 0] = "Stop drinking from the river, and tighten up the supplies.";
		AnswerArr [1, 1] = "Keep the supply up, we need the water.";

		AnswerArr [2, 0] = "Of course I will help you, here take some supplies with you, I hope to see you again.";
		AnswerArr [2, 1] = "Who do you think you are! We need our supplies!";
		//answers to the good outcome of people returning
		AnswerArr [3, 0] = "...";
		AnswerArr [3, 1] = "...";
		//answers to the bad outcome of people returning
		AnswerArr [4, 0] = "...";
		AnswerArr [4, 1] = "...";

		AnswerArr [5, 0] = "Use them to produce weapons for the hunters! ";
		AnswerArr [5, 1] = "I could use some new valuables give them to me! ";

		AnswerArr [6, 0] = "Okay mighty gods I will do as you ask.";
		AnswerArr [6, 1] = "Im the one with the power hear, I do not bend to your will!";

		AnswerArr [7, 0] = "Throw them on the fire, our people could do with some warmth.";
		AnswerArr [7, 1] = "Use them to build more huts, our people would do with some shelter. ";

		AnswerArr [8, 0] = "Prepare a mighty feast let no person go unfed.";
		AnswerArr [8, 1] = "Put them in the storage with the rest, we must be careful with supplies.";

		AnswerArr [9, 0] = "Its fine I’m sure you tried your best. ";
		AnswerArr [9, 1] = "Your incompetence cannot be forgiven, of with your head!";

		AnswerArr [10, 0] = "Make a sacrifice to please the rain god.";
		AnswerArr [10, 1] = "Do nothing it will rain soon.";

		AnswerArr [11, 0] = "Kill ill people, save the rest of us.";
		AnswerArr [11, 1] = "Help them as much as you can.";

		AnswerArr [12, 0] = "Oh s*$t!";
		AnswerArr [12, 1] = "Oh f#%k!";

		AnswerArr [13, 0] = "You are foolish if you think you can best me in a fight.";
		AnswerArr [13, 1] = "You are smart enough to challenge me, you are smart enough to win the fight";

		AnswerArr [14, 0] = "Bring me the finest mate, I need an heir.";
		AnswerArr [14, 1] = "I am still young yet.";

		AnswerArr [15, 0] = "Invite them to join us.";
		AnswerArr [15, 1] = "Don’t allow them in they must leave.";

		AnswerArr [16, 0] = "Punish him, this crime cannot go unpunished!";
		AnswerArr [16, 1] = "Leave him be, we can let him off, this time.";

		AnswerArr [17, 0] = "This claim is ridiculous banish him for his incompetence.";
		AnswerArr [17, 1] = "I cant say for sure if he is or not, but I shall adopt him for the mena time.";

		AnswerArr [18, 0] = "It takes time to be a good hunter, let him be.";
		AnswerArr [18, 1] = "Train him with the other hunters, he can learn.";

		AnswerArr [19, 0] = "I cant possibly go and see her I have a village to run.";
		AnswerArr [19, 1] = "Okay lets go, but pack some supplies for the trip.";

		AnswerArr [20, 0] = "we can train them and use them to aid our hunting!";
		AnswerArr [20, 1] = "just kill them we could do with the extra food.";

		AnswerArr [21, 0] = "That’s my dinner sorted, bring them to me!";
		AnswerArr [21, 1] = "Store them, every little helps!";

		AnswerArr [22, 0] = "Artists aren’t helpful were not helping him!";
		AnswerArr [22, 1] = "Supply him with a few canvas, maybe we can display his work!";

		AnswerArr [23, 0] = "Harvest it now, we cant risk it with the weather!";
		AnswerArr [23, 1] = "Wait a few days, it could get better!";

		AnswerArr [24, 0] = "Cut the tree down it wont cause any harm.";
		AnswerArr [24, 1] = "Leave the tree, im sure he can live with it. ";

		AnswerArr [25, 0] = "Send out the hunters, see if they can gather some food. ";
		AnswerArr [25, 1] = "Just be careful with the supplies we can make it through the week. ";

		AnswerArr [26, 0] = "Its only a sweet roll this is no concern to me!";
		AnswerArr [26, 1] = "Discipline him, I think an arrow to the knee should suffice!";

		AnswerArr [27, 0] = "Build some farms, it might use some supplies now but it will be worth it in the long run. ";
		AnswerArr [27, 1] = "We don’t need anymore farms, well be ok.";

		AnswerArr [28, 0] = "Sure we can trade, let him in!";
		AnswerArr [28, 1] = "No we don’t need anyone from the outside interfering with us, send him away!";

		AnswerArr [29, 0] = "...,";
		AnswerArr [29, 1] = "...";

		AnswerArr [30, 0] = "...";
		AnswerArr [30, 1] = "...";

		AnswerArr [31, 0] = "That’s very invasive, don’t let him continue ";
		AnswerArr [31, 1] = "Let him do it, it keeps up appearances ";

		AnswerArr [32, 0] = "Sanction.";
		AnswerArr [32, 1] = "Don’t ask.";

		AnswerArr [33, 0] = "a.	Clean it up ";
		AnswerArr [33, 1] = "b.	Leave it ";

		AnswerArr [34, 0] = "a.	Cleanse the cattle ";
		AnswerArr [34, 1] = "b.	Cleanse Dan.  ";

		AnswerArr [35, 0] = "a.	These things happen from time to time, its fine.";
		AnswerArr [35, 1] = "b.	This is unacceptable, to miss a hunt, he needs to be punished! ";

		AnswerArr [36, 0] = "a.	Let him carry on, its not to severe.";
		AnswerArr [36, 1] = "b.	This is unacceptable it cannot continue ";

		AnswerArr [37, 0] = "a.	Fight him";
		AnswerArr [37, 1] = "b.	Make him flee";

		AnswerArr [38, 0] = "a.	Fight her";
		AnswerArr [38, 1] = "b.	Make her flee";

		AnswerArr [39, 0] = "Fight him";
		AnswerArr [39, 1] = "Make him flee";

		AnswerArr [40, 0] = "Fight him.";
		AnswerArr [40, 1] = "Make him flee.";
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
