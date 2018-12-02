using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour 
{
	
	public GameObject optionsPanel;
	public GameObject controlsPanel;
	public GameObject creditsPanel;

	void Start () 
	{
		CloseOptionsPanel();
		CloseControlsPanel();
		CloseCreditsPanel();
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene(2);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void OpenOptionsPanel()
	{
		optionsPanel.SetActive(true);
	}

	public void CloseOptionsPanel()
	{
		optionsPanel.SetActive(false);
	}

	public void OpenControlsPanel()
	{
		controlsPanel.SetActive(true);
	}

	public void CloseControlsPanel()
	{
		controlsPanel.SetActive(false);
	}

	public void OpenCreditsPanel()
	{
		creditsPanel.SetActive(true);
	}

	public void CloseCreditsPanel()
	{
		creditsPanel.SetActive(false);
	}
}
