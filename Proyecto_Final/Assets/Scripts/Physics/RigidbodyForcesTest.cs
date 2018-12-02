using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForcesTest : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown (KeyCode.Space))
        {
            //rb.AddExplosionForce(force, Vector3.zero, 5)
            //rb.AddForce(Vector3.up * force);
            rb.AddTorque(Vector3.up);
;       }
	}
}
