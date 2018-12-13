using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    // falta hacer el feedback del enemigo
    

    public float axisX;
    public float speed;

    private float timeCounter;

    public enum State { Idle, Patrol, Chase, Attack, Death};
    public State state;

    public bool detectTarget;
    public Transform targetTransform;

    public float radius;
    public LayerMask mask;

    public float distanceAttack;

    [Header("Propiedades del Proyectil")]

    public GameObject bulletPrefab;
    private bool shoot;
    private Vector2 directionBullet;

    private int lifeEnemy;

    public AudioSource attackSound;
    //flutter es aleteo
    public AudioSource flutterSound;



    // Use this for initialization
    void Start () {
        SetIdle();

        lifeEnemy = 2;

        attackSound = GameObject.FindGameObjectWithTag("ghostEnemy").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(lifeEnemy <= 0)
        {
            SetDeath();
        }
       
        switch (state)
        {
            case State.Idle:
                IdleUpdate();// aqui va un metodo
                break;

            case State.Patrol:
                PatrolUpdate();// aqui va un metodo
                break;

            case State.Chase:
                ChaseUpdate();// aqui va un metodo
                break;

            case State.Attack:
                AttackUpdate();// aqui va un metodo
                break;

            case State.Death:
                DeathUpdate();// aqui va un metodo
                break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        detectTarget = false;

        Collider2D [] hit = Physics2D.OverlapCircleAll(transform.position, radius, mask);

        if(hit.Length != 0)
        {
            detectTarget = true;
            targetTransform = hit[0].transform;
        }
    }



    private void IdleUpdate()
    {
        //poner aqui lo de la animacion de idle cuando la tenga para el prox futuro 
        if (timeCounter >= 3)
        {
            SetPatrol();
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
        if (detectTarget)
        {
            SetChase();
        }
    }

    private void PatrolUpdate()
    {
        transform.Translate(new Vector3(axisX, 0, 0) * speed * Time.deltaTime);

        if (timeCounter >= 2)
        {
            SetIdle(); 
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
        if (detectTarget)
        {
            SetChase();
        }

    }

    private void ChaseUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, targetTransform.position, speed/2 * Time.deltaTime);
        if (!detectTarget)
        {
            SetIdle();
        }
        if(Vector3.Distance(transform.position, targetTransform.position)<= distanceAttack) // la distancia de ataque, por ahora/aqui no hace nada
        {
            SetAttack();
        }
    }

    private void AttackUpdate()
    {
        if (shoot)
        {
            GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);

            Enemy2Bullet bullet = obj.GetComponent<Enemy2Bullet>();

            directionBullet = targetTransform.position - transform.position;

            directionBullet = directionBullet.normalized;

            bullet.SetDirection(directionBullet);

            shoot = false;
        }
        
        //logica de disparar el proyectil, dar velocidad, direccion, daño
    }

    private void DeathUpdate()
    {
        //Aqui iria poner la animacion que se muera, desvanecerse, ya que es un fantasma
        Destroy(gameObject);
    }



    public void SetIdle()
    {
        timeCounter = 0;
        radius = 4.5f;
        state = State.Idle;
        flutterSound.Stop();
    }
    private void SetPatrol()
    {
        timeCounter = 0;
        axisX *= -1;
        state = State.Patrol;
        flutterSound.Play();
    }
    private void SetChase()
    {
        radius *= 2;
        state = State.Chase;
        flutterSound.Play();
    }
    private void SetAttack()
    {
        shoot = true;
        state = State.Attack;
        flutterSound.Stop();

        attackSound.Play();
    }
    public void SetDeath()
    {
        state = State.Death;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.magenta;
        color.a = 0.1f;
        Gizmos.color = color;

        Gizmos.DrawSphere(transform.position, radius);
    }

    // falta hacer persecucion y atacar MISSION IMPOSIBLE C: 

    public void Damage()
    {
        lifeEnemy -= 1;
    }
}
