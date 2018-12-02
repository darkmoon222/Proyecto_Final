using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsCollision : MonoBehaviour
{
    /*public Vector3 position;
    public float radius;
    public LayerMask mask;
	

    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(position + transform.position, radius, mask);
        if(hitColliders.Length > 0)
        {
            Debug.Log(hitColliders[0].name);
        }
 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(position + transform.position, radius);
    }*/

    protected Rigidbody2D rb;
    protected Transform myTransform;

    [Header("Physics Properties")]
    public float gravityMagnitude = 1;

    [Header("Collision state")]
    public bool isGrounded;
    public bool wasGrounded;
    protected bool justGrounded;
    protected bool justNotGrounded;
    public bool isTouchingWall;
    public bool wasTouchingWall;
    protected bool justTouchingWall;
    protected bool justNotTouchingWall;

    public bool isFacingRight;

    [Header("Ground collision")]    
    public Vector3 gDirection;
    public LayerMask gMask;
    public float gDistance;

    public int gNumRays;
    public float gDistanceRays;


    [Header("Wall collision")]
    public Vector3 wDirection;
    public LayerMask wMask;
    public float wDistance;

    public int wNumRays;
    public float wDistanceRays;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        myTransform = transform;
        //GetComponent<Transform>();

        isFacingRight = true;
    }

    protected virtual void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityMagnitude, ForceMode2D.Force);

        CheckGround();
        CheckWall();
    }

    private void OnDrawGizmos()
    {
        DrawGroundRays();
        DrawWallRays();
    }

    void CheckGround()
    {
        //Default collisions state
        wasGrounded = isGrounded;
        isGrounded = false;
        justGrounded = false;
        justNotGrounded = false;

        Vector3 pos = myTransform.position;
        //int sign = 1;
        /*
        for(int i = 0; i < gNumRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(myTransform.position, Vector2.right, gDistance, gMask);
            if (hit)
            {
                if(Mathf.Abs(hit.normal.y) > 0.85f)
                {
                    //Debug.Log(hit.transform.name);
                    isGrounded = true;
                    if (!wasGrounded) justGrounded = true;
                    break;
                }
                
            }

            pos.x += sign * ((i + 1) * gDistanceRays);
            sign *= -1;
        }
        */
        if (rb.IsTouchingLayers(gMask))
            isGrounded = true;
        if(!isGrounded && wasGrounded) justNotGrounded = true;
    }

    void CheckWall()
    {
        //Default collisions state
        wasTouchingWall = isTouchingWall;
        isTouchingWall = false;
        justTouchingWall = false;
        justNotTouchingWall = false;

        Vector3 pos = myTransform.position;
        int sign = 1;
        for(int i = 0; i < wNumRays; i++)
        {


            RaycastHit2D hit = Physics2D.Raycast(myTransform.position, Vector2.right, wDistance, wMask);
            if (hit)
            {
                if(Mathf.Abs(hit.normal.x) > 0.85f)
                {
                    //Debug.Log(hit.transform.name);
                    isTouchingWall = true;
                    if (!wasTouchingWall) justTouchingWall = true;
                    break;
                }
                
            }

            pos.y += sign * ((i + 1) * wDistanceRays);
            sign *= -1;
        }

        if(!isTouchingWall && wasTouchingWall) justNotTouchingWall = true;
    }

    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        if(isFacingRight)
        {
            wDirection = Vector3.right;
        }
        else wDirection = Vector3.left;
    }

    void DrawGroundRays()
    {
        Gizmos.color = Color.blue;
        if(!isGrounded) Gizmos.color = Color.red;

        Vector3 pos = new Vector3();
        if (myTransform != null)
            pos = myTransform.position; // aquí hay un error?
        int sign = 1;
        for(int i = 0; i < gNumRays; i++)
        {

            Gizmos.DrawRay(pos, gDirection * gDistance);
            pos.x += sign * ((i + 1) * gDistanceRays);
            sign *= -1;
        }
    }

    void DrawWallRays()
    {
        Gizmos.color = Color.blue;
        if(!isTouchingWall) Gizmos.color = Color.red;

        Vector3 pos = new Vector3();

        if(myTransform != null)
            pos = myTransform.position;
        int sign = 1;
        for(int i = 0; i < wNumRays; i++)
        {

            Gizmos.DrawRay(pos, wDirection * wDistance);
            pos.y += sign * ((i + 1) * wDistanceRays);
            sign *= -1;
        }
    }
}
