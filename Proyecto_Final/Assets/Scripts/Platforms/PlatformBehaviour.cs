using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour {

    public enum PlatformMovement { HORIZONTAL, VERTICAL}

    public PlatformMovement platformMovement;

    public float speed;
    private Transform myTransform;

    public bool right;
    public bool up;

    [Header("Time Counter")]
    public float maxTime;
    public float currentTime;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = 0;
            right = !right;
            up = !up;
        }

        switch (platformMovement)
        {
            case PlatformMovement.HORIZONTAL:
                {
                    if (right)
                    {
                        myTransform.Translate(Vector3.right * speed * Time.deltaTime);
                    }
                    else
                    {
                        myTransform.Translate(Vector3.left * speed * Time.deltaTime);
                    }
                    break;
                }
            case PlatformMovement.VERTICAL:
                {
                    if (up)
                    {
                        myTransform.Translate(Vector3.up * speed * Time.deltaTime);
                    }
                    else
                    {
                        myTransform.Translate(Vector3.down * speed * Time.deltaTime);
                    }
                    break;
                }
        }

	}

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Transform pt = collision.transform;
            pt.parent = myTransform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Transform pt = collision.transform;
            pt.parent = null;
        }
    }
    #endregion
}
