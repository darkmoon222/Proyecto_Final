using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsCollision
{

    // AL HACER DOBLE SALTO TE CAES AL VACIO WTF D: retocar eso. 

    // hacer un trigger collider de death zone, 6/12/18

    [Header("Permissions")]
    //bool -> boleano puede ser true o false
    private bool jump;
    private bool doublejump;
    [Header("Properties")]
    //float -> numero decimal
    private float jumpForce;
    public float jumpForceRelease;
    public float jumpMinTime;
    public float jumpMaxTime;
    private float jumpPressedTime;
    private bool jumpReleased;

    public float axis;
    private float speed;
    public float walkjump;
    public float runjump;

    public float runspeed;
    public float walkspeed;

    private Collider2D collider;

    private float xVelocity;

    private float yVelocity;

    private float flyAxis;

    private bool godMode;

    private Energy energy;

    private GameManager manager;

    private bool mirrorUp;

    public AudioSource jumpSound;
    public AudioSource energyUPSound;
    public AudioSource runSound;

    protected override void Start()
    {
        base.Start();

        collider = GetComponent<Collider2D>();

        energy = GameObject.FindGameObjectWithTag("hud").GetComponentInChildren<Energy>();

        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManager>();

        /*jumpSound = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        energyUPSound = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();*/
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Si estamos en modo dios no saltamos, porque volamos
        if (!godMode)
        {
            if (jumpPressedTime != 0 && Time.time - jumpPressedTime >= jumpMinTime && Time.time - jumpPressedTime <= jumpMaxTime && jumpReleased
                || Time.time - jumpPressedTime > jumpMaxTime && !isGrounded)
            {
                ReleaseJump();
            }

            if (isGrounded)
            {
                jump = false;
                doublejump = false;
                
            }

            rb.velocity = new Vector3(xVelocity, rb.velocity.y, 0);
        }
        else
        {
            // Cambiamos la velocity (moveser con fisicas)
            rb.velocity = new Vector3(xVelocity, yVelocity, 0);
        }

    }

    private void Update()
    {
        //condiciones para flip
        // si esta mirando para un lado y se mueve para el otro gira (flip)
        if ((isFacingRight && axis < 0) || (!isFacingRight && axis > 0)) Flip();

        xVelocity = axis * speed; // aun no hemos perdido te puedes mover, pero una vez hayas perdido no te podras mover.

        // Si estamos en el modo dios, volamos!!
        if (godMode)
            yVelocity = flyAxis * speed;

        //condiciones para moverse
        //si toca el suelo
        if (isTouchingWall)
        {
            //la velocidad en x (a los lados) es 0 (que se para)
            if ((axis > 0 && isFacingRight) || (axis < 0 && !isFacingRight)) xVelocity = 0;
        }

        //Debug.Log(rb.velocity);
    }

    protected override void Flip()
    {
        base.Flip();
        myTransform.localScale = new Vector3 (myTransform.localScale.x * -1, 1, 1);
    }

    void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
    }

    public void StartJump()
    {
        if(doublejump) return;

        jump = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);

        if (!doublejump && jump)
            doublejump = true;

        jumpReleased = false;
        jumpPressedTime = Time.time;
        Jump(jumpForce);

        jumpSound.Play();
    }

    public void ReleaseJump()
    {
        jumpReleased = true;
        Jump(-jumpForceRelease);
    }

    public void Run()
    {
        speed = runspeed;
        jumpForce = runjump;
        
        //runSound.Play();
    }

    public void Walk()
    {
        speed = walkspeed;
        jumpForce = walkjump;
    }

    public void SetAxis(float h)
    {
        axis = h;

        if (Mathf.Abs(h) != 0)
        {
            if (isGrounded && !runSound.isPlaying)
            {
                runSound.Play();
                Debug.Log(runSound.isPlaying);
            }
            else
                runSound.Stop();
        }
        else
        {
            runSound.Stop();
        }
    }

    public void MirrorUp()
    {
        mirrorUp = true;
    }

    public void MirrorDown()
    {
        mirrorUp = false;
    }

    public void Fly(float v)
    {
        // Si no estamos en modo dios no volamos
        if (!godMode)
            return;
        // asignamos el valor que viene del inputManager el movimiento del vuelo
        flyAxis = v;
    }

    public void ActivateGodMode()
    {
        //al llamar la funcion se activa o desactiva
        godMode = !godMode;

        if (godMode)
        {
            // si se activa el modo dios pasamos el rigidbody a kinematic
            rb.bodyType = RigidbodyType2D.Kinematic;
            // desactivamos el collider
            collider.enabled = false;
            godMode = true;
        }
        else
        {
            // pasamos el rigidbody a dynamic
            rb.bodyType = RigidbodyType2D.Dynamic;
            // activamos el collider
            collider.enabled = true;
            godMode = false;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) 
    {
        if ((collision.tag == "enemy") || (collision.tag == "ghostEnemy"))
        {
            //Debug.Log("Me han echo pupa");

            energy.Damage(); // ahora si me golpean me quitara vida el max de seis golpes y si llega a zero  muero


        }

        if(collision.tag == "deathZone")
        {
            //Debug.Log("Me he caido D:");

            manager.LessLife();
            
        }

        if (collision.tag == "energyUP")
        {
            //Debug.Log("Me han echo pupa");

            energy.IniBar(); // ahora si me golpean me quitara vida el max de seis golpes y si llega a zero  muero

            energyUPSound.Play();


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Enemy2Bullet go = collision.gameObject.GetComponent<Enemy2Bullet>();

            //go.InvertDirection();// cuando colisione con el player tendira que dar media vuelta.
            if (mirrorUp)
            {
                go.InvertDirection();

                //Debug.Log("Revota");
            }
            else
            {
                energy.Damage();
                go.Destroied();

                //Debug.Log("No rebota");
            }
        }
        
    }


}
