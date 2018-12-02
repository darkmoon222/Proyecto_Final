using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsCollision
{
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

    protected override void Start()
    {
        base.Start();

        collider = GetComponent<Collider2D>();
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

        xVelocity = axis * speed;

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

        Debug.Log(rb.velocity);
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
    }

    public void Walk()
    {
        speed = walkspeed;
        jumpForce = walkjump;
    }

    public void SetAxis(float h)
    {
        axis = h;
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
}
