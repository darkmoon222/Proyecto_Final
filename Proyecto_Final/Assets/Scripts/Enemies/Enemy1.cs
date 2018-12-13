using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

    public float axisX;
    public float speed; 

    public AudioSource aleteoSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate (new Vector3(axisX, 0, 0) * speed * Time.deltaTime);

        //aleteoSound.Play();

	}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndEnemy")  //cuando choque me dara el collider
        {
            //Debug.Log("Me he chocado");
            Destroy(gameObject);
        }
        //Debug.Log(collision);
    }
    

}

