using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public CanvasGroup menuPanel;
    public CanvasGroup menuPanel2;
    public CanvasGroup settingsPanel;
	public CanvasGroup creditsPanel;
	public CanvasGroup tutorialPanel;

    // Use this for initialization
    void Start () {
	
        menuPanel2.interactable = false;
        menuPanel2.blocksRaycasts = false;

        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
        settingsPanel.alpha = 0f;

		creditsPanel.interactable = false;
		creditsPanel.blocksRaycasts = false;
		creditsPanel.alpha = 0f;

		tutorialPanel.interactable = false;
		tutorialPanel.blocksRaycasts = false;
		tutorialPanel.alpha = 0f;


    }

    public void OnClickPlayGame()
    {
        Application.LoadLevel (1);
    }

    public void OnClickSettings()
    {
        settingsPanel.interactable = true;
        settingsPanel.blocksRaycasts = true;
        settingsPanel.alpha = 1f;
    }

    public void OnClickSettingsClose()
    {
        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
        settingsPanel.alpha = 0f;
    }

	public void OnClickCredits()
	{
		creditsPanel.interactable = true;
		creditsPanel.blocksRaycasts = true;
		creditsPanel.alpha = 1f;

		menuPanel.interactable = false;
		menuPanel.blocksRaycasts = false;
		menuPanel.alpha = 0f;

		settingsPanel.interactable = false;
		settingsPanel.blocksRaycasts = false;
		settingsPanel.alpha = 0f;

	}

	public void OnClickCreditsClose()
	{
		creditsPanel.interactable = false;
		creditsPanel.blocksRaycasts = false;
		creditsPanel.alpha = 0f;

		menuPanel.interactable = true;
		menuPanel.blocksRaycasts = true;
		menuPanel.alpha = 1f;
	}

	public void OnClickTutorial()
	{
		tutorialPanel.interactable = true;
		tutorialPanel.blocksRaycasts = true;
		tutorialPanel.alpha = 1f;
		
		menuPanel.interactable = false;
		menuPanel.blocksRaycasts = false;
		menuPanel.alpha = 0f;
		
		settingsPanel.interactable = false;
		settingsPanel.blocksRaycasts = false;
		settingsPanel.alpha = 0f;

		creditsPanel.interactable = false;
		creditsPanel.blocksRaycasts = false;
		creditsPanel.alpha = 0f;
	}

	public void OnClickTutorialClose()
	{
		
		menuPanel.interactable = true;
		menuPanel.blocksRaycasts = true;
		menuPanel.alpha = 1f;

		tutorialPanel.interactable = false;
		tutorialPanel.blocksRaycasts = false;
		tutorialPanel.alpha = 0f;

		settingsPanel.interactable = false;
		settingsPanel.blocksRaycasts = false;
		settingsPanel.alpha = 0f;
		
		creditsPanel.interactable = false;
		creditsPanel.blocksRaycasts = false;
		creditsPanel.alpha = 0f;
	}


    public void OnClickExitGame()
    {
        Application.Quit ();
    }

    // Update is called once per frame
    void Update () {
	
    }
}
