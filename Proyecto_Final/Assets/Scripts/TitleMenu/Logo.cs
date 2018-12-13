using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour 
{

	private float timeCounter;
	// Use this for initialization
	void Start () 
	{
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCounter += Time.deltaTime;
		if(timeCounter >= 3) SceneManager.LoadScene(1);
	}
}
