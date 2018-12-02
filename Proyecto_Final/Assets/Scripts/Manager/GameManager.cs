using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    LevelManager levelManager;
    
	// Use this for initialization
	void Start () {
        levelManager = GetComponent<LevelManager>();
	}
	
	public void WinState()
    {
        //NO ES FINAL!! HAY QUE AÑADIR COSAS
        levelManager.LoadNextScene();
    }
}
