using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{

	public GameObject pausePanel;
	// Use this for initialization
	void Start () 
	{
		ClosePausePanel();
	}
	
	public void OpenPausePanel()
	{
		pausePanel.SetActive(true);
	}

	public void ClosePausePanel()
	{
		pausePanel.SetActive(false);
	}

	/*void Update()
	{
		if((Input.GetKeyDown(KeyCode.P)) || (Input.GetKeyDown(KeyCode.Escape)))
		{
			if (gamePaused) ClosePausePanel();
			else OpenPausePanel();
		}
	}

	/*LA PAUSA FUNCIONA PERO ESTÁ TODO PUESTO EN ESTE SCRIPT Y DEBERÍA ESTAR UNA PARTE EN EL INPUT MANAGER Y OTRA EN EL GAME MANAGER
	PERO ESQUE DE ESA MANERA NO FUNCIONABA Y NO SÉ POR QUÉ*/
}
