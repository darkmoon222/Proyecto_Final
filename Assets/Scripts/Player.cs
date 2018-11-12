using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsCollision
{
    [Header("Permissions")]
    //bool -> boleano puede ser true o false
    public bool jump;
    public bool doublejump;
    [Header("Properties")]
    //float -> numero decimal
    public float jumpForce;
    public float jumpForceRelease;
    public float jumpMinTime;
    public float jumpMaxTime;
    private float jumpPressedTime;
    private bool jumpReleased;

    public float axis;
    private float speed;

    public float runspeed;
    public float walkspeed;

    private float xVelocity;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(jumpPressedTime != 0 && Time.time - jumpPressedTime >= jumpMinTime && Time.time - jumpPressedTime <= jumpMaxTime && jumpReleased
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

    private void Update()
    {
        //condiciones para flip
        // si esta mirando para un lado y se mueve para el otro gira (flip)
        if ((isFacingRight && axis < 0) || (!isFacingRight && axis > 0)) Flip();

        xVelocity = axis * speed;

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
    }

    public void Walk()
    {
        speed = walkspeed;
    }

    public void SetAxis(float h)
    {
        axis = h;
    }
}
