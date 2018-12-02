using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public int currentScene;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetAllScenes()[0].buildIndex;
	}
	
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextScene()
    {
        currentScene++;
        if (currentScene == SceneManager.sceneCountInBuildSettings)
            currentScene = 0;

        SceneManager.LoadScene(currentScene);
    }

    public void LoadPreviousScene()
    {
        currentScene--;
        if (currentScene == 0)
            currentScene = 0;

        SceneManager.LoadScene(currentScene);
    }
}
