using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public CanvasGroup menuPanel;
	public CanvasGroup menuPanel2;
	public CanvasGroup settingsPanel;

	// Use this for initialization
	void Start () {
	
		menuPanel2.interactable = false;
		menuPanel2.blocksRaycasts = false;

		settingsPanel.interactable = false;
		settingsPanel.blocksRaycasts = false;
		settingsPanel.alpha = 0f;


	}

	public void OnClickPlayGame()
	{
		Application.LoadLevel ("");
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

	public void OnClickExitGame()
	{
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
