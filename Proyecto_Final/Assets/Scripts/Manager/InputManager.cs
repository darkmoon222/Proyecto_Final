using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Player player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Walk();
	}
	
	// Update is called once per frame
	void Update ()
    {
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
    }
}
