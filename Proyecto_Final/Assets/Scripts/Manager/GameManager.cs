using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

    LevelManager levelManager;

    public bool gamePaused;
    private HUD hud;

    private int life;
    private int iniLife;

    private Player player;

    private Energy energy;

    public Text lifeText;

    public GameObject gameOverPanel;

    
    
	// Use this for initialization
	void Start () 
    {
        levelManager = GetComponent<LevelManager>();
        hud = GameObject.FindGameObjectWithTag("hud").GetComponent<HUD>();

        life = 3;
        iniLife = life;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        energy = GameObject.FindGameObjectWithTag("hud").GetComponentInChildren<Energy>();

        gameOverPanel.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {

        if(life <= 0)
        {
            GameOver();  
        }
    }



    public void LessLife() //restar vida = getDamage, porque existe lo de la energia que dice roger y bueno esta puesto pero lo hemos adaptado a lo nuestro :D
    {

        life -= 1;
        ResetPlayer(); // falta resetear el nivel en si, con los enemigos, la musica y las particulas y los sprites!!!!!!!!!!!!!! ESTA MAS O MENOS

        energy.IniBar();

        lifeText.text = "Vidas: " + life.ToString(); // preguntar a quiensea porque se queja de esto, sale un mensajito en rojo alla a la pantalla de unity, HECHO
    }
   
    public void ResetPlayer()
    {
        Transform teleport = GameObject.FindGameObjectWithTag("teleport").GetComponent<Transform>(); // ya hemos creado el teleport :D

        player.transform.position = teleport.position; // que el player se teletransporte a la posicion del teleport
    }

	
	public void WinState()
    {
        //NO ES FINAL!! HAY QUE AÑADIR COSAS
        levelManager.LoadNextScene();
    }

     public void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1;
        hud.ClosePausePanel();
    }

    public void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0;
        hud.OpenPausePanel();
    }

    public void GameOver()
    {
        //gameOverPanel.SetActive(true);

        SceneManager.LoadScene("GameOver");

        Time.timeScale = 0;
        
    }

}
