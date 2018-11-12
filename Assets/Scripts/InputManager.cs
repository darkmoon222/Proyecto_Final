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

        //if(Input.GetButton("Horizontal"))
        {
            player.SetAxis(axisH);
        }

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
    }
}
