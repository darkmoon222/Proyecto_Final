using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private Player player;
    private GameManager gameManager;
    private HUD hud;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Walk();
        hud = GameObject.FindGameObjectWithTag("hud").GetComponent<HUD>();
        gameManager = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if((Input.GetKeyDown(KeyCode.P)) || (Input.GetKeyDown(KeyCode.Escape)))
        {
            if(gameManager.gamePaused) gameManager.Resume();
            else gameManager.Pause();
            
        }

        if(gameManager.gamePaused) return;
        
        
        float axisH = Input.GetAxis("Horizontal");

        player.SetAxis(axisH);
        //Movimiento para volar en modo dios
        player.Fly(Input.GetAxis("Vertical"));
        
        if(Input.GetButtonDown("Jump"))
        {
            player.StartJump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            player.ReleaseJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.Walk();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            player.ActivateGodMode();
        }

        if (Input.GetKey(KeyCode.F))
        {
            player.MirrorUp();
        }
        else
        {
            player.MirrorDown();
        }
    }
}
