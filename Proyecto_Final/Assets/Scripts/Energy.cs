using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

    private Slider energyBar;

    private float energy;
    private float iniEnergy;
    private float damage = 20;

    private GameManager gameManager; 

	// Use this for initialization
	void Start () {
        energy = 100;
        iniEnergy = energy;
        energyBar = GetComponent<Slider>();

        gameManager = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        energyBar.value = energy;
        if(energyBar.value <= 0)
        {
            gameManager.LessLife(); //aqui va el game manager se restara una vida

            IniBar(); // faltaria restar vida al player 
        }
	}

    public void Damage() // este seria el GETDAMAGE QUE DICES MARC,
    {
        energy -= damage;
    }

    public void IniBar() // iniciar barra de energia
    {
        energy = iniEnergy;
    }
}
