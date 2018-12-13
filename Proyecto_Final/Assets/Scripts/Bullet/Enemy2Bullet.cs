using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Bullet : MonoBehaviour {

    // la bala ya hace pupa, ahora falta devolverla D:

    public float speed;
    private Vector2 direction;

    private Enemy2 enemy;
    private float currentTime;

    private Energy energy;

    private bool inverted;

	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<Enemy2>();
        currentTime = 0;
        energy = GameObject.FindGameObjectWithTag("hud").GetComponentInChildren<Energy>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        if(currentTime >= 10)
        {
            Destroied();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
	}

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
           
            return;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Destroied(); 
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // este esta fallando D:
    {
        if(collision.tag == "ghostEnemy") //AUQI ANTES ESTAVA ENEMY
        {
            Debug.Log("pls funciona");

            if (inverted)
            {
                enemy.Damage();
                Debug.Log("Enemigo muerto loser");
                Destroied(); // cuando revote la bala, sera dañina para el enemigo y morira muahjajaja
            }
        }
    }



    public void Destroied()
    {
        enemy.SetIdle();
        Destroy(gameObject);

    }

    public void InvertDirection()
    {
        direction *= -1; // no estamos seguros aqui tendria que invertir la bala.

        inverted = true;
    }

}
