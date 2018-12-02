using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameOver : MonoBehaviour 
{

	public void LoadScene(int numScene)
	{
		SceneManager.LoadScene(numScene);
	}
}
