using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameOver : MonoBehaviour 
{

	public void LoadScene(int numScene)
	{
        Time.timeScale = 1;

        SceneManager.LoadScene(numScene);

	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
