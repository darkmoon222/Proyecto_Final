using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy1;

    public float minTime;
    public float maxTime;
    public float axisY;

    private float currentTime;
    private float spawnTime; 
	
    // Use this for initialization
	void Start () {
        currentTime = 0;
        spawnTime = minTime;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (currentTime >= spawnTime)
        {
            SpawnEnemy();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
	}
    void SpawnEnemy ()
    {
        currentTime = 0;

        spawnTime = Random.Range(minTime, maxTime);

        Vector2 spawnPosition = transform.position;

        spawnPosition.y += Random.Range(-axisY, axisY); //tendremos más control de los bichos

        GameObject go = Instantiate(enemy1, spawnPosition, Quaternion.identity); // spawnear el enemigo en la posicion que queremos.

        go.name = enemy1.name + "_" + Time.time.ToString("00.00"); // decirle que el nombre sea el nombre del enemigo(prefab) seguido de una barra baja y el segundo al cual ha spawneado
    }
}
